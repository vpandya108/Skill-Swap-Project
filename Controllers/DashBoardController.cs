using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult DashBoardview()
        {
            return View();
        }
    }
}
