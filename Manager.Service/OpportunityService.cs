using Manager.Domain.Commands.OpportunityCommands;
using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using Manager.SharedKernel.Validations;
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
            var opportunity = Get(id);

            if (!ValidateObject(opportunity, "Proposta não encontrada."))
                return false;

            if (!ExprationDateValid(opportunity))
                return false;

            opportunity.Delete();
            _repository.Delete(opportunity);

            if (Commit())
                return true;

            return false;
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
                    Status = x.Status.Description,
                    Creation = x.Creation,
                    Expiration = x.Expiration
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
            if (!ValidateObject(command, "Objeto proposta desconhecido."))
                return null;

            var opportunity = new Opportunity(
                GetVehicle(command.Vehicle),
                GetVendor(command.Vendor),
                command.Amount);

            if (opportunity.Vehicle != null &&
                opportunity.Vendor != null &&
                !ValidateDuplicateOpportunity(opportunity.Vehicle.Id, opportunity.Vendor.Id))
                return null;

            _repository.New(opportunity);

            if (Commit())
                return opportunity;

            return null;
        }

        public Opportunity Update(OpportunityUpdateCommand command)
        {
            if (!ValidateObject(command, "Objeto proposta desconhecido."))
                return null;

            var opportunity = Get(command.Id);

            if (!ValidateObject(opportunity, "Proposta não encontrado."))
                return null;

            if (!ExprationDateValid(opportunity))
                return null;

            opportunity.Update(
                GetVehicle(command.Vehicle),
                command.Amount);

            _repository.Update(opportunity);

            if (Commit())
                return opportunity;

            return null;
        }

        public bool Accept(int id)
        {
            var opportunity = Get(id);

            if (!ValidateObject(opportunity, "Proposta não encontrada."))
                return false;

            if (!ExprationDateValid(opportunity))
                return false;

            if (opportunity.Accept())
            {
                opportunity.Vehicle.Sold();

                var opportunities = _repository.GetByVehicle(id, opportunity.VehicleId);
                foreach (var opp in opportunities)
                {
                    if (ExprationDateValid(opp))
                        opp.Cancel();

                    _repository.Update(opp);
                }
            }

            _repository.Update(opportunity);

            if (Commit())
                return true;

            return false;
        }

        public bool Cancel(int id)
        {
            var opportunity = Get(id);

            if (!ValidateObject(opportunity, "Proposta não encontrada."))
                return false;

            if (!ExprationDateValid(opportunity))
                return false;

            opportunity.Cancel();
            _repository.Update(opportunity);

            if (Commit())
                return true;

            return false;
        }

        private bool ValidateDuplicateOpportunity(int vehicleId, int vendorId)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertTrue(!_repository.IsDuplicate(vehicleId, vendorId), "Proposta já adicionada para esse vendedor.")
            );
        }

        private bool ExprationDateValid(Opportunity opportunity)
        {
            if (!opportunity.IsExpired)
                return true;

            opportunity.Expired();
            _repository.Update(opportunity);
            Commit();

            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertTrue(false, "Proposta com data de validade expirada.")
            );
        }
    }
}
