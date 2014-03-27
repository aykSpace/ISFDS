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
        private GenericRepository<MassInertialCharacteristic> _micRepository;
        private GenericRepository<Engine> _engineRepository;
        private GenericRepository<SpaceсraftCommonData> _spcraftCommonDataRepository;
        private GenericRepository<SpacecraftsEngine> _spcraftEnginesRepository; 

        public GenericRepository<SpacecraftInitialData> SpacecraftInfoRepository
        {
            get { return _spacecraftInfoRepository ?? (_spacecraftInfoRepository = new GenericRepository<SpacecraftInitialData>(_context)); }
        }

        public GenericRepository<SpaceсraftCommonData> SpacecraftCommonDataRepository
        {
            get { return _spcraftCommonDataRepository ?? (_spcraftCommonDataRepository = new GenericRepository<SpaceсraftCommonData>(_context)); }
        }

        public GenericRepository<SpacecraftsEngine> SpacecraftEnginesRepository
        {
            get
            {
                return _spcraftEnginesRepository ??
                       (_spcraftEnginesRepository = new GenericRepository<SpacecraftsEngine>(_context));
            }
        } 


        public GenericRepository<NU> NuRepository
        {
            get { return _nuRepository ?? (_nuRepository = new GenericRepository<NU>(_context)); }
        }

        public GenericRepository<MassInertialCharacteristic> MicRepository
        {
            get { return _micRepository ?? (_micRepository = new GenericRepository<MassInertialCharacteristic>(_context)); }
        }

        public GenericRepository<Engine> EngineRepository
        {
            get { return _engineRepository ?? (_engineRepository = new GenericRepository<Engine>(_context)); }
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