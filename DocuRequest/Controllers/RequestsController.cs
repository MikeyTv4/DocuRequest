using DocuRequest.Data;
using DocuRequest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocuRequest.Controllers;

public class RequestsController : Controller
{
    private readonly AppDbContext _db;

    public RequestsController(AppDbContext db) => _db = db;

    // GET /Requests/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(new DocumentRequest { PickupDate = DateTime.Today.AddDays(3) });
    }

    // POST /Requests/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DocumentRequest model)
    {
        if (model.PickupDate is { } pickup && pickup.Date < DateTime.Today)
            ModelState.AddModelError(nameof(model.PickupDate), "Pickup date cannot be in the past.");

        if (!ModelState.IsValid)
            return View(model);

        // Server controls these fields, never the form.
        model.Id = 0;
        model.Status = RequestStatus.Pending;
        model.StaffRemarks = null;
        model.CreatedAt = model.UpdatedAt = DateTime.Now;
        model.ReferenceNo = await GenerateReferenceAsync();

        _db.DocumentRequests.Add(model);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Your request was submitted. Save your reference number below.";
        return RedirectToAction(nameof(Details), new { referenceNo = model.ReferenceNo });
    }

    // GET /Requests/Track?studentId=...
    [HttpGet]
    public async Task<IActionResult> Track(string? studentId)
    {
        ViewBag.StudentId = studentId;
        if (string.IsNullOrWhiteSpace(studentId))
            return View(new List<DocumentRequest>());

        var results = await _db.DocumentRequests
            .AsNoTracking()
            .Where(r => r.StudentId == studentId.Trim())
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return View(results);
    }

    // GET /Requests/Details?referenceNo=DR-...
    [HttpGet]
    public async Task<IActionResult> Details(string referenceNo)
    {
        var request = await _db.DocumentRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReferenceNo == referenceNo);

        return request is null ? NotFound() : View(request);
    }

    private async Task<string> GenerateReferenceAsync()
    {
        while (true)
        {
            var candidate = $"DR-{DateTime.Now:yyMMdd}-{Random.Shared.Next(1000, 10000)}";
            if (!await _db.DocumentRequests.AnyAsync(r => r.ReferenceNo == candidate))
                return candidate;
        }
    }
}
