using System;
using System.Collections.Generic;
using System.Text;
using Task3.Interfaces;

namespace Task3
{
    public class ExceptionService : IExceptionService
    {
        public void ExceptionHundler(UserException exception)
        {
            // Logging for example.
        }
    }
}
