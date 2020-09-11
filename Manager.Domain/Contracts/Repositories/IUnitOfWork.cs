using System;

namespace Manager.Domain.Contracts.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}
