using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class PlanAppLogic
    {
        PlanBusinessLogic business;

        public PlanAppLogic()
        {
            // create reference to business logic
            business = new PlanBusinessLogic();
        }

        public Payload<List<Plan>> Get()
        {
            // create payload
            var payload = new Payload<List<Plan>>();

            // todo: check security

            // get from db
            using (var queries = new PlanQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }

        public Payload<Plan> Get(int id)
        {
            // create payload
            var payload = new Payload<Plan>();

            // todo: check security

            // get from db
            using (var queries = new PlanQueries())
            {
                payload.Data = queries.Get(id);
            }

            // Check if null to add error
            if (payload.Data == null)
            {
                payload.Errors.Add("00002", Resources.Errors.ERR00002);
            }

            // return payload
            return payload;
        }
    }
}