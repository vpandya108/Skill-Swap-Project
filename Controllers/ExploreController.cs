using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class ExploreController : Controller
    {
        public IActionResult Explore()
        {
            return View("Exploreview");
        }
    }
}
