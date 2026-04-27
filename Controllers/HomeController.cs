using Microsoft.AspNetCore.Mvc;
using HospitalQueueMS.Models;
using HospitalQueueMS.Data;
using Microsoft.AspNetCore.Http;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    // صفحة الـ Login
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult CreateUser() => View();

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // تسجيل خروج
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        // أمثلة ثابتة لليوزر والباسورد
        if (username == "reception" && password == "123")
        {
            HttpContext.Session.SetString("Role", "Reception");
            return RedirectToAction("ReceptionDashboard");
        }
        else if (username == "doctor" && password == "123")
        {
            HttpContext.Session.SetString("Role", "Doctor");
            return RedirectToAction("DoctorDashboard");
        }
        else if (username == "admin" && password == "123")
        {
            HttpContext.Session.SetString("Role", "Admin");
            return RedirectToAction("AdminDashboard");
        }

        // لو البيانات غلط
        ViewBag.Error = "Invalid username or password";
        return View();
    }

    // صفحة الاستقبال
    public IActionResult ReceptionDashboard()
    {
        var role = HttpContext.Session.GetString("Role");
        if (role != "Reception") return RedirectToAction("Index");
        return View();
    }

    // صفحة الدكتور
    public IActionResult DoctorDashboard()
    {
        var role = HttpContext.Session.GetString("Role");
        if (role != "Doctor") return RedirectToAction("Index");
        return View();
    }

    // صفحة الأدمن
    public IActionResult AdminDashboard()
    {
        var role = HttpContext.Session.GetString("Role");
        if (role != "Admin") return RedirectToAction("Index");
        return View();
    }
}
