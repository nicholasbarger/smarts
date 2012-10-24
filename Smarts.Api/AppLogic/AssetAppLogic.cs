using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using Smarts.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Payload<Comment> Comment(Comment obj)
        {
            // create payload
            var payload = new Payload<Comment>();

            // Prep obj
            obj.Created = DateTime.Now;

            // validate
            var rules = new ValidationRules();
            rules.Validate(obj);

            // check if valid
            if (rules.IsValid)
            {
                // db save
                using (var queries = new CommentQueries())
                {
                    queries.Save(ref obj);
                }

                payload.Data = obj;
            }

            // todo: next steps in workflow

            // return payload
            return payload;
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
            bool isNewAsset = (obj.Id <= 0);
            business.SetDefaults(ref obj);

            // Check if we need to create new subject
            foreach (var association in obj.SubjectAssociations)
            {
                // Get from db and see if it already exists
                var subjectLogic = new SubjectAppLogic();
                if (association.Subject == null)
                {
                    // check if exists
                    var subjectIsNew = (subjectLogic.Get(association.Hashtag).Data == null);
                    if (subjectIsNew)
                    {
                        // create new subject
                        association.Subject = new Subject()
                        {
                            Hashtag = association.Hashtag,
                            ContributorGuid = association.ContributorGuid,
                            Contributor = association.Contributor,
                            Created = DateTime.Now
                        };
                    }
                }
            }

            // validate
            var rules = new ValidationRules();
            rules.Validate(obj);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // if existing asset, check the properties that have changed prior to update
                var changedProperties = new StringBuilder();
                if (!isNewAsset)
                {
                    var originalAsset = Get(obj.Id).Data;
                    CheckChangedProperties(originalAsset, obj, ref changedProperties);
                }

                // save to db
                using (var queries = new AssetQueries())
                {
                    queries.Save(ref obj);
                }

                // assign primary data
                payload.Data = obj;

                // log activity
                if (isNewAsset)
                {
                    // new asset
                    AuditUtilities.Log(obj.Contributor, ActivityEventItem.AssetCreated,
                        string.Format(Resources.AuditEntries.AssetCreated, obj.Contributor.Username));
                }
                else
                {
                    // updated asset
                    AuditUtilities.Log(obj.Contributor, ActivityEventItem.AssetModified,
                        string.Format(Resources.AuditEntries.AssetModified, obj.Contributor.Username, changedProperties));
                }
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }

        private void CheckChangedProperties(Asset original, Asset updated, ref StringBuilder changedProperties)
        {
            if (original.AssetTypeId != updated.AssetTypeId)
            {
                changedProperties.AppendFormat("Original asset type: {0}, updated asset type: {1}\n", original.AssetTypeId, updated.AssetTypeId);
            }

            if (original.ContributorGuid != updated.ContributorGuid)
            {
                changedProperties.AppendFormat("Original contributor: {0}, updated contributor: {1}\n", original.ContributorGuid, updated.ContributorGuid);
            }

            if (original.Cost != updated.Cost)
            {
                changedProperties.AppendFormat("Original cost: {0}, updated cost: {1}\n", original.Cost, updated.Cost);
            }

            if (original.Description != updated.Description)
            {
                changedProperties.AppendFormat("Original description: {0}, updated description: {1}\n", original.Description, updated.Description);
            }

            if (original.IsActive != updated.IsActive)
            {
                changedProperties.AppendFormat("Original active: {0}, updated active: {1}\n", original.IsActive, updated.IsActive);
            }

            if (original.IsScoreable != updated.IsScoreable)
            {
                changedProperties.AppendFormat("Original scoreable: {0}, updated scorable: {1}\n", original.IsScoreable, updated.IsScoreable);
            }

            if (original.IsTestRequired != updated.IsTestRequired)
            {
                changedProperties.AppendFormat("Original test required: {0}, updated test required: {1}\n", original.IsTestRequired, updated.IsTestRequired);
            }

            if (original.PassingScore != updated.PassingScore)
            {
                changedProperties.AppendFormat("Original passing score: {0}, updated passing score: {1}\n", original.PassingScore, updated.PassingScore);
            }

            if (original.PictureUri != updated.PictureUri)
            {
                changedProperties.AppendFormat("Original picture: {0}, updated picture: {1}\n", original.PictureUri, updated.PictureUri);
            }

            if (original.Title != updated.Title)
            {
                changedProperties.AppendFormat("Original title: {0}, updated title: {1}\n", original.Title, updated.Title);
            }

            if (original.Uri != updated.Uri)
            {
                changedProperties.AppendFormat("Original uri: {0}, updated uri: {1}\n", original.Uri, updated.Uri);
            }
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