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
            throw new NotImplementedException();
        }

        // GET api/asset/5
        public HttpResponseMessage Get(int id)
        {
            throw new NotImplementedException();
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
                    //obj.ContributorGuid = contributor; - // todo: remove - this is just temporary

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
                        payload.Errors.Add("000", string.Format("Property: {0} Error: {1}" + Environment.NewLine, validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                // todo: add exception logging here

                payload.Errors.Add("-001", ex.Message);
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
        public HttpResponseMessage Put(int id, AssetQueries obj)
        {
            throw new NotImplementedException();
        }

        // DELETE api/asset/5
        public HttpResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
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
                payload.Errors.Add("000", "Error saving to the database.");
            }
        }
    }
}
