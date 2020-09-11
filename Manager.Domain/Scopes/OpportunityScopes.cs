using Manager.Domain.Entities;
using Manager.SharedKernel.Validations;

namespace Manager.Domain.Scopes
{
    public static class OpportunityScopes
    {
        public static bool NewScopesValid(this Opportunity opportunity)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(opportunity.Vehicle, "Veículo inválido."),
                AssertionConcern.AssertNotNull(opportunity.Vendor, "Vendedor inválido."),
                AssertionConcern.AssertTrue(!opportunity.Vehicle.Deleted, "O veículo não encontrado, selecione outro"),
                AssertionConcern.AssertTrue(!opportunity.Vehicle.Sold, "O veículo já foi vendido, selecione outro")
            ); ;
        }
    }
}
