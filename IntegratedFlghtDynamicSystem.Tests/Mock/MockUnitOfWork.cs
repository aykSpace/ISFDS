using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Setup(p => p.Save()).Verifiable("fail save");
        }
    }
}
