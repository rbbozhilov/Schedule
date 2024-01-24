

namespace Schedule.Data.Models
{
    public class EmployeePositions
    {

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

    }
}
