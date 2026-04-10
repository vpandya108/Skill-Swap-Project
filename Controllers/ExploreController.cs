using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Controllers
{
    public class ExploreController : Controller
    {
        private readonly AppDbContext _context;

        public ExploreController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Exploreview(string searchTerm)
        {
            var users = _context.Users.AsQueryable();

            // Exclude current user if logged in
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                users = users.Where(u => u.Id != userId);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.Skill.Contains(searchTerm) || 
                                       u.FullName.Contains(searchTerm) ||
                                       u.LearnSkill.Contains(searchTerm));
            }

            return View(users.ToList());
        }
    }
}
