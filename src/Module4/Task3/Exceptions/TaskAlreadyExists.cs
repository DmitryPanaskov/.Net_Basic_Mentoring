using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task3.Exceptions
{
    [Serializable]
    public class TaskAlreadyExists : UserException
    {
        public TaskAlreadyExists()
        {
        }

        public TaskAlreadyExists(string message) : base(message)
        {
        }

        public TaskAlreadyExists(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TaskAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
