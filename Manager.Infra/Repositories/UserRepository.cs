using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using Manager.SharedKernel.Helpers;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ManagerDataContext _context;

        public UserRepository(ManagerDataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string passord)
        {
            string encriptedPassord = PasswordHelper.Encrypt(passord);
            return _context.Users
                .Include(x=> x.Vendor)
                .FirstOrDefault(x => x.Username == username && x.Password == encriptedPassord);
        }

        public bool IsAdmin(int id)
            => _context.Users.Any(x => x.VendorId == id && x.IsAdmin);

        public void New(User user)
         => _context.Users.Add(user);
    }
}
