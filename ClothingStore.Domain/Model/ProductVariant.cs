using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class ProductVariant : Entity

{

    public int? ProductId { get; set; }

    public string? Size { get; set; }

    public string? Color { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product? Product { get; set; }
}
