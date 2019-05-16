using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConvertNumericToWords
{
    public class DateToWordConverter
    {

        enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        public static string ConvertDateToWords(DateTime dateTime)
        {
            string value = null;
            if (dateTime == null)
            {
                return null;
            }

            int dd = dateTime.Day;
            int mm = dateTime.Month;
            int yy = dateTime.Year;

            if ((new[] { 1, 01, 21, 31 }).Contains(dd))
            {
                value += $"{dd}st day of {(Month)Convert.ToInt32(mm)},{yy}";
            }
            else if ((new[] { 2, 02, 22 }).Contains(dd))
            {
                value += $"{dd}nd day of {(Month)Convert.ToInt32(mm)},{yy}";
            }
            else if ((new[] { 3, 03, 23 }).Contains(dd))
            {
                value += $"{dd}rd day of {(Month)Convert.ToInt32(mm)},{yy}";
            }
            else
            {
                value += $"{dd}th day of {(Month)Convert.ToInt32(mm)},{yy}";
            }

            return value;
        }

    }
}
