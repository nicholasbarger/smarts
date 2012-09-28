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
    internal class SubjectQueries : IDisposable
    {
        private SmartsDbContext context;

        public SubjectQueries(SmartsDbContext context)
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

        public Subject Get(string hashTag)
        {
            Subject subject = null;
            if (!string.IsNullOrEmpty(hashTag))
            {
                subject = context.Subjects.SingleOrDefault(a => a.Hashtag == hashTag);
            }

            return subject;
        }

        public IQueryable<Subject> GetQuery()
        {
            return context.Subjects
                .Include("Contributor");
        }

        public bool Save(ref Subject obj)
        {
            bool result = false;
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.Hashtag))
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

        public List<Subject> Search(string q)
        {
            return SearchQuery(q).ToList();
        }

        public IQueryable<Subject> SearchQuery(string q)
        {
            return GetQuery().Where(a => a.Hashtag.Contains(q) || a.Title.Contains(q) || a.Description.Contains(q));
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}