using Manager.Domain.Commands.FuelCommand;
using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Contracts.Services
{
    public interface IFuelService
    {
        List<FuelListCommand> Get();
        Fuel Get(int id);
    }
}
