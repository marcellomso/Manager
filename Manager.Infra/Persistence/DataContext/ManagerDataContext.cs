using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Persistence.DataContext
{
    public class ManagerDataContext: DbContext
    {
        public Vendor Vendores { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }

    }
}
