﻿using System;
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

        #region GET Actions

        /// <summary>
        /// Retrieve list of assets (unfiltered)
        ///     USAGE: GET api/asset    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var payload = new HttpResponsePayload<List<Asset>>();

            try
            {
                // Get assets, using queries to ensure consistency of includes
                List<Asset> assets = null;
                using (var queries = new AssetQueries(db))
                {
                    assets = queries.GetQuery().ToList();
                }

                // Check if null to add error
                if (assets == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = assets;
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
            var payload = new HttpResponsePayload<Asset>();

            try
            {
                // Get asset, using queries to ensure consistency of includes
                Asset asset = null;
                using (var queries = new AssetQueries(db))
                {
                    asset = queries.Get(id);
                }

                // Check if null to add error
                if (asset == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = asset;
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
            var payload = new HttpResponsePayload<List<Asset>>();

            try
            {
                // Get asset, using queries to ensure consistency of includes
                List<Asset> assets = null;
                using (var queries = new AssetQueries(db))
                {
                    assets = queries.Search(q);
                }

                // Check if null to add error
                if (assets == null)
                {
                    payload.Errors.Add("00002", Resources.Errors.ERR00002);
                }

                payload.Data = assets;
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

        // POST api/asset
        [HttpPost]
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

        // PUT api/asset/5
        [HttpPut]
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

        // DELETE api/asset/5
        [HttpDelete]
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
