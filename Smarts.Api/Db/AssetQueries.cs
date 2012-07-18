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

        public Asset GetAsset(int id)
        {
            return context.Assets.Single(a => a.Id == id);
        }

        public IQueryable<Asset> GetAssetsQuery()
        {
            return context.Assets;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}