using System;
using System.Collections.Generic;


namespace Schedule.Service.Employees
{
    public interface IEmployeeService
    {

        void AddEmployee(string firstname,
                         string lastname,
                         string shiftName,
                         DateTime shiftDate,
                         string position,
                         DateTime positionDate);

        void AddEmployeeWithMorePositions(string firstname,
                                string lastname,
                                string shiftName,
                                DateTime shiftDate,
                                string position,
                                DateTime positionDate);

        List<string> GetAllEmployees();

        List<string> ShiftOfEmployeeForMonth(string firstName, string lastName);

        List<string> GetPositions(string firstName, string lastName, DateTime date);


    }
}
