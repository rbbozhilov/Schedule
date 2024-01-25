using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Service.Date
{
    public interface IDateService
    {
        ICollection<string> GetDaysFromMonth();
    }
}
