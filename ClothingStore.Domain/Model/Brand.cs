using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class Brand: Entity
{
    public string BrandName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
