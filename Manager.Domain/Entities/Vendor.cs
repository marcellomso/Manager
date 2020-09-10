using System;

namespace Manager.Domain.Entities
{
    public class Vendor: BaseEntity
    {
        public string Name { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }
        public double CustomCommission { get; set; }

        protected Vendor() {}

        public Vendor(string name, Role role, double customCommission)
        {
            Name = name;
            Role = role;
            CustomCommission = customCommission;
        }
    }
}
