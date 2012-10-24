using Smarts.Api.Models;
using Smarts.Api.Utilities;
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

        public Comment Get(int id)
        {
            Comment comment = null;
            if (id > 0)
            {
                comment = GetQuery().SingleOrDefault(a => a.Id == id);
            }

            return comment;
        }

        public List<Comment> GetByAsset(int assetId)
        {
            return GetQuery()
                .Where(a => a.AssetId == assetId)
                .ToList();
        }

        public IQueryable<Comment> GetQuery()
        {
            return context.Comments
                .Include("Contributor")
                .OrderByDescending(a => a.Created);
        }

        public void Save(ref Comment obj)
        {
            if (obj != null)
            {
                if (obj.Id == 0)
                {
                    // Add to collection
                    context.Comments.Add(obj);
                }
                else
                {
                    // Map from original record
                    LoadOriginalFromDbAndMap(ref obj);

                    // Attach to collection
                    context.Comments.Attach(obj);
                    context.Entry(obj).State = System.Data.EntityState.Modified;
                }

                // Commit changes
                var utility = new DbUtilities();
                utility.SaveWithExpectedSuccess(context.SaveChanges());
            }
        }

        private void LoadOriginalFromDbAndMap(ref Comment obj)
        {
            // Use utility for easier readability
            var utility = new DbUtilities();

            // Get original
            var original = Get(obj.Id);

            // Map if the value has changed
            original.AssetId = utility.Map(original.AssetId, obj.AssetId);
            original.ContributorGuid = utility.Map(original.ContributorGuid, obj.ContributorGuid);
            original.Text = utility.Map(original.Text, obj.Text);

            // Set obj to new merged values (original)
            obj = original;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}