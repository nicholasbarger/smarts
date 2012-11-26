using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class ActivityAppLogic
    {
        public Payload<List<Activity>> Get()
        {
            // create payload
            var payload = new Payload<List<Activity>>();

            // todo: check security

            // get from db
            using (var queries = new ActivityQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }

        public Payload<List<Activity>> GetByResource(int id)
        {
            // create payload
            var payload = new Payload<List<Activity>>();

            // todo: check security

            // get from db
            using (var queries = new ActivityQueries())
            {
                payload.Data = queries.GetByResource(id);
            }

            // return payload
            return payload;
        }
    }
}