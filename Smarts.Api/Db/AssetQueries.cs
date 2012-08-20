using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Db
{
    /// <summary>
    /// Database queries and interaction goes here.
    /// All get queries should be marked as IQueryable to allow for filtering at the requestor level.
    /// </summary>
    internal class AssetQueries : IDbCrud<Asset>, IDisposable
    {
        private SmartsDbContext context;

        public AssetQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            bool result = false;
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
                    result = Save(ref asset);
                }
            }

            return result;
        }

        public Asset Get(int id)
        {
            Asset asset = null;
            if (id > 0)
            {
                asset = context.Assets.SingleOrDefault(a => a.Id == id);
            }

            return asset;
        }

        public IQueryable<Asset> GetQuery()
        {
            return context.Assets.Where(a => a.IsActive == true);
        }

        public bool Save(ref Asset obj)
        {
            bool result = false;
            if (obj != null)
            {
                if (obj.Id == 0)
                {
                    // Add to collection
                    context.Assets.Add(obj);
                }
                else
                {
                    // Attach to collection
                    context.Assets.Attach(obj);
                    context.Entry(obj).State = System.Data.EntityState.Modified;
                }

                // Commit changes
                result = context.SaveChanges() > 0;
            }

            return result;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}