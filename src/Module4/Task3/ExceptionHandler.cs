using System;
using System.Collections.Generic;
using System.Linq;
using Task3.DoNotChange;
using Task3.Exceptions;
using Task3.Interfaces;

namespace Task3
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static Dictionary<Type, Action<UserException, IResponseModel>> dict =
            new Dictionary<Type, Action<UserException, IResponseModel>>
            {
                { typeof(UserNotFound), (ex, model) => model.AddAttribute("action_result", ex.Message) },

                { typeof(InvalidUserIdException), (ex, model) =>
                    {
                        Log();
                        model.AddAttribute("action_result", ex.Message);
                    }
                },

                { typeof(TaskAlreadyExists), (ex, model) => throw new Exception("some new message") },
        };

        public void Handle(UserException ex, IResponseModel model)
        {
            if (dict.TryGetValue(ex.GetType(), out var action))
            {
                action(ex, model);
            }
            else
            {
                throw ex;
            }
        }

        private static void Log()
        {
            // Logging.
        }
    }
}
