using Manager.Domain.Scopes;
using Manager.SharedKernel.Helpers;

namespace Manager.Domain.Entities
{
    public class User: BaseEntity
    {
        public int VendorId { get; private set; }
        public Vendor Vendor { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; private set; }

        protected User() {}
        public User(Vendor vendor, string username)
        {
            Vendor = vendor;
            Username = username;

            this.ScopesValid();
        }

        public void ConfigurePassord(string password, string passwordConfirmation)
        {
            if (!this.PassordScopesValid(password, passwordConfirmation))
                return;

            Password = PasswordHelper.Encrypt(password);
        }

        public void DefineAdministrator()
        {
            IsAdmin = true;
        }
    }
}
