using System;
using System.Collections.Generic;

namespace EcoMeal1.Entities;

public partial class BusinessType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
}
