using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Smarts.Api.Db;
using Smarts.Api.BusinessLogic;
using Smarts.Api.Models;
using Smarts.Api.Utilities;
using Smarts.Api.AppLogic;

namespace Smarts.Api.Controllers
{
    public class ResourceController : ApiController
    {
        private ResourceAppLogic logic;
        private Guid contributor;

        public ResourceController()
        {   
            // initialize logic
            logic = new ResourceAppLogic();

            // assign contributor
            var utility = new ControllerUtilities();
            contributor = utility.GetWebUserGuidFromCookies();
        }

        #region GET Actions

        /// <summary>
        /// Retrieve list of assets (unfiltered)
        ///     USAGE: GET api/asset    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Resource>>();

            try
            {
                // Get full list
                payload = new HttpResponsePayload<List<Resource>>(logic.Get());
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
        /// Get a specific asset.
        ///     USAGE: GET api/asset/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var payload = new HttpResponsePayload<Resource>();

            try
            {
                // get specific
                payload = new HttpResponsePayload<Resource>(logic.Get(id));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        [HttpGet]
        public HttpResponseMessage GetBySubject(string hashtag)
        {
            var payload = new HttpResponsePayload<List<Resource>>();

            // Prep from controller to add back the '#', which was removed for WebApi
            hashtag = '#' + hashtag;

            try
            {
                // get specific
                payload = new HttpResponsePayload<List<Resource>>(logic.GetBySubject(hashtag));
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
        /// Get the list of comments associated with an asset.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetComments(int id)
        {
            var payload = new HttpResponsePayload<List<Comment>>();

            try
            {
                // get comments
                payload = new HttpResponsePayload<List<Comment>>(logic.GetComments(id));
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
        /// Get the list of subjects (tags) associated with an asset.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetSubjects(int id)
        {
            var payload = new HttpResponsePayload<List<Topic>>();

            try
            {
                // get subjects
                payload = new HttpResponsePayload<List<Topic>>(logic.GetSubjects(id));
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
        /// Retrieve a list of assets matching a search term
        ///     USAGE: GET api/asset/search/chem  
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Search(string q)
        {
            var payload = new HttpResponsePayload<List<Resource>>();

            try
            {
                // search
                payload = new HttpResponsePayload<List<Resource>>(logic.Search(q));
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

        // POST api/asset/comment
        [HttpPost]
        public HttpResponseMessage Comment(int id, string comment)
        {
            var payload = new HttpResponsePayload<Comment>();

            try
            {
                // Prep from controller level
                var obj = new Comment()
                {
                    Id = id,
                    Text = comment,
                    ContributorGuid = contributor
                };

                // Save through logic
                payload = new HttpResponsePayload<Comment>(logic.Comment(obj));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        // POST api/asset/complete/5
        [HttpPost]
        public HttpResponseMessage Complete(Resource obj)
        {
            var payload = new HttpResponsePayload<Comment>();

            try
            {
                // Prep from controller level
                obj.ContributorGuid = contributor;

                // Save through logic
                //payload = new HttpResponsePayload<Comment>(logic.Complete(obj));

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

        // POST api/asset
        [HttpPost]
        public HttpResponseMessage Post(Resource obj)
        {
            var payload = new HttpResponsePayload<Resource>();

            try
            {
                // Prep from controller level
                if (obj != null)
                {
                    obj.ContributorGuid = contributor;

                    // Prep children - Subject associations 
                    if (obj.TopicAssociations != null)
                    {
                        foreach (var association in obj.TopicAssociations)
                        {
                            // Assign contributor to the subject associations
                            association.ContributorGuid = contributor;
                        }
                    }
                }

                // Save through logic
                payload = new HttpResponsePayload<Resource>(logic.Save(obj));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        // PUT api/asset/5
        [HttpPut]
        public HttpResponseMessage Put(Resource obj)
        {
            var payload = new HttpResponsePayload<Resource>();

            try
            {
                // Save through logic
                payload = new HttpResponsePayload<Resource>(logic.Save(obj));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(ex);
                payload.AssignExceptionErrors(ex);
            }

            // Return proper response message
            return Request.CreateResponse(payload.HttpStatusCode, payload);
        }

        // DELETE api/asset/5
        [HttpDelete]
        public HttpResponseMessage Delete(Resource obj)
        {
            var payload = new HttpResponsePayload<Resource>();

            try
            {
                // Delete through logic
                payload = new HttpResponsePayload<Resource>(logic.Delete(obj));
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
