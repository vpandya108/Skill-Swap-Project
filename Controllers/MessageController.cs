using Microsoft.AspNetCore.Mvc;

namespace Skill_Swap_Project.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
