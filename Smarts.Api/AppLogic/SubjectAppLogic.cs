using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class SubjectAppLogic
    {
        SubjectBusinessLogic business;

        public SubjectAppLogic()
        {
            // create reference to business logic
            business = new SubjectBusinessLogic();
        }

        public Payload<List<Subject>> Get()
        {
            // create payload
            var payload = new Payload<List<Subject>>();

            // todo: check security

            // get from db
            using (var queries = new SubjectQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }

        public Payload<Subject> Get(string hashtag)
        {
            // create payload
            var payload = new Payload<Subject>();

            // todo: check security

            // prep
            // todo: add appending of # if necessary

            // get from db
            using (var queries = new SubjectQueries())
            {
                payload.Data = queries.Get(hashtag);
            }

            // Check if null to add error
            if (payload.Data == null)
            {
                payload.Errors.Add("00002", Resources.Errors.ERR00002);
            }

            // return payload
            return payload;
        }

        public Payload<List<Subject>> Search(string q)
        {
            // create payload
            var payload = new Payload<List<Subject>>();

            // todo: check security

            // get from db
            using (var queries = new SubjectQueries())
            {
                payload.Data = queries.Search(q);
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