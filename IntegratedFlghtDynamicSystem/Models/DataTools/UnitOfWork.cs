using System;

namespace IntegratedFlghtDynamicSystem.Models.DataTools
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        
        public UnitOfWork()
        {
            _context = new ISFDSEntities();
        }

        private readonly ISFDSEntities _context;
        private GenericRepository<SpacecraftInitialData> _spacecraftInfoRepository;
        private GenericRepository<NU> _nuRepository;

        public GenericRepository<SpacecraftInitialData> SpacecraftInfoRepository
        {
            get { return _spacecraftInfoRepository ?? (_spacecraftInfoRepository = new GenericRepository<SpacecraftInitialData>(_context)); }
        }

        public GenericRepository<NU> NuRepository
        {
            get { return _nuRepository ?? (_nuRepository = new GenericRepository<NU>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}