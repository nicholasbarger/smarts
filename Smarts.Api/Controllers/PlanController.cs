using Smarts.Api.Db;
using Smarts.Api.BusinessLogic;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Smarts.Api.AppLogic;
using Smarts.Api.Utilities;

namespace Smarts.Api.Controllers
{
    public class PlanController : ApiController
    {
        private PlanAppLogic logic;
        private Guid contributor;

        public PlanController()
        {   
            // initialize logic
            logic = new PlanAppLogic();

            // assign contributor
            var utility = new ControllerUtilities();
            contributor = utility.GetWebUserGuidFromCookies();
        }

        #region GET Actions

        /// <summary>
        /// Retrieve list of curriculums (unfiltered).
        ///     Usage: GET api/curriculum/      
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Plan>>();

            try
            {
                // Get full list
                payload = new HttpResponsePayload<List<Plan>>(logic.Get());
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
        /// Get a specific curriculum by id.
        ///     Usage: GET api/curriculum/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var payload = new HttpResponsePayload<Plan>();

            try
            {
                // Get specific
                payload = new HttpResponsePayload<Plan>(logic.Get(id));
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
        /// Retrieve a list of curriculums matching a search term.
        ///     Usage: api/curriculum/search/chem  
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

        // POST api/curriculum
        [HttpPost]
        public HttpResponseMessage Post(Plan obj)
        {
            var payload = new HttpResponsePayload<Plan>();

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

        // PUT api/curriculum/5
        [HttpPut]
        public HttpResponseMessage Put(int id, Plan obj)
        {
            var payload = new HttpResponsePayload<Plan>();

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

        // DELETE api/curriculum/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
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
