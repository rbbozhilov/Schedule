using System.ComponentModel.DataAnnotations;

namespace Schedule.Data.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
