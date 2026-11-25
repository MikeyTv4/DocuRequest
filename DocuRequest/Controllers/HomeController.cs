using DocuRequest.Data;
using Microsoft.AspNetCore.Mvc;

namespace DocuRequest.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db) => _db = db;

    public IActionResult Index()
    {
        ViewBag.TotalRequests = _db.DocumentRequests.Count();
        ViewBag.Released = _db.DocumentRequests.Count(r => r.Status == Models.RequestStatus.Released);
        return View();
    }

    public IActionResult Error() => View();
}
