using HospitalQueueMS.Data;
using HospitalQueueMS.Hubs;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HospitalQueueMS.Controllers
{
    public class TokensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<QueueHub> _hubContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TokensController(ApplicationDbContext context,
                                IHubContext<QueueHub> hubContext,
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            ViewBag.Clinics = _context.Clinics
                .Select(c => new SelectListItem
                {
                    Value = c.ClinicId.ToString(),
                    Text = c.ClinicName
                }).ToList();

            if (!User.IsInRole("Reception") && !User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            ViewBag.Departments = _context.Departments.Include(d => d.Clinics).ToList();
            return View();


        }
        [HttpPost]
        public IActionResult Create(int clinicId)
        {
            // نجيب القسم المرتبط بالعيادة
            var clinic = _context.Clinics
                .Include(c => c.Department)
                .FirstOrDefault(c => c.ClinicId == clinicId);

            if (clinic == null) return NotFound();

            int departmentId = clinic.DepartmentId;

            // نجيب آخر توكن في نفس القسم (بغض النظر عن العيادة)
            var lastToken = _context.Tokens
                .Include(t => t.Clinic)
                .Where(t => t.Clinic.DepartmentId == departmentId)
                .OrderByDescending(t => t.TokenNumber)
                .FirstOrDefault();

            int nextNumber = lastToken != null ? lastToken.TokenNumber + 1 : 1;

            var token = new Token
            {
                ClinicId = clinicId,
                DepartmentId = departmentId,
                TokenNumber = nextNumber,
                CreatedAt = DateTime.Now,
                Status = TokenStatus.Waiting,
                Priority = "Normal"
            };

            _context.Tokens.Add(token);
            _context.SaveChanges();

            ViewBag.TokenNumber = nextNumber;
            return View("TokenCreated", token);
        }

        // عرض كل التوكنز (Manage Tokens)
        public IActionResult Index()
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status == TokenStatus.Waiting || t.Status == TokenStatus.InProgress) // ✅ فلترة
                .ToList();

            if (User.IsInRole("Doctor"))
            {
                var userId = _userManager.GetUserId(User);
                var doctorClinic = _context.Clinics.FirstOrDefault(c => c.DoctorUserId == userId);

                if (doctorClinic != null)
                {
                    tokens = tokens.Where(t => t.ClinicId == doctorClinic.ClinicId).ToList();
                }
                else
                {
                    tokens = new List<Token>(); // لو الدكتور مش مربوط بعيادة
                }
            }
            // لو Admin → يشوف الكل عادي

            return View(tokens);
        }



        // شاشة الانتظار (تتغير حسب الـ Role)
        public IActionResult WaitingRoom()
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status != TokenStatus.Completed)
                .ToList();

            if (User.IsInRole("Doctor"))
            {
                var userId = _userManager.GetUserId(User);
                var doctorClinic = _context.Clinics.FirstOrDefault(c => c.DoctorUserId == userId);

                if (doctorClinic != null)
                {
                    tokens = tokens.Where(t => t.ClinicId == doctorClinic.ClinicId).ToList();
                }
                else
                {
                    tokens = new List<Token>();
                }
            }

            return View(tokens);
        }

        [HttpGet]
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
                // تحقق من رقم الموبايل: لازم يبدأ بـ 0 ويكون 10 أرقام
                if (string.IsNullOrEmpty(vm.MobileNumber) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(vm.MobileNumber, @"^0\d{9}$"))
                {
                    ModelState.AddModelError("MobileNumber", "Phne number must be a Saudi number");
                    return View(vm);
                }

                // حساب رقم التوكن بناءً على القسم
                var lastToken = _context.Tokens
                    .Where(t => t.DepartmentId == vm.DepartmentId)
                    .OrderByDescending(t => t.TokenNumber)
                    .FirstOrDefault();

                int nextNumber = lastToken != null ? lastToken.TokenNumber + 1 : 1;

                int clinicId;
                if (vm.ClinicId.HasValue)
                {
                    clinicId = vm.ClinicId.Value;
                }
                else
                {
                    clinicId = _context.Clinics
                        .Where(c => c.DepartmentId == vm.DepartmentId &&
                                    !_context.Tokens.Any(t => t.ClinicId == c.ClinicId && t.Status == TokenStatus.Waiting))
                        .Select(c => c.ClinicId)
                        .FirstOrDefault();

                    if (clinicId == 0)
                    {
                        clinicId = _context.Clinics
                            .Where(c => c.DepartmentId == vm.DepartmentId)
                            .Select(c => c.ClinicId)
                            .FirstOrDefault();
                    }
                }

                var token = new Token
                {
                    ClinicId = clinicId,
                    DepartmentId = vm.DepartmentId,
                    MobileNumber = vm.MobileNumber,
                    TokenNumber = nextNumber,
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


        public IActionResult GetClinicsByDepartment(int departmentId)
        {
            var clinics = _context.Clinics
                .Where(c => c.DepartmentId == departmentId)
                .Select(c => new SelectListItem
                {
                    Value = c.ClinicId.ToString(),
                    Text = c.ClinicName
                }).ToList();

            return Json(clinics);
        }

        // استدعاء المريض التالي
        [HttpPost]
        public IActionResult NextToken(int id)
        {

            if (!User.IsInRole("Doctor") && !User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var token = _context.Tokens.FirstOrDefault(t => t.TokenId == id);
            if (token != null)
            {
                token.Status = TokenStatus.InProgress;
                _context.Update(token);
                _context.SaveChanges();
                _hubContext.Clients.All.SendAsync("UpdateWaitingList");
                return Json(new { success = true, tokenId = id });
            }

            return Json(new { success = false });
        }



        // إنهاء المريض الحالي
        [HttpPost]
        public IActionResult CompleteToken(int id)
        {
            // اطبع القيمة اللي وصلت
            Console.WriteLine($"وصل CompleteToken بالـ id = {id}");

            var token = _context.Tokens.FirstOrDefault(t => t.TokenId == id);
            if (token != null)
            {
                token.Status = TokenStatus.Completed;
                token.CompletedAt = DateTime.Now;
                _context.Update(token);
                _context.SaveChanges();
                _hubContext.Clients.All.SendAsync("UpdateWaitingList");
                return Json(new { success = true, tokenId = id });
            }

            return Json(new { success = false });
        }

        public IActionResult CompletedTokens()
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status == TokenStatus.Completed)
                .ToList();

            if (User.IsInRole("Doctor"))
            {
                var userId = _userManager.GetUserId(User);
                var doctorClinic = _context.Clinics.FirstOrDefault(c => c.DoctorUserId == userId);

                if (doctorClinic != null)
                {
                    tokens = tokens.Where(t => t.ClinicId == doctorClinic.ClinicId).ToList();
                }
                else
                {
                    tokens = new List<Token>(); // لو الدكتور مش مربوط بعيادة
                }
            }
            // لو Admin أو Reception → يشوف الكل عادي

            return View(tokens);
        }



        public IActionResult Edit(int id)
        {
            var token = _context.Tokens
                .Include(t => t.Clinic)
                .Include(t => t.Clinic.Department)
                .FirstOrDefault(t => t.TokenId == id);

            if (token == null) return NotFound();

            ViewBag.Departments = _context.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                }).ToList();

            ViewBag.Clinics = _context.Clinics
                .Select(c => new SelectListItem
                {
                    Value = c.ClinicId.ToString(),
                    Text = c.ClinicName
                }).ToList();

            ViewBag.Priorities = new List<SelectListItem>
    {
        new SelectListItem { Value = "Normal", Text = "Normal" },
        new SelectListItem { Value = "Urgent", Text = "Urgent" }
    };

            return View(token);
        }


        [HttpPost]
        public IActionResult Edit(Token token)
        {
            if (ModelState.IsValid)
            {
                _context.Tokens.Update(token);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(token);
        }

        public IActionResult Delete(int id)
        {
            var token = _context.Tokens.Find(id);
            if (token == null) return NotFound();

            return View(token);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var token = _context.Tokens.Find(id);
            if (token == null) return NotFound();

            _context.Tokens.Remove(token);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
