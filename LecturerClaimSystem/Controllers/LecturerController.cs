using LecturerClaimSystem.Data;
using LecturerClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LecturerClaimSystem.Controllers
{
        public class LecturerController : Controller
        {
            private readonly AppDbContext _context;

            public LecturerController(AppDbContext context)
            {
                _context = context;
            }

            //make a claim method
            public IActionResult MakeAClaim()
            {
                return View();
            }
            //claim feedback method
            public IActionResult ClaimFeedback()
            {
                return View();
            }
        }
    }