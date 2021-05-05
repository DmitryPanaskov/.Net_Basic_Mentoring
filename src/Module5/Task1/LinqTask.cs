using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        // Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X.
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            ChekNull(customers);

            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        // Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе.
        // Сделайте задания без использованием группировки.
        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers)
        {
            ChekNull(customers);
            ChekNull(suppliers);

            return customers.Select(customer => (customer,
                                                    suppliers.Where(supplier => supplier.City == customer.City &&
                                                        supplier.Country == customer.Country)));
        }

        // Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе.
        // Сделайте задания с использованием группировки.
        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers)
        {
            ChekNull(customers);
            ChekNull(suppliers);

            return from customer in customers
                   join supplier in suppliers on new { customer.City, customer.Country }
                       equals new { supplier.City, supplier.Country } into gr
                   select (customer, gr);
        }

        // Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X.
        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            ChekNull(customers);

            return customers.Where(customer => customer.Orders.Any(order => order.Total > limit));
        }

        // Выдайте список клиентов с указанием, начиная с какой даты они стали клиентами
        // (принять за таковую дату самого первого заказа)
        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers)
        {
            ChekNull(customers);

            return from customer in customers
                   where customer.Orders.Any()
                   select (customer, customer.Orders.First().OrderDate);
        }

        //Сделайте предыдущее задание, но выдайте список отсортированным по году,
        //месяцу, оборотам клиента (от максимального к минимальному) и имени клиента
        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers)
        {
            ChekNull(customers);

            return from customer in customers
                   where customer.Orders.Any()
                   orderby customer.Orders.FirstOrDefault()?.OrderDate.Year, customer.Orders.FirstOrDefault()?.OrderDate.Month,
                       customer.Orders.Sum(order => order.Total) descending, customer.CompanyName
                   select (customer, customer.Orders.First().OrderDate);
        }

        // Укажите всех клиентов, у которых указан нецифровой почтовый код или
        // не заполнен регион или в телефоне не указан код оператора
        // (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).
        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            ChekNull(customers);

            return from customer in customers
                   where customer.PostalCode == null || customer.PostalCode.Any(p => !char.IsDigit(p))
                                                     || string.IsNullOrWhiteSpace(customer.Region)
                                                     || new Regex(@"^\s*(\s*\b+\s*)").IsMatch(customer.Phone)
                   select customer;
        }

        // Сгруппируйте все продукты по категориям, внутри – по наличию на складе,
        // внутри последней группы отсортируйте по стоимости
        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            ChekNull(products);

            return from prod in products
                   group prod by prod.Category into category
                   select new Linq7CategoryGroup
                   {
                       Category = category.Key,
                       UnitsInStockGroup = from exist in category
                                           group exist.UnitPrice by exist.UnitsInStock
                                                      into stock
                                           select new Linq7UnitsInStockGroup
                                           {
                                               UnitsInStock = stock.Key,
                                               Prices = stock,
                                           },
                   };
        }

        // Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие».
        // (0, дешевые] U (дешевые, средняя цена] U (средняя цена, дорогие].
        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive)
        {
            ChekNull(products);

            var categories = new List<decimal> { cheap, middle, expensive };

            return from prod in products
                   group prod by categories.First(cat => prod.UnitPrice <= cat) into output
                   select (output.Key, output.Select(product => product).AsEnumerable());
        }

        // Рассчитайте среднюю прибыльность каждого города (средняя сумма заказов на каждого клиента)
        // и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)
        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers)
        {
            ChekNull(customers);

            return from customer in customers
                   group customer by customer.City into cityGroup
                   select (cityGroup.Key,
                              Convert.ToInt32(cityGroup.Average(customer => customer.Orders.Sum(order => order.Total))),
                              Convert.ToInt32(cityGroup.Average(customer => customer.Orders.Length)));
        }

        // Соберите строку, состоящую из уникальных названий стран поставщиков,
        // отсортированную сначала по длине, потом по названию страны.
        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            ChekNull(suppliers);

            return string.Join("", suppliers.Select(sup => sup.Country)
                                                       .Distinct()
                                                       .OrderBy(country => country.Length)
                                                       .ThenBy(country => country));
        }

        private static void ChekNull<T>(T t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }
        }
    }
}