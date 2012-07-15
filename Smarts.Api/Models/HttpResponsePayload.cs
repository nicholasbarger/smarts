using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    public class HttpResponsePayload<T> : Payload<T>
    {
        public List<HttpResponsePayloadLink> Links { get; set; }
    }

    public class HttpResponsePayloadLink
    {
        public string Rel { get; set; }
        public string Title { get; set; }
        public Uri Uri { get; set; }
    }    
}