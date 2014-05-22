﻿using System.Web.Http.Dependencies;
using Ninject;

namespace IntegratedFlghtDynamicSystem.Ninject
{
    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }
}