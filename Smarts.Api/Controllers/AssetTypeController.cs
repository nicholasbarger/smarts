using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Smarts.Api.Db;
using Smarts.Api.Models;
using Smarts.Api.Utilities;
using Smarts.Api.AppLogic;

namespace Smarts.Api.Controllers
{
    public class AssetTypeController : ApiController
    {
        private AssetTypeAppLogic logic;
        private Guid contributor;

        public AssetTypeController()
        {   
            // initialize logic
            logic = new AssetTypeAppLogic();

            // assign contributor
            var utility = new ControllerUtilities();
            contributor = utility.GetWebUserGuidFromCookies();
        }

        #region GET Actions

        /// <summary>
        /// Retrieve list of all asset types (unfiltered)
        ///     Usage: GET api/assettype/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<AssetType>>();

            try
            {
                // Get full list
                payload = new HttpResponsePayload<List<AssetType>>(logic.Get());
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
