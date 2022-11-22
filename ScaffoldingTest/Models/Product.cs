using System;
using System.Collections.Generic;

namespace ScaffoldingTest.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool Discontinued { get; set; }

    public byte[]? RowVersion { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
