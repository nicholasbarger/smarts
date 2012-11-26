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
    internal class PlanQueries : IDisposable
    {
        private SmartsDbContext context;

        public PlanQueries()
        {
            this.context = new SmartsDbContext();
        }

        public PlanQueries(SmartsDbContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            bool result = false;
            if (id > 0)
            {
                // Get curriculum
                var curriculum = Get(id);

                // If not null, delete
                if (curriculum != null)
                {
                    // Remove from db collection
                    context.Curriculums.Remove(curriculum);

                    // Save changes to db
                    result = context.SaveChanges() > 0;
                }
            }

            return result;
        }

        public Plan Get(int id)
        {
            Plan curriculum = null;
            if (id > 0)
            {
                curriculum = context.Curriculums.SingleOrDefault(a => a.Id == id);
            }

            return curriculum;
        }

        public IQueryable<Plan> GetQuery()
        {
            return context.Curriculums;
        }

        public bool Save(ref Plan obj)
        {
            bool result = false;
            if (obj != null)
            {
                if (obj.Id > 0)
                {
                    // Add to collection
                    context.Curriculums.Add(obj);
                }
                else
                {
                    // Attach to collection
                    context.Curriculums.Attach(obj);
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