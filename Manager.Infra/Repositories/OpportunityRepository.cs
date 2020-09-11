using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ManagerDataContext _context;

        public OpportunityRepository(ManagerDataContext context)
        {
            _context = context;
        }

        public void Delete(Opportunity opportunity)
        {
            opportunity.Delete();
            Update(opportunity);
        }

        public IQueryable<Opportunity> Get()
            => _context.Set<Opportunity>().Where(x => !x.Deleted).AsNoTracking();

        public Opportunity Get(int id)
            => _context.Opportunities.FirstOrDefault(x => x.Id == id);

        public void New(Opportunity opportunity)
            => _context.Opportunities.Add(opportunity);

        public void Update(Opportunity opportunity)
            => _context
                .Entry<Opportunity>(opportunity)
                .State = EntityState.Modified;
    }
}
