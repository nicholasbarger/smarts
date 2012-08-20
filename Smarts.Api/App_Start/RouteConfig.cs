using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Smarts.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Purpose:
            // **********************************************************************
            // To display documentation information about the API project.
            // This documentation will include information on WebAPI (REST) as well
            // as WCF service information.
            //
            // The info controllers are all MVC controllers as opposed to most other
            // controllers within the project which are API controllers.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/info/asset/
            // GET api/info/curriculum/
            // **********************************************************************
            routes.MapRoute(
                name: "DefaultInfo",
                url: "info/{action}/{id}",
                defaults: new { controller = "Info", action = "Index", id = UrlParameter.Optional }
            );

            // Purpose:
            // **********************************************************************
            // A catch all for annotated verb explict actions that do not match the
            // REST-style CRUD methods on models.  These are purely actions that
            // need to be called in a REST-style from ajax.
            //
            // Examples of this include:
            //      ProcessImportedLineItems
            //      ProspectiveCustomer (Email-only activity)
            //      ApproveAsset
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // POST api/do/ApproveAsset/
            // POST api/do/ProspectiveCustomer/
            // GET  api/do/GetSalesForceDashboardMetrics/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "DefaultActionApi",
                routeTemplate: "do/{action}/{id}",
                defaults: new { controller = "Do", id = RouteParameter.Optional }
            );

            // Purpose:
            // **********************************************************************
            // To call Http Verb convention REST-style controllers.
            // These are typically CRUD style invocations of models.
            //
            // Many of these controllers read cookie information from context to
            // help filter actions or data being returned to the rights of the 
            // user within context.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET      api/asset/  (all assets)
            // GET      api/asset/1 (the asset with id = 1)
            // POST     api/asset/  (creation of a new asset)
            // PUT      api/asset/1 (update of an existing asset with id = 1)
            // DELETE   api/asset/1 (delete an existing asset with id = 1)
            // **********************************************************************
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}