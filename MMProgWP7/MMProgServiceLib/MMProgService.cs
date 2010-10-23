using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMProgServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MMProgService : IMMProgService
    {



        #region IMMProgService Members

        public void SendMessageToServer(MyMessage message)
        {
            MyServiceHost host = OperationContext.Current.Host as MyServiceHost;
            if (host != null)
            {
                host.FireEvent("MessageReceived");
            }
            
        }

        #endregion
    }
}
