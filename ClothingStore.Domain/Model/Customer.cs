using System;
using System.Collections.Generic;

using ClothingStore.Domain;

public partial class Customer: Entity
{
    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
