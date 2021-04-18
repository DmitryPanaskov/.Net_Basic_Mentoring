using System;
using System.Linq;
using LibraryStandard.Interfaces;

namespace LibraryStandard
{
    public class Messenger : IMessenger
    {
        /// <inheritdoc/>
        public string GetGreeting(string[] args)
        {
            return $"{GetCurrentDate()} Hello {string.Join(" ", args)}!";
        }

        private static DateTimeOffset GetCurrentDate()
        {
            return DateTimeOffset.Now;
        }
    }
}
