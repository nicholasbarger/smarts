using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class CurriculumAppLogic
    {
        CurriculumBusinessLogic business;

        public CurriculumAppLogic()
        {
            // create reference to business logic
            business = new CurriculumBusinessLogic();
        }

        public Payload<List<Curriculum>> Get()
        {
            // create payload
            var payload = new Payload<List<Curriculum>>();

            // todo: check security

            // get from db
            using (var queries = new CurriculumQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }

        public Payload<Curriculum> Get(int id)
        {
            // create payload
            var payload = new Payload<Curriculum>();

            // todo: check security

            // get from db
            using (var queries = new CurriculumQueries())
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