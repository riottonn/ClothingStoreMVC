using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class OrderItem : Entity
{

    public int? OrderId { get; set; }

    public int? ProductVariantId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ProductVariant? ProductVariant { get; set; }
}
