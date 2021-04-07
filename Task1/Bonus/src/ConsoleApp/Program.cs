using System;
using LibraryStandard;
using LibraryStandard.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    public static class Program
    {
        private static IMessager _messager;
        private static ServiceProvider _serviceProvider;

        public static IMessager Messager { get => _messager; set => _messager = value; }

        public static void Main(string[] args)
        {
            RegisterDI();
            Messager = _serviceProvider.GetService<IMessager>();
            Messager.GetGreetingWithDate(args);
        }

        private static void RegisterDI()
        {
             _serviceProvider = new ServiceCollection()
             .AddSingleton<IMessager>()
             .BuildServiceProvider();
        }
    }
}
