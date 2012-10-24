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
            // To get activities for a specific asset
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/activity/asset/5/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "GetActivityByAsset",
                routeTemplate: "asset/{id}/activity/",
                defaults: new { controller = "Activity", action = "GetByAsset" }
            );

            // Purpose
            // **********************************************************************
            // To comment on a specific asset
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // POST api/asset/comment/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "CreateAssetCommentOverride",
                routeTemplate: "asset/comment/{id}&{comment}",
                defaults: new { controller = "Asset", action = "Comment" }
            );

            // Purpose
            // **********************************************************************
            // To get the comments for a specified Asset
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/asset/1001/comments/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "AssetCommentsOverride",
                routeTemplate: "asset/{id}/comments/",
                defaults: new { controller = "Asset", action = "GetComments" }
            );

            // Purpose
            // **********************************************************************
            // To get the subjects for a specified Asset
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/asset/1001/subjects/
            // **********************************************************************
            routes.MapHttpRoute(
                name: "AssetSubjectsOverride",
                routeTemplate: "asset/{id}/subjects/",
                defaults: new { controller = "Asset", action = "GetSubjects" }
            );

            // Purpose
            // **********************************************************************
            // To get a list of assets by specified subject (hashtag).
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/asset/subject/computerscience
            // **********************************************************************
            routes.MapHttpRoute(
                name: "AssetBySubjectOverride",
                routeTemplate: "asset/subject/{hashtag}",
                defaults: new { controller = "Asset", action = "GetBySubject" }
            );

            // Purpose
            // **********************************************************************
            // Because I have two Get Actions with the same signature I need to
            // explicitly define which route goes to which action.  This is a bummer
            // and I hope is something that can be done in a more elegant way.
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // GET api/asset/1001
            // **********************************************************************
            routes.MapHttpRoute(
                name: "AssetGetByIdExplict",
                routeTemplate: "asset/{id}",
                defaults: new { controller = "Asset", action = "Get" }
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
            //      ApproveAsset
            // **********************************************************************
            // Usage:
            // **********************************************************************
            // POST api/do/ApproveAsset/
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
            // GET api/asset/search/bio
            // GET api/subject/search/science
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

            #endregion
        }
    }
}