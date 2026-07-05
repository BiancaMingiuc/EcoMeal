using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal1.Entities_CodeFirst
{
    public class EcoMealUser : IdentityUser
    {
        public required string FullName { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
