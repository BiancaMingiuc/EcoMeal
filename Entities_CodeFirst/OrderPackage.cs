using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal1.Entities_CodeFirst
{
    public class OrderPackage
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public Guid PackageId { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        public int Quantity { get; set; }
    }
}
