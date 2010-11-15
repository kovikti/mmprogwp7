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
    public class Hoster:IHoster, IDisposable
    {
        MyServiceHost host;
        public Hoster()
        {
            
            host = new MyServiceHost(typeof(MMProgServiceLib.MMProgService));
            host.Hoster=this;
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
                    AddDtoToList(dto);
                    //MyMessage msg = new MyMessage(dto); //not here! threadp problems...
                    if (MessageReceivedEvent != null)
                    {
                        MessageReceivedEvent( dto);
                    }
                }
            }
        }

        List<MyMessageDTO> dtoList = new List<MyMessageDTO>();
        object listSync = new object();

        void AddDtoToList(MyMessageDTO dto)
        {
            lock (listSync)
            {
                dtoList.Add(dto);
            }
        }

        public IList<MyMessageDTO> GetDTOUntilGuid(Guid? id, int maxnum)
        {
            List<MyMessageDTO> list = new List<MyMessageDTO>();

            lock (listSync)
            {
                int n = 0;
                int i=dtoList.Count-1;
                while (i >= 0 && id != dtoList[i].Id && n < maxnum)
                {
                    list.Add(dtoList[i]);
                    n++;
                    i--;
                }

            }
            return list;

        }




        //KV: EventHandler or Action? Do we need to know the sender? MyMessage has to be inherited from TEventArgs
        public event Action<MyMessageDTO> MessageReceivedEvent;

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
