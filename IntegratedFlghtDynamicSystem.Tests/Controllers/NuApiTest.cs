using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using IntegratedFlghtDynamicSystem.Areas.Default.Controllers;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Tests.Fake;
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
