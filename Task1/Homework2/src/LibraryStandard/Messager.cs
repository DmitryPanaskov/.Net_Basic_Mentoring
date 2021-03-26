using System;

namespace LibraryStandard
{
    public class Messager
    {
        public string GetGreetingWithDate(string userName)
        {
            var currentDateTime = DateTimeOffset.Now;
            return $"{currentDateTime} Hello {userName}!";
        }
    }
}
