using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Data.Models
{
    public class Employee
    {

        public Employee()
        {
            this.Positions = new HashSet<Position>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [ForeignKey(nameof(Shift))]
        public int ShiftId { get; set; }

        public Shift Shift { get; set; }

        public ICollection<Position> Positions { get; set; }

    }
}
