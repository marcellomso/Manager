using Manager.Domain.Commands.FuelCommands;
using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Contracts.Services;
using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Service
{
    public class FuelService : ServiceBase, IFuelService
    {
        private readonly IFuelRepository _repository;

        public FuelService(
            IFuelRepository repository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        public List<FuelListCommand> Get()
        {
            return _repository.Get()
                .Where(x => !x.Deleted)
                .Select(x => new FuelListCommand
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();
        }

        public Fuel Get(int id)
            => _repository.Get(id);
    }
}
