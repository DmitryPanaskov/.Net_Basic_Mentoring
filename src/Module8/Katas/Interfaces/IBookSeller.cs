using System;
using System.Collections.Generic;
using System.Text;

namespace Katas.Interfaces
{
    public interface IBookSeller
    {
        decimal TotalSumWithDiscount(List<string> books);
    }
}
