using HospitalQueueMS.Data;
using HospitalQueueMS.Hubs;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HospitalQueueMS.Controllers
{
    public class TokensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<QueueHub> _hubContext;

        public TokensController(ApplicationDbContext context, IHubContext<QueueHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // شاشة الاستقبال لإنشاء توكن جديد
        public IActionResult CreateToken()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Reception" && role != "Admin") return RedirectToAction("Index", "Home");

            ViewBag.Departments = _context.Departments.Include(d => d.Clinics).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateToken(int clinicId)
        {
            // احسب آخر رقم مستخدم في نفس العيادة
            var lastToken = _context.Tokens
                .Where(t => t.ClinicId == clinicId)
                .OrderByDescending(t => t.TokenNumber)
                .FirstOrDefault();

            int nextNumber = lastToken != null ? lastToken.TokenNumber + 1 : 1;

            var token = new Token
            {
                ClinicId = clinicId,
                TokenNumber = nextNumber,
                CreatedAt = DateTime.Now
            };

            _context.Tokens.Add(token);
            _context.SaveChanges();

            // رجّع الرقم علشان تطبعه
            ViewBag.TokenNumber = nextNumber;
            return View("TokenCreated", token);
        

        }

        // شاشة الانتظار (تتغير حسب الـ Role)
        public IActionResult WaitingRoom()
        {
            var role = HttpContext.Session.GetString("Role");
            var username = HttpContext.Session.GetString("Username");

            IQueryable<Token> tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department);

            if (role == "Doctor")
            {
                var doctor = _context.Users.FirstOrDefault(u => u.Username == username);
                if (doctor != null && doctor.Role == UserRole.Doctor)
                {
                    tokens = tokens.Where(t => t.Clinic.DoctorId == doctor.UserId);
                }
            }
            else if (role == "Reception" || role == "Admin")
            {
                // يشوف كل التوكنز
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(tokens.OrderBy(t => t.CreatedAt).ToList());
        }

        // Unified Create (مريض + توكن)
        public IActionResult CreateUnified()
        {
            var vm = new CreateUnifiedViewModel
            {
                Departments = _context.Departments
                 .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.DepartmentName })
                 .ToList(),
                Clinics = _context.Clinics
                 .Select(c => new SelectListItem { Value = c.ClinicId.ToString(), Text = c.ClinicName })
                 .ToList(),
                Priorities = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Normal", Text = "Normal" },
                    new SelectListItem { Value = "Urgent", Text = "Urgent" }
                }
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CreateUnified(CreateUnifiedViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    FullName = vm.FullName,
                    PatientCode = vm.PatientCode
                };
                _context.Patients.Add(patient);
                _context.SaveChanges();

                var token = new Token
                {
                    ClinicId = vm.ClinicId,
                    PatientId = patient.PatientId,
                    CreatedAt = DateTime.Now,
                    Status = TokenStatus.Waiting,
                    Priority = vm.Priority
                };
                _context.Tokens.Add(token);
                _context.SaveChanges();

                _hubContext.Clients.All.SendAsync("UpdateWaitingList");

                return RedirectToAction("WaitingRoom");
            }

            return View(vm);
        }

        // استدعاء المريض التالي
        public IActionResult NextToken(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Doctor" && role != "Admin") return RedirectToAction("Index", "Home");

            var token = _context.Tokens.Find(id);
            if (token == null) return NotFound();

            token.Status = TokenStatus.InProgress;
            _context.Tokens.Update(token);
            _context.SaveChanges();

            _hubContext.Clients.All.SendAsync("UpdateWaitingList");

            return RedirectToAction("WaitingRoom");
        }

        // إنهاء المريض الحالي
        public IActionResult CompleteToken(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Doctor" && role != "Admin") return RedirectToAction("Index", "Home");

            var token = _context.Tokens.Find(id);
            if (token == null) return NotFound();

            token.Status = TokenStatus.Completed;
            _context.Tokens.Update(token);
            _context.SaveChanges();

            _hubContext.Clients.All.SendAsync("UpdateWaitingList");

            return RedirectToAction("WaitingRoom");
        }
    }
}
