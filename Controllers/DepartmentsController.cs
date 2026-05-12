using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalQueueMS.Data;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.SignalR;
using HospitalQueueMS.Hubs;
using System.Threading.Tasks;
using System.Linq;

namespace HospitalQueueMS.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<WaitingRoomHub> _hubContext;

        public DepartmentsController(ApplicationDbContext context, IHubContext<WaitingRoomHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        } 

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                // الخاص بالتحديث SignalR
                await _hubContext.Clients.All.SendAsync("UpdateDepartments");

                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();

                    await _hubContext.Clients.All.SendAsync("UpdateDepartments");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departments.Any(e => e.DepartmentId == department.DepartmentId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);

            if (department == null) return NotFound();

            return View(department);
        }


        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            { 
                return NotFound(); 
            }
            
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("UpdateDepartments");
            
            return RedirectToAction(nameof(Index));
        }
    }
}
