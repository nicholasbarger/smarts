using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Db
{
    internal class CommentQueries : IDisposable
    {
        private SmartsDbContext context;

        public CommentQueries()
        {
            this.context = new SmartsDbContext();
        }

        public CommentQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Comment> GetQuery()
        {
            return context.Comments
                .Include("Contributor");
        }

        public List<Comment> GetByAsset(int assetId)
        {
            return GetQuery()
                .Where(a => a.AssetId == assetId)
                .ToList();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}