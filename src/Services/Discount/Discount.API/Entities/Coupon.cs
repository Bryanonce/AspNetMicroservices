using System.ComponentModel.DataAnnotations;

namespace Discount.API.Entities
{
    public class Coupon
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(24)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
