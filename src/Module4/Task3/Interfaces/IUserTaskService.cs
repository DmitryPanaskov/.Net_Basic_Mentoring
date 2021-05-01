using Task3.DoNotChange;

namespace Task3
{
    public interface IUserTaskService
    {
        void AddTaskForUser(int userId, UserTask task);
    }
}
