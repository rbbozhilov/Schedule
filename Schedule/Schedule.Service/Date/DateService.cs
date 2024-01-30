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

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;


            DateTime firstDayOfMonth = new DateTime(year, month, 1);


            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            List<DateTime> allDatesOfMonth = Enumerable.Range(0, (lastDayOfMonth - firstDayOfMonth).Days + 1)
                .Select(offset => firstDayOfMonth.AddDays(offset))
                .ToList();


            List<string> allDaysOfMonth = allDatesOfMonth.Select(date => date.ToString("dddd", CultureInfo.InvariantCulture)).ToList();

            List<string> returnDates = new List<string>();


            Console.WriteLine("Dates\t\tDays");
            for (int i = 0; i < allDatesOfMonth.Count; i++)
            {
               returnDates.Add($"{allDatesOfMonth[i].ToString("dd/MM/yyyy")}\t{allDaysOfMonth[i]}");
            }

            return returnDates;
        }
    }
}
