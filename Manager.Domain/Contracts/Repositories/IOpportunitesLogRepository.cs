using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IOpportunitesLogRepository
    {
        IQueryable<OpportunitesLog> Get();
        void New(OpportunitesLog log);
    }
}
