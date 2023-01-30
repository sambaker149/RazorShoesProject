using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        [StringLength(25)]
        public string HouseName { get; set; } = String.Empty;

        [Required, StringLength(50)]
        public string RoadName { get; set; } = String.Empty;

        [Required, StringLength(50)]
        public string PostTown { get; set; } = String.Empty;

        [Required, StringLength(8), DataType(DataType.PostalCode)]
        public string Postcode { get; set; } = String.Empty;
    }
}
