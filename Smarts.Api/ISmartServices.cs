using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Smarts.Api
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmartServices" in both code and config file together.
    [ServiceContract]
    public interface ISmartServices
    {
        [OperationContract]
        void Log(Activity activity);
    }
}
