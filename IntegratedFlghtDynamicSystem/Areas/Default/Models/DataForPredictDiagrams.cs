using System.Collections.Generic;
using System.Linq;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    public class DataForPredictDiagrams : IVizualizeble
    {

        //public List<DiagramData> GetData(List<BalVector> vectors)
        //{
        //    var orbitElements = vectors.Select(balVector => new OrbitElementsCLI(balVector)).ToList();
        //    var points = orbitElements.Select(element =>
        //    {
        //        element.GetElements();
        //        return new PointsToDiagram {XValue = element.Vitok, YValue = element.I*180/3.141519};
        //    }).ToList();
        //    for (int i = 0; i < vectors.Count; i++)
        //    {
        //        points[i].XValue = vectors[i].Vitok;
        //    }
        //    var result = new List<DiagramData>();
        //    if (_operationPredict.PredictTaskViewModel.GrafICircle)
        //    {
        //        result.Add(new DiagramData{Points = points});
        //    }
        //    return result;

        //}

        public DiagramData GetData(double[] xValues, double[] yValues)
        {
            var result = new DiagramData();
            for (int i = 0; i < xValues.Length; i++)
            {
                var points = new PointsToDiagram
                {
                    XValue = xValues[i],
                    YValue = yValues[i]
                };
                result.Points.Add(points);
            }
            return result;
        }
    }
}