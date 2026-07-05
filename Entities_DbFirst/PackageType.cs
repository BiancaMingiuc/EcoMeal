using System;
using System.Collections.Generic;

namespace EcoMeal1.Entities;

public partial class PackageType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
