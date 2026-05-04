using HospitalQueueMS.Data;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalQueueMS.Controllers
{
    public class ClinicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض كل العيادات
        public IActionResult Index()
        {
            var clinics = _context.Clinics.Include(c => c.Department).ToList();
            return View(clinics);
        }

        // إنشاء عيادة جديدة (GET)
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                }).ToList();

            return View();
        }

        // إنشاء عيادة جديدة (POST)
        [HttpPost]
        public IActionResult Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _context.Clinics.Add(clinic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _context.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                }).ToList();

            return View(clinic);
        }
        [HttpGet]
        public IActionResult GetByDepartment(int departmentId)
        {
            var clinics = _context.Clinics
                .Where(c => c.DepartmentId == departmentId)
                .Select(c => new { clinicId = c.ClinicId, clinicName = c.ClinicName })
                .ToList();

            return Json(clinics);
        }

        // GET: Clinics/Edit/5
        public IActionResult Edit(int id)
        {
            var clinic = _context.Clinics.Find(id);
            if (clinic == null) return NotFound();

            ViewBag.Departments = _context.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                }).ToList();

            return View(clinic);
        }

        [HttpPost]
        public IActionResult Edit(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _context.Clinics.Update(clinic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinic);
        }

        [HttpGet]
        public IActionResult DeleteConfirmed()
        {
            return RedirectToAction("Index");
        }
        // GET: Clinics/Delete/5
        public IActionResult Delete(int id)
        {
            var clinic = _context.Clinics
                .Include(c => c.Department)
                .FirstOrDefault(c => c.ClinicId == id);

            if (clinic == null) return NotFound();

            return View(clinic);
        }

        // POST: Clinics/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var clinic = _context.Clinics.Find(id);
            if (clinic == null) return NotFound();

            _context.Clinics.Remove(clinic);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
