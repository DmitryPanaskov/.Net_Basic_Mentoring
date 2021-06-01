using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Katas.Interfaces;
using Moq;
using NUnit.Framework;

namespace Katas.Tests
{
    public class LeapYearUnitTests
    {
        [TestCaseSource(nameof(GetPositiveTestCases))]
        public void IsLeapYear_ValidYear_ReturnsTrue(int year, bool isLeapYear)
        {
            // Arrange
            ILeapYear leapYear = new LeapYear();

            // Act
            var act = leapYear.IsLeapYear(year);

            // Assert
            act.Should().BeTrue();
        }

        [TestCaseSource(nameof(GetNegativeTestCases))]
        public void IsLeapYear_ValidYear_ReturnsFasle(int year, bool isLeapYear)
        {
            // Arrange
            ILeapYear leapYear = new LeapYear();

            // Act
            var act = leapYear.IsLeapYear(year);

            // Assert
            act.Should().BeFalse();
        }

        [TestCase(-1)]
        [TestCase(-8)]
        [TestCase(-99999)]
        [TestCase(int.MinValue)]
        [TestCase(int.MinValue)]
        public void IsLeap_WhenYearNegative_ShouldThrowException(int year)
        {
            // Arrange
            var errorMessage = $"The value must be between 0 and 3000";
            ILeapYear leapYear = new LeapYear();

            // Act
            Action act = () => leapYear.IsLeapYear(year);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"{errorMessage} (Parameter '{nameof(year)}')");
        }

        private static IEnumerable<object[]> GetPositiveTestCases()
        {
            yield return new object[]
            {
                    0,
                    true,
            };

            yield return new object[]
            {
                    4,
                    true,
            };

            yield return new object[]
            {
                    1988,
                    true,
            };

            yield return new object[]
            {
                    2020,
                    true,
            };
        }

        private static IEnumerable<object[]> GetNegativeTestCases()
        {
            yield return new object[]
            {
                   900,
                   false,
            };

            yield return new object[]
            {
                    1992,
                    false,
            };

            yield return new object[]
            {
                    1994,
                    false,
            };

            yield return new object[]
            {
                    1900,
                    false,
            };

            yield return new object[]
            {
                    2100,
                    false,
            };

            yield return new object[]
            {
                    2200,
                    false,
            };
        }
    }
}
