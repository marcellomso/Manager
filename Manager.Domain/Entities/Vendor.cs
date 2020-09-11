using Manager.Domain.Scopes;

namespace Manager.Domain.Entities
{
    public class Vendor: BaseEntity
    {
        public string Name { get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }
        public double CustomCommission { get; private set; }

        protected Vendor() {}

        public Vendor(string name, Role role, double customCommission)
        {
            SetProperties(name, role, customCommission);
        }

        public void Update(string name, Role role, double customCommsion)
        {
            SetProperties(name, role, customCommsion);
        }

        private void SetProperties(string name, Role role, double customCommsion)
        {
            Name = name;
            Role = role;
            CustomCommission = customCommsion;

            this.ScopesValid();
        }
    }
}
