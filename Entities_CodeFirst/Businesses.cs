using EcoMeal1.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace EcoMeal1.Entities_CodeFirst
{
    public class Businesses
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual EcoMealUser Owner { get; set; }
        public required string Address { get; set; }
        public required string ImageURL { get; set; }
        public required int BusinessTypeId { get; set; }
        public BusinessesType BusinessType { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Package> Packages { get; set; } = new List<Package>();
    }
}
