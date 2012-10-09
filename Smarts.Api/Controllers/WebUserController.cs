using Smarts.Api.Db;
using Smarts.Api.BusinessLogic;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Smarts.Api.AppLogic;
using Smarts.Api.Utilities;

namespace Smarts.Api.Controllers
{
    public class WebUserController : ApiController
    {
        private WebUserAppLogic logic;
        private Guid contributor;

        public WebUserController()
        {   
            // initialize logic
            logic = new WebUserAppLogic();

            // assign contributor
            var utility = new ControllerUtilities();
            contributor = utility.GetWebUserGuidFromCookies();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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

        // POST api/user/login
        [HttpPost]
        public HttpResponseMessage Login(WebUser obj)
        {
            var payload = new HttpResponsePayload<WebUser>();

            try
            {
                // assign email and username to same value to assist in login
                if (string.IsNullOrEmpty(obj.Username) && !string.IsNullOrEmpty(obj.Email))
                {
                    obj.Username = obj.Email;
                }
                if (string.IsNullOrEmpty(obj.Email) && !string.IsNullOrEmpty(obj.Username))
                {
                    obj.Email = obj.Username;
                }

                // login through logic using either username or email and matching password
                payload = new HttpResponsePayload<WebUser>(logic.Login(obj.Username, obj.Email, obj.Password));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        // POST api/user/logout
        [HttpPost]
        public HttpResponseMessage Logout(WebUser obj)
        {
            throw new NotImplementedException();
        }

        // POST api/user
        [HttpPost]
        public HttpResponseMessage Post(WebUser obj)
        {
            var payload = new HttpResponsePayload<WebUser>();

            try
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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