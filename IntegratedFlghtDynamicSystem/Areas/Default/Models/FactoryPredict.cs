using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Models.DataTools;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    public class FactoryPredict : IFactory
    {
        private readonly PredictTaskViewModel _predictTaskViewModel;
        private readonly IUnitOfWork _unitOfWork;

        public FactoryPredict(PredictTaskViewModel predictTaskViewModel, IUnitOfWork unitOfWork)
        {
            _predictTaskViewModel = predictTaskViewModel;
            _unitOfWork = unitOfWork;
        }

        public IOperation CreateOperation()
        {
            return new OperationPredict(_predictTaskViewModel, _unitOfWork);
        }
    }
}