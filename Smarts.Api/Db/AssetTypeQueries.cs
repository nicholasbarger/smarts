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
    internal class AssetTypeQueries : IDbReadOnly<AssetType>, IDisposable
    {
        private SmartsDbContext context;

        public AssetTypeQueries()
        {
            this.context = new SmartsDbContext();
        }

        public AssetTypeQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public AssetType Get(int id)
        {
            AssetType assetType = null;
            if (id > 0)
            {
                assetType = context.AssetTypes.SingleOrDefault(a => a.Id == id);
            }

            return assetType;
        }

        public IQueryable<AssetType> GetQuery()
        {
            return context.AssetTypes;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}