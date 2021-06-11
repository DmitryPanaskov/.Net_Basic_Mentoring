using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Interfaces;

namespace Katas.BookSellerTask
{
    public class BookSeller : IBookSeller
    {
        private readonly decimal cost = 8;

        public decimal TotalSumWithDiscount(List<string> books)
        {
            if (books is null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            var sets = GetListUniqueSetsOfBook(books);

            var totalsum = default(decimal);

            Discount discount;
            foreach (var count in sets)
            {
                discount = new Discount(cost, count);
                totalsum += discount.GetCostForSet();
            }

            return totalsum;
        }

        private static List<int> GetListUniqueSetsOfBook(List<string> bookList)
        {
            var uniqueSetList = new List<List<string>>();
            while (bookList.Any())
            {
                var setList = bookList.GroupBy(item => item)
                                      .Select(item => item.Key)
                                      .ToList();

                foreach (var item in setList)
                {
                    bookList.Remove(item);
                }

                uniqueSetList.Add(setList);
            }

            return uniqueSetList.Select(item => item.Count).ToList();
        }
    }
}
