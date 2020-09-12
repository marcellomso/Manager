using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class OpportunitesLogRepository : IOpportunitesLogRepository
    {
        private readonly ManagerDataContext _context;

        public OpportunitesLogRepository(ManagerDataContext context)
        {
            _context = context;
        }

        public IQueryable<OpportunitesLog> Get()
            => _context.Set<OpportunitesLog>().Where(x => !x.Deleted).AsNoTracking();


        public void New(OpportunitesLog log)
            => _context.Logs.Add(log);
    }
}
