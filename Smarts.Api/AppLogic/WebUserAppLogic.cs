using Smarts.Api.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class WebUserAppLogic
    {
        WebUserBusinessLogic business;

        public WebUserAppLogic()
        {
            // create reference to business logic
            business = new WebUserBusinessLogic();
        }
    }
}