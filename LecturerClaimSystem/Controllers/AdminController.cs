using LecturerClaimSystem.Data;
using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LecturerClaimSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context) => _context = context;

        // Coordinator: review submitted claims
        public async Task<IActionResult> ReviewClaims()
        {
            var claims = await _context.Claims
                .Include(c => c.Lecturer)
                .Where(c => c.Status == ClaimStatus.Submitted)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            return View(claims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForwardClaim(int id, string coordinatorMessage)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Forwarded;
            _context.Feedbacks.Add(new Feedback
            {
                ClaimId = id,
                Role = "Coordinator",
                Message = string.IsNullOrWhiteSpace(coordinatorMessage) ? "Forwarded to manager" : coordinatorMessage,
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ReviewClaims));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectByCoordinator(int id, string coordinatorMessage)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            _context.Feedbacks.Add(new Feedback
            {
                ClaimId = id,
                Role = "Coordinator",
                Message = string.IsNullOrWhiteSpace(coordinatorMessage) ? "Rejected by coordinator" : coordinatorMessage,
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ReviewClaims));
        }

        // Manager views forwarded claims
        public async Task<IActionResult> VerifyClaims()
        {
            var claims = await _context.Claims
                .Include(c => c.Lecturer)
                .Where(c => c.Status == ClaimStatus.Forwarded)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            return View(claims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveClaim(int id, string managerMessage)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Approved;
            _context.Feedbacks.Add(new Feedback
            {
                ClaimId = id,
                Role = "Manager",
                Message = string.IsNullOrWhiteSpace(managerMessage) ? "Approved" : managerMessage,
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VerifyClaims));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectClaimByManager(int id, string managerMessage)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            _context.Feedbacks.Add(new Feedback
            {
                ClaimId = id,
                Role = "Manager",
                Message = string.IsNullOrWhiteSpace(managerMessage) ? "Rejected by manager" : managerMessage,
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(VerifyClaims));
        }
    }
}
