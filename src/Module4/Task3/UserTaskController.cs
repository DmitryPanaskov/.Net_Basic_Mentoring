using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly IUserTaskService _taskService;

        public UserTaskController(IUserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            try
            {
                _taskService.AddTaskForUser(userId, new UserTask(description));

            }
            catch (UserException ex)
            {
                model.AddAttribute("action_result", ex.Message);
                return false;
            }

            return true;
        }
    }
}