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
            UserExceptionService.ExceptionHandled(this);
        }

        public UserException(string message)
            : base(message)
        {
            UserExceptionService.ExceptionHandled(this);
        }

        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
            UserExceptionService.ExceptionHandled(this);
        }

        protected UserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            UserExceptionService.ExceptionHandled(this);
        }
    }
}
