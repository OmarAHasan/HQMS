using HospitalQueueMS.Data;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalQueueMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(ApplicationDbContext context,
                               UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                    return RedirectToAction("AdminDashboard", "Home");
                else if (roles.Contains("Doctor"))
                    return RedirectToAction("Index", "Tokens");
                else if (roles.Contains("Reception"))
                    return RedirectToAction("ReceptionDashboard", "Home");

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        //// GET
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //// POST: Register
        //[HttpPost]
        //public async Task<IActionResult> Register(string username, string password, string role)
        //{
        //    var user = new IdentityUser { UserName = username };
        //    var result = await _userManager.CreateAsync(user, password);

        //    if (result.Succeeded)
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole(role));
        //        }

        //        await _userManager.AddToRoleAsync(user, role);
        //        return RedirectToAction("Login");
        //    }

        //    ViewBag.Error = "Failed to register user";
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = roles
                });
            }

            return View(model);
        }


        // GET
        public IActionResult Create()
        {
            ViewBag.Roles = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "Doctor", Text = "Doctor" },
        new SelectListItem { Value = "Reception", Text = "Reception" }
    };

            ViewBag.Clinics = _context.Clinics
                .Select(c => new SelectListItem
                {
                    Value = c.ClinicId.ToString(),
                    Text = c.ClinicName
                }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string username, string password, string role, int? clinicId)
        {
            var user = new IdentityUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // التأكد من الصلاحية 
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                // ربطها باليوزر 
                await _userManager.AddToRoleAsync(user, role);

                // لو الصلاحية دكتور اربطه بالعيادة
                if (role == "Doctor" && clinicId.HasValue)
                {
                    var clinic = _context.Clinics.Find(clinicId.Value);
                    if (clinic != null)
                    {
                        clinic.DoctorUserId = user.Id;
                        _context.Clinics.Update(clinic);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.Error = string.Join(", ", result.Errors.Select(e => e.Description));
            return View();
        }


        // GET
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string username, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.UserName = username;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // تحديث الدور
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, role);

                return RedirectToAction("Index");
            }

            ViewBag.Error = string.Join(", ", result.Errors.Select(e => e.Description));
            return View(user);
        }

        // GET
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = string.Join(", ", result.Errors.Select(e => e.Description));
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");

            return RedirectToAction("Login", "Users");
        }

    }
}
