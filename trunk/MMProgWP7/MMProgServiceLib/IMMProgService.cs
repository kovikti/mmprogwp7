using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace MMProgServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMMProgService
    {
        [OperationContract]
        void SendMessageToServer(MyMessageDTO message);

        [OperationContract]
        IList<MyMessageDTO> GetNewMessages(Guid? lastGuid, int maxnum);//nullable?

        // TODO: Add your service operations here
    }


}
