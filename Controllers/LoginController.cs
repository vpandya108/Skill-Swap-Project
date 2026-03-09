using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class LoginController : Controller
    {
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
            // Temporary login check (you can connect DB later)
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // Redirect to Dashboard
                return RedirectToAction("DashBoardview", "Dashboard");
            }

            ViewBag.Error = "Invalid Login";
            return View();
        }
    }
}