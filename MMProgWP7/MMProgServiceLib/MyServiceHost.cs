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
        //Or have a reference to the appropriate object+interface!... or through singleton... lots of options
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


        IHoster hoster;

        public IHoster Hoster
        {
            get { return hoster; }
            set { hoster = value; }
        }



        
    }
}
