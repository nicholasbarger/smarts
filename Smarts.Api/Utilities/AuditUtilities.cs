using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Utilities
{
    public static class AuditUtilities
    {
        public static void Log(WebUser user, ActivityEventItem activityEvent, string value)
        {
            // todo: validate inputs

            var activity = new Activity()
            {
                EventId = (int)activityEvent,
                Value = value
            };

            if (user != null)
            {
                activity.UserGuid = user.Guid;
            }

            Log(activity);
        }

        /// <summary>
        /// Log activities and related information to the Activities table.
        /// </summary>
        public static void Log(Activity activity)
        {
            // set create date
            activity.Created = DateTime.Now;

            // add to db
            using (var queries = new ActivityQueries())
            {
                queries.Save(ref activity);
            }
        }
    }
}