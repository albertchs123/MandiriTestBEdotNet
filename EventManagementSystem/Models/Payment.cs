using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RegistrationId { get; set; }
        [Required]
        [StringLength(100)]
        public string PayerName { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }

}