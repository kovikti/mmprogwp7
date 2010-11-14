using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MMProgServiceLib
{

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
