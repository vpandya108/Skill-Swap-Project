using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserSkill", user.Skill);
                HttpContext.Session.SetInt32("UserId", user.Id);

                return RedirectToAction("DashBoardview", "DashBoard");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View("~/Views/Login/Login.cshtml");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View("~/Views/SignUp/SignUp.cshtml");
        }

        [HttpPost]
        public IActionResult SignUp(string fullname, string email, string password, string skill)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    ViewBag.Error = "User with this email already exists.";
                    return View("~/Views/SignUp/SignUp.cshtml");
                }

                var user = new User
                {
                    FullName = fullname,
                    Email = email,
                    Password = password,
                    Skill = skill
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View("~/Views/SignUp/SignUp.cshtml");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
