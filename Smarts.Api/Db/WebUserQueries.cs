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
    internal class WebUserQueries : IDisposable
    {
        private SmartsDbContext context;

        public WebUserQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public bool Delete(Guid guid)
        {
            bool result = false;
            if (guid != null)
            {
                // Get user
                var user = Get(guid);

                // If not null, set inactive and save
                if (user != null)
                {
                    // Set user to inactive
                    user.IsActive = false;

                    // Save user
                    result = Save(ref user);
                }
            }

            return result;
        }

        public WebUser Get(Guid guid)
        {
            WebUser user = null;
            if (guid != null)
            {
                user = context.WebUsers.SingleOrDefault(a => a.Guid == guid);
            }

            return user;
        }

        public IQueryable<WebUser> GetQuery()
        {
            return context.WebUsers.Where(a => a.IsActive == true);
        }

        public bool Save(ref WebUser obj)
        {
            bool result = false;
            if (obj != null)
            {
                if (obj.Guid != null)
                {
                    // Add to collection
                    context.WebUsers.Add(obj);
                }
                else
                {
                    // Attach to collection
                    context.WebUsers.Attach(obj);
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