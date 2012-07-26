using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    public class HttpResponsePayload<T> : Payload<T>
    {
        /// <summary>
        /// A list of links that may help in navigation from one action to another or discovery of a REST pattern.
        /// </summary>
        public List<HttpResponsePayloadLink> Links { get; set; }
    }

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