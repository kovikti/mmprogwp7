using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MMProgServiceLib;
using System.ServiceModel.Description;
using System.Windows;

namespace WPFServer
{
    public class Hoster:IDisposable
    {
        MyServiceHost host;
        public Hoster()
        {
            
            host = new MyServiceHost(typeof(MMProgServiceLib.MMProgService));
            host.EventFired += new Action<string,object>(host_EventFired);
           try{
                host.Open();
            }
            catch { 
                //TODO error handling, notification
            }
            
        }

        void host_EventFired(string eventName,object param)
        {
            if (eventName == "MessageReceived")
            {
                MyMessageDTO dto = param as MyMessageDTO;
                if (dto != null)
                {
                    MyMessage msg = new MyMessage(dto);
                    if (MessageReceivedEvent != null)
                    {
                        MessageReceivedEvent( msg);
                    }
                }
            }
        }
        //KV: EventHandler or Action? Do we need to know the sender? MyMessage has to be inherited from TEventArgs
        public event Action<MyMessage> MessageReceivedEvent;

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                host.Close();
            }
            catch { }
        }

        #endregion
    }
}
