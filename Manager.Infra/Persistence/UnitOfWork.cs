using Manager.Infra.Persistence.DataContext;

namespace Manager.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ManagerDataContext _context;

        public UnitOfWork(ManagerDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disponsing)
        {
            if (disponsing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
