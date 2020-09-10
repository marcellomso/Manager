using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IVehicleRepository
    {
        IQueryable<Vehicle> Get();
        Vehicle Get(int id);
        void New(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
    }
}
