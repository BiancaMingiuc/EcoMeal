using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EcoMeal1.Enitites;

public partial class UserEcoMeal : IdentityUser
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public required string FullName { get; set; }

}
