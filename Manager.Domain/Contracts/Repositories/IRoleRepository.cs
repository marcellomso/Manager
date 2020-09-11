using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IRoleRepository
    {
        IQueryable<Role> Get();
        Role Get(int id);
    }
}
