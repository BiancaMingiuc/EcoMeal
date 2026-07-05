using System;
using System.Collections.Generic;

namespace EcoMeal1.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserEcoMeal> UserEcoMeals { get; set; } = new List<UserEcoMeal>();
}
