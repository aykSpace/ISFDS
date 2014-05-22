using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BalPredictCLR;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using OrbitElementsCalc;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Models
{
    internal class OperationPredict : IOperation
    {
        private readonly PredictTaskViewModel _predictTaskViewModel;
        private readonly IUnitOfWork _unitOfWork;

        public OperationPredict(PredictTaskViewModel predictTaskViewModel, IUnitOfWork unitOfWork)
        {
            _predictTaskViewModel = predictTaskViewModel;
            _unitOfWork = unitOfWork;
        }

        public string OperationName { get; set; }

        public PredictTaskViewModel PredictTaskViewModel
        {
            get { return _predictTaskViewModel; }
        }

        public Task<List<DiagramData>> StartOperationAsycnc()
        {
            var func = new Func<List<DiagramData>>(CalculatePredict);
            return Task.Run(func);
        }

        /// <summary>
        /// Вычисление прогноза положения КА по заданным условиям
        /// </summary>
        /// <exception cref="Exception()"></exception>
        /// <returns></returns>
        private List<DiagramData> CalculatePredict()
        {
            var grafDatas = new List<DiagramData>();
            try
            {
                NU nu = _unitOfWork.OracleServer ? _unitOfWork.OracleNuData.GetById(_predictTaskViewModel.IdNu) : _unitOfWork.NuRepository.GetById(_predictTaskViewModel.IdNu);
                var vector = new BalVector((uint) nu.Vitok, nu.t, nu.X, nu.Y, nu.Z, nu.VX, nu.VY, nu.VZ, nu.Sbal,
                    nu.DateNU);
                BalVector.ap = 110;
                BalVector.f = 16;
                var resultVectors = new List<BalVector>();
                if (_predictTaskViewModel.Circle > 0)
                {
                    resultVectors = vector.Prognoz((uint) _predictTaskViewModel.Circle);
                }
                else if (_predictTaskViewModel.DateTimePredict > DateTime.MinValue)
                {
                    resultVectors.Add(vector.Prognoz(_predictTaskViewModel.DateTimePredict));
                }
                else if (_predictTaskViewModel.U >= 0)
                {
                    resultVectors.Add(vector.Prognoz(_predictTaskViewModel.U));
                }
                else
                {
                    throw new ArgumentException("Задание на расчет выдано не верно!");
                }

                if (_predictTaskViewModel.GrafUt || _predictTaskViewModel.Graf || _predictTaskViewModel.GrafICircle)
                {
                    var listOfOrbitElements = new List<OrbitElementsCLI>();
                    var vizualizeData = new DataForPredictDiagrams();
                    foreach (var balVector in resultVectors)
                    {
                        var orbitElements = new OrbitElementsCLI(balVector);
                        orbitElements.GetElements();
                        listOfOrbitElements.Add(orbitElements);
                    }
                    if (_predictTaskViewModel.GrafICircle)
                    {
                        var count = resultVectors.Count;
                        var xDoubles = new double[count];
                        var yDoubles = new double[count];
                        for (int i = 0; i < resultVectors.Count; i++)
                        {
                            xDoubles.SetValue(resultVectors[i].Vitok, i);
                            yDoubles.SetValue(listOfOrbitElements[i].I *180/3.141519, i);
                        }
                        grafDatas.Add(vizualizeData.GetData(xDoubles, yDoubles)); 
                    }
                    if (_predictTaskViewModel.GrafUt)
                    {
                        var count = resultVectors.Count;
                        var xDoubles = new double[count];
                        var yDoubles = new double[count];
                        for (int i = 0; i < resultVectors.Count; i++)
                        {
                            xDoubles.SetValue(resultVectors[i].Vitok, i);
                            yDoubles.SetValue(listOfOrbitElements[i].A,i);
                        }
                        grafDatas.Add(vizualizeData.GetData(xDoubles, yDoubles));
                    }
                }
                
            }
            catch (Exception exception)
            {
                OperationName = String.Format("Операция прогноза положения КА завершилась с ошибкой: {0}",
                    exception.Message);
                throw new Exception(OperationName);

            }
            return grafDatas;
        }
    }
}
