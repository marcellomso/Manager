using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Domain.Enuns;
using Manager.Infra.Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Manager.Infra.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ManagerDataContext _context;

        public OpportunityRepository(ManagerDataContext context)
        {
            _context = context;
        }

        public void Delete(Opportunity opportunity)
        {
            opportunity.Delete();
            Update(opportunity);
        }

        private static Expression<Func<Opportunity, bool>> Predicate(bool isAdmin, int vendorId)
        {
            if (isAdmin)
                return x => !x.Deleted;

            return x => !x.Deleted && x.VendorId == vendorId;
        }

        public IQueryable<Opportunity> Get(bool isAdmin, int vendorId)
            => _context.Set<Opportunity>().Where(Predicate(isAdmin, vendorId)).AsNoTracking();

        public Opportunity Get(int id, int vendorId)
            => _context.Opportunities
            .Include(x => x.Vehicle)
            .FirstOrDefault(x => x.Id == id && x.VendorId == vendorId);

        public List<Opportunity> GetByVehicle(int id, int vehicleId)
            => _context.Opportunities
                    .Where(x => x.Id != id &&
                           x.VehicleId == vehicleId &&
                           x.StatusId == (int)EOpportunityStatus.Created &&
                           !x.Deleted)
                    .ToList();

        public Opportunity GetFull(int id, int vendorId)
            => _context.Opportunities
            .Include(x => x.Vehicle)
            .Include(x=> x.Vendor)
            .Include(x=> x.Vendor.Role)
            .FirstOrDefault(x => x.Id == id && x.VendorId == vendorId);

        public bool IsDuplicate(int vehicleId, int vendorId)
            => _context.Opportunities
                .Any(x => x.VehicleId == vehicleId && x.VendorId == vendorId);

        public void New(Opportunity opportunity)
        {
            _context.Logs.Add(RegisterLog(opportunity));
            _context.Opportunities.Add(opportunity);
        }

        public void Update(Opportunity opportunity)
        {
            _context.Logs.Add(RegisterLog(opportunity));
            _context
                .Entry<Opportunity>(opportunity)
                .State = EntityState.Modified;
        }

        private OpportunitesLog RegisterLog(Opportunity opportunity)
            => new OpportunitesLog(opportunity.Vendor, opportunity.StatusId);
    }
}
