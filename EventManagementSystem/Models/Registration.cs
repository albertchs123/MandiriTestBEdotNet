using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
    }

}
