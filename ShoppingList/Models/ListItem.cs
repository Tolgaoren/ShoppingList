using System;
using System.Collections.Generic;

namespace ShoppingList.Models;

public partial class ListItem
{
    public int ListItemId { get; set; }

    public int? ListId { get; set; }

    public int? ItemId { get; set; }

    public bool IsBought { get; set; }

    public string? Description { get; set; }

    public virtual Item? Item { get; set; }

    public virtual ShoppingList? List { get; set; }
}
