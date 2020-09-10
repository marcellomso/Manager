using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Service
{
    public interface IVehicleService
    {
        IQueryable<Vehicle> Get();
        Vehicle Get(int id);
        void New(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
    }
}
