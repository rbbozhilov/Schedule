using Schedule.Data;
using Schedule.Data.Models;
using Schedule.Service.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Service.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private ScheduleDbContext data;

        public EmployeeService(ScheduleDbContext data)
        {
            this.data = data;
        }



        public void AddEmployee(string firstname,
                                string lastname,
                                string shiftName,
                                DateTime date,
                                string position)
        {


            Employee employee = this.data.Employees
                                         .Where(x => x.FirstName == firstname && x.LastName == lastname)
                                         .FirstOrDefault();

            if (employee == null)
            {
                employee = new Employee()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Shift = new Shift()
                    {
                        Date = date,
                        ShiftName = shiftName
                    }
                };
            }
            else
            {
                bool isHaveShift = this.data.Employees.Any(x => x.Shift.Date == date);

                if (!isHaveShift)
                {
                    employee.Shift = new Shift() { Date = date, ShiftName = shiftName };
                }
            }

            var currentPosition = this.data.Positions.Where(x => x.PositionName == position).FirstOrDefault();

            if (currentPosition == null)
            {
                Position newPosition = new Position() { PositionName = position };
                employee.Positions.Add(newPosition);
            }
            else
            {
                employee.Positions.Add(currentPosition);
            }


        }

        public List<string> GetAllEmployees()
        {
            var employees = this.data.Employees.Select(x => x.FirstName + " " + x.LastName).ToList();
            return employees;
        }

        public List<string> ShiftOfEmployeeForMonth(int daysOfMonth)
        {
            throw new NotImplementedException();
        }
    }
}
