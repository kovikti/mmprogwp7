﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMProgServiceLib;
using System.Windows.Media.Imaging;
using System.IO;

namespace WPFServer
{
    public class MyMessage:MyMessageDTO
    {
        public MyMessage(MyMessageDTO orig)
        {
            this.Owner = orig.Owner;
            this.Text = orig.Text;
            this.ImageData = orig.ImageData;
        }

        public BitmapImage Image
        {
            get
            {
                BitmapImage img = new BitmapImage();

                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.CreateOptions = BitmapCreateOptions.None;
                /*var fs=File.Open(@"d:\temp\pp\p.bmp",FileMode.Create);
                fs.Write(ImageData,0,ImageData.Length);
                fs.Close();*/
                MemoryStream ms = new MemoryStream(ImageData);
                ms.Seek(0,SeekOrigin.Begin);
                img.StreamSource = ms;
                System.Windows.Media.Imaging.Rotation rot = System.Windows.Media.Imaging.Rotation.Rotate0;
                switch (Rotation)
                {
                    
                    case 90: rot = System.Windows.Media.Imaging.Rotation.Rotate90;
                        break;
                    case 180: rot=System.Windows.Media.Imaging.Rotation.Rotate180;
                        break;
                    case 270: rot=System.Windows.Media.Imaging.Rotation.Rotate270;
                        break;
                     default: rot=System.Windows.Media.Imaging.Rotation.Rotate0;
                        break;
                }
                img.Rotation = rot;
                img.EndInit();
                 //KV: Actually this is still funny, how it manages memory...
                img.Freeze();
                ms.Close();
                return img;
            }
        }
    }
}