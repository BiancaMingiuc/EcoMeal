using EcoMeal1.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal1.Entities_CodeFirst
{
    public class Package
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        [ForeignKey("BusinessId")]
        public virtual Businesses Businesses { get; set; }
        public required int PackageTypeId { get; set; }
        public PackageType PackageType { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime PickupStart { get; set; }
        public DateTime PickupEnd { get; set; }
        public DateTime ExpirationDate { get; set; }
        public required string ImageURL { get; set; }
        public ICollection<OrderPackage> OrderPackages { get; set; } = new List<OrderPackage>();
    }
}
