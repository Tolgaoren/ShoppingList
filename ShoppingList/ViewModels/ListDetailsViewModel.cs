﻿using ShoppingList.Models;

namespace ShoppingList.ViewModels
{
    public class ListDetailsViewModel
    {
        public List<ListItems> Items { get; set; }

        public List<Items> AllItems { get; set; }

        public List<Categories> AllCategories { get; set; }
    }
}