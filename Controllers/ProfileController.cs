using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult ViewProfile()
        {
            return View();
        }
    }
}
