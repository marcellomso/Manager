using Manager.Domain.Entities;
using System.Linq;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IVendorRepoitory
    {
        IQueryable<Vendor> Get();
        Vendor Get(int id);
        void New(Vendor vendor);
        void Update(Vendor vendor);
        void Delete(Vendor vendor);
    }
}
