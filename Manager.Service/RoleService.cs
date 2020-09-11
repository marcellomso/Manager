using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Service
{
    public class RoleService : ServiceBase, IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(
            IRoleRepository repository,
            IUnitOfWork unitOfWork): base(unitOfWork)
        {
            _repository = repository;
        }

        public List<Role> Get()
            => _repository.Get().ToList();
    }
}
