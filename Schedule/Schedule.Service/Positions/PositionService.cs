using Schedule.Data;
using Schedule.Data.Models;


namespace Schedule.Service.Positions
{
    public class PositionService : IPositionService
    {

        private ScheduleDbContext db;

        public PositionService()
        {
            this.db = new ScheduleDbContext();
        }


        public void AddPosition(string name)
        {
            this.db.Positions.Add(new Position() { PositionName = name });

            this.db.SaveChanges();
        }
    }
}
