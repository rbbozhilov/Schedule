using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Service.Date
{
    public class DateService : IDateService
    {
        public ICollection<string> GetDaysFromMonth()
        {
            // Get the current month and year
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            // Get the first day of the month
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            // Get the last day of the month
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Generate all dates of the current month
            List<DateTime> allDatesOfMonth = Enumerable.Range(0, (lastDayOfMonth - firstDayOfMonth).Days + 1)
                .Select(offset => firstDayOfMonth.AddDays(offset))
                .ToList();

            // Get the corresponding day names for all dates
            List<string> allDaysOfMonth = allDatesOfMonth.Select(date => date.ToString("dddd", CultureInfo.InvariantCulture)).ToList();

            List<string> returnDates = new List<string>();

            // Output the dates and days
            Console.WriteLine("Dates\t\tDays");
            for (int i = 0; i < allDatesOfMonth.Count; i++)
            {
               returnDates.Add($"{allDatesOfMonth[i].ToString("dd/MM/yyyy")}\t{allDaysOfMonth[i]}");
            }

            return returnDates;
        }
    }
}
