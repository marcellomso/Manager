﻿using Manager.Domain.Commands.VendoresCommands;
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
        private readonly IUserRepository _userRepository;

        public VendorService(
            IVendorRepository repository,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public bool Delete(int id)
        {
            var vendor = Get(id);

            if (!ValidateObject(vendor, "Vendedor não encontrado."))
                return false;

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
            if (!ValidateObject(command, "Objeto vendedor desconhecido."))
                return null;

            var vendor = new Vendor(
                command.Name,
                GetRole(command.Role),
                command.CustomCommission);

            var user = new User(vendor, command.Username);
            user.ConfigurePassord(command.Password, command.PasswordConfirmation);

            _repository.New(vendor);
            _userRepository.New(user);

            if (Commit())
                return vendor;

            return null;
        }

        public Vendor Update(VendorUpdateCommand command)
        {
            if (!ValidateObject(command, "Objeto vendedor desconhecido."))
                return null;

            var vendor = Get(command.Id);

            if (!ValidateObject(vendor, "Vendedor não encontrado."))
                return null;

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
