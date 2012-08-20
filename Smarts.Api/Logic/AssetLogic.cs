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
    internal class AssetLogic
    {
        /// <summary>
        /// Set the default values when creating a new educational asset.
        /// </summary>
        /// <param name="asset"></param>
        public void SetDefaults(ref Asset asset)
        {
            asset.Created = DateTime.Now;
        }
    }
}