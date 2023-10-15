using System;
using System.Collections.Generic;

namespace Shopping.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public string? Imagepath { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();
}
