using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Katas.Interfaces;

namespace Katas
{
    public class LCDTranslator : ILCDTranslator
    {
        private readonly StringBuilder line1 = new StringBuilder();

        private readonly StringBuilder line2 = new StringBuilder();

        private readonly StringBuilder line3 = new StringBuilder();

        public string Translate(long number)
        {
            if (number > 9999999999 || number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "The value must be between 0 and 9999999999");
            }

            var listOfDigit = new List<long>();

            while (number > 0)
            {
                listOfDigit.Add(number % 10);
                number = number / 10;
            }

            listOfDigit.Reverse();

            foreach (var digit in listOfDigit)
            {
                TranslateNumberToLcdString(digit);
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(line1.ToString());
            stringBuilder.AppendLine(line2.ToString());
            stringBuilder.AppendLine(line3.ToString());

            return stringBuilder.ToString();
        }

        private void TranslateNumberToLcdString(long digit)
        {
            switch (digit)
            {
                case 0:
                    line1.Append("._.");
                    line2.Append("|.|");
                    line3.Append("|_|");
                    break;
                case 1:
                    line1.Append("...");
                    line2.Append("..|");
                    line3.Append("..|");
                    break;
                case 2:
                    line1.Append("._.");
                    line2.Append("._|");
                    line3.Append("|_.");
                    break;
                case 3:
                    line1.Append("._.");
                    line2.Append("._|");
                    line3.Append("._|");
                    break;
                case 4:
                    line1.Append("...");
                    line2.Append("|_|");
                    line3.Append("..|");
                    break;
                case 5:
                    line1.Append("._.");
                    line2.Append("|_.");
                    line3.Append("._|");
                    break;
                case 6:
                    line1.Append("._.");
                    line2.Append("|_.");
                    line3.Append("|_|");
                    break;
                case 7:
                    line1.Append("._.");
                    line2.Append("..|");
                    line3.Append("..|");
                    break;
                case 8:
                    line1.Append("._.");
                    line2.Append("|_|");
                    line3.Append("|_|");
                    break;
                case 9:
                    line1.Append("._.");
                    line2.Append("|_|");
                    line3.Append("._|");
                    break;
                default:
                    break;
            }
        }
    }
}
