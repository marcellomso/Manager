using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IFuelRepository
    {
        IQueryable<Fuel> Get();
        Fuel Get(int id);
    }
}
