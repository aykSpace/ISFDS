using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    public class Client
    {
        private readonly IOperation _operation;

        public Client(IFactory factory)
        {
            _operation = factory.CreateOperation();
        }

        /// <summary>
        /// Запускает перацию асинхронно
        /// </summary>
        /// <returns>Возвращает горячую задачу</returns>
        public Task<List<DiagramData>> RunAsync()
        {
            return _operation.StartOperationAsycnc();
        }
    }
}