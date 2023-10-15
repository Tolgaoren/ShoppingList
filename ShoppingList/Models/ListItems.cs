namespace Shopping.Models
{
    public class ListItems
    {
        public int ListItemId { get; set; }

        public int? ListId { get; set; }

        public int? ItemId { get; set; }

        public bool IsBought { get; set; }

        public string? Description { get; set; }

    }
}
