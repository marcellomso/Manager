using Manager.Domain.Commands.VehicleCommands;
using Manager.Domain.Entities;
using System.Collections.Generic;

namespace Manager.Domain.Contracts.Services
{
    public interface IVehicleService
    {
        List<VehicleListCommand> Get();
        Vehicle Get(int id);
        Vehicle New(VehicleCommand command);
        Vehicle Update(VehicleUpdateCommand command);
        bool Delete(int id);
    }
}
