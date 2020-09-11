using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IOpportunityRepository
    {
        IQueryable<Opportunity> Get();
        Opportunity Get(int id);
        void New(Opportunity opportunity);
        void Update(Opportunity opportunity);
        void Delete(Opportunity opportunity);
    }
}
