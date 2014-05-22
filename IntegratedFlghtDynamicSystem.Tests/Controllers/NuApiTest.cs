using System.Collections.Generic;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.Controllers
{
    [TestFixture]
    public class NuApiTest
    {
        private NuController _nuController;

        [SetUp]
        public void Setup()
        {
            _nuController = DependencyResolver.Current.GetService<NuController>();
        }

        [Test]
        public void GetNus_Execute_ResultIsListNuVieModel()
        {
            var result = _nuController.GetNUs();
            Assert.That(result, Is.InstanceOf<List<NuViewModel>>());
        }

        [Test]
        public void GetNu_Execute_ResultIsNuViewModel()
        {
            var result = _nuController.GetNu(2);
            Assert.That(result, Is.InstanceOf<NuViewModel>());
        }
    }
}
