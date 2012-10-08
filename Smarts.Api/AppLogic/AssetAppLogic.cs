using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using Smarts.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class AssetAppLogic
    {
        AssetBusinessLogic business;

        public AssetAppLogic()
        {
            // create reference to business logic
            business = new AssetBusinessLogic();
        }

        public Payload<Asset> Delete(Asset obj)
        {
            // create payload
            var payload = new Payload<Asset>();

            // todo: check security

            // validate
            var rules = new ValidationRules();
            rules.ValidateHasId(obj);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // db delete (inactive)
                using (var queries = new AssetQueries())
                {
                    queries.Delete(ref obj);
                }

                payload.Data = obj;
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }

        public Payload<List<Asset>> Get()
        {
            // create payload
            var payload = new Payload<List<Asset>>();

            // todo: check security

            // get from db
            using (var queries = new AssetQueries())
            {
                payload.Data = queries.GetQuery().ToList();
            }
            
            // return payload
            return payload;
        }

        public Payload<Asset> Get(int id)
        {
            // create payload
            var payload = new Payload<Asset>();

            // todo: check security

            // get from db
            using (var queries = new AssetQueries())
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

        public Payload<List<Asset>> GetBySubject(string hashtag)
        {
            // create payload
            var payload = new Payload<List<Asset>>();

            // todo: check security

            // get from db
            using (var queries = new AssetQueries())
            {
                payload.Data = queries.GetBySubject(hashtag);
            }

            // return payload
            return payload;
        }

        public Payload<List<Comment>> GetComments(Asset obj)
        {
            return GetComments(obj.Id);
        }

        public Payload<List<Comment>> GetComments(int assetId)
        {
            // create payload
            var payload = new Payload<List<Comment>>();

            // todo: check security

            // get from db
            using (var queries = new CommentQueries())
            {
                payload.Data = queries.GetByAsset(assetId);
            }

            // return payload
            return payload;
        }

        public Payload<List<Subject>> GetSubjects(Asset obj)
        {
            return GetSubjects(obj.Id);
        }

        public Payload<List<Subject>> GetSubjects(int assetId)
        {
            // create payload
            var payload = new Payload<List<Subject>>();

            // todo: check security

            // get from db
            using (var queries = new SubjectQueries())
            {
                payload.Data = queries.GetByAsset(assetId);
            }

            // return payload
            return payload;
        }

        public Payload<Asset> Save(Asset obj)
        {
            // create payload
            var payload = new Payload<Asset>();

            // todo: check security

            // Prep obj
            business.SetDefaults(ref obj);

            // validate
            var rules = new ValidationRules();
            rules.Validate(obj);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // save to db
                using (var queries = new AssetQueries())
                {
                    queries.Save(ref obj);
                }

                // assign primary data
                payload.Data = obj;
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }

        public Payload<List<Asset>> Search(string q)
        {
            // create payload
            var payload = new Payload<List<Asset>>();

            // todo: check security

            // validate
            var rules = new ValidationRules();
            rules.ValidateIsNotEmpty(q);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // search db
                using (var queries = new AssetQueries())
                {
                    payload.Data = queries.Search(q);
                }
            }

            // return payload
            return payload;
        }
    }
}