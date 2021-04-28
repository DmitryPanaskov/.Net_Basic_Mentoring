using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            CheckNull(stringValue);
            stringValue = stringValue.Trim();
            IsNumber(stringValue);
            IsPermissibleLengthForString(stringValue);
            return ConvertStringToInt(stringValue);
        }

        private int ConvertStringToInt(string stringValue)
        {
            double number = 0;
            if (stringValue[0] == '-')
            {
                stringValue = stringValue.Remove(0, 1);
                number = ConvertStringToDouble(stringValue) * -1;
            }
            else if (stringValue[0] == '+')
            {
                stringValue = stringValue.Remove(0, 1);
                number = ConvertStringToDouble(stringValue);
            }
            else
            {
                number = ConvertStringToDouble(stringValue);
            }

            CheckRange(number);

            return (int)number;
        }

        private double ConvertStringToDouble(string stringValue)
        {
            double number = 0;
            for (int i = 0; i < stringValue.Length; i++)
            {
                int fraction = stringValue.Length - (i + 1);
                number += ConvertCharToDouble(stringValue[i]) * Math.Pow(10, fraction);
            }

            return number;
        }

        private void IsPermissibleLengthForString(string stringValue)
        {
            if (stringValue[0] == '-' || stringValue[0] == '+')
            {
                if (stringValue.Length > 11)
                {
                    throw new OverflowException();
                }
            }
            else
            {
                if (stringValue.Length > 10)
                {
                    throw new OverflowException();
                }
            }

        }

        private bool IsNumber(string stringValue)
        {
            bool isNubmer = false;

            if (string.IsNullOrEmpty(stringValue))
            {
                throw new FormatException();
            }

            for (int i = 0; i < stringValue.Length; ++i)
            {
                char c = stringValue[i];

                if (i == 0 && stringValue.Length > 1)
                {
                    if (c >= '0' || c <= '9' || c == '-' || c == '+')
                    {
                        isNubmer = true;
                        continue;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }

                if (c < '0' || c > '9')
                {
                    throw new FormatException();
                }
            }

            return isNubmer;
        }

        private void CheckNull(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException();
            }
        }

        private void CheckRange(double number)
        {
            if (number > int.MaxValue || number < int.MinValue)
            {
                throw new OverflowException();
            }
        }

        private double ConvertCharToDouble(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                default:
                    throw new ArgumentException();
            }
        }
    }
}