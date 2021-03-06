﻿using System;
using System.Collections.Specialized;
using System.Globalization;
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
        public void ISSIndex_GetModeId_IdNotNull()
        {

            //act
            var result = _controller.Index();

            //assert
            Assert.IsInstanceOf<SpacecraftViewModel>(((ViewResult)result).Model);
            var resultId = ((SpacecraftViewModel)((ViewResult)result).Model).SpacecraftNumber;

            Assert.That(resultId, Is.Not.Null.And.EqualTo(1));
        }

        [Test]
        public void MassInerCharacteristic_Get_ResutSesssionIsEqual2()
        {
            _controller.Session["SpCrId"] = 1;
            _controller.MassInertialCharachteristic();
            var result = _controller.Session["MicId"];
            

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SetCurrentMic_Execute_ResultIsRedirectToAction()
        {
            _controller.Session["SpCrId"] = 1;
            var result = _controller.SetCurrentMic(2);
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        }

        [Test]
        public void GetMic_Execute_ResulHasProp()
        {
            var result = (JsonResult)_controller.GetMic(2);
            Assert.That(result.Data, Has.Property("ID_MIC"));
        }

        //[Test]
        //public void AddMic_Execute_ResultIsSuccess()
        //{
        //    var controller = DependencyResolver.Current.GetService<MassInerCharacteristicController>();
        //    var formPar = new NameValueCollection { { "DateOfID", DateTime.Now.ToString("d", CultureInfo.InvariantCulture) } };
        //    controller.ControllerContext = new FakeControllerContext(_controller, "a.y.kutomanov@gmail.com", null, formPar, null, null, _sessionItems);
        //    controller.Session["SpCrId"] = 1;
        //    var micViwModel = new MassInertialCharactViewModel
        //    {
        //        ID_MIC = 4,
        //        Mass = 333,
        //        XT = -1,
        //        YT = -2,
        //        ZT = -3,
        //        DateOfID = DateTime.Now,
        //        Liter = "w",
        //        Sbal = 0.123,
        //        Comment = "from test"
        //    };
        //    var result = controller.AddMic(micViwModel);



        //    Assert.That(result, Is.Not.Null);
        //}

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
