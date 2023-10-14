namespace ShoppingList.Models
{
    public class Items
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public int? CategoryId { get; set; }
    }
}
