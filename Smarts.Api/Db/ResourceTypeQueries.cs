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
    internal class ResourceTypeQueries : IDbReadOnly<ResourceType>, IDisposable
    {
        private SmartsDbContext context;

        public ResourceTypeQueries()
        {
            this.context = new SmartsDbContext();
        }

        public ResourceTypeQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public ResourceType Get(int id)
        {
            ResourceType assetType = null;
            if (id > 0)
            {
                assetType = context.AssetTypes.SingleOrDefault(a => a.Id == id);
            }

            return assetType;
        }

        public IQueryable<ResourceType> GetQuery()
        {
            return context.AssetTypes;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}