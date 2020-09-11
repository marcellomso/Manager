using Manager.Domain.Commands.VendoresCommands;
using Manager.Domain.Entities;
using System.Collections.Generic;

namespace Manager.Domain.Contracts.Services
{
    public interface IVendorService
    {
        List<VendorListCommand> Get();
        Vendor Get(int id);
        Vendor New(VendorCommand command);
        Vendor Update(VendorUpdateCommand command);
        bool Delete(int id);
    }
}
