namespace Manager.Infra.Migrations
{
    using Manager.Infra.Migrations.Seed;
    using Manager.Infra.Persistence.DataContext;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ManagerDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Manager.Infra.Persistence.DataContext.ManagerDataContext";
        }

        protected override void Seed(ManagerDataContext context)
        {
            RoleSeed.Seed(context);
        }
    }
}
