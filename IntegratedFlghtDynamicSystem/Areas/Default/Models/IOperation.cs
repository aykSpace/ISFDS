using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    public interface IOperation
    {
        string OperationName { get; set; }

        Task<List<DiagramData>> StartOperationAsycnc();
    }
}