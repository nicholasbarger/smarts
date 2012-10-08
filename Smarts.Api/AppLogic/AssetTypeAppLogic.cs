using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class AssetTypeAppLogic
    {
        AssetTypeBusinessLogic business;

        public AssetTypeAppLogic()
        {
            // create reference to business logic
            business = new AssetTypeBusinessLogic();
        }

        public Payload<List<AssetType>> Get()
        {
            // create payload
            var payload = new Payload<List<AssetType>>();

            // todo: check security

            // get from db
            using (var queries = new AssetTypeQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }

            // return payload
            return payload;
        }
    }
}