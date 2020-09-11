using Manager.Domain.Commands.VendoresCommands;
using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Service
{
    public class VendorService : ServiceBase, IVendorService
    {
        private readonly IVendorRepository _repository;
        private readonly IRoleRepository _roleRepository;

        public VendorService(
            IVendorRepository repository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _roleRepository = roleRepository;
        }


        public bool Delete(int id)
        {
            var vendor = Get(id);

            if (vendor != null)
                _repository.Delete(vendor);

            if (Commit())
                return true;

            return false;
        }

        public List<VendorListCommand> Get()
        {
            return _repository.Get()
                .Where(x => !x.Deleted)
                .Select(x => new VendorListCommand
                {
                    Id = x.Id,
                    Name = x.Name,
                    CustomCommission = x.CustomCommission,
                    Role = x.Role.Name
                })
                .ToList();
        }

        public Vendor Get(int id)
            => _repository.Get(id);

        private Role GetRole(int roleId)
            => _roleRepository.Get(roleId);

        public Vendor New(VendorCommand command)
        {
            ValidateObject(command, "Objeto vendedor desconhecido.");

            var vendor = new Vendor(
                command.Name,
                GetRole(command.Role),
                command.CustomCommission);

            _repository.New(vendor);

            if (Commit())
                return vendor;

            return null;
        }

        public Vendor Update(VendorUpdateCommand command)
        {
            ValidateObject(command, "Objeto vendedor desconhecido.");
            var vendor = Get(command.Id);

            if (vendor != null)
                vendor.Update(
                    command.Name,
                    GetRole(command.Role),
                    command.CustomCommission);

            _repository.Update(vendor);

            if (Commit())
                return vendor;

            return null;
        }
    }
}
