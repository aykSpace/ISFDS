using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Admin.Controllers;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.Controllers
{
    [TestFixture]
    public class SpacecraftApiTest
    {
        private SpacecraftController _spacecraftController;

        [SetUp]
        public void Setup()
        {
            _spacecraftController = DependencyResolver.Current.GetService<SpacecraftController>();
            _spacecraftController.Request = new HttpRequestMessage();
            _spacecraftController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [Test]
        public void DeleteSpCr_Excute_SpCrafaftCountMoreThenPrevious()
        {
            var countBeforeDelete = _spacecraftController.GetSpacecraftInitialDatas().Count();
            var actRes = _spacecraftController.DeleteSpacecraftInitialData(1);
            var countAfterDelete = _spacecraftController.GetSpacecraftInitialDatas().Count();

            Assert.That(countBeforeDelete - countAfterDelete, Is.EqualTo(1));
        }

    }
}
