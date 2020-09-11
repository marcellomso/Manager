using Manager.Domain.Entities;
using System.Collections.Generic;

namespace Manager.Domain.Contracts.Services
{
    public interface IRoleService
    {
        List<Role> Get();
    }
}
