using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smarts.Api.Models;

namespace Smarts.Api.Controllers
{
    public class AssetController : ApiController
    {
        // GET api/asset
        public HttpResponsePayload<IEnumerable<Asset>> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/asset/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/asset
        public void Post(string value)
        {
        }

        // PUT api/asset/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/asset/5
        public void Delete(int id)
        {
        }
    }
}
