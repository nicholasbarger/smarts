using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;
using Smarts.Api.Utilities;

namespace Smarts.Api.Db
{
    /// <summary>
    /// Database queries and interaction goes here.
    /// All get queries should be marked as IQueryable to allow for filtering at the requestor level.
    /// </summary>
    internal class AssetQueries : IDbCrud<Asset>, IDisposable
    {
        private SmartsDbContext context;

        /// <summary>
        /// The default constructor.
        /// </summary>
        public AssetQueries()
        {
            this.context = new SmartsDbContext();
        }

        /// <summary>
        /// Constructor for creating an asset queries context which is going to need to use the context
        /// outside of the AssetQueries scope, for example, collapsing the linq statement after business logic determines
        /// additional filter criteria.
        /// </summary>
        /// <param name="context"></param>
        public AssetQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public void Delete(ref Asset obj)
        {
            if (obj != null)
            {
                Delete(obj.Id);
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                // Get asset
                var asset = Get(id);

                // If not null, set inactive and save
                if (asset != null)
                {
                    // Set asset to inactive
                    asset.IsActive = false;

                    // Save asset
                    Save(ref asset);
                }
            }
        }

        public Asset Get(int id)
        {
            Asset asset = null;
            if (id > 0)
            {
                asset = GetQuery().SingleOrDefault(a => a.Id == id);
            }

            return asset;
        }

        public List<Asset> GetBySubject(string hashtag)
        {
            return GetQuery().Where(a => a.SubjectAssociations.Any(b => b.Hashtag == hashtag))
                .ToList();
        }

        public IQueryable<Asset> GetQuery()
        {
            return context.Assets
                .Include("AssetType")
                .Include("Contributor")
                .Where(a => a.IsActive == true);
        }

        public void Save(ref Asset obj)
        {
            if (obj != null)
            {
                if (obj.Id == 0)
                {
                    // Add to collection
                    context.Assets.Add(obj);
                }
                else
                {
                    // Map from original record
                    LoadOriginalFromDbAndMap(ref obj);

                    // Attach to collection
                    context.Assets.Attach(obj);
                    context.Entry(obj).State = System.Data.EntityState.Modified;
                }

                // Commit changes
                var utility = new DbUtilities();
                utility.SaveWithExpectedSuccess(context.SaveChanges());
            }
        }

        private void LoadOriginalFromDbAndMap(ref Asset obj)
        {
            // Use utility for easier readability
            var utility = new DbUtilities();

            // Get original
            var original = Get(obj.Id);

            // Map if the value has changed
            original.AssetTypeId = utility.Map(original.AssetTypeId, obj.AssetTypeId);
            original.Comments = utility.Map(original.Comments, obj.Comments);
            original.Cost = utility.Map(original.Cost, obj.Cost);
            original.Description = utility.Map(original.Description, obj.Description);
            original.Difficulty = utility.Map(original.Difficulty, obj.Difficulty);
            original.Importance = utility.Map(original.Importance, obj.Importance);
            original.IsScoreable = utility.Map(original.IsScoreable, obj.IsScoreable);
            original.IsTestRequired = utility.Map(original.IsTestRequired, obj.IsTestRequired);
            original.PassingScore = utility.Map(original.PassingScore, obj.PassingScore);
            original.PictureUri = utility.Map(original.PictureUri, obj.PictureUri);
            original.Title = utility.Map(original.Title, obj.Title);
            original.Uri = utility.Map(original.Uri, obj.Uri);

            // Set obj to new merged values (original)
            obj = original;
        }

        public List<Asset> Search(string q)
        {
            return SearchQuery(q).ToList();
        }

        public IQueryable<Asset> SearchQuery(string q)
        {
            return GetQuery().Where(a => a.Title.Contains(q) || a.Description.Contains(q));
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}