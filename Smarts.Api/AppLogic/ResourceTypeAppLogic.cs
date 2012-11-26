using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class ResourceTypeAppLogic
    {
        ResourceTypeBusinessLogic business;

        public ResourceTypeAppLogic()
        {
            // create reference to business logic
            business = new ResourceTypeBusinessLogic();
        }

        public Payload<List<ResourceType>> Get()
        {
            // create payload
            var payload = new Payload<List<ResourceType>>();

            // todo: check security

            // get from db
            using (var queries = new ResourceTypeQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }
    }
}