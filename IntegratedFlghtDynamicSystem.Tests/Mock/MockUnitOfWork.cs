using IntegratedFlghtDynamicSystem.Models.DataTools;
using Moq;

namespace IntegratedFlghtDynamicSystem.Tests.Mock
{
    public partial class MockUnitOfWork : Mock<IUnitOfWork>
    {
        public MockUnitOfWork(MockBehavior mockBehavior = MockBehavior.Strict)
            : base(mockBehavior)
        {
            GenerateNUs();
            GenerateSpCrItitialData();
            GenerateSpcrComonData();
            GenerateMicData();
            GenerateSpacecraft();
            Setup(p => p.Save()).Verifiable("fail save");
            Setup(p => p.OracleServer).Returns(false);
        }
    }
}
