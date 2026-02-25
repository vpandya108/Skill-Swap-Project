using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string fullname, string email, string password)
        {
            // Here you can save user to database

            // Then redirect to Profile Creation page
            //
            return RedirectToAction("Account", "CreateProfile");
        }
    }
}
