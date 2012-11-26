using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Smarts.Api.Db;
using Smarts.Api.BusinessLogic;
using Smarts.Api.Models;
using Smarts.Api.Utilities;
using Smarts.Api.AppLogic;

namespace Smarts.Api.Controllers
{
    public class ActivityController : ApiController
    {
        private ActivityAppLogic logic;
        private Guid contributor;

        public ActivityController()
        {   
            // initialize logic
            logic = new ActivityAppLogic();

            // assign contributor
            var utility = new ControllerUtilities();
            contributor = utility.GetWebUserGuidFromCookies();
        }

        #region GET Actions

        /// <summary>
        /// Retrieve list of activities (unfiltered)
        ///     USAGE: GET api/activity    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Activity>>();

            try
            {
                // Get full list
                payload = new HttpResponsePayload<List<Activity>>(logic.Get());
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        /// <summary>
        /// Retrieve list of activities by specified asset
        ///     USAGE: GET api/activity/asset/5    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetByAsset(int id)
        {
            var payload = new HttpResponsePayload<List<Activity>>();

            try
            {
                // Get full list
                payload = new HttpResponsePayload<List<Activity>>(logic.GetByResource(id));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        #endregion
    }
}
