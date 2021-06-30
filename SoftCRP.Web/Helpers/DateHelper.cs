using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Helpers
{
    public class DateHelper
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateHelper(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool WithInRange(DateTime value)
        {
            return (Start <= value) && (value <= End);
        }

    }
}
