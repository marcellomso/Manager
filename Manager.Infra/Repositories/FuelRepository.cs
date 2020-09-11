using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class FuelRepository : IFuelRepository
    {
        private readonly ManagerDataContext _context;

        public FuelRepository(ManagerDataContext context)
        {
            _context = context;
        }
        public IQueryable<Fuel> Get()
            => _context.Set<Fuel>().Where(x => !x.Deleted).AsNoTracking();

        public Fuel Get(int id)
            => _context.Fuels.Where(x => x.Id == id).FirstOrDefault();
    }
}
