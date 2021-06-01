using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Katas.Interfaces;

namespace Katas
{
    public class BookSeller : IBookSeller
    {
        public const decimal BookCost = 8;

        public const decimal Discount5 = 5M;

        public const decimal Discount10 = 10M;

        public const decimal Discount20 = 20M;

        public const decimal Discount25 = 25M;

        public decimal TotalSumWithDiscount(List<string> books)
        {
            if (books is null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            var grouping = books.GroupBy(x => x);

            decimal result = ApplyDiscount(grouping.Count());

            foreach (var group in grouping)
            {
                if ((group.Count() - 1) > 0)
                {
                    result += (decimal)((group.Count() - 1) * 8);
                }
            }

            return result;
        }

        private decimal ApplyDiscount(int count)
        {
            decimal result = 8 * count;

            if (count == 2)
            {
                return (result - (result / 100 * Discount5));
            }

            if (count == 3)
            {
                return (result - (result / 100 * Discount10));
            }

            if (count == 4)
            {
                return (result - (result / 100 * Discount20));
            }

            if (count >= 5)
            {
                return (result - (result / 100 * Discount25));
            }

            return result;
        }
    }
}
