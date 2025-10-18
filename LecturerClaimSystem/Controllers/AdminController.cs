using LecturerClaimSystem.Data;
using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LecturerClaimSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        //verify claims method
        public IActionResult VerifyClaims()
        {
            return View();
        }
        //review claims method
        public IActionResult ReviewClaims()
        {
            return View();
        }
    }
}
