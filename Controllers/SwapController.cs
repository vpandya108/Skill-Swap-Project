using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Controllers
{
    public class SwapController : Controller
    {
        private readonly AppDbContext _context;

        public SwapController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult RequestSwap(int receiverId)
        {
            var senderId = HttpContext.Session.GetInt32("UserId");
            if (senderId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Check if request already exists
            var existingRequest = _context.SwapRequests.FirstOrDefault(r => 
                r.SenderId == senderId && r.ReceiverId == receiverId && r.Status == "Pending");

            if (existingRequest != null)
            {
                TempData["Message"] = "Swap request already sent.";
                return RedirectToAction("Exploreview", "Explore");
            }

            var swapRequest = new SwapRequest
            {
                SenderId = senderId.Value,
                ReceiverId = receiverId,
                Status = "Pending",
                RequestDate = DateTime.Now
            };

            _context.SwapRequests.Add(swapRequest);
            _context.SaveChanges();

            TempData["Message"] = "Swap request sent successfully!";
            return RedirectToAction("Exploreview", "Explore");
        }

        public IActionResult MySwaps()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var incomingRequests = _context.SwapRequests.Where(r => r.ReceiverId == userId).ToList();
            var outgoingRequests = _context.SwapRequests.Where(r => r.SenderId == userId).ToList();

            ViewBag.Incoming = incomingRequests;
            ViewBag.Users = _context.Users.ToDictionary(u => u.Id, u => u.FullName);
            ViewBag.UserSkills = _context.Users.ToDictionary(u => u.Id, u => u.Skill); // Added missing skills dictionary

            return View(outgoingRequests); // Passed outgoingRequests as the model
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

            return RedirectToAction("MySwaps");
        }
    }
}
