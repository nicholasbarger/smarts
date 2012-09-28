using Smarts.Api.Db;
using Smarts.Api.Logic;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Smarts.Api.Controllers
{
    public class CurriculumController : ApiController
    {
        private SmartsDbContext db;
        private Guid contributor;

        public CurriculumController()
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
        /// Retrieve list of curriculums (unfiltered).
        ///     Usage: GET api/curriculum/      
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Curriculum>>();

            // todo: Match signature to operation

            try
            {
                // Get curriculums, using queries to ensure consistency of includes
                List<Curriculum> curriculums = null;
                using (var queries = new CurriculumQueries(db))
                {
                    curriculums = queries.GetQuery().ToList();
                }

                // Check if null to add error
                if (curriculums == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = curriculums;
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
            var payload = new HttpResponsePayload<Curriculum>();

            try
            {
                // Get curriculums, using queries to ensure consistency of includes
                Curriculum curriculum = null;
                using (var queries = new CurriculumQueries(db))
                {
                    curriculum = queries.Get(id);
                }

                // Check if null to add error
                if (curriculum == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = curriculum;
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
        public HttpResponseMessage Post(Curriculum obj)
        {
            var payload = new HttpResponsePayload<Curriculum>();

            try
            {
                // Prep
                var logic = new CurriculumLogic();
                logic.SetDefaults(ref obj);
                obj.ContributorGuid = contributor;

                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new CurriculumQueries(db))
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

        // PUT api/curriculum/5
        [HttpPut]
        public HttpResponseMessage Put(int id, Curriculum obj)
        {
            var payload = new HttpResponsePayload<Curriculum>();

            try
            {
                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new CurriculumQueries(db))
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

        // DELETE api/curriculum/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var payload = new HttpResponsePayload<bool>();

            try
            {
                using (var queries = new CurriculumQueries(db))
                {
                    payload.Data = queries.Delete(id);
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
