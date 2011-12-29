using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SignalR;
using SignalR.Routing;
using SignalR_Demo.Hubs;
using SignalR_Demo.Models;
using Ninject;
using SignalR_Demo.Business;
using SignalR.Ninject;

namespace SignalR_Demo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");

            routes.MapConnection<CPUDataStream>("cpuDataStream", "cpuDataStream/{*operation}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // a signalR configuration to set the timeout for long-polling
            //Signaler.Instance.DefaultTimeout = TimeSpan.FromSeconds(5);

            StandardKernel kernel = new StandardKernel();

            kernel.Bind<IGameManager>()
                .To<GameManager>()
                .InSingletonScope();

            SignalR.Infrastructure.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}