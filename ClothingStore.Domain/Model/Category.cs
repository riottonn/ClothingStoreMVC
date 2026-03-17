using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class Category : Entity
{
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
