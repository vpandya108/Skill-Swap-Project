using System.ComponentModel.DataAnnotations;

namespace Skill_Swap_Project.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Skill { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string LearnSkill { get; set; } = string.Empty;
    }
}
