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
    public class SubjectController : ApiController
    {
        private SmartsDbContext db;
        private Guid contributor;

        public SubjectController()
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
                // Get subjects, using queries to ensure consistency of includes
                List<Subject> subjects = null;
                using (var queries = new SubjectQueries(db))
                {
                    subjects = queries.GetQuery().ToList();
                }

                // Check if null to add error
                if (subjects == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = subjects;
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

            // todo: add appending of # if necessary

            try
            {
                // Get subject, using queries to ensure consistency of includes
                Subject subject = null;
                using (var queries = new SubjectQueries(db))
                {
                    subject = queries.Get(hashTag);
                }

                // Check if null to add error
                if (subject == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = subject;
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
                // Get subjects, using queries to ensure consistency of includes
                List<Subject> subjects = null;
                using (var queries = new SubjectQueries(db))
                {
                    subjects = queries.Search(q);
                }

                // Check if null to add error
                if (subjects == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = subjects;
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
                // Prep
                var logic = new SubjectLogic();
                logic.SetDefaults(ref obj);
                obj.ContributorGuid = contributor;

                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new SubjectQueries(db))
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

        // PUT api/subject/c#
        [HttpPut]
        public HttpResponseMessage Put(string hashTag, Subject obj)
        {
            var payload = new HttpResponsePayload<Subject>();

            try
            {
                // Validate
                var rules = new ValidationRules();
                rules.Validate(obj);

                // Check if valid
                if (rules.IsValid)
                {
                    // Save
                    using (var queries = new SubjectQueries(db))
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

        // DELETE api/subject/c#
        [HttpDelete]
        public HttpResponseMessage Delete(string hashTag)
        {
            var payload = new HttpResponsePayload<bool>();

            try
            {
                using (var queries = new SubjectQueries(db))
                {
                    payload.Data = queries.Delete(hashTag);
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