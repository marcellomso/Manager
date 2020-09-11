using Manager.Domain.Enuns;
using Manager.Domain.Scopes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager.Domain.Entities
{
    public class Opportunity: BaseEntity
    {
        public int VehicleId { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public int VendorId { get; private set; }
        public Vendor Vendor { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime Expiration { get; private set; }
        public int StatusId { get; private set; }
        public OpportunityStatus Status { get; private set; }
        public decimal Amount { get; private set; }

        public decimal Comission { get; set; }

        [NotMapped]
        public bool IsExpired
        {
            get { return 
                    StatusId == (int)EOpportunityStatus.Created && 
                    Expiration < DateTime.Now; }
        }

        protected Opportunity() {}

        public Opportunity(Vehicle vehicle, Vendor vendor, decimal amount)
        {
            Vehicle = vehicle;
            Vendor = vendor;
            Amount = amount;

            Creation = DateTime.Now;
            Expiration = Creation.AddDays(7);
            StatusId = (int)EOpportunityStatus.Created;

            this.NewScopesValid();
        }

        public void  Update(Vehicle vehicle, decimal amount)
        {
            Vehicle = vehicle;
            Amount = amount;

            this.UpddateScopesValid();
        }

        public override void Delete()
        {
            if (!this.DeleteScopesValid())
                return;

            base.Delete();
        }

        public bool Cancel()
        {
            if (!this.ChangeStatusScopesValid())
                return false;

            StatusId = (int)EOpportunityStatus.Canceled;
            return true;
        }

        public bool Accept(decimal percentage)
        {
            if (!this.ChangeStatusScopesValid())
            return false;

            StatusId = (int)EOpportunityStatus.Accept;
            Comission = Math.Round(Amount * percentage * (decimal)0.01);
            return true;
        }

        public bool Expired()
        {
            if (!this.ChangeExpiredStatusScopesValid())
                return false;

            StatusId = (int)EOpportunityStatus.Expired;
            return true;
        }
    }
}
