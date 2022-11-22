using System;
using System.Collections.Generic;

namespace ScaffoldingTest.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    public decimal? Freight { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
