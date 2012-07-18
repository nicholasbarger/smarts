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
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        payload.Errors.Add("00001", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
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
                // Validate
                if (id > 0)
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
                else
                {
                    payload.Errors.Add("00003", Resources.Errors.ERR00003);
                }
            }
            catch (DbEntityValidationException dbex)
            {
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        payload.Errors.Add("00001", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
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
                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);
                if (rules.IsValid)
                {
                    // Prep asset data
                    obj.Created = DateTime.Now;
                    obj.ContributorGuid = contributor;

                    SaveAsset(obj, payload);
                }
                else
                {
                    // I've tried concat, union, and a few other methods and none are adding to error list properly
                    // going back to brute force looping for now
                    foreach (var error in rules.Errors)
                    {
                        payload.Errors.Add(error.Key, error.Value);
                    }
                }
            }
            catch(DbEntityValidationException dbex)
            {
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        payload.Errors.Add("00001", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
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
                if (rules.IsValid)
                {
                    // Prep asset data
                    obj.Created = DateTime.Now;
                    obj.ContributorGuid = contributor;

                    SaveAsset(obj, payload);
                }
                else
                {
                    // I've tried concat, union, and a few other methods and none are adding to error list properly
                    // going back to brute force looping for now
                    foreach (var error in rules.Errors)
                    {
                        payload.Errors.Add(error.Key, error.Value);
                    }
                }
            }
            catch (DbEntityValidationException dbex)
            {
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        payload.Errors.Add("00001", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
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
            var payload = new HttpResponsePayload<Asset>();

            try
            {
                // Validate
                if (id > 0)
                {
                    // Get asset, using queries to ensure consistency of includes
                    Asset asset = null;
                    using (var queries = new AssetQueries(db))
                    {
                        asset = queries.GetAsset(id);
                    }

                    // If not null, set inactive and save
                    if (asset != null)
                    {
                        // Set asset to inactive
                        asset.IsActive = false;

                        // Update (archived delete)
                        db.SaveChanges();

                        // Update payload with modified asset
                        payload.Data = asset;
                    }
                    else
                    {
                        payload.Errors.Add("00002", string.Format(Resources.Errors.ERR00002, "asset"));
                    }
                }
                else
                {
                    payload.Errors.Add("00003", Resources.Errors.ERR00003);
                }
            }
            catch (DbEntityValidationException dbex)
            {
                foreach (var validationErrors in dbex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        payload.Errors.Add("00001", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
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

        private void SaveAsset(Asset obj, HttpResponsePayload<Asset> payload)
        {
            if (obj.Id == 0)
            {
                // Add to collection
                obj.Created = DateTime.Now;
                db.Assets.Add(obj);
            }
            else
            {
                // Attach to collection
                db.Assets.Attach(obj);
                db.Entry(obj).State = System.Data.EntityState.Modified;
            }            

            // Commit changes
            var result = db.SaveChanges();

            // Check result
            if (result > 0)
            {
                payload.IsSuccess = true;
            }
            else
            {
                payload.Errors.Add("00001", "Error saving to the database.");
            }
        }
    }
}
