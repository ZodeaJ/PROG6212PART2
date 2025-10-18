using LecturerClaimSystem.Data;
using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LecturerClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private readonly AppDbContext _context;

        public ClaimController(AppDbContext context)
        {
            _context = context;
        }
        //track claim status feedback
        public IActionResult TrackClaimStatus()
        {
            return View();
        }
    }
}
