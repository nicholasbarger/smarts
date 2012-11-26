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

            #region Controller-specific Routes

            // Purpose
            // **********************************************************************
            // To complete an educational resource.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // POST api/resource/5/complete/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "CompleteResource",
                routeTemplate: "resource/{id}/complete",
                defaults: new { controller = "Resource", action = "Complete" }
            );

            // Purpose
            // **********************************************************************
            // To update a completed educational resource (primarily voting importance 
            // and difficulty).
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // PUT api/resource/5/complete/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "CompleteUpdateResource",
                routeTemplate: "resource/{id}/complete",
                defaults: new { controller = "Resource", action = "CompleteUpdate" }
            );

            // Purpose
            // **********************************************************************
            // To get activities for a specific educational resource.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/resource/5/activities/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "GetActivityByResource",
                routeTemplate: "resource/{id}/activities/",
                defaults: new { controller = "Activity", action = "GetByResource" }
            );

            // Purpose
            // **********************************************************************
            // To comment on a specific educational resource.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // POST api/resource/comment/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "CreateResourceCommentOverride",
                routeTemplate: "resource/comment/{id}&{comment}",
                defaults: new { controller = "Resource", action = "Comment" }
            );

            // Purpose
            // **********************************************************************
            // To get the comments for a specified resource.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/resource/1001/comments/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "ResourceCommentsOverride",
                routeTemplate: "resource/{id}/comments/",
                defaults: new { controller = "Resource", action = "GetComments" }
            );

            // Purpose
            // **********************************************************************
            // To get the topics for a specified educational resource.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/asset/1001/topics/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "ResourceTopicsOverride",
                routeTemplate: "resource/{id}/topics/",
                defaults: new { controller = "Resource", action = "GetTopics" }
            );

            // Purpose
            // **********************************************************************
            // To get a list of educational resources by specified topic (tag).
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/resource/topic/computerscience
            // **********************************************************************
            routes.MapHttpRoute(
                name: "ResourceByTopicOverride",
                routeTemplate: "resource/topic/{tag}",
                defaults: new { controller = "Resource", action = "GetByTopic" }
            );

            // Purpose
            // **********************************************************************
            // Because I have two Get Actions with the same signature I need to
            // explicitly define which route goes to which action.  This is a bummer
            // and I hope is something that can be done in a more elegant way.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/resource/1001
            // **********************************************************************
            routes.MapHttpRoute(
                name: "ResourceGetByIdExplict",
                routeTemplate: "resource/{id}",
                defaults: new { controller = "Resource", action = "Get" }
            );

            routes.MapHttpRoute(
                name: "WebUserLogin",
                routeTemplate: "webuser/login/",
                defaults: new { controller = "WebUser", action = "Login" }
            );

            routes.MapHttpRoute(
                name: "WebUserLogout",
                routeTemplate: "webuser/logout/",
                defaults: new { controller = "WebUser", action = "Logout" }
            );

            #endregion

            #region Generic Routes

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
            //      ApproveResource
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // POST api/do/ApproveResource/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "DefaultActionApi",
                routeTemplate: "do/{action}/{id}",
                defaults: new { controller = "Do", id = RouteParameter.Optional }
            );

            // Purpose:
            // **********************************************************************
            // To add search functionality to the standard HttpVerbs.
            // This was intentionally designed not using the GET generic verb due to
            // some controllers requiring id lookups for a single result to use a
            // string instead of id, thereby creating a conflict between search by
            // string and lookup of id that is a string (or guid).
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/resource/search/bio
            // GET api/topic/search/science
            // GET api/user/search/john
            // **********************************************************************
            routes.MapHttpRoute(
                name: "SearchVerbOverrideApi",
                routeTemplate: "{controller}/search/{q}",
                defaults: new { action = "Search", id = RouteParameter.Optional }
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
            // GET      api/resource/  (all resources)
            // GET      api/resource/1 (the resource with id = 1)
            // POST     api/resource/  (creation of a new resource)
            // PUT      api/resource/1 (update of an existing resource with id = 1)
            // DELETE   api/resource/1 (delete an existing resource with id = 1)
            // **********************************************************************
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #endregion
        }
    }
}