using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.BusinessLogic
{
    public class WebUserBusinessLogic
    {
        /// <summary>
        /// Set the default values when creating a new educational asset.
        /// </summary>
        /// <param name="user"></param>
        public void SetDefaults(ref WebUser user)
        {
            user.Created = DateTime.Now;
            user.Guid = new Guid();
            user.IsActive = true;
            user.IsLockedOut = false;
        }
    }
}