using Microsoft.AspNetCore.Mvc;

namespace HospitalQueueMS.Controllers
{
    public class HomeController : Controller
    {
        // صفحة الأدمن
        public IActionResult AdminDashboard()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Login", "Users");

            return View();
        }

        // صفحة الدكتور
        public IActionResult DoctorDashboard()
        {
            if (!User.IsInRole("Doctor"))
                return RedirectToAction("Login", "Users");

            return View();
        }

        // صفحة الاستقبال
        public IActionResult ReceptionDashboard()
        {
            if (!User.IsInRole("Reception"))
                return RedirectToAction("Login", "Users");

            return View();
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("AdminDashboard");
            else if (User.IsInRole("Doctor"))
                return RedirectToAction("DoctorDashboard");
            else if (User.IsInRole("Reception"))
                return RedirectToAction("ReceptionDashboard");
            else
                return RedirectToAction("Login", "Users");
        }

    }
}
