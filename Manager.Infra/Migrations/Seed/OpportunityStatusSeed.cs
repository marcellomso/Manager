using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Linq;

namespace Manager.Infra.Migrations.Seed
{
    public static class OpportunityStatusSeed
    {
        public static void Seed(ManagerDataContext context)
        {
            if (context.OpportunitiesStatus.Count() == 0)
            {
                context.OpportunitiesStatus.Add(new OpportunityStatus("Criada"));
                context.OpportunitiesStatus.Add(new OpportunityStatus("Aceita"));
                context.OpportunitiesStatus.Add(new OpportunityStatus("Cancelada"));
                context.OpportunitiesStatus.Add(new OpportunityStatus("Expirada"));
            }
        }
    }
}
