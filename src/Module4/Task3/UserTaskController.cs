using Task3.DoNotChange;
using Task3.Exceptions;
using Task3.Interfaces;

namespace Task3
{
    public class UserTaskController
    {
        private readonly IUserTaskService _taskService;
        private readonly IExceptionService _exceptionService;

        public UserTaskController(IUserTaskService taskService,
            IExceptionService exceptionService)
        {
            _taskService = taskService;
            _exceptionService = exceptionService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            try
            {
                _taskService.AddTaskForUser(userId, new UserTask(description));

            }
            catch (UserException ex)
            {
                _exceptionService.ExceptionHundler(ex);
                model.AddAttribute("action_result", ex.Message);
                return false;
            }
            
            return true;
        }
    }
}