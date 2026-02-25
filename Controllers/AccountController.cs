using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult CreateProfile()
        {
            return View();
        }
    }
}
