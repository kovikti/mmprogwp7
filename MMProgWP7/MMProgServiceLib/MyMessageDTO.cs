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
        Guid id;

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
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
