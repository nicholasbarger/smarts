using Smarts.Api.Models;
using Smarts.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Smarts.Api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmartServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmartServices.svc or SmartServices.svc.cs at the Solution Explorer and start debugging.
    public class SmartServices : ISmartServices
    {
        /// <summary>
        /// Log an activity.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public void Log(Activity activity)
        {
            // log through audit utility
            AuditUtilities.Log(activity);
        }
    }
}
