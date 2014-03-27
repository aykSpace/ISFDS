using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        GenericRepository<SpacecraftsEngine> SpacecraftEnginesRepository
        {
            get;
        }

        void Save();

    }
}
