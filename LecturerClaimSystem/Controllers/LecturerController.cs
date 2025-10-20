using System.Linq;
using System.Threading.Tasks;
using LecturerClaimSystem.Data;
using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LecturerClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileStorage _fileStorage;

        public LecturerController(AppDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public IActionResult MakeAClaim()
        {
            // Fetch lecturers from the database and create a SelectList
            var lecturers = _context.Lecturers
                .Select(l => new { l.LecturerId, l.Name })
                .ToList();

            ViewBag.Lecturers = new SelectList(lecturers, "LecturerId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAClaim(Claim claim, IFormFile SupportingDocument)
        {
            if (!ModelState.IsValid)
            {
                // repopulate dropdown if the form is redisplayed
                var lecturers = _context.Lecturers
                    .Select(l => new { l.LecturerId, l.Name })
                    .ToList();
                ViewBag.Lecturers = new SelectList(lecturers, "LecturerId", "Name");

                TempData["ErrorMessage"] = "Please recheck the form values.";
                return View(claim);
            }

            // Optional: validate lecturer exists
            var lecturer = await _context.Lecturers.FindAsync(claim.LecturerId);
            if (lecturer == null)
            {
                var lecturers = _context.Lecturers
                    .Select(l => new { l.LecturerId, l.Name })
                    .ToList();
                ViewBag.Lecturers = new SelectList(lecturers, "LecturerId", "Name");

                TempData["ErrorMessage"] = "Lecturer does not exist.";
                return View(claim);
            }

            try
            {
                if (SupportingDocument != null)
                {
                    var savedName = await _fileStorage.SaveFile(SupportingDocument);
                    claim.SupportingDocument = savedName;
                }
                else
                {
                    ModelState.AddModelError("SupportingDocument", "Supporting document is required.");

                    var lecturers = _context.Lecturers
                        .Select(l => new { l.LecturerId, l.Name })
                        .ToList();
                    ViewBag.Lecturers = new SelectList(lecturers, "LecturerId", "Name");

                    return View(claim);
                }

                claim.CreatedAt = DateTime.UtcNow;
                claim.Status = ClaimStatus.Submitted;

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction(nameof(MakeAClaim));
            }
            catch (Exception ex)
            {
                var lecturers = _context.Lecturers
                    .Select(l => new { l.LecturerId, l.Name })
                    .ToList();
                ViewBag.Lecturers = new SelectList(lecturers, "LecturerId", "Name");

                TempData["ErrorMessage"] = "Error submitting claim: " + ex.Message;
                return View(claim);
            }
        }
    }
}
