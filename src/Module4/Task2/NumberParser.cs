using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException();
            }

            stringValue = stringValue.Trim();
            return ConvertStringToInt(stringValue);
        }

        private int ConvertStringToInt(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new FormatException();
            }

            bool isNegative = false;

            if (stringValue[0] == '-')
            {
                isNegative = true;
                stringValue = stringValue.Remove(0, 1);
            }
            else if (stringValue[0] == '+')
            {
                stringValue = stringValue.Remove(0, 1);
            }

            double number = 0;

            for (int i = 0; i < stringValue.Length; i++)
            {
                if (char.GetNumericValue(stringValue[i]) == -1)
                {
                    throw new FormatException();
                }

                int fraction = stringValue.Length - (i + 1);

                number += char.GetNumericValue(stringValue[i]) * Math.Pow(10, fraction);
            }

            if (isNegative)
            {
                number = number * -1;
            }

            if (number > int.MaxValue || number < int.MinValue)
            {
                throw new OverflowException();
            }

            return (int)number;
        }
    }
}