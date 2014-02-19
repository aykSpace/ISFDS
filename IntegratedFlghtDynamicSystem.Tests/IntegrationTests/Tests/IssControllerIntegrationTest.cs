using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using IntegratedFlghtDynamicSystem.Areas.Default.Controllers;
using IntegratedFlghtDynamicSystem.Models.Grid;
using IntegratedFlghtDynamicSystem.Tests.Fake;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.IntegrationTests
{
   
    public class IssControllerIntegrationTest
    {
        
        public void Index_GetData_NotNull()
        {
            var controller = DependencyResolver.Current.GetService<ISSController>();
            var sessionItems = new SessionStateItemCollection();
            sessionItems["item1"] = "foo";
            controller.ControllerContext = new FakeControllerContext(controller, sessionItems);
            var result = controller.GetData(new GridSettings());

            Assert.IsInstanceOf<JsonResult>(result);
        }
    }
}
