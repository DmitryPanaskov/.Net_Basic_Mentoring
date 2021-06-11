namespace Katas.BookSellerTask
{
    public class Discount
    {

        private readonly decimal _coefficientForTwoBooks = 0.05m;
        private readonly decimal _coefficientForThreebooks = 0.1m;
        private readonly decimal _coefficientForFourbooks = 0.2m;
        private readonly decimal _coefficientForFiveOrMoreBooks = 0.25m;

        private decimal _count;
        private decimal _cost;

        public Discount(decimal cost, decimal count)
        {
            _cost = cost;
            _count = count;
        }

        public decimal GetCostForSet()
        {
            var groupCost = _count * _cost;

            if (_count == 2)
            {
                groupCost -= groupCost * _coefficientForTwoBooks;
            }

            if(_count == 3)
            {
                groupCost -= groupCost * _coefficientForThreebooks;
            }

            if (_count == 4)
            {
                groupCost -= groupCost * _coefficientForFourbooks;
            }

            if (_count >= 5)
            {
                groupCost -= groupCost * _coefficientForFiveOrMoreBooks;
            }

            return groupCost;
        }
    }
}
