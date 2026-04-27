using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HospitalQueueMS.Data;
using HospitalQueueMS.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

public class LoginController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public LoginController(ApplicationDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        var user = _context.Users
      .FirstOrDefault(u => u.Username == username && u.PasswordHash == password);


        if (user != null)
        {
            HttpContext.Session.SetString("Role", user.Role.ToString()); 
            HttpContext.Session.SetString("Username", user.Username);

            if (user.Role == UserRole.Reception) return RedirectToAction("ReceptionDashboard");
            if (user.Role == UserRole.Doctor) return RedirectToAction("DoctorDashboard");
            if (user.Role == UserRole.Admin) return RedirectToAction("AdminDashboard");
        }

        ViewBag.Error = "Invalid username or password";
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetString("Role", user.Role.ToString());
                HttpContext.Session.SetString("Username", user.Username);

                // توجيه حسب الدور
                if (user.Role == UserRole.Reception)
                    return RedirectToAction("ReceptionDashboard", "Home");
                if (user.Role == UserRole.Doctor)
                    return RedirectToAction("DoctorDashboard", "Home");
                if (user.Role == UserRole.Admin)
                    return RedirectToAction("AdminDashboard", "Home");
            }
        }

        ViewBag.Error = "Invalid username or password";
        return View();
    }


}
