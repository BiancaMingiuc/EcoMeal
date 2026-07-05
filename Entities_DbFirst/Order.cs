using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal1.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public required string UserId { get; set; }

    public int? BusinessId { get; set; }

    public int? StatusId { get; set; }

    public required string OrderNumber { get; set; }
    [ForeignKey("UserId")]
    public virtual Business? Business { get; set; }

    public virtual ICollection<OrderPackage> OrderPackages { get; set; } = new List<OrderPackage>();

    public virtual Status? Status { get; set; }

    public required UserEcoMeal User { get; set; }
}
