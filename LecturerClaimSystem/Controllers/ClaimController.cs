using LecturerClaimSystem.Data;
using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LecturerClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClaimController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> FeedbackForClaim(int claimId)
        {
            var claim = await _context.Claims
                        .Include(c => c.FeedbackMessages)
                        .Include(c => c.Lecturer)
                        .FirstOrDefaultAsync(c => c.ClaimId == claimId);

            if (claim == null) return NotFound();

            return View(claim);
        }

        public async Task<IActionResult> ViewDocument(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim == null || string.IsNullOrEmpty(claim.SupportingDocument))
                return NotFound();

            var path = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", claim.SupportingDocument);
            var ext = Path.GetExtension(path).ToLowerInvariant();
            var contentType = ext switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream",
            };

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, contentType, Path.GetFileName(path));
        }

        // Delete action (if needed)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int claimId)
        {
            var claim = await _context.Claims
                                      .Include(c => c.FeedbackMessages)
                                      .FirstOrDefaultAsync(c => c.ClaimId == claimId);

            if (claim == null) return NotFound();

            _context.Feedbacks.RemoveRange(claim.FeedbackMessages);
            _context.Claims.Remove(claim);

            await _context.SaveChangesAsync();

            // Replace with your listing page, e.g., TrackClaimStatus or Index
            return RedirectToAction("TrackClaimStatus", "Lecturer", new { lecturerId = claim.LecturerId });
        }
    }
}
