using System;
using Task3.DoNotChange;
using Task3.Exceptions;
using Task3.Interfaces;

namespace Task3
{
    public class UserTaskController
    {
        private readonly IUserTaskService _taskService;
        private readonly IExceptionHandler _exceptionHandler;

        public UserTaskController(IUserTaskService taskService, ExceptionHandler exceptionHandler)
        {
            _taskService = taskService;
            _exceptionHandler = exceptionHandler;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            try
            {
                _taskService.AddTaskForUser(userId, new UserTask(description));
            }
            catch (UserException ex)
            {
                _exceptionHandler.Handle(ex, model);
                return false;
            }

            return true;
        }
    }
}