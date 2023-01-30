using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    [Authorize(Roles = "Staff")]
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required, Range(15.00, 100)]
        public double SalePrice { get; set; }

        [Required, Range(1, 5)]
        public int Quantity { get; set; }

        [Required, Range(1, 20)]
        public decimal Size { get; set; }

        // Navigation Properties
        public int ShoeId { get; set; }
        public virtual Shoe Shoe { get; set; } = default!;

        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;
    }
}