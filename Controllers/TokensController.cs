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
        private readonly IHubContext<WaitingRoomHub> _hubContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TokensController(ApplicationDbContext context,
                          IHubContext<WaitingRoomHub> hubContext,
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
            var clinic = _context.Clinics
                .Include(c => c.Department)
                .FirstOrDefault(c => c.ClinicId == clinicId);

            if (clinic == null) return NotFound();

            string prefix = clinic.Department.Prefix ?? "X";

            var lastToken = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Clinic.DepartmentId == clinic.DepartmentId)
                .OrderByDescending(t => t.TokenNumber)
                .FirstOrDefault();

            int nextNumber = lastToken != null ? lastToken.TokenNumber + 1 : 1;

            var token = new Token
            {
                ClinicId = clinicId,
                TokenNumber = nextNumber,
                TokenCode = $"{prefix}-{nextNumber}", 
                CreatedAt = DateTime.Now,
                Status = TokenStatus.Waiting
            };

            _context.Tokens.Add(token);
            Console.WriteLine($"TokenCode: {token.TokenCode}");

            _context.SaveChanges();

            return RedirectToAction("WaitingRoom");
        }


        public IActionResult Index()
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status == TokenStatus.Waiting || t.Status == TokenStatus.InProgress) 
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
            //  في حالة الادمن فايقدر يشوف الكل عادي

            return View(tokens);
        }



        public IActionResult WaitingRoom()
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status != TokenStatus.Completed)
                .OrderByDescending(t => t.Priority == "Urgent" ? 1 : 0) // الأولوية للـ urgent
                .ThenBy(t => t.CreatedAt)
                .ToList();

            // فلترة حسب العيادة لو المستخدم دكتور
            if (User.IsInRole("Doctor"))
            {
                var userId = _userManager.GetUserId(User);
                var doctorClinic = _context.Clinics.FirstOrDefault(c => c.DoctorUserId == userId);

                if (doctorClinic != null)
                {
                    ViewBag.ClinicId = doctorClinic.ClinicId;
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
                // check ان الرقم سعودي 
                if (string.IsNullOrEmpty(vm.MobileNumber) ||
                    !System.Text.RegularExpressions.Regex.IsMatch(vm.MobileNumber, @"^0\d{9}$"))
                {
                    ModelState.AddModelError("MobileNumber", "Phone number must be a Saudi number");
                    return View(vm);
                }

                var today = DateTime.Today;

                var lastToken = _context.Tokens
                    .Where(t => t.DepartmentId == vm.DepartmentId && t.CreatedAt.Date == today)
                    .OrderByDescending(t => t.TokenNumber)
                    .FirstOrDefault();

                int nextNumber = lastToken != null ? lastToken.TokenNumber + 1 : 1;

                // تحديد العيادة كل مريض 
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

                // جِب الـ Department علشان نطلع الـ Prefix 
                var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == vm.DepartmentId);
                string prefix = !string.IsNullOrEmpty(department?.Prefix) ? department.Prefix : "X";

                var token = new Token
                {
                    ClinicId = clinicId,
                    DepartmentId = vm.DepartmentId,
                    MobileNumber = vm.MobileNumber,
                    TokenNumber = nextNumber,
                    CreatedAt = DateTime.Now,
                    Status = TokenStatus.Waiting,
                    Priority = vm.Priority,
                    TokenCode = $"{prefix}-{nextNumber}" //  كل يوم يبدأ العد من جديد 
                };



                _context.Tokens.Add(token);
                _context.SaveChanges();
                _hubContext.Clients.All.SendAsync("UpdateDepartments");
                _hubContext.Clients.All.SendAsync("UpdateWaitingList");
                return RedirectToAction("Index", "Home");
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

        [HttpPost]
        public async Task<IActionResult> NextToken(int id)
        {
            if (!User.IsInRole("Doctor") && !User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.TokenId == id);
            if (token != null)
            {
                token.Status = TokenStatus.InProgress;
                _context.Update(token);
                await _context.SaveChangesAsync();

                // فقط تحديث القوائم
                // تحديث عام للـ Reception/Admin
                await _hubContext.Clients.Group("Reception").SendAsync("UpdateWaitingList");
                await _hubContext.Clients.Group("Admin").SendAsync("UpdateWaitingList");

                // تحديث خاص للدكتور صاحب العيادة
                await _hubContext.Clients.Group($"Clinic_{token.ClinicId}").SendAsync("UpdateWaitingList");

                return Json(new { success = true, tokenId = id });
            }

            return Json(new { success = false });
        }


        [HttpPost]
        public async Task<IActionResult> CompleteToken(int id)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.TokenId == id);
            if (token != null)
            {
                token.Status = TokenStatus.Completed; 
                token.CompletedAt = DateTime.Now;
                _context.Update(token);
                await _context.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("UpdateDepartments");
                await _hubContext.Clients.Group("Reception").SendAsync("UpdateWaitingList");
                await _hubContext.Clients.Group("Admin").SendAsync("UpdateWaitingList");

                await _hubContext.Clients.Group($"Clinic_{token.ClinicId}").SendAsync("UpdateWaitingList"); return Json(new { success = true, tokenId = id });
            }

            return Json(new { success = false });
        }
        public IActionResult TokensPartial()
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status == TokenStatus.Waiting || t.Status == TokenStatus.InProgress)
                .ToList();

            return PartialView("_TokensTable", tokens);
        }

        public IActionResult WaitingRoomPartial(int clinicId)
        {
            var tokens = _context.Tokens
                .Include(t => t.Clinic)
                .ThenInclude(c => c.Department)
                .Where(t => t.Status != TokenStatus.Completed)
                .OrderByDescending(t => t.Priority == "Urgent" ? 1 : 0) 
                .ThenBy(t => t.CreatedAt)
                .ToList();

            if (User.IsInRole("Doctor"))
            {
                tokens = tokens.Where(t => t.ClinicId == clinicId).ToList();
            }

            return PartialView("_WaitingRoomTable", tokens);
        }



        public IActionResult CompletedTokens()
        {
            var tokens = _context.Tokens
       .Include(t => t.Clinic)
       .ThenInclude(c => c.Department)
       .Where(t => t.Status == TokenStatus.Completed)
       .OrderByDescending(t => t.CompletedAt)
       .ToList();

            if (User.IsInRole("Admin"))
            {
                var cutoff = DateTime.Now.AddDays(-3);
                tokens = tokens.Where(t => t.CompletedAt >= cutoff).ToList();
            }
            else
            {
                var cutoff = DateTime.Now.AddDays(-1);
                tokens = tokens.Where(t => t.CompletedAt >= cutoff).ToList();
            }

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
        [HttpPost]
        public async Task<IActionResult> DeleteCompleted()
        {
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            var tokens = _context.Tokens.Where(t => t.Status == TokenStatus.Completed).ToList();
            _context.Tokens.RemoveRange(tokens);
            await _context.SaveChangesAsync();

            return RedirectToAction("CompletedTokens");
        }

    }
}
