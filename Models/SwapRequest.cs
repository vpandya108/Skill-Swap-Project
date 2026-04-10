using System.ComponentModel.DataAnnotations;

namespace Skill_Swap_Project.Models
{
    public class SwapRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Declined

        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
