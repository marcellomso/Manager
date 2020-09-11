using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class VendorRepoitory : IVendorRepository
    {
        private readonly ManagerDataContext _context;

        public VendorRepoitory(ManagerDataContext context)
        {
            _context = context;
        }

        public void Delete(Vendor vendor)
        {
            vendor.Delete();
            Update(vendor);
        }

        public IQueryable<Vendor> Get()
             => _context.Set<Vendor>().Where(x => !x.Deleted).AsNoTracking();

        public Vendor Get(int id)
            => _context.Vendores.FirstOrDefault(x => x.Id == id);

        public void New(Vendor vendor)
            => _context.Vendores.Add(vendor);

        public void Update(Vendor vendor)
             => _context
                .Entry<Vendor>(vendor)
                .State = EntityState.Modified;
    }
}
