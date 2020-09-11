using Manager.Domain.Commands.VehicleCommands;
using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Service
{
    public class VehicleService : ServiceBase, IVehicleService
    {
        private readonly IVehicleRepository _repository;
        private readonly IFuelRepository _fuelRepository;

        public VehicleService(
            IVehicleRepository repository,
            IFuelRepository fuelRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _fuelRepository = fuelRepository;
        }

        public bool Delete(int id)
        {
            var vehicle = Get(id);

            if (vehicle != null)
                _repository.Delete(vehicle);

            if (Commit())
                return true;

            return false;
        }

        public List<VehicleListCommand> Get()
        {
            return _repository.Get()
                .Where(x=> !x.Deleted)
                .Select(x => new VehicleListCommand
                {
                    Id = x.Id,
                    Name = x.Name,
                    Model = x.Model,
                    Year = x.Year,
                    Amount = x.Amount,
                    Status = x.Status,
                    Fuel = x.Fuel.Name
                })
                .ToList();
        }

        public Vehicle Get(int id)
            => _repository.Get(id);

        private Fuel GetFuel(int fuelId)
            => _fuelRepository.Get(fuelId);

        public Vehicle New(VehicleCommand command)
        {
            ValidateObject(command, "Objeto veículo desconhecido.");

            var vehicle = new Vehicle(
                command.Name,
                command.Year,
                command.Model,
                GetFuel(command.Fuel),
                command.Amount,
                command.Status);

            _repository.New(vehicle);

            if (Commit())
                return vehicle;

            return null;
        }

        public Vehicle Update(UpdateVehicleCommand command)
        {
            ValidateObject(command, "Objeto veículo desconhecido.");
            var vehicle = Get(command.Id);

            if (vehicle != null)
                vehicle.Update(
                    command.Name,
                    command.Year,
                    command.Model,
                    GetFuel(command.Fuel),
                    command.Amount,
                    command.Status);

            _repository.Update(vehicle);

            if (Commit())
                return vehicle;

            return null;
        }
    }
}
