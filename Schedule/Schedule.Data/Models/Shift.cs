using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Data.Models
{
    public class Shift
    {

        public Shift()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string ShiftName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
