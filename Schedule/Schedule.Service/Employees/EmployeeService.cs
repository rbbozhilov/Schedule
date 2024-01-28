using Schedule.Data;
using Schedule.Data.Models;
using Schedule.Service.Date;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                                DateTime shiftDate,
                                string positions)
        {

            var employee = this.data.Employees.Where(x => x.FirstName == firstname &&
                                                          x.LastName == lastname &&
                                                          x.Shift.Date == shiftDate &&
                                                          x.Shift.ShiftName == shiftName)
                                              .FirstOrDefault();

            //Employee have already shift for this date
            if (employee != null)
            {
                return;
            }

            employee = new Employee()
            {
                FirstName = firstname,
                LastName = lastname,
                Shift = new Shift()
                {
                    Date = shiftDate,
                    ShiftName = shiftName
                }
            };

            if (shiftName == "П")
            {
                this.data.Employees.Add(employee);

                this.data.SaveChanges();

                return;
            }

            var currentPositions = positions.Split(',', (char)StringSplitOptions.RemoveEmptyEntries).ToArray();

            EmployeePositions employeePosition = null;

            foreach (var currentPosition in currentPositions)
            {
                var getPosition = this.data.Positions
                                        .Where(x => x.PositionName == currentPosition)
                                        .FirstOrDefault();

                //same position on same date
                if (employee.Positions.Any(x => x.Position.PositionName == currentPosition &&
                                               x.Date == shiftDate))
                {
                    return;
                }

                if (getPosition == null)
                {
                    getPosition = new Position() { PositionName = currentPosition };
                }

                employeePosition = new EmployeePositions()
                {
                    Position = getPosition,
                    Date = shiftDate
                };

                employee.Positions.Add(employeePosition);

            }

            employee.Positions.Add(employeePosition);

            this.data.Employees.Add(employee);

            this.data.SaveChanges();

        }

        public int DaysOfWork(string firstname,
                              string lastname,
                              DateTime startDate,
                              DateTime endDate)
        {
            return this.data.Employees.Where(x => x.FirstName == firstname &&
                                                 x.LastName == lastname &&
                                                 x.Shift.ShiftName != "П" &&
                                                 x.Shift.Date >= startDate &&
                                                 x.Shift.Date <= endDate)
                                      .Count();
        }

        public bool EditEmployee(string firstname,
                                 string lastname,
                                 string shiftName,
                                 DateTime shiftDate,
                                 string positions)
        {

            var currentEmployee = this.data.Employees.Where(x => x.FirstName == firstname &&
                                                                 x.LastName == lastname &&
                                                                 x.Shift.Date == shiftDate)
                                                     .FirstOrDefault();


            if (currentEmployee == null)
            {
                return false;
            }

            var currentPositions = this.data.EmployeePositions
                                                .Where(x => x.Employee.FirstName == firstname &&
                                                            x.Employee.LastName == lastname &&
                                                            x.Date == shiftDate)
                                                .ToList();


            this.data.EmployeePositions.RemoveRange(currentPositions);
            this.data.Employees.Remove(currentEmployee);
            this.data.SaveChanges();

            this.AddEmployee(firstname,
                             lastname,
                             shiftName,
                             shiftDate,
                             positions);


            return true;
        }

        public List<string> GetAllEmployees()
        {
            var employees = this.data.Employees.Select(x => x.FirstName + " " + x.LastName).Distinct().ToList();
            return employees;
        }

        public List<string> GetPositions(string firstName, string lastName, DateTime date)
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
                var positions = this.GetPositions(firstName, lastName, date);

                var getCurrentShift = allShifts.Where(x => x.CurrentDate == date).FirstOrDefault();

                if (getCurrentShift != null)
                {
                    if (getCurrentShift.ShiftName == "П")
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
