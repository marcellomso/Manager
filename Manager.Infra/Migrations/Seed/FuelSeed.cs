using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Linq;

namespace Manager.Infra.Migrations.Seed
{
    public static class FuelSeed
    {
        public static void Seed(ManagerDataContext context)
        {
            if (context.Fuels.Count() == 0)
            {
                context.Fuels.Add(new Fuel("Gasolina"));
                context.Fuels.Add(new Fuel("Disel"));
                context.Fuels.Add(new Fuel("Etanol"));
            }
        }
    }
}
