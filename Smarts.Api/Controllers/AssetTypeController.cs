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

namespace Smarts.Api.Controllers
{
    public class AssetTypeController : ApiController
    {
        private SmartsDbContext db;
        private Guid contributor;

        public AssetTypeController()
        {
            // initialize the db context
            db = new SmartsDbContext();
            
            // get cookie from requestor if applicable
            var cookie = HttpContext.Current.Request.Cookies["userid"];
            if(cookie != null)
            {
                contributor = new Guid(cookie.Value);
            }
        }

        // GET api/assettype
        // Examples: 
        //      api/assettype/     Retrieve list of all asset types (unfiltered)
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<AssetType>>();

            try
            {
                // Get list of asset types, using queries to ensure consistency of includes
                List<AssetType> assetTypes = null;
                using (var queries = new AssetTypeQueries(db))
                {
                    assetTypes = queries.GetQuery().ToList();
                }

                // Check if null to add error
                if (assetTypes == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = assetTypes;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }
    }
}
