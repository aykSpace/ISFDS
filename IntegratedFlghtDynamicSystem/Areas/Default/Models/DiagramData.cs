using System.Collections.Generic;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    public class DiagramData
    {
        public DiagramData()
        {
            Points = new List<PointsToDiagram>();
        }

        public List<PointsToDiagram> Points { get; set; }
    }
}