using Manager.Domain.Entities;
using System.Collections.Generic;

namespace Manager.Domain.Contracts.Service
{
    public interface IRoleService
    {
        List<Role> Get();
    }
}
