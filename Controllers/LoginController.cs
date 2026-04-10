using Microsoft.AspNetCore.Mvc;
using Skill_Swap_Project.Data;

namespace Skill_Swap_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        // Shows Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Runs when Login button is clicked
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Store user info in session
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserSkill", user.Skill);
                HttpContext.Session.SetInt32("UserId", user.Id);

                // Redirect to Dashboard
                return RedirectToAction("DashBoardview", "Dashboard");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View();
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.Password = newPassword;
                _context.SaveChanges();
                ViewBag.Message = "Password has been reset successfully.";
                return View();
            }

            ViewBag.Error = "Email not found.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}