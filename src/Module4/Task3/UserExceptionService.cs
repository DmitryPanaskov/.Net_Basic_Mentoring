using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Threading.Tasks;
using Task3.Interfaces;

namespace Task3
{
    public class UserExceptionService
    {
        private Exception _exception;


        public UserExceptionService(Exception ex)
        {
            this._exception = ex;
        }

        public bool ExceptionHandled()
        {
            // log
            return true;
        }
    }
}
