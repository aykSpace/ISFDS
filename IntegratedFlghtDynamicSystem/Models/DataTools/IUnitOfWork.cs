namespace IntegratedFlghtDynamicSystem.Models.DataTools
{
    public interface IUnitOfWork
    {
        GenericRepository<SpacecraftInitialData> SpacecraftInfoRepository
        {
            get;
        }

        GenericRepository<NU> NuRepository
        {
            get;
        }

        GenericRepository<MassInertialCharacteristic> MicRepository
        {
            get;
        }

        GenericRepository<Engine> EngineRepository
        {
            get;
        }

        GenericRepository<SpaceсraftCommonData> SpacecraftCommonDataRepository
        {
            get;
        }

        GenericRepository<SpacecraftsEngines> SpacecraftEnginesRepository
        {
            get;
        }
        bool OracleServer { get; set; }

        IRepository<NU> OracleNuData
        {
            get; set;
        } 

        void Save();

    }
}
