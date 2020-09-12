using Manager.Domain.Entities;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        User Authenticate(string username, string passord);
        void New(User user);
        bool IsAdmin(int id);
    }
}
