using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class Product : Entity
{
    public string Name { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public decimal? Price { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
