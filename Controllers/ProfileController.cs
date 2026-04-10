using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        // View your own or another user's profile
        public IActionResult Userprofile(int? id)
        {
            var userId = id ?? HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/UserProfile/Userprofile.cshtml", user);
        }

        // Show Edit Form
        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(userId);
            return View("~/Views/Account/CreateProfile.cshtml", user);
        }

        // Save Profile Changes
        [HttpPost]
        public IActionResult UpdateProfile(string bio, string location, string skillTeach, string skillLearn)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.Bio = bio;
                user.Skill = skillTeach;
                user.LearnSkill = skillLearn;
                // Add more fields if needed (location is not yet in User model, but we can add it later)
                
                _context.SaveChanges();
                
                // Update session if skill changed
                HttpContext.Session.SetString("UserSkill", user.Skill);
            }

            return RedirectToAction("Userprofile");
        }
    }
}
