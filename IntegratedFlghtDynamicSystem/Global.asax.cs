using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IntegratedFlghtDynamicSystem.Areas.Admin;
using IntegratedFlghtDynamicSystem.Areas.Default;
using IntegratedFlghtDynamicSystem.Extensions;

namespace IntegratedFlghtDynamicSystem
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            ViewEngines.Engines.Clear();
            var engine = new ExtendedRazorViewEngine();
            engine.AddPartialViewLocationFormat("~/Areas/Default/Views/SpacecraftBase/{0}.cshtml");
            engine.AddViewLocationFormat("~/Areas/Default/Views/SpacecraftBase/{0}.cshtml");

            ViewEngines.Engines.Add(engine);

            var adminArea = new AdminAreaRegistration();
            var adminAreaContext = new AreaRegistrationContext(adminArea.AreaName, RouteTable.Routes);
            adminArea.RegisterArea(adminAreaContext);

            var defaultArea = new DefaultAreaRegistration();
            var defaultAreaContext = new AreaRegistrationContext(defaultArea.AreaName, RouteTable.Routes);
            defaultArea.RegisterArea(defaultAreaContext);


            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();
             var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal(lastException);
        }
    }
}