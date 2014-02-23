using System;
using System.Collections.Generic;
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

        public List<SpacecraftInitialData> SpacecraftInitialDatas { get; set; }
        public GenericRepository<SpacecraftInitialData> SpCRepository { get; set; } 

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
        }

    }
   
}
