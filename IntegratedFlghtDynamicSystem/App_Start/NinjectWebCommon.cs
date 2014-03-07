using System.Web.Http;
using IntegratedFlghtDynamicSystem.Mappers;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using IntegratedFlghtDynamicSystem.Ninject;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivator.PreApplicationStartMethod(typeof(IntegratedFlghtDynamicSystem.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(IntegratedFlghtDynamicSystem.App_Start.NinjectWebCommon), "Stop")]

namespace IntegratedFlghtDynamicSystem.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRepository<SpacecraftInitialData>>().To<GenericRepository<SpacecraftInitialData>>().InRequestScope();
            kernel.Bind<IRepository<NU>>().To<GenericRepository<NU>>().InRequestScope();
            kernel.Bind<IRepository<MassInertialCharacteristic>>().To<GenericRepository<MassInertialCharacteristic>>().InRequestScope();
            kernel.Bind<IRepository<Engine>>().To<GenericRepository<Engine>>().InRequestScope();
            kernel.Bind<IRepository<SpaceñraftCommonData>>().To<GenericRepository<SpaceñraftCommonData>>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IMapper>().To<SpacecraftMapper>().InSingletonScope();
        }
    }
}