using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class Order: Entity
{
    public DateTime? OrderDate { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
