using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using IntegratedFlghtDynamicSystem.Areas.Default.Controllers;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Tests.Fake;
using NUnit.Framework;
using OrbitElementsCalc;

namespace IntegratedFlghtDynamicSystem.Tests.Controllers
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class ISSControllerTest
    {
        private ISSController _controller;
        private SessionStateItemCollection _sessionItems;
        private PredictTaskViewModel _predictViewModel;

        [SetUp]
        public void Setup()
        {
            //init
            _controller = DependencyResolver.Current.GetService<ISSController>();
            _sessionItems = new SessionStateItemCollection();
            _controller.ControllerContext = new FakeControllerContext(_controller, _sessionItems);
            _predictViewModel = new PredictTaskViewModel
            {
                Circle = 0,
                DateTimePredict = DateTime.MinValue,
                Graf = false,
                GrafICircle = false,
                GrafUt = false,
                IdNu = 3,
                U = -1
            };
        }

        [Test]
        public void Predict_Calculate_ResultError()
        {

            var result = _controller.Predict(_predictViewModel);
            Assert.That(result.Result.ViewBag.Error, Is.StringStarting("Операция прогноза положения КА завершилась с ошибкой:"));
        }



        [Test]
        public void Predict_CalculateToCircle_ResultIsString()
        {
            _predictViewModel.Circle = 2900;
            var result = _controller.Predict(_predictViewModel);

            Assert.That(result, Is.InstanceOf<Task<PartialViewResult>>());
            Assert.That(result.Result.ViewBag.OperationName, Is.EqualTo("Операция прогноза положения КА завершилась успешно!"));

        }


        [Test]
        public void Predict_CalculateToDateTime_ResultIsString()
        {
            _predictViewModel.Circle = 0;
            _predictViewModel.DateTimePredict = new DateTime(2014, 2, 1, 1, 2, 3);
            var result = _controller.Predict(_predictViewModel);

            Assert.That(result, Is.InstanceOf<Task<PartialViewResult>>());
            Assert.That(result.Result.ViewBag.OperationName, Is.EqualTo("Операция прогноза положения КА завершилась успешно!"));
        }

        [Test]
        public void Predict_CalculateToU_ResultIsString()
        {
            _predictViewModel.Circle = 0;
            _predictViewModel.U = 15;
            var result = _controller.Predict(_predictViewModel);
            Assert.That(result.Result.ViewBag.OperationName, Is.EqualTo("Операция прогноза положения КА завершилась успешно!"));
            _predictViewModel.U = 400;
            var resultError = _controller.Predict(_predictViewModel);
            Assert.That(resultError.Result.ViewBag.Error, Is.StringStarting("Операция прогноза положения КА завершилась с ошибкой:"));
        }

        [Test]
        public void Predict_CalculateWithFiLambdaGraf_ResulNotNull()
        {
            ResetPredictViewModelValues();
            _predictViewModel.Circle = 2900;
            _predictViewModel.GrafICircle = true;
            var result = _controller.Predict(_predictViewModel);

            Assert.That(result, Is.InstanceOf<Task<PartialViewResult>>());
            Assert.That(result.Result.ViewBag.OperationName, Is.EqualTo("Операция прогноза положения КА завершилась успешно!"));
            Assert.That(_controller.DiagramDatas.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Predict_CalculateWithTwoGrafics()
        {
            ResetPredictViewModelValues();
            _predictViewModel.Circle = 2900;
            _predictViewModel.GrafICircle = true;
            _predictViewModel.GrafUt = true;
            var result = _controller.Predict(_predictViewModel);

            Assert.That(result, Is.InstanceOf<Task<PartialViewResult>>());
            Assert.That(result.Result.ViewBag.OperationName, Is.EqualTo("Операция прогноза положения КА завершилась успешно!"));
            Assert.That(_controller.DiagramDatas.Count, Is.EqualTo(2));
        }


        [Test]
        public void CalculateOrbitElements_Get_ResNotNull()
        {
            var resut = (OrbitElementsCLI)(_controller.CalculateOrbitElements(1).Model);
            Assert.That(resut.I, Is.GreaterThan(0));
        }

        [Test]
        public void ISSInitialData_GetModeId_IdNotNull()
        {

            //act
            var result = _controller.ISSInitialData(1);

            //assert
            Assert.IsInstanceOf<SpacecraftViewModel>(((ViewResult)result).Model);
            var resultId = ((SpacecraftViewModel)((ViewResult)result).Model).SpacecraftNumber;

            Assert.That(resultId, Is.Not.Null.And.EqualTo(1));
        }

        private void ResetPredictViewModelValues()
        {
            _predictViewModel.Circle = 0;
            _predictViewModel.DateTimePredict = DateTime.MinValue;
            _predictViewModel.U = -1;
            _predictViewModel.GrafICircle = false;
            _predictViewModel.GrafUt = false;
            _predictViewModel.Graf = false;
        }

    }
}
