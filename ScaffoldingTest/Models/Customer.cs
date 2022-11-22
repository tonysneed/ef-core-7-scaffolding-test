using System;
using System.Collections.Generic;

namespace ScaffoldingTest.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual CustomerSetting? CustomerSetting { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
