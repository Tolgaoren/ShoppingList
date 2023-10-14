namespace ShoppingList.Models
{
    public class ShoppingLists
    {
        public int ListId { get; set; }

        public int? UserId { get; set; }

        public string ListName { get; set; } = null!;

    }
}
