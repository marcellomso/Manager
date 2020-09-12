using Manager.Domain.Entities;
using Manager.SharedKernel.Validations;

namespace Manager.Domain.Scopes
{
    public static class UserScopes
    {
        public static bool ScopesValid(this User user)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(user.Vendor, "Vendedor inválido."),
                AssertionConcern.AssertNotEmpty(user.Username, "Username inválido.")
            );
        }

        public static bool PassordScopesValid(this User _, string password, string passwordConfirmation)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertLength(password, 3, 6, "A senha deve conter entre 3 e 6 caracteres."),
                AssertionConcern.AssertAreEquals(password, passwordConfirmation, "Confirmação de senha inválida")
            );
        }
    }
}
