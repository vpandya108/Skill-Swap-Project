using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Controllers
{
    public class MySwapController : Controller
    {
        private readonly AppDbContext _context;

        public MySwapController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Swaps()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var incomingRequests = _context.SwapRequests.Where(r => r.ReceiverId == userId).ToList();
            var outgoingRequests = _context.SwapRequests.Where(r => r.SenderId == userId).ToList();

            ViewBag.Incoming = incomingRequests;
            var users = _context.Users.ToDictionary(u => u.Id, u => u.FullName);
            var userSkills = _context.Users.ToDictionary(u => u.Id, u => u.Skill);
            
            ViewBag.Users = users;
            ViewBag.UserSkills = userSkills;

            return View(outgoingRequests);
        }

        [HttpPost]
        public IActionResult RespondToRequest(int requestId, string status)
        {
            var request = _context.SwapRequests.Find(requestId);
            if (request != null)
            {
                request.Status = status; // "Accepted" or "Declined"
                _context.SaveChanges();
            }

            return RedirectToAction("Swaps");
        }
    }
}
