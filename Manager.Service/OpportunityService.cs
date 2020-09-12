using Manager.Domain.Commands.OpportunityCommands;
using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using Manager.SharedKernel.Events;
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
        private readonly IUserRepository _userRepository;

        public OpportunityService(
            IOpportunityRepository repository,
            IVendorRepository vendorRepository,
            IVehicleRepository vehicleRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _vendorRepository = vendorRepository;
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;
        }

        public bool Delete(int id, int vendorId)
        {
            var opportunity = GetId(id, vendorId);

            if (!ValidateObject(opportunity, "Proposta não encontrada."))
                return false;

            if (!ExprationDateValidate(opportunity))
                return false;

            opportunity.Delete();
            _repository.Delete(opportunity);

            if (Commit())
                return true;

            return false;
        }

        public List<OpportunityListCommand> Get(int vendorId)
        {
            bool admin = _userRepository.IsAdmin(vendorId);

            return _repository.Get(admin, vendorId)
                .Select(x => new OpportunityListCommand
                {
                    Id = x.Id,
                    Vendor = x.Vendor.Name,
                    Veiche = x.Vehicle.Name,
                    Amount = x.Amount,
                    Status = x.Status.Description,
                    Creation = x.Creation,
                    Expiration = x.Expiration,
                    Commision = x.Comission
                })
                .ToList();
        }

        private Opportunity GetId(int id, int vendorId)
            => _repository.Get(id, vendorId);

        private Vehicle GetVehicle(int vehicleId)
            => _vehicleRepository.Get(vehicleId);

        private Vendor GetVendor(int vendorId)
            => _vendorRepository.Get(vendorId);

        public Opportunity New(OpportunityCommand command, int vendorId)
        {
            if (!ValidateObject(command, "Objeto proposta desconhecido."))
                return null;

            var opportunity = new Opportunity(
                GetVehicle(command.Vehicle),
                GetVendor(vendorId),
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

        public Opportunity Update(OpportunityUpdateCommand command, int vendorId)
        {
            if (!ValidateObject(command, "Objeto proposta desconhecido."))
                return null;

            var opportunity = GetId(command.Id, vendorId);

            if (!ValidateObject(opportunity, "Proposta não encontrado."))
                return null;

            if (!ExprationDateValidate(opportunity))
                return null;

            opportunity.Update(
                GetVehicle(command.Vehicle),
                command.Amount);

            _repository.Update(opportunity);

            if (Commit())
                return opportunity;

            return null;
        }

        public bool Accept(int id, int vendorId)
        {
            var opportunity = _repository.GetFull(id, vendorId);

            if (!ValidateObject(opportunity, "Proposta não encontrada."))
                return false;

            if (!ExprationDateValidate(opportunity))
                return false;

            decimal percentege = GetPercentageCommission(opportunity);

            if (opportunity.Accept(percentege))
            {
                opportunity.Vehicle.Sold();

                var opportunities = _repository.GetByVehicle(id, opportunity.VehicleId);
                foreach (var opp in opportunities)
                {
                    if (opp.IsExpired)
                        opp.Expired();
                    else
                        opp.Cancel();

                    _repository.Update(opp);
                }
            }

            _repository.Update(opportunity);

            if (Commit())
                return true;

            return false;
        }

        public bool Cancel(int id, int vendorId)
        {
            var opportunity = GetId(id, vendorId);

            if (!ValidateObject(opportunity, "Proposta não encontrada."))
                return false;

            if (!ExprationDateValidate(opportunity))
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

        private bool ExprationDateValidate(Opportunity opportunity)
        {
            if (!opportunity.IsExpired)
                return true;

            opportunity.Expired();
            _repository.Update(opportunity);
            Commit();

            return AssertionConcern.IsSatisfiedBy
            (
                new DomainNotification("ExprationDateValidate", "Proposta com data de validade expirada.")
            );
        }

        private decimal GetPercentageCommission(Opportunity opportunity)
        {
            if (opportunity.Vendor.CustomCommission > 0)
                return opportunity.Vendor.CustomCommission;
            else
                return (decimal)opportunity.Vendor.Role.Commission;
        }
    }
}
