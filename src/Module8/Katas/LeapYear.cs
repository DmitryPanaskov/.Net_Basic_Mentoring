using System;
using System.Collections.Generic;
using System.Text;
using Katas.Interfaces;

namespace Katas
{
    public class LeapYear : ILeapYear
    {
        public bool IsLeapYear(int year)
        {
            if (year > 3000 || year <= 0)
            {
                throw new ArgumentException("The value must be between 1 and 3000", nameof(year));
            }

            // return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
            return DateTime.IsLeapYear(year);
        }
    }
}
