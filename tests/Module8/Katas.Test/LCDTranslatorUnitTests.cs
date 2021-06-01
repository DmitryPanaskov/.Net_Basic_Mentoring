using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Katas;
using Katas.Interfaces;
using Moq;
using NUnit.Framework;

namespace Katas.Tests
{
    class LCDTranslatorUnitTests
    {
        private const string zeroLCD = "._.\r\n" +
                                       "|.|\r\n" +
                                       "|_|\r\n";

        private const string sevenLCD = "._.\r\n" +
                                        "..|\r\n" +
                                        "..|\r\n";

        private const string tenLCD = "...._.\r\n" +
                                      "..||.|\r\n" +
                                      "..||_|\r\n";


        private const string oneThousandSevenHundredAndFiftyLCD = "...._.._.._.\r\n" +
                                                                  "..|..||_.|.|\r\n" +
                                                                  "..|..|._||_|\r\n";

        private const string tenNinesLCD = "._.._.._.._.._.._.._.._.._.._.\r\n" +
                                           "|_||_||_||_||_||_||_||_||_||_|\r\n" +
                                           "._|._|._|._|._|._|._|._|._|._|\r\n";

        [TestCaseSource(nameof(GetPositiveTestCases))]
        public void Translate_ValidNumber_ReturnsLcdStringValue(long value, string resultLCD)
        {
            // Arrange
            ILCDTranslator translator = new LCDTranslator();

            // Act
            var act = translator.Translate(value);

            // Assert
            act.Equals(resultLCD);
        }

        [TestCase(-8)]
        [TestCase(-99999)]
        [TestCase(999999999999)]
        [TestCase(long.MaxValue)]
        public void Translate_InvalidNumber_ThrowArgumentOutOfRangeException(long number)
        {
            // Arrange
            var errorMessage = $"The value must be between 0 and 9999999999";
            ILCDTranslator translator = new LCDTranslator();

            // Act
            Action act = () => translator.Translate(number);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"{errorMessage} (Parameter '{nameof(number)}')");
        }


        private static IEnumerable<object[]> GetPositiveTestCases()
        {
            yield return new object[]
            {
                    (long)0,
                    zeroLCD,
            };

            yield return new object[]
            {
                    (long)7,
                    sevenLCD,
            };

            yield return new object[]
            {
                   (long)10,
                   tenLCD,
            };

            yield return new object[]
            {
                    (long)1750,
                    oneThousandSevenHundredAndFiftyLCD,
            };

            yield return new object[]
            {
                    (long)9999999999,
                    tenNinesLCD,
            };
        }
    }
}
