using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ManagerDataContext _context;

        public VehicleRepository(ManagerDataContext context)
        {
            _context = context;
        }

        public void Delete(Vehicle vehicle)
        {
            vehicle.Delete();
            Update(vehicle);
        }

        public IQueryable<Vehicle> Get()
            => _context.Set<Vehicle>().Where(x => !x.Deleted).AsNoTracking();

        public Vehicle Get(int id)
            => _context.Vehicles.FirstOrDefault(x => x.Id == id);

        public List<Vehicle> GetByOpportunity(int opportunityId)
        {
            return _context.Vehicles
                .Where(x => x.Oportunidades.Any(y => y.Id == opportunityId))
                .ToList();
        }

        public void New(Vehicle vehicle)
            => _context.Vehicles.Add(vehicle);

        public void Update(Vehicle vehicle)
            => _context
                .Entry<Vehicle>(vehicle)
                .State = EntityState.Modified;
    }
}
