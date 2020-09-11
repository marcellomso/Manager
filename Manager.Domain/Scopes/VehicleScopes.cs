using Manager.Domain.Entities;
using Manager.SharedKernel.Validations;

namespace Manager.Domain.Scopes
{
    public static class VehicleScopes
    {
        public static bool ScopesValid(this Vehicle vehicle)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotEmpty(vehicle.Name, "Informe o nome."),
                AssertionConcern.AssertNotEmpty(vehicle.Model, "Informe o modelo."),
                AssertionConcern.AssertNotEmpty(vehicle.Year, "Informe o ano"),
                AssertionConcern.AssertNotNull(vehicle.Fuel, "Tipo combustível invalido."),
                AssertionConcern.AssertIsGreaterThan(vehicle.Amount, 0, "Valor inválido.")
            );
        }
    }
}
