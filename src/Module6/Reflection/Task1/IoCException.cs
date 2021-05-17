using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task1
{
    [Serializable]
    public class IoCException : Exception
    {
        public IoCException()
        {
        }

        public IoCException(string message) : base(message)
        {
        }

        public IoCException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IoCException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
