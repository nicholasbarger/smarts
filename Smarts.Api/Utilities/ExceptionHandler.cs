using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smarts.Api.Utilities
{
    /// <summary>
    /// A centralized exception handler for logging, error handing, and routing of exceptions.
    /// </summary>
    public static class ExceptionHandler
    {
        // for now, I am just going to write to the db activities table
        // in the future, I may want to have this optional route to different exception handling locations
        // such as the event viewer, db, txt file, email, or some combination.
        public static void Log(Exception ex)
        {
            // Prepare message
            var sb = new StringBuilder();
            sb.Append(ex.Message);
            if (ex.InnerException != null)
            {
                sb.Append(" - " + ex.InnerException);
            }

            // See if user is available through cookie
            WebUser user = new WebUser();
            var context = HttpContext.Current;
            if (context != null && context.Request != null && context.Request.Cookies["uid"] != null)
            {
                user.Guid = new Guid(context.Request.Cookies["uid"].Value);
            }

            // Log entry to db
            AuditUtilities.Log(user, Models.ActivityEventItem.Error, sb.ToString());
        }
    }
}