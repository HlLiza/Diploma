using Network.DAL.EFModel;
using System;

namespace Network.DAL.Repositories
{
    public class RepositoryBase : IDisposable
    {
        protected InstitutNetworkContext _context;

        private bool disposed = false;

        public RepositoryBase() { }

        public RepositoryBase(InstitutNetworkContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposed)
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
