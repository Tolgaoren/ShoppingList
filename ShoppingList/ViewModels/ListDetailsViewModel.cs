using Shopping.Models;

namespace Shopping.ViewModels
{
    public class ListDetailsViewModel
    {
        public int ListId {  get; set; }
        
        public List<ListItems> Items { get; set; }

        public List<Items> AllItems { get; set; }

        public List<Categories> AllCategories { get; set; }
    }
}
