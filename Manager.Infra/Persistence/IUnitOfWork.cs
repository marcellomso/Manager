using System;

namespace Manager.Infra.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}
