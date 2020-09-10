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
            }
        }
    }
}
