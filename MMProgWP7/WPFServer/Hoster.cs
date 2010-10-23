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
            host.EventFired += new Action<string>(host_EventFired);
           try{
                host.Open();
            }
            catch { 
                //TODO error handling, notification
            }
            
        }

        void host_EventFired(string obj)
        {
            MessageBox.Show(obj);
        }

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
