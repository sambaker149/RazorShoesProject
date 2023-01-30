using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    [Authorize(Roles = "Staff")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime PlannedDelivery { get; set; }

        [DataType(DataType.Date)]
        public DateTime ActualDelivery { get; set; }

        [StringLength(20)]
        public string OrderStatus { get; set; } = String.Empty;

        // Navigation Properties
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public bool IsPaid { get; set; } = false;

        public List<OrderItem> OrderItems { get; set; } = default!;
    }
}
