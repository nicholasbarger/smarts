using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Db
{
    public class AssetQueries : IDisposable
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
                var asset = GetAsset(id);

                // If not null, set inactive and save
                if (asset != null)
                {
                    // Set asset to inactive
                    asset.IsActive = false;

                    // Save asset
                    result = SaveAsset(ref asset);
                }
            }

            return result;
        }

        public Asset GetAsset(int id)
        {
            Asset asset = null;
            if (id > 0)
            {
                asset = context.Assets.Single(a => a.Id == id);
            }

            return asset;
        }

        public IQueryable<Asset> GetAssetsQuery()
        {
            return context.Assets.Where(a => a.IsActive == true);
        }

        public bool SaveAsset(ref Asset obj)
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