using GetADoctor.Web.Infrastructure.Mapping;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GetADoctor.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfiguration.RegisterMaps();
        }
    }
}
