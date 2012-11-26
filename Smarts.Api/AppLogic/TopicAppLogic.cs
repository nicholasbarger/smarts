using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class TopicAppLogic
    {
        TopicBusinessLogic business;

        public TopicAppLogic()
        {
            // create reference to business logic
            business = new TopicBusinessLogic();
        }

        public Payload<List<Topic>> Get()
        {
            // create payload
            var payload = new Payload<List<Topic>>();

            // todo: check security

            // get from db
            using (var queries = new TopicQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }

        public Payload<Topic> Get(string hashtag)
        {
            // create payload
            var payload = new Payload<Topic>();

            // todo: check security

            // prep
            // todo: add appending of # if necessary

            // get from db
            using (var queries = new TopicQueries())
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

        public Payload<List<Topic>> Search(string q)
        {
            // create payload
            var payload = new Payload<List<Topic>>();

            // todo: check security

            // get from db
            using (var queries = new TopicQueries())
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