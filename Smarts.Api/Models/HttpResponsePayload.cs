using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// A response payload for REST from web api.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResponsePayload<T> : Payload<T>
    {
        private HttpStatusCode httpStatusCode { get; set; }

        /// <summary>
        /// The http status code associated with success/errors in the payload.
        /// </summary>
        public HttpStatusCode HttpStatusCode 
        {
            get
            {
                // Check if the httpStatusCode has been manually set to a value and if so, use that
                if (httpStatusCode != System.Net.HttpStatusCode.Unused)
                {
                    return httpStatusCode;
                }

                // else, check to see if we can infer the statuscode based on the error collection
                else
                {
                    // Check for server-errrors or db errors
                    // 00000 - Generic server error
                    // 00001 - Db error
                    if (this.Errors.ContainsKey("00000") || this.Errors.ContainsKey("00001"))
                    {
                        return System.Net.HttpStatusCode.InternalServerError;
                    }
                    
                    // Check for unauthorized errors
                    // 00005 - Unauthorized
                    if(this.Errors.ContainsKey("00005"))
                    {
                        return System.Net.HttpStatusCode.Unauthorized;
                    }

                    // Check for null or not found errors
                    // 00002 - Item not found
                    // 00003 - Required id not specified
                    // 00004 - Item is null
                    if (this.Errors.ContainsKey("00002") || this.Errors.ContainsKey("00003") | this.Errors.ContainsKey("00004"))
                    {
                        return System.Net.HttpStatusCode.NotFound;
                    }

                    // Return success 200 OK
                    if (this.IsSuccess == true)
                    {
                        return System.Net.HttpStatusCode.OK;
                    }

                    // Catch for all other errors (assuming they are validation related)
                    else
                    {
                        return System.Net.HttpStatusCode.BadRequest;
                    }
                }
            }
            set
            {
                httpStatusCode = value;
            }
        }

        /// <summary>
        /// A list of links that may help in navigation from one action to another or discovery of a REST pattern.
        /// </summary>
        public List<HttpResponsePayloadLink> Links { get; set; }

        /// <summary>
        /// Constructor for response payload.
        /// </summary>
        public HttpResponsePayload()
        {
            // Set default http status code
            this.httpStatusCode = System.Net.HttpStatusCode.Unused;
        }
    }

    /// <summary>
    /// A navigational link for REST from web api.
    /// </summary>
    public class HttpResponsePayloadLink
    {
        /// <summary>
        /// The relationship of the link, such as Child, Parent, Sibling, etc.
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// The title of the link.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The location of the link.
        /// </summary>
        public Uri Uri { get; set; }
    }    
}