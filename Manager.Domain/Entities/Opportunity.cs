using Manager.Domain.Enuns;
using System;

namespace Manager.Domain.Entities
{
    public class Opportunity
    {
        public Guid VeicheId { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public Guid VendorId { get; private set; }
        public Vendor Vendor { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime Expiration { get => Creation.AddDays(7); }
        public EStatusOpportunity Status { get; private set; }
        public double Amount { get; private set; }

        protected Opportunity() {}

        public Opportunity(Vehicle vehicle, Vendor vendor, double amount)
        {
            Vehicle = vehicle;
            Vendor = vendor;
            Amount = amount;

            Creation = DateTime.Now;
            Status = EStatusOpportunity.Criada;
        }
    }
}
