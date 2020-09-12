using System;

namespace Manager.Domain.Entities
{
    public class OpportunitesLog: BaseEntity
    {
        public DateTime Update { get; private set; }
        public int VendorId { get; private set; }
        public Vendor Vendor { get; private set; }

        public int StatusId { get; private set; }
        public OpportunityStatus Status { get; private set; }

        protected OpportunitesLog() { }

        public OpportunitesLog(Vendor vendor, int statusId)
        {
            Update = DateTime.Now;
            Vendor = vendor;
            StatusId = statusId;
        }
    }
}
