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

        public IActionResult ListDetails(int id)
        {
            // List Items
            var resultList = dbContext.ListItems.Where(a => a.ListId == id).ToList();
            List<ListItems> Items = new List<ListItems>();
            if (resultList != null)
            {
                foreach (var item in resultList)
                {
                    ListItems listItems = new ListItems()
                    {
                        ListId = item.ListId,
                        ListItemId = item.ListItemId,
                        ItemId = item.ItemId,
                        Description = item.Description,
                        IsBought = item.IsBought
                    };
                    Items.Add(listItems);
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
                ListId = id,
                Items = Items,
                AllItems = allItems,
                AllCategories = allCategories
            };

            return View(listDetailsViewModel);
        }

        [HttpPost]
        public IActionResult SaveList(List<int> itemIds, int listId)
        {
            if (itemIds != null)
            {   
                foreach (var i in itemIds)
                {
                    ListItem listItem = new ListItem()
                    {
                        ListId = listId,
                        ItemId = i,
                        IsBought = false
                    };
                    dbContext.ListItems.Add(listItem);
                }
                dbContext.SaveChanges();
            }

            return RedirectToAction("ListDetails", "List", listId);
        }

        public IActionResult GoShopping(int ListId)
        {
            return View();
        }
    }
}
