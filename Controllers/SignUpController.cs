using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Controllers
{
    public class SignUpController : Controller
    {
        private readonly AppDbContext _context;

        public SignUpController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string fullname, string email, string password, string skill)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    ViewBag.Error = "User with this email already exists.";
                    return View();
                }

                var user = new User
                {
                    FullName = fullname,
                    Email = email,
                    Password = password, // In a real app, hash this!
                    Skill = skill
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login", "Login");
            }

            return View();
        }
    }
}
