using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.ComponentModel;
using WPClient.MMProgService;

namespace WPClient
{
    public class MyMessageSL: INotifyPropertyChanged //DependencyObject
    {
        public MyMessageSL()
        {
            this.Text = "Text";
            this.Owner = "Owner";
            //TODO
            //this.Image = new BitmapImage(new Uri("def.jpg",UriKind.RelativeOrAbsolute));
            this.id = Guid.NewGuid();
        }

        public MyMessageSL(MyMessageDTO orig)
        {
            this.Owner = orig.Owner;
            this.Text = orig.Text;
            this.id = orig.Id;

            CreateImage(orig.ImageData);
           // this.Image = new BitmapImage(new Uri("def.jpg", UriKind.RelativeOrAbsolute));
        }

        public MyMessageSL(string owner, string text, BitmapImage image)
        {
            this.owner = owner;
            this.text = text;
            this.image = image;
        }


        string owner;
        string text;
        BitmapImage image;
        Guid id;
        Stream BitmapStream;

        public string Owner
        {
            get { return owner; }
            set
            {
                if (owner != value)
                {
                    owner = value;
                    NotifyPropertyChanged("Owner");
                }
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        public Guid Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        

         

        void CreateImage(byte[] data)
        {
            
            try
            {
                Image = new BitmapImage();
                MemoryStream ms=new MemoryStream(data);
                BitmapStream = ms;
                Image.SetSource(ms);
                //ms.Close();
            }
            catch { }
            
        }


        public MyMessageDTO ToMyMessageDTO()
        {
            MyMessageDTO dto = new MyMessageDTO();

            dto.Owner = Owner;
            dto.Text = Text;
            dto.Id = Id;
            byte[] data = null;
            
            if (BitmapStream != null && BitmapStream.Length>0)
            {
                BitmapStream.Read(data, 0, (Int32)BitmapStream.Length);
            }
            dto.ImageData = data;

            return dto;

        }
        

       
    }
}
