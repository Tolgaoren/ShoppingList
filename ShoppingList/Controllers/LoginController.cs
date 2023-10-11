using Microsoft.AspNetCore.Mvc;

namespace ShoppingList.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
