using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System;
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

            _context
                .Entry<Vehicle>(vehicle)
                .State = System.Data.Entity.EntityState.Modified;
        }

        public IQueryable<Vehicle> Get()
        {
            throw new NotImplementedException();
        }

        public Vehicle Get(int id)
        {
            throw new NotImplementedException();
        }

        public void New(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
