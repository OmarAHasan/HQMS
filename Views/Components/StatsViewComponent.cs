using HospitalQueueMS.Data;
using HospitalQueueMS.Models;
using Microsoft.AspNetCore.Mvc;

public class StatsViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public StatsViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var waitingCount = _context.Tokens.Count(t => t.Status == TokenStatus.Waiting);
        var completedCount = _context.Tokens.Count(t => t.Status == TokenStatus.Completed);
        var urgentCount = _context.Tokens.Count(t => t.Priority == "Urgent" && t.Status == TokenStatus.Waiting);
        var normalCount = _context.Tokens.Count(t => t.Priority == "Normal" && t.Status == TokenStatus.Waiting);

        ViewBag.WaitingCount = waitingCount;
        ViewBag.CompletedCount = completedCount;
        ViewBag.UrgentCount = urgentCount;
        ViewBag.NormalCount = normalCount;

        return View("Default"); // هيعرض Views/Shared/Components/Stats/Default.cshtml
    }
}
