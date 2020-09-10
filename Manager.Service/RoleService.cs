using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Service;
using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public List<Role> Get()
            => _repository.Get().ToList();
    }
}
