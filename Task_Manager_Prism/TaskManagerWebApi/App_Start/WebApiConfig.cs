using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Task_Manager_Prism.DatabaseAccess;
using Unity;
using Unity.Lifetime;

namespace TaskManagerWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

           
            var container = new UnityContainer();
            container.RegisterType<IDatabaseAccessService, DatabaseAccessService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
