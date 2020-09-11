using Manager.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Manager.Infra.Persistence.DataContext
{
    public class ManagerDataContext: DbContext
    {
        public DbSet<Vendor> Vendores { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Fuel> Fuels { get; set; }

        public ManagerDataContext(string stringConexao) : base(stringConexao)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public ManagerDataContext() : base("BitzenConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public static void Update(string connectionString)
        {
            var configuration = new Migrations.Configuration
            {
                TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
            };
            new System.Data.Entity.Migrations.DbMigrator(configuration).Update();
        }
    }
}
