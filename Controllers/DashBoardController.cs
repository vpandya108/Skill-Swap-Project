using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;

namespace Skill_Swap_Project.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _context;

        public DashBoardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult DashBoardview()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Stats
            ViewBag.SkillsOffered = _context.Users.Count(u => u.Id == userId);
            ViewBag.ActiveSwaps = _context.SwapRequests.Count(r => (r.SenderId == userId || r.ReceiverId == userId) && r.Status == "Accepted");
            
            // Recommended (just some users for now)
            var recommended = _context.Users.Where(u => u.Id != userId).Take(4).ToList();

            return View(recommended);
        }

        public IActionResult Seed()
        {
            if (!_context.Users.Any(u => u.Email == "rahul@example.com"))
            {
                _context.Users.AddRange(new List<Models.User>
                {
                    new Models.User { FullName = "Rahul Sharma", Email = "rahul@example.com", Password = "password123", Skill = "Web Development", LearnSkill = "Data Science", Bio = "Passionate dev looking for data insights." },
                    new Models.User { FullName = "Ananya Patel", Email = "ananya@example.com", Password = "password123", Skill = "Graphic Design", LearnSkill = "Public Speaking", Bio = "Designer wanting to improve communication." },
                    new Models.User { FullName = "Alex Thomas", Email = "alex@example.com", Password = "password123", Skill = "Photography", LearnSkill = "Video Editing", Bio = "Capturing moments, now wanting to edit them." }
                });
                _context.SaveChanges();
            }
            return RedirectToAction("DashBoardview");
        }
    }
}
