using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Task3
{
    [Serializable]
    public class UserException : Exception, ISerializable
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
    }
}
