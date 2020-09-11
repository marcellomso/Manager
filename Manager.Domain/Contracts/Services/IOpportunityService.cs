using Manager.Domain.Commands.OpportunityCommands;
using Manager.Domain.Entities;
using System.Collections.Generic;

namespace Manager.Domain.Contracts.Services
{
    public interface IOpportunityService
    {
        List<OpportunityListCommand> Get();
        Opportunity Get(int id);
        Opportunity New(OpportunityCommand command);
        Opportunity Update(OpportunityUpdateCommand command);
        bool Delete(int id);
    }
}
