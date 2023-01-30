using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    [Authorize(Roles = "Staff")]
    public class Shoe
    {
        [Key]
        public int ShoeId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = String.Empty;

        [StringLength(250)]
        public string Description { get; set; } = String.Empty;

        [Required, Range(15.00, 100.00)]
        public double Price { get; set; }

        [Required, StringLength(20)]
        public string Type { get; set; } = String.Empty;

        [Required, StringLength(20)]
        public string Brand { get; set; } = String.Empty;

        [StringLength(20)]
        public string Gender { get; set; } = String.Empty;

        // Navigation Properties
        public List<Image> Images { get; set; } = default!;
    }
}
