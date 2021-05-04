using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task3.Exceptions
{
    [Serializable]
    public class UserNotFound : UserException
    {
        public UserNotFound()
        {
        }

        public UserNotFound(string message) : base(message)
        {
        }

        public UserNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
