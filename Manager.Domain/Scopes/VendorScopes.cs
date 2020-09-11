using Manager.Domain.Entities;
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
                AssertionConcern.AssertNotNull(vendor.Role, "Cargo inválido.")
            ); ;
        }
    }
}
