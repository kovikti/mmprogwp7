using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MMProgServiceLib
{
    public class MyServiceHost:ServiceHost
    {
        public MyServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {

        }
        public void FireEvent(string eventName, object data)
        {
            if (EventFired != null)
            {
                EventFired.BeginInvoke(eventName,data, 
                    delegate(IAsyncResult iar){
                        EventFired.EndInvoke(iar);
                    }
                    , null);
                
            }
        }
        public event Action<string,object> EventFired; 
        
    }
}
