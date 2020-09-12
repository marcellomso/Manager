using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IOpportunityRepository
    {
        IQueryable<Opportunity> Get(bool isAdmin, int vendorId);
        Opportunity Get(int id, int vendorId);
        Opportunity GetFull(int id, int vendorId);
        List<Opportunity> GetByVehicle(int id, int vehicleId);
        void New(Opportunity opportunity);
        void Update(Opportunity opportunity);
        void Delete(Opportunity opportunity);
        bool IsDuplicate(int vehicleId, int vendorId);
    }
}
