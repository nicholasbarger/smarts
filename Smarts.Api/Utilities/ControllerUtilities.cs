using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Utilities
{
    public class ControllerUtilities
    {
        public Guid GetWebUserGuidFromCookies()
        {
            // construct user
            Guid contributor = Guid.Empty;

            // get cookie from requestor if applicable
            var cookie = HttpContext.Current.Request.Cookies["userid"];
            if (cookie != null)
            {
                contributor = new Guid(cookie.Value);
            }
            else
            {
                // todo: remove this - it is just temp for testing
                contributor = new Guid("38A52BE4-9352-453E-AF97-5C3B448652F0");
            }

            return contributor;
        }
    }
}