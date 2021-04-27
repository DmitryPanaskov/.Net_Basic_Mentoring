using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;
        private readonly UserException _userException;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
            _userException = new UserException();
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            int result = _taskService.AddTaskForUser(userId, new UserTask(description));

            if (result == 0)
            {
                model.AddAttribute("action_result", _userException.UserExceptionHandler(result));
                return false;
            }

            return true;
        }
    }
}