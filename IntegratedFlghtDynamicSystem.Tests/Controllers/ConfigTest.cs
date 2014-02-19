using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Global;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.Controllers
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void ConnectionString_ExistString_Exist()
        {
            var config = DependencyResolver.Current.GetService<IConfig>();
            var conString = config.ConnectionStrings("ISFDSDB");
            Assert.IsNotNull(conString);
        }
    }
}
