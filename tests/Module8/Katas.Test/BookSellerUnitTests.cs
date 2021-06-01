using System;
using System.Collections.Generic;
using FluentAssertions;
using Katas;
using Katas.Interfaces;
using Moq;
using NUnit.Framework;

namespace Katas.Tests
{
    public class BooksellerUnitTests
    {
        private const string firstBook = "Philosophers Stone";
        private const string secondBook = "Chamber Of Secrets";
        private const string thirdBook = "Prisoner Of Azkaban";
        private const string fourthBook = "Goblet Of Fire";
        private const string fifthBook = "Order Of The Phoenix";
        private const string sixthBook = "Half Blood Prince";
        private const string seventhBook = "DeathlyHallows";



        [TestCaseSource(nameof(GetPositiveTestCases))]
        public void TotalSumWithDiscount_ValidList_TotalSumShouldBeCorrect(List<string> books, decimal expectedSum)
        {
            // Arrange
            IBookSeller seller = new BookSeller();

            // Act
            var totalSum = seller.TotalSumWithDiscount(books);

            // Assert
            totalSum.Should().Be(expectedSum);
        }

        [Test]
        public void TotalSumWithDiscount_ListIsNull_ThrowArgumentNullException()
        {
            // Arrange
            List<string> books = null;
            IBookSeller seller = new BookSeller();

            // Act
            Action act = () => seller.TotalSumWithDiscount(books);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{nameof(books)}')");
        }

        private static IEnumerable<object[]> GetPositiveTestCases()
        {
            yield return new object[]
            {
                new List<string>
                {
                    secondBook,
                    secondBook,
                },
                16M,
            };

            yield return new object[]
            {
                new List<string>
                {
                    firstBook,
                    secondBook,
                    thirdBook,
                    fourthBook,
                    fifthBook,
                    sixthBook,
                    seventhBook,
                },
                42M,
            };

            yield return new object[]
            {
                new List<string>
                {
                    firstBook,
                    firstBook,
                    secondBook,
                    secondBook,
                    thirdBook,
                    thirdBook,
                    fourthBook,
                    fifthBook,
                },
                54M,
            };

            yield return new object[]
            {
                new List<string>
                {
                    firstBook,
                    secondBook,
                    thirdBook,
                    fourthBook,
                    fifthBook,
                    sixthBook,
                    seventhBook,
                    firstBook,
                    secondBook,
                    thirdBook,
                    fourthBook,
                    fifthBook,
                    sixthBook,
                    seventhBook,
                },
                98M,
            };

            yield return new object[]
            {
                new List<string>
                {
                    secondBook,
                    secondBook,
                    secondBook,
                    secondBook,
                    secondBook,
                    secondBook,
                    secondBook,
                },
                56M,
            };

            yield return new object[]
            {
                new List<string>
                {
                    firstBook,
                    firstBook,
                    secondBook,
                    secondBook,
                    secondBook,
                },
                39.2M,
            };
        }
    }
}

