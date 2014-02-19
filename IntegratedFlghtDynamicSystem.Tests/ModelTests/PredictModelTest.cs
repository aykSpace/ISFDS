using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Areas.Default.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.ModelTests
{
    [TestFixture]
    public class PredictModelTest
    {
        private PredictTaskViewModel _predictTaskViewModel;
        private FactoryPredict _predictFactory;
        private Client _client;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _predictTaskViewModel = new PredictTaskViewModel();
            _unitOfWork = new UnitOfWork();
            _predictFactory = new FactoryPredict(_predictTaskViewModel, _unitOfWork);
            _client = new Client(_predictFactory);
        }
        
        [Test]
        public void Client_CreateClient_IsNotNull()
        {
            
            Assert.That(_client, Is.InstanceOf<Client>());
        }
    }
}
