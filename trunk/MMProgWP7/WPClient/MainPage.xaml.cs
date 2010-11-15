﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WPClient.MMProgService;
using Microsoft.Phone.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Devices.Sensors;


namespace WPClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
           
        }

 

        void client_SendMessageToServerCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //TODO error handling?
            (sender as MMProgServiceClient).CloseAsync();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Devices.Sensors.Accelerometer acc = new Microsoft.Devices.Sensors.Accelerometer();
            acc.ReadingChanged += new EventHandler<Microsoft.Devices.Sensors.AccelerometerReadingEventArgs>(acc_ReadingChanged);
            accEvent.Reset();
            acc.Start();
            Microsoft.Phone.Tasks.CameraCaptureTask capture = new Microsoft.Phone.Tasks.CameraCaptureTask();
            capture.Completed += new EventHandler<Microsoft.Phone.Tasks.PhotoResult>(capture_Completed);
            capture.Show();
        }
        AutoResetEvent accEvent = new AutoResetEvent(false);
        void acc_ReadingChanged(object sender, Microsoft.Devices.Sensors.AccelerometerReadingEventArgs e)
        {
            Microsoft.Devices.Sensors.Accelerometer acc = new Microsoft.Devices.Sensors.Accelerometer();
            acceleroData = e;
            accEvent.Set();
            acc.Stop();
        }
        AccelerometerReadingEventArgs acceleroData;
        void capture_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                /*byte[] data = new byte[e.ChosenPhoto.Length];
                e.ChosenPhoto.Read(data, 0, data.Length);
                int rotate = 0;
                if (accEvent.WaitOne(100))
                {
                    //signal arrived
                    //KV: See http://windowsteamblog.com/windows_phone/b/wpdev/archive/2010/09/08/using-the-accelerometer-on-windows-phone-7.aspx
                    //for accelero directions
                    if (acceleroData.Y < -0.5)
                        rotate = 90;
                    else if (acceleroData.X > 0.5)
                        rotate = 180;
                    else if (acceleroData.Y > 0.5)
                        rotate = 270;
                }*/

                int angle = ImageHelper.GetAngleFromJpegStream(e.ChosenPhoto);//HACK HERE!!!
                MemoryStream s = ImageHelper.ResampleRotateBitmapStream(e.ChosenPhoto, angle, 640, 70);
                byte[] data = s.ToArray();
                s.Close();
                int rotate = 0;

                MMProgServiceClient client = new MMProgServiceClient();
                client.SendMessageToServerCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_SendMessageToServerCompleted);
                client.SendMessageToServerAsync(new MyMessageDTO() { Owner = tbName.Text, Text = tbMessage.Text, ImageData=data });
                
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            byte[] imgdata;
            
            using (Stream fs = Application.GetResourceStream(new Uri("def.jpg",UriKind.RelativeOrAbsolute)).Stream)
            {
                MemoryStream ms = new MemoryStream();
                byte[] data = new byte[2048];
                int offset=0;
                while (fs.Position < fs.Length)
                {
                    int num=fs.Read(data,0,data.Length);
                    ms.Write(data, offset, num);
                }
                imgdata = ms.ToArray();
                ms.Dispose();
            }
            
            MMProgServiceClient client = new MMProgServiceClient();
            client.SendMessageToServerCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_SendMessageToServerCompleted);
            client.SendMessageToServerAsync(new MyMessageDTO() { Owner = tbName.Text, Text = tbMessage.Text, ImageData = imgdata, Rotation = 0 });
            

            
            

        }
    }
}