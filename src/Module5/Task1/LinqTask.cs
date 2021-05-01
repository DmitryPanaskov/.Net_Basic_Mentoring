using System;
using System.Collections.Generic;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        // Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X.
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            throw new NotImplementedException();
        }

        // Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе.
        // Сделайте задания без использованием группировки.
        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }

        // Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе.
        // Сделайте задания с использованием группировки.
        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }

        // Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X.
        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            throw new NotImplementedException();
        }

        // Выдайте список клиентов с указанием, начиная с какой даты они стали клиентами
        // (принять за таковую дату самого первого заказа)
        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        //Сделайте предыдущее задание, но выдайте список отсортированным по году,
        //месяцу, оборотам клиента (от максимального к минимальному) и имени клиента
        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        // Укажите всех клиентов, у которых указан нецифровой почтовый код или
        // не заполнен регион или в телефоне не указан код оператора
        // (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).
        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        // Сгруппируйте все продукты по категориям, внутри – по наличию на складе,
        // внутри последней группы отсортируйте по стоимости
        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            throw new NotImplementedException();
        }

        // Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие».
        // (0, дешевые] U (дешевые, средняя цена] U (средняя цена, дорогие].
        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive)
        {
            throw new NotImplementedException();
        }

        // Рассчитайте среднюю прибыльность каждого города (средняя сумма заказов на каждого клиента)
        // и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)
        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers)
        {
            throw new NotImplementedException();
        }

        // Соберите строку, состоящую из уникальных названий стран поставщиков,
        // отсортированную сначала по длине, потом по названию страны.
        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}