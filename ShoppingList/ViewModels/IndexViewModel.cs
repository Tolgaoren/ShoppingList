using ShoppingList.Models;

namespace ShoppingList.ViewModels
{
    public class IndexViewModel
    {
        public List<ShoppingLists> Lists { get; set; }

        public ShoppingLists NewList { get; set; }

    }
}
