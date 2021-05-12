using System;
using System.Collections.Generic;
using System.Linq;
using Task3.Exceptions;

namespace Task3
{
    public static class UserExceptionService
    {
        private static Dictionary<Type, Action> dict = new Dictionary<Type, Action>
        {
            { typeof(UserNotFound), new Action(Log) },
            { typeof(InvalidUserIdException), new Action(Log) },
            { typeof(TaskAlreadyExists), new Action(Log) },
        };

        public static bool ExceptionHandled(UserException ex)
        {
            dict.FirstOrDefault(x => x.Key == ex.GetType()).Value.Invoke();
            return true;
        }

        private static void Log()
        {
            // Logging.
        }
    }
}
