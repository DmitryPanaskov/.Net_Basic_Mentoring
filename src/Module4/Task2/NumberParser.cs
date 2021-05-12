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

            if (string.IsNullOrEmpty(stringValue))
            {
                throw new FormatException();
            }

            int sign = 1;

            if (stringValue[0] == '-')
            {
                sign = -1;
                stringValue = stringValue.Remove(0, 1);
            }
            else if (stringValue[0] == '+')
            {
                stringValue = stringValue.Remove(0, 1);
            }

            int number = 0;

            checked
            {
                for (int i = 0; i < stringValue.Length; i++)
                {
                    if (char.GetNumericValue(stringValue[i]) == -1)
                    {
                        throw new FormatException();
                    }

                    number = number * 10 + (int)char.GetNumericValue(stringValue[i]) * sign;
                }
            }

            return number;
        }
    }
}