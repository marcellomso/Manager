using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IVehicleRepository
    {
        IQueryable<Vehicle> Get();
        Vehicle Get(int id);
        List<Vehicle> GetByOpportunity(int opportunityId);

        void New(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
    }
}
