﻿using Manager.Domain.Contracts.Repositories;
using Manager.Domain.Entities;
using Manager.Infra.Persistence.DataContext;
using System.Data.Entity;
using System.Linq;

namespace Manager.Infra.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ManagerDataContext _context;

        public RoleRepository(ManagerDataContext context)
        {
            _context = context;
        }

        public IQueryable<Role> Get()
            => _context.Set<Role>().Where(x=> !x.Deleted).AsNoTracking();

        public Role Get(int id)
            => _context.Roles.FirstOrDefault(x => x.Id == id);
    }
}
