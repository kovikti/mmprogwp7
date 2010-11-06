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

       

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class MyMessageDTO
    {
        string owner;
        string text;
        byte[] imageData;
        int rotation;

        [DataMember]
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        [DataMember]
        public byte[] ImageData
        {
            get { return imageData; }
            set { imageData = value; }
        }
        [DataMember]
        public int Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        

    }
}
