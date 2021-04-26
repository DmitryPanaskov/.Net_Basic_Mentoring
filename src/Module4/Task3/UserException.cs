using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Task3.DoNotChange;

namespace Task3
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException()
        {
        }

        public UserException(string message)
            : base(message)
        {

        }

        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string UserExceptionHandler(int value)
        {
            if (value == -1)
            {
                return "Invalid userId";
            }

            if (value == -2)
            {
                return "User not found";
            }

            if (value == -3)
            {
                return "The task already exists";
            }

            return null;
        }
    }
}
