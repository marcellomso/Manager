using Manager.Domain.Enuns;
using Manager.Domain.Scopes;
using System;

namespace Manager.Domain.Entities
{
    public class Opportunity: BaseEntity
    {
        public int VeicheId { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public int VendorId { get; private set; }
        public Vendor Vendor { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime Expiration { get => Creation.AddDays(7); }
        public EStatusOpportunity Status { get; private set; }
        public decimal Amount { get; private set; }

        protected Opportunity() {}

        public Opportunity(Vehicle vehicle, Vendor vendor, decimal amount)
        {
            Vehicle = vehicle;
            Vendor = vendor;
            Amount = amount;

            Creation = DateTime.Now;
            Status = EStatusOpportunity.Criada;

            Validate();
        }
        private void Validate()
        {
            this.NewScopesValid();
        }

    }
}
