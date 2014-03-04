using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using Moq;

namespace IntegratedFlghtDynamicSystem.Tests.Mock
{
    public partial class MockUnitOfWork
    {
        public  List<NU> Nus { get; set; }
        public GenericRepository<NU> NuRepository { get; set; }  

        public void GenerateNUs()
        {
            NuRepository = new GenericRepository<NU>();
            Nus = new List<NU>();
            var nu1 = new NU
            {
                Comment = "mock NU",
                DateNU = new DateTime(2014, 1, 24, 6, 54, 00),
                ID_NU = 1,
                Modification = 1,
                N_NU = 170,
                Sbal = 0.0459d,
                SpacecraftInitialData_ID = 1,
                t = 24840,
                Vitok = 2898,
                X = 424.78293230,
                Y = -6774.344163,
                Z = 299.88650640,
                VX = 4.23755698,
                VY = 0.54054299,
                VZ = 6.00138935
            };
            Nus.Add(nu1);

            Setup(p => p.NuRepository).Returns(NuRepository);
            Setup(p => p.NuRepository.Get()).Returns(Nus.AsEnumerable());
            Setup(p => p.NuRepository.GetById(It.IsAny<int>())).Returns(Nus.First());
        }

        public List<SpacecraftInitialData> SpacecraftInitialDatas { get; set; }
        public GenericRepository<SpacecraftInitialData> SpCRepository { get; set; } 

        public void GenerateSpCrItitialData()
        {
            SpCRepository = new GenericRepository<SpacecraftInitialData>();
            SpacecraftInitialDatas = new List<SpacecraftInitialData>();
            var spCraftInData = new SpacecraftInitialData
            {
                SpacecraftInitDataId = 1,
                SpacecraftNumber = 1,
                InternationalNumber = "1-345",
                CCSANumber = null,
                NORADNumber = null,
                SpacecraftName = "ISS",
                SpacecraftType = "Space station",
                OrbitType = "low orbit",
                ReboostBlockType = null,
                Launcher = "Proton",
                DateOfLaunch = DateTime.Now,
                MassInerCharacteristicId = 2,
                EngineID = 0,
                Comments = "mock ISS",
                NUs = null
            };
            SpacecraftInitialDatas.Add(spCraftInData);

            Setup(p => p.SpacecraftInfoRepository).Returns(SpCRepository);
            Setup(p => p.SpacecraftInfoRepository.Get()).Returns(SpacecraftInitialDatas.AsEnumerable());
            Setup(p => p.SpacecraftInfoRepository.GetById(It.IsAny<int>())).Returns((int id) => SpacecraftInitialDatas.FirstOrDefault(p => p.SpacecraftInitDataId == id));
            Setup(p => p.SpacecraftInfoRepository.Update(It.IsAny<SpacecraftInitialData>())).Verifiable("fail");
        }

        public List<SpaceсraftCommonData> SpacecraftCommonData { get; set; }
        public GenericRepository<SpaceсraftCommonData> SpacecraftCommonRepository { get; set; }

        public void GenerateSpcrComonData()
        {
            SpacecraftCommonRepository = new GenericRepository<SpaceсraftCommonData>();
            SpacecraftCommonData = new List<SpaceсraftCommonData>();

            var spcrafrtCommonData = new SpaceсraftCommonData
            {
                SpacecraftCommonDataId = 1,
                SpacecraftInitDataId = 1,
                MIC_Id = 1,
                EngineId = 0
            };

            var spcrafrtCommonData1 = new SpaceсraftCommonData
            {
                SpacecraftCommonDataId = 2,
                SpacecraftInitDataId = 1,
                MIC_Id = 2,
                EngineId = 0
            };

            SpacecraftCommonData.Add(spcrafrtCommonData);
            SpacecraftCommonData.Add(spcrafrtCommonData1);

            Setup(p => p.SpacecraftCommonDataRepository).Returns(SpacecraftCommonRepository);
            Setup(p => p.SpacecraftCommonDataRepository.Get()).Returns(SpacecraftCommonData.AsEnumerable);
            Setup(p => p.SpacecraftCommonDataRepository.GetById(It.IsAny<int>()))
                .Returns((int id) => SpacecraftCommonData.FirstOrDefault(p => p.SpacecraftCommonDataId == id));
            Setup(p => p.SpacecraftCommonDataRepository.Insert(It.IsAny<SpaceсraftCommonData>())).Verifiable();
        }

        public List<MassInertialCharacteristic> MicData { get; set; }
        public GenericRepository<MassInertialCharacteristic> MassInerRepository { get; set; }

        public void GenerateMicData()
        {
            MicData = new List<MassInertialCharacteristic>();
            MassInerRepository = new GenericRepository<MassInertialCharacteristic>();

            var mic = new MassInertialCharacteristic
            {
                ID_MIC = 1,
                ID_Aero = 0,
                Mass = 40000,
                MassOfCommonBay = 0,
                MassOfDeorbitSpCr = 1370,
                Sbal = 0.0085,
                XT = 0.034,
                YT = -0.2,
                ZT = 0,
                Liter = "F",
                DateOfID = DateTime.Now,
                Comment = "Mock mic1 for ISS"
            };
            var mic1 = new MassInertialCharacteristic
            {
                ID_MIC = 2,
                ID_Aero = 0,
                Mass = 50000,
                MassOfCommonBay = 1234,
                MassOfDeorbitSpCr = 1491,
                Sbal = 0.0035,
                XT = 0.05444,
                YT = -0.234,
                ZT = 0.233,
                Liter = "K",
                DateOfID = DateTime.Now,
                Comment = "Mock mic2 for ISS"
            };

            MicData.Add(mic);
            MicData.Add(mic1);

            Setup(p => p.MicRepository).Returns(MassInerRepository);
            Setup(p => p.MicRepository.Get()).Returns(MicData.AsEnumerable);
            Setup(p => p.MicRepository.GetById(It.IsAny<int>()))
                .Returns((int id) => MicData.FirstOrDefault(p => p.ID_MIC == id));
            Setup(p => p.MicRepository.Insert(It.IsAny<MassInertialCharacteristic>())).Verifiable();
        }


    }
   
}
