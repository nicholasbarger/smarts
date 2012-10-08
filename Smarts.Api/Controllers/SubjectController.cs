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
using Smarts.Api.Utilities;
using Smarts.Api.AppLogic;

namespace Smarts.Api.Controllers
{
    public class SubjectController : ApiController
    {
        private SubjectAppLogic logic;
        private Guid contributor;

        public SubjectController()
        {   
            // initialize logic
            logic = new SubjectAppLogic();

            // assign contributor
            var utility = new ControllerUtilities();
            contributor = utility.GetWebUserGuidFromCookies();
        }

        #region GET Actions

        /// <summary>
        /// Retrieve list of subjects (unfiltered).
        ///     Usage: GET api/subject/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Subject>>();

            try
            {
                // get full list
                payload = new HttpResponsePayload<List<Subject>>(logic.Get());
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
        /// Retrieve a specific subject by hashtag.
        ///     Usage: GET api/subject/#csharp
        ///            GET api/subject/csharp
        /// </summary>
        /// <param name="hashTag"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(string hashTag)
        {
            var payload = new HttpResponsePayload<Subject>();

            try
            {
                // get specific
                payload = new HttpResponsePayload<Subject>(logic.Get(hashTag));
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
        /// Search for subjects based on a specified search term.
        ///     Usage: GET api/subject/search/bio
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Search(string q)
        {
            var payload = new HttpResponsePayload<List<Subject>>();

            try
            {
                // search
                payload = new HttpResponsePayload<List<Subject>>(logic.Search(q));
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

        // POST api/subject
        [HttpPost]
        public HttpResponseMessage Post(Subject obj)
        {
            var payload = new HttpResponsePayload<Subject>();

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

        // PUT api/subject/c#
        [HttpPut]
        public HttpResponseMessage Put(string hashTag, Subject obj)
        {
            var payload = new HttpResponsePayload<Subject>();

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

        // DELETE api/subject/c#
        [HttpDelete]
        public HttpResponseMessage Delete(string hashTag)
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