using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMProgServiceLib;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.ComponentModel;

namespace WPFServer
{
    public class MyMessage: INotifyPropertyChanged //DependencyObject
    {
        public MyMessage()
        {
            this.Text = "Text";
            this.Owner = "Owner";
            this.Image = new BitmapImage(new Uri("def.jpg",UriKind.RelativeOrAbsolute));
            this.id = new Guid();
        }

        public MyMessage(MyMessageDTO orig)
        {
            this.Owner = orig.Owner;
            this.Text = orig.Text;
            this.id = orig.Id;
            this.Image = CreateImage(orig.ImageData);
           // this.Image = new BitmapImage(new Uri("def.jpg", UriKind.RelativeOrAbsolute));
        }


        string owner;
        string text;
        BitmapImage image;
        Guid id;

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
        

        //public string Owner { get; set; }
        //public string Text { get; set; }
        //public BitmapSource Image { get; set; }
         /*public string Owner
         {
             get { return (string)GetValue(OwnerProperty); }
             set { SetValue(OwnerProperty, value); }
         }

         // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
         public static readonly DependencyProperty OwnerProperty =
             DependencyProperty.Register("Owner", typeof(string), typeof(MyMessage), new UIPropertyMetadata(0));



         public string Text
         {
             get { return (string)GetValue(TextProperty); }
             set { SetValue(TextProperty, value); }
         }

         // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
         public static readonly DependencyProperty TextProperty =
             DependencyProperty.Register("Text", typeof(string), typeof(MyMessage), new UIPropertyMetadata(0));




         public BitmapSource Image
         {
             get { return (BitmapSource)GetValue(ImageProperty); }
             set { SetValue(ImageProperty, value); }
         }

         // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
         public static readonly DependencyProperty ImageProperty =
             DependencyProperty.Register("Image", typeof(BitmapSource), typeof(MyMessage), new UIPropertyMetadata(0));
        */
         

        BitmapImage CreateImage(byte[] data)
        {
            BitmapImage img = null;
            try
            {
                img = new BitmapImage();

                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.CreateOptions = BitmapCreateOptions.None;
                /*var fs=File.Open(@"d:\temp\pp\p.bmp",FileMode.Create);
                fs.Write(ImageData,0,ImageData.Length);
                fs.Close();*/
                MemoryStream ms = new MemoryStream(data);
                ms.Seek(0, SeekOrigin.Begin);
                img.StreamSource = ms;
                /*System.Windows.Media.Imaging.Rotation rot = System.Windows.Media.Imaging.Rotation.Rotate0;
                switch (Rotation)
                {

                    case 90: rot = System.Windows.Media.Imaging.Rotation.Rotate90;
                        break;
                    case 180: rot = System.Windows.Media.Imaging.Rotation.Rotate180;
                        break;
                    case 270: rot = System.Windows.Media.Imaging.Rotation.Rotate270;
                        break;
                    default: rot = System.Windows.Media.Imaging.Rotation.Rotate0;
                        break;
                }
                img.Rotation = rot;*/
                img.EndInit();
                //KV: Actually this is still funny, how it manages memory...
                img.Freeze();
                ms.Close();
            }
            catch { }
            return img;
        }


        public MyMessageDTO ToMyMessageDTO()
        {
            MyMessageDTO dto = new MyMessageDTO();

            dto.Owner = Owner;
            dto.Text = Text;
            dto.Id = Id;
            byte[] data = null;
            Stream s = Image.StreamSource;
            if (s != null && s.Length>0)
            {
                s.Read(data, 0, (Int32)s.Length);
            }
            dto.ImageData = data;

            return dto;

        }
        

       
    }
}
