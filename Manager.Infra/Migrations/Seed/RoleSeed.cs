using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Linq;

namespace Manager.Infra.Migrations.Seed
{
    public static class RoleSeed
    {
        public static void Seed(ManagerDataContext context)
        {
            if (context.Roles.Count() == 0)
            {
                context.Roles.Add(new Role("Vendedor Júnior", 5));
                context.Roles.Add(new Role("Vendedor Pleno", 7.5));
                context.Roles.Add(new Role("Vendedor Sênior", 11));
                
                var manager = new Role("Gerente", 0);
                context.Roles.Add(manager);

                var vendorManager = new Vendor("Gerente", manager, 0);
                context.Vendores.Add(vendorManager);

                var user = new User(vendorManager, "admin");
                user.ConfigurePassord("123456", "123456");
                user.DefineAdministrator();
                context.Users.Add(user);
            }
        }
    }
}
