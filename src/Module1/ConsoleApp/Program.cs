using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Input;
using LibraryStandard;
using LibraryStandard.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    public static partial class Program
    {
        private static IMessager _messager;
        private static ServiceProvider _serviceProvider;

        public static IMessager Messager { get => _messager; set => _messager = value; }

        public static void Main(string[] args)
        {
            RegisterDI();
            Messager = _serviceProvider.GetService<IMessager>();
            var message = Messager.GetGreetingFromConsoleParameters(args);
            Console.WriteLine(message);
        }

        private static void RegisterDI()
        {
            _serviceProvider = new ServiceCollection()
            .AddSingleton<IMessager, Messager>()
            .BuildServiceProvider();
        }
    }
}
