using EcoMeal1.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace EcoMeal1.Entities_CodeFirst
{
    public class Order
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual EcoMealUser User { get; set; }
        public Guid BusinessId { get; set; }          
        [ForeignKey("BusinessId")]
        public virtual Businesses Business { get; set; }
        public required string OrderNumber { get; set; }
        public required StatusEnum Status { get; set; }
        public virtual ICollection<OrderPackage> OrderPackages { get; set; } = new List<OrderPackage>();
    }
}
