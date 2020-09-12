using Manager.Domain.Commands.OpportunityCommands;
using Manager.Domain.Entities;
using System.Collections.Generic;

namespace Manager.Domain.Contracts.Services
{
    public interface IOpportunityService
    {
        List<OpportunityListCommand> Get(int vendorId);
        Opportunity New(OpportunityCommand command, int vendorId);
        Opportunity Update(OpportunityUpdateCommand command, int vendorId);
        bool Delete(int id, int vendorId);
        bool Accept(int id, int vendorId);
        bool Cancel(int id, int vendorId);
    }
}
