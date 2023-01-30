using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    [Authorize(Roles = "Staff")]
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required, MaxLength(120)]
        public string ImageUrl { get; set; } = String.Empty;

        // Navigation Properties
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; } = default!;
    }
}
