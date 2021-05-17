using Task3.DoNotChange;

namespace Task3.Interfaces
{
    public interface IExceptionHandler
    {
        void Handle(UserException user, IResponseModel model);
    }
}
