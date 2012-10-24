using Smarts.Api.Models;
using Smarts.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Db
{
    internal class ActivityQueries : IDbCrud<Activity>, IDisposable
    {
        private SmartsDbContext context;

        /// <summary>
        /// The default constructor.
        /// </summary>
        public ActivityQueries()
        {
            this.context = new SmartsDbContext();
        }

        public ActivityQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            // todo
            throw new NotImplementedException();
        }

        public void Delete(ref Activity obj)
        {
            // todo
            Delete(obj.Id);
        }

        public Activity Get(int id)
        {
            Activity activity = null;
            if (id > 0)
            {
                activity = GetQuery().SingleOrDefault(a => a.Id == id);
            }

            return activity;
        }

        public List<Activity> GetByAsset(int assetId)
        {
            return GetQuery().Where(a => a.AssetId == assetId).ToList();
        }

        public IQueryable<Activity> GetQuery()
        {
            return this.context.Activities.Include("User");
        }

        public void Save(ref Activity obj)
        {
            if (obj != null)
            {
                if (obj.Id == 0)
                {
                    // Add to collection
                    context.Activities.Add(obj);
                }
                else
                {
                    // Map from original record
                    LoadOriginalFromDbAndMap(ref obj);

                    // Attach to collection
                    context.Activities.Attach(obj);
                    context.Entry(obj).State = System.Data.EntityState.Modified;
                }

                // Commit changes
                var utility = new DbUtilities();
                utility.SaveWithExpectedSuccess(context.SaveChanges());
            }
        }

        private void LoadOriginalFromDbAndMap(ref Activity obj)
        {
            // Use utility for easier readability
            var utility = new DbUtilities();

            // Get original
            var original = Get(obj.Id);

            // Map if the value has changed
            original.AssetId = utility.Map(original.AssetId, obj.AssetId);
            original.EventId = utility.Map(original.EventId, obj.EventId);
            original.UserGuid = utility.Map(original.UserGuid, obj.UserGuid);
            original.Value = utility.Map(original.Value, obj.Value);

            // Set obj to new merged values (original)
            obj = original;
        }

        public List<Activity> Search(string q)
        {
            return SearchQuery(q).ToList();
        }

        public IQueryable<Activity> SearchQuery(string q)
        {
            return GetQuery().Where(a => a.Value.Contains(q));
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}