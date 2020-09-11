using Manager.Domain.Entities;
using Manager.Domain.Enuns;
using Manager.SharedKernel.Events;
using Manager.SharedKernel.Validations;

namespace Manager.Domain.Scopes
{
    public static class OpportunityScopes
    {
        public static bool NewScopesValid(this Opportunity opportunity)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(opportunity.Vendor, "Vendedor inválido."),
                AssertionConcern.AssertNotNull(opportunity.Vehicle, "Veículo inválido."),
                CanSoldVehicle(opportunity.Vehicle),
                CanUseVehicle(opportunity.Vehicle),
                AssertionConcern.AssertIsGreaterThan(opportunity.Amount, 0, "Valor da proposta inválido.")
            );
        }

        public static bool UpddateScopesValid(this Opportunity opportunity)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                CanUpdate(opportunity),
                AssertionConcern.AssertNotNull(opportunity.Vehicle, "Veículo inválido."),
                CanSoldVehicle(opportunity.Vehicle),
                CanUseVehicle(opportunity.Vehicle),
                AssertionConcern.AssertIsGreaterThan(opportunity.Amount, 0, "Valor da proposta inválido.")
            );
        }

        public static bool DeleteScopesValid(this Opportunity opportunity)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(opportunity.Deleted, "Proposta já foi excluída."),
                CanDelete(opportunity)
            );
        }

        public static bool ChangeStatusScopesValid(this Opportunity opportunity)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(opportunity.Deleted, "Proposta já foi excluída."),
                CanUpdate(opportunity)
            );
        }

        public static bool ChangeExpiredStatusScopesValid(this Opportunity opportunity)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertTrue(
                    opportunity.StatusId == (int)EOpportunityStatus.Created && 
                    opportunity.IsExpired, "Não é permitido alterar o status da proposta.")
            );
        }

        private static DomainNotification CanDelete(Opportunity opportunity)
        {
            return CanChange(opportunity)
                ? null
                : new DomainNotification("CanDelete", "Não é permitido excluir a proposta.");
        }

        private static DomainNotification CanUpdate(Opportunity opportunity)
        {
            return CanChange(opportunity)
                ? null
                : new DomainNotification("CanUpdate", "Não é permitido alterar a proposta.");
        }

        private static DomainNotification CanSoldVehicle(Vehicle vehicle)
        {
            return vehicle == null
                ? null
                : AssertionConcern.AssertTrue(!vehicle.IsSold, "O veículo já foi vendido, selecione outro");
        }

        private static DomainNotification CanUseVehicle(Vehicle vehicle)
        {
            return vehicle == null
                ? null
                : AssertionConcern.AssertTrue(!vehicle.Deleted, "O veículo já foi vendido, selecione outro");
        }

        private static bool CanChange(Opportunity opportunity)
            => opportunity.StatusId == (int)EOpportunityStatus.Created && !opportunity.IsExpired;
    }
}
