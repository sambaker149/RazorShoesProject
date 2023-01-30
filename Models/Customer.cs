using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    [Authorize(Roles = "Staff")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, StringLength(20)]
        public string Name { get; set; } = String.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(100), DataType(DataType.EmailAddress)] 
        public string Email { get; set; } = String.Empty;

        [StringLength(100), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = String.Empty;

        // Navigation Properties
        //public int AddressId { get; set; }
        //public Address Address { get; set; } = default!;
        public List<Order> Orders { get; set; } = default!;
    }
}
