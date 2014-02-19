namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    public interface IVizualizeble
    {
        DiagramData GetData(double[] xValues, double[] yValues);
    }
}