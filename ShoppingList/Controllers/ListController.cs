using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.ViewModels;

namespace Shopping.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        ShoppingDbContext dbContext;
        public ListController()
        {
            dbContext = new ShoppingDbContext();
        }

        public IActionResult ListDetails(int ListId)
        {
            // List Items
            var resultList = dbContext.ListItems.Where(a => a.ListId == ListId).ToList();
            List<ListItems> Items = new List<ListItems>();
            if (resultList != null)
            {
                foreach (var item in resultList)
                {
                    ListItems listItems = new ListItems()
                    {
                        ListId = item.ListId,
                        ListItemId = item.ListItemId,
                        ItemId = item.ListItemId,
                        Description = item.Description,
                        IsBought = item.IsBought
                    };
                }
            }

            // All Items
            var resultItems = dbContext.Items.ToList();
            List<Items> allItems = new List<Items>();
            if (resultList != null)
            {
                foreach (var item in resultItems)
                {
                    Items i = new Items()
                    {
                        ItemId = item.ItemId,
                        CategoryId = item.CategoryId,
                        ItemName = item.ItemName
                    };
                    allItems.Add(i);
                }
            }

            // All Categories
            var resultCategories = dbContext.Categories.ToList();
            List<Categories> allCategories = new List<Categories>();
            if (resultCategories != null)
            {
                foreach (var category in resultCategories)
                {
                    Categories c = new Categories()
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName
                    };
                    allCategories.Add(c);
                }
            }

            ListDetailsViewModel listDetailsViewModel = new ListDetailsViewModel()
            {
                Items = Items,
                AllItems = allItems,
                AllCategories = allCategories
            };

            return View(listDetailsViewModel);
        }

        [HttpPost]
        public IActionResult SaveList(List<string> itemList, List<Items> allItems)
        {
            if (itemList != null || allItems != null)
            {
                ShoppingList shoppingList = new ShoppingList() { };

            }

            return View();
        }

        public IActionResult GoShopping(int ListId)
        {
            return View();
        }
    }
}
