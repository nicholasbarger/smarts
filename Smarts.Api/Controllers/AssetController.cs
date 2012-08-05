using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Smarts.Api.Db;
using Smarts.Api.Logic;
using Smarts.Api.Models;

namespace Smarts.Api.Controllers
{
    public class AssetController : ApiController
    {
        private SmartsDbContext db;
        private Guid contributor;

        public AssetController()
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

        // GET api/asset
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Asset>>();

            try
            {
                // Get assets, using queries to ensure consistency of includes
                List<Asset> assets = null;
                using (var queries = new AssetQueries(db))
                {
                    assets = queries.GetAssetsQuery().ToList();
                }

                // If not null, add to payload
                if (assets != null)
                {
                    payload.Data = assets;
                }
                else
                {
                    payload.Errors.Add("00002", string.Format(Resources.Errors.ERR00002, "asset"));
                }                
            }
            catch (DbEntityValidationException dbex)
            {
                // Assign errors from db
                payload.AssignDbErrors(dbex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);

                payload.Errors.Add("00000", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, payload);
            }

            // Return proper response message
            if (payload.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK, payload);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, payload);
            }
        }

        // GET api/asset/5
        public HttpResponseMessage Get(int id)
        {
            var payload = new HttpResponsePayload<Asset>();

            try
            {
                // Get asset, using queries to ensure consistency of includes
                Asset asset = null;
                using (var queries = new AssetQueries(db))
                {
                    asset = queries.GetAsset(id);
                }

                // If not null, add to payload
                if (asset != null)
                {
                    payload.Data = asset;
                }
                else
                {
                    payload.Errors.Add("00002", string.Format(Resources.Errors.ERR00002, "asset"));
                }
            }
            catch (DbEntityValidationException dbex)
            {
                // Assign errors from db
                payload.AssignDbErrors(dbex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);

                payload.Errors.Add("00000", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, payload);
            }

            // Return proper response message
            if (payload.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK, payload);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, payload);
            }
        }

        // POST api/asset
        public HttpResponseMessage Post(Asset obj)
        {
            var payload = new HttpResponsePayload<Asset>();

            try
            {
                // Prep
                var logic = new AssetLogic();
                logic.SetDefaults(ref obj);
                obj.ContributorGuid = contributor;

                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new AssetQueries(db))
                    {
                        queries.SaveAsset(ref obj);
                    }

                    // Update payload
                    payload.Data = obj;
                }
                else
                {
                    // Assign errors from validation
                    payload.AssignValidationErrors(rules.Errors);
                }
            }
            catch(DbEntityValidationException dbex)
            {
                // Assign errors from db
                payload.AssignDbErrors(dbex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);

                payload.Errors.Add("00000", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, payload);
            }

            // Return proper response message
            if (payload.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK, payload);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, payload);
            }
        }

        // PUT api/asset/5
        public HttpResponseMessage Put(int id, Asset obj)
        {
            var payload = new HttpResponsePayload<Asset>();

            try
            {
                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new AssetQueries(db))
                    {
                        queries.SaveAsset(ref obj);
                    }

                    // Update payload
                    payload.Data = obj;
                }
                else
                {
                    // Assign errors from validation
                    payload.AssignValidationErrors(rules.Errors);
                }
            }
            catch (DbEntityValidationException dbex)
            {
                // Assign errors from db
                payload.AssignDbErrors(dbex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);

                payload.Errors.Add("00000", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, payload);
            }

            // Return proper response message
            if (payload.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK, payload);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, payload);
            }
        }

        // DELETE api/asset/5
        public HttpResponseMessage Delete(int id)
        {
            var payload = new HttpResponsePayload<bool>();

            try
            {
                using (var queries = new AssetQueries(db))
                {
                    payload.Data = queries.Delete(id);
                }
            }
            catch (DbEntityValidationException dbex)
            {
                // Assign errors from db
                payload.AssignDbErrors(dbex);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);

                payload.Errors.Add("00000", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, payload);
            }

            // Return proper response message
            if (payload.IsSuccess)
            {
                return Request.CreateResponse(HttpStatusCode.OK, payload);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, payload);
            }
        }
    }
}
