using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitProfile(string bio, string location,
                                  string skillTeach, string skillLearn)
        {
            return RedirectToAction("DashBoardview", "DashBoard");
        }
    }
}
