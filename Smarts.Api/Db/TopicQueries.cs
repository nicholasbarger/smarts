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
    internal class TopicQueries : IDisposable
    {
        private SmartsDbContext context;

        public TopicQueries()
        {
            this.context = new SmartsDbContext();
        }

        public TopicQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public bool Delete(string hashTag)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(hashTag))
            {
                // Get subject
                var subject = Get(hashTag);

                // If not null, delete
                if (subject != null)
                {
                    // Remove from db collection
                    context.Subjects.Remove(subject);

                    // Save changes to db
                    result = context.SaveChanges() > 0;
                }
            }

            return result;
        }

        public Topic Get(string hashTag)
        {
            Topic subject = null;
            if (!string.IsNullOrEmpty(hashTag))
            {
                subject = GetQuery().SingleOrDefault(a => a.Tag == hashTag);
            }

            return subject;
        }

        public List<Topic> GetByAsset(int assetId)
        {
            return GetQuery()
                .Where(a => a.ResourceAssociations.Any(b => b.ResourceId == assetId))
                .ToList();
        }

        public IQueryable<Topic> GetQuery()
        {
            return context.Subjects
                .Include("Contributor");
        }

        public bool Save(ref Topic obj)
        {
            bool result = false;
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Tag))
                {
                    // Add to collection
                    context.Subjects.Add(obj);
                }
                else
                {
                    // Attach to collection
                    context.Subjects.Attach(obj);
                    context.Entry(obj).State = System.Data.EntityState.Modified;
                }

                // Commit changes
                result = context.SaveChanges() > 0;
            }

            return result;
        }

        public List<Topic> Search(string q)
        {
            return SearchQuery(q).ToList();
        }

        public IQueryable<Topic> SearchQuery(string q)
        {
            return GetQuery().Where(a => a.Tag.Contains(q) || a.Title.Contains(q) || a.Description.Contains(q));
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}