using Smarts.Api.Db;
using Smarts.Api.Logic;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Smarts.Api.Controllers
{
    public class WebUserController : ApiController
    {
        private SmartsDbContext db;
        private Guid contributor;
        
        public WebUserController()
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

        #region GET Actions

        /// <summary>
        /// Retrieve list of users (unfiltered).
        ///     Usage: GET /api/user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<WebUser>>();

            // todo: Match signature to operation

            try
            {
                // Get users, using queries to ensure consistency of includes
                List<WebUser> users = null;
                using (var queries = new WebUserQueries(db))
                {
                    users = queries.GetQuery().ToList();
                }

                // Check if null to add error
                if (users == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = users;
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
        /// Retrieve a specific user by unique id.
        ///     Usage: GET api/user/5
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(Guid guid)
        {
            var payload = new HttpResponsePayload<WebUser>();

            try
            {
                // Get user, using queries to ensure consistency of includes
                WebUser user = null;
                using (var queries = new WebUserQueries(db))
                {
                    user = queries.Get(guid);
                }

                // Check if null to add error
                if (user == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = user;
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
        /// Search for users based on specified search term.
        ///     Usage: GET /api/user/search/Nicholas
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Search(string q)
        {
            // TODO
            throw new NotImplementedException();
        }

        #endregion

        // POST api/user
        [HttpPost]
        public HttpResponseMessage Post(WebUser obj)
        {
            var payload = new HttpResponsePayload<WebUser>();

            try
            {
                // Prep
                var logic = new WebUserLogic();
                logic.SetDefaults(ref obj);

                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new WebUserQueries(db))
                    {
                        queries.Save(ref obj);
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
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        // PUT api/user/[add sample guid here]
        [HttpPut]
        public HttpResponseMessage Put(Guid guid, WebUser obj)
        {
            var payload = new HttpResponsePayload<WebUser>();

            try
            {
                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new WebUserQueries(db))
                    {
                        queries.Save(ref obj);
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
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        // DELETE api/user/[put sample guid here]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid guid)
        {
            var payload = new HttpResponsePayload<bool>();

            try
            {
                using (var queries = new WebUserQueries(db))
                {
                    payload.Data = queries.Delete(guid);
                }
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