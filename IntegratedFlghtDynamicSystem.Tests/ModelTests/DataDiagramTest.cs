using IntegratedFlghtDynamicSystem.Areas.Default.Models;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.ModelTests
{
    [TestFixture]
    public class DataDiagramTest
    {
        [Test]
        public void IVizualizeble_GetData_ResultIsDiagramData()
        {
            IVizualizeble dataForPredictDiagram = new DataForPredictDiagrams();
            var xValues = new[] {1.0, 2.0, 3.0, 4.0};
            var yValues = new[] { 10.0, 20.0, 30.7, 70.9 };
            var result = dataForPredictDiagram.GetData(xValues, yValues);
            Assert.That((object) result, Is.InstanceOf<DiagramData>());
        }
    }
}
