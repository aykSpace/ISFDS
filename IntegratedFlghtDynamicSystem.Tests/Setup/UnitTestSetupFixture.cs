using System.IO;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Global;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using IntegratedFlghtDynamicSystem.Tests.Mock;
using IntegratedFlghtDynamicSystem.Tests.Tools;
using Ninject;
using NUnit.Framework;

namespace IntegratedFlghtDynamicSystem.Tests.Controllers
{
    [SetUpFixture]
    public class UnitTestSetupFixture
    {

        protected static string Sandbox = "../../Sandbox";

        [SetUp]
        public virtual void Setup()
        {
            InitKernel();
        }

        protected virtual IKernel InitKernel()
        {
            var kernel = new StandardKernel();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            InitConfig(kernel);
            InitRepository(kernel);
            return kernel;
        }

        protected virtual void InitRepository(StandardKernel kernel)
        {
            kernel.Bind<MockUnitOfWork>().To<MockUnitOfWork>().InThreadScope();
            kernel.Bind<IUnitOfWork>().ToMethod(p => kernel.Get<MockUnitOfWork>().Object);
        }

        protected virtual void InitConfig(StandardKernel kernel)
        {
            var fullPath = new FileInfo(Sandbox + "/Web.config").FullName;
            kernel.Bind<IConfig>().ToMethod(c => new TestConfig(fullPath));
        }

    }
}
