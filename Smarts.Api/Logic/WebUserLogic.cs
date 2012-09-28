using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Logic
{
    /// <summary>
    /// Place all business logic methods in here.
    /// </summary>
    internal class WebUserLogic
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