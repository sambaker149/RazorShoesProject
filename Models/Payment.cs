using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace RazorShoesProject.Models
{
    [Authorize(Roles = "Staff")]
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required, MaxLength(16)]
        public long CardNumber { get; set; }

        [Required, MaxLength(3)]
        public long CVVNumber { get; set; }

        [Required, DataType(DataType.DateTime),
            DisplayFormat(ApplyFormatInEditMode = true, 
            DataFormatString = "{0: MM/yyyy}")]
        public DateTime ExpiryDate { get; set; }
    }
}
