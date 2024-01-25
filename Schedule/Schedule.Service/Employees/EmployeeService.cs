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

            //if (currentPosition == null)
            //{
            //    Position newPosition = new Position() { PositionName = position };
            //    employee.Positions.Add(newPosition);
            //}
            //else
            //{
            //    employee.Positions.Add(currentPosition);
            //}


        }

        public List<string> GetAllEmployees()
        {
            var employees = this.data.Employees.Select(x => x.FirstName + " " + x.LastName).Distinct().ToList();
            return employees;
        }

        public List<string> GetPositions(string firstName, string lastName,DateTime date)
        {
            var positionsOfEmployee = this.data.EmployeePositions
                                                .Where(x => x.Employee.FirstName == firstName &&
                                                            x.Employee.LastName == lastName &&
                                                            x.Date == date)
                                                .Select(e => e.Position.PositionName)
                                                .ToList();

            return positionsOfEmployee;
        }

        public List<string> ShiftOfEmployeeForMonth(string firstName, string lastName)
        {

            var shiftOfEmployee = new List<string>();

   

            // Get the current month and year
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            var dates = Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                          .Select(day => new DateTime(year, month, day)) // Map each day to a date
                          .ToList(); // Load dates into a list

            var firstDate = dates[0];
            var lastDate = dates[dates.Count - 1];

            var allShifts = this.data.Employees
                                    .Where(x => (x.Shift.Date >= firstDate && x.Shift.Date <= lastDate) &&
                                                 x.FirstName == firstName && x.LastName == lastName)
                                    .Select(x => new
                                    {
                                        CurrentDate = x.Shift.Date,
                                        ShiftName = x.Shift.ShiftName
                                    })
                                    .ToList();

            foreach (var date in dates)
            {
                var positions = this.GetPositions(firstName, lastName,date);

                var getCurrentShift = allShifts.Where(x => x.CurrentDate == date).FirstOrDefault();

                if (getCurrentShift != null)
                {
                    if(getCurrentShift.ShiftName == "П")
                    {
                        shiftOfEmployee.Add("Почивка");

                        continue;
                    }

                    string shiftWithPositions = getCurrentShift.ShiftName + " Смяна. " + String.Join(", ", positions);

                    shiftOfEmployee.Add(shiftWithPositions);
                }
                else
                {
                    shiftOfEmployee.Add("-");
                }
            }


            return shiftOfEmployee;
        }
    }
}
