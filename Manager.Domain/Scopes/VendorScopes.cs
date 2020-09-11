using Manager.Domain.Entities;
using Manager.SharedKernel.Events;
using Manager.SharedKernel.Validations;

namespace Manager.Domain.Scopes
{
    public static class VendorScopes
    {
        public static bool ScopesValid(this Vendor vendor)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotEmpty(vendor.Name, "Informe o nome."),
                AssertionConcern.AssertNotNull(vendor.Role, "Cargo inválido."),
                ValidadeCustomCommission(vendor.CustomCommission)
            );
        }

        private static DomainNotification ValidadeCustomCommission(decimal customCommission)
        {
            if (customCommission > 0)
                return AssertionConcern.AssertIsRange(customCommission, 5, 18, "Comissão customizada inválida. Informe um valor entre 5 e 18%");

            return null;
        }
    }
}
