using Manager.Domain.Commands.OpportunityCommands;
using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Manager.Service
{
    public class OpportunityService : ServiceBase, IOpportunityService
    {
        private readonly IOpportunityRepository _repository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public OpportunityService(
            IOpportunityRepository repository,
            IVendorRepository vendorRepository,
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _vendorRepository = vendorRepository;
            _vehicleRepository = vehicleRepository;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OpportunityListCommand> Get()
        {
            return _repository.Get()
                .Where(x => !x.Deleted)
                .Select(x => new OpportunityListCommand
                {
                    Id = x.Id,
                    Vendor = x.Vendor.Name,
                    Veiche = x.Vehicle.Name,
                    Amount = x.Amount,
                    Status = ""
                })
                .ToList();
        }

        public Opportunity Get(int id)
            => _repository.Get(id);

        private Vehicle GetVehicle(int vehicleId)
            => _vehicleRepository.Get(vehicleId);

        private Vendor GetVendor(int vendorId)
            => _vendorRepository.Get(vendorId);

        public Opportunity New(OpportunityCommand command)
        {
            ValidateObject(command, "Objeto oportunidade desconhecido.");

            var opportunity = new Opportunity(
                GetVehicle(command.Veiche),
                GetVendor(command.Vendor),
                command.Amount);

            _repository.New(opportunity);

            if (Commit())
                return opportunity;

            return null;
        }

        public Opportunity Update(OpportunityUpdateCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
