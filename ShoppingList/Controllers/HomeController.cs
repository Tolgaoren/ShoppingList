using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using ShoppingList.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ShoppingList.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ShoppingDbContext dbContext;

        public HomeController(ILogger<HomeController> logger)
        {
            dbContext = new ShoppingDbContext();
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userClaims = HttpContext.User.Claims;
            var user = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (user != null) {
                int userId = int.Parse(user);
                var DbLists = dbContext.ShoppingLists.Where(u => u.UserId == userId);
                if (DbLists != null)
                {
                    List<ShoppingLists> shoppingLists = new List<ShoppingLists>();
                    foreach (var i in DbLists)
                    {
                        ShoppingLists shoppingList = new ShoppingLists()
                        {
                            ListId = i.ListId,
                            ListName = i.ListName,
                            UserId = i.UserId
                        };
                        shoppingLists.Add(shoppingList);
                    }
                    ShoppingLists newList = new ShoppingLists()
                    {
                        UserId = userId
                    };
                    IndexViewModel indexViewModel = new IndexViewModel()
                    {
                        Lists = shoppingLists,
                        NewList = newList
                    };

                    return View(indexViewModel);
                }

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewList(ShoppingLists newList) {

            if (ModelState.IsValid)
            {
                Models.ShoppingList shoppingList = new Models.ShoppingList()
                {
                    UserId = newList.UserId,
                    ListName = newList.ListName,
                    IsShopping = false
                };
                dbContext.Add(shoppingList);
                dbContext.SaveChanges();
            }


            return RedirectToAction("Index","Home");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}