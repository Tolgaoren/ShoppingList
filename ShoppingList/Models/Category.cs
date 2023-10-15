using System;
using System.Collections.Generic;

namespace Shopping.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
