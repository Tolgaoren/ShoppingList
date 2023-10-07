using System;
using System.Collections.Generic;

namespace ShoppingList.Models;

public partial class ShoppingList
{
    public int ListId { get; set; }

    public int? UserId { get; set; }

    public string ListName { get; set; } = null!;

    public bool IsShopping { get; set; }

    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();

    public virtual User? User { get; set; }
}
