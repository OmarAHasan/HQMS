using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalQueueMS.Data;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.SignalR;
using HospitalQueueMS.Hubs;

namespace HospitalQueueMS.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<QueueHub> _hubContext;

        public DepartmentsController(ApplicationDbContext context, IHubContext<QueueHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }
        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                Console.WriteLine("Department created successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) 
            {
                return NotFound(); 
            }
            var department = await _context.Departments.FindAsync(id); 
            if (department == null) 
            {
                return NotFound(); 
            }
            return View(department); 
        }
        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId) { return NotFound();
            }
            if (ModelState.IsValid)
            {
                try 
                {
                    _context.Update(department); await _context.SaveChangesAsync();
                    Console.WriteLine("Department updated successfully!"); 
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!_context.Departments.Any(e => e.DepartmentId == department.DepartmentId)) 
                    {
                        return NotFound(); 
                    }
                    else 
                    {
                        throw; 
                    }
                } 
                return RedirectToAction(nameof(Index));
            } 
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                Console.WriteLine("Department deleted successfully!");
            }
            return RedirectToAction(nameof(Index));
        }




    }
}
