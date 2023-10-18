using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.ViewModels;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ShoppingDbContext dbContext;
        public AdminController() 
        {
            dbContext = new ShoppingDbContext();
        }

        public IActionResult Panel()
        {

            var categories = dbContext.Categories;
            var items = dbContext.Items;

            List<Categories> categoryList = new List<Categories>();

            List<Items> itemList = new List<Items>();

            foreach (var i in items)
            {
                Items item = new Items()
                {
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    CategoryId = i.CategoryId
                };

                itemList.Add(item);
            }
            foreach (var c in categories)
            {
                Categories category = new Categories()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                };
                categoryList.Add(category);
            }


            PanelViewModel panelViewModel = new PanelViewModel()
            {
                Categories = categoryList,
                Items = itemList
            };

            return View(panelViewModel);
        }



        [HttpPost]
        public IActionResult AddItem(string itemName, string categoryId)
        {
            Item newItem = new Item()
            {
                ItemName = itemName,
                CategoryId = int.Parse(categoryId)
            };
            dbContext.Items.Add(newItem);
            dbContext.SaveChanges();

            return RedirectToAction("Panel");
        }



        [HttpPost]
        public IActionResult AddCategory(string categoryName)
        {
            Category newCategory = new Category()
            {
                CategoryName = categoryName
            };
            dbContext.Categories.Add(newCategory);
            dbContext.SaveChanges();

            return RedirectToAction("Panel");
        }



        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {

            var category = dbContext.Categories.FirstOrDefault(s => s.CategoryId == id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
            }
            dbContext.SaveChanges();

            return RedirectToAction("Panel");
        }



        [HttpPost]
        public IActionResult DeleteItem(int id)
        {

            var item = dbContext.Items.FirstOrDefault(s => s.ItemId == id);
            if (item != null)
            {
                dbContext.Items.Remove(item);
            }
            dbContext.SaveChanges();

            return RedirectToAction("Panel");
        }

    }
}
