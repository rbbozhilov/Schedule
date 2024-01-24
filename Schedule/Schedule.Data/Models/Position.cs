
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Schedule.Data.Models
{
    public class Position
    {

        public Position()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string PositionName { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
