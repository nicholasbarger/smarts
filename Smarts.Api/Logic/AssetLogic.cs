using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Logic
{
    public class AssetLogic
    {
        public void SetDefaults(ref Asset asset)
        {
            asset.Created = DateTime.Now;
        }
    }
}