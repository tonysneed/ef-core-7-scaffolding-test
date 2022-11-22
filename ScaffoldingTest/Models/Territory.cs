using System;
using System.Collections.Generic;

namespace ScaffoldingTest.Models;

public partial class Territory
{
    public string TerritoryId { get; set; } = null!;

    public string TerritoryDescription { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
