using System;
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
using System.IO.IsolatedStorage;
using System.Threading;
using Microsoft.Devices.Sensors;
using Microsoft.Phone;
using System.Windows.Threading;
using System.Collections.ObjectModel;


namespace WPClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        public DispatcherTimer timer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            FavsList.ItemsSource = App.Favs;
            timer = new DispatcherTimer();

            listBox1.ItemsSource = App.newMessages;

            tbName.Text = App.MyName;
        
            timer.Tick += new EventHandler(timer_Tick);
            
        }

        

        static Guid? lastReceivedGuid = null;
 

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

                //KV: See http://windowsteamblog.com/windows_phone/b/wpdev/archive/2010/09/08/using-the-accelerometer-on-windows-phone-7.aspx
                //for accelero directions


                App.CapturedImage = PictureDecoder.DecodeJpeg(e.ChosenPhoto);


                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    

                    string name = tbName.Text;
                    string text = tbMessage.Text;
                    NavigationService.Navigate(new Uri("/PreviewPage.xaml?PreviewType=Preview&name=" + name + "&Text=" + text, UriKind.Relative));
                }


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
            MyMessageDTO dto = new MyMessageDTO() { Owner = tbName.Text, Text = tbMessage.Text, ImageData = imgdata };
            dto.Id = Guid.NewGuid();
            client.SendMessageToServerAsync(dto);
            

            
            

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MMProgServiceClient client = new MMProgServiceClient();
            client.GetNewMessagesCompleted += new EventHandler<GetNewMessagesCompletedEventArgs>(client_GetNewMessagesCompleted);
            client.GetNewMessagesAsync(lastReceivedGuid, 5);
            
        }

        void client_GetNewMessagesCompleted(object sender, GetNewMessagesCompletedEventArgs e)
        {
            foreach (var item in e.Result)
            {
                MyMessageSL mymsg = new MyMessageSL(item);
                listBox1.Items.Add(mymsg);
                lastReceivedGuid = mymsg.Id;
            }
            
        }

        void ShowSelectedItemDetails()
        {
            if (listBox1.SelectedItem != null)
            {
                MyMessageSL message = (listBox1.SelectedItem as MyMessageSL);
                App.CapturedImage = new System.Windows.Media.Imaging.WriteableBitmap(message.Image);
                NavigationService.Navigate(new Uri("/PreviewPage.xaml?PreviewType=Details&name=" + message.Owner + "&Text=" + message.Text, UriKind.Relative));
            }
            else
                MessageBox.Show("Please select an item in the list above");
        }

        private void buttonDetails_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            ShowSelectedItemDetails();
        }

        private void buttonRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (FavsList.SelectedItem != null)
            {
                MyMessageSL message = (FavsList.SelectedItem as MyMessageSL);
                App.Favs.Remove(message);
            }
            else
                MessageBox.Show("Please select an item in the list above");
        }

        void ShowSelectedFavItemDetails()
        {
            if (FavsList.SelectedItem != null)
            {
                MyMessageSL message = (FavsList.SelectedItem as MyMessageSL);
                App.CapturedImage = new System.Windows.Media.Imaging.WriteableBitmap(message.Image);
                NavigationService.Navigate(new Uri("/PreviewPage.xaml?PreviewType=Favorite&name=" + message.Owner + "&Text=" + message.Text, UriKind.Relative));
            }
            else
                MessageBox.Show("Please select an item in the list above");
        }
        private void buttonFavsDetails_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowSelectedFavItemDetails();
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem pi = (MainPivot.SelectedItem as PivotItem);
            if (pi.Name == "PItemView")
            {
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Start();
            }
            else
            {
                
                timer.Stop();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(5000);
            ListBox lb = listBox1;
            MMProgServiceClient client = new MMProgServiceClient();
            client.GetNewMessagesCompleted += new EventHandler<GetNewMessagesCompletedEventArgs>(
                delegate(object sender2, GetNewMessagesCompletedEventArgs e2)
            {
                foreach (var item in e2.Result)
                {
                    MyMessageSL mymsg = new MyMessageSL(item);
                    App.newMessages.Add(mymsg);
                    lastReceivedGuid = mymsg.Id;
                }
            }
            );
            client.GetNewMessagesAsync(lastReceivedGuid, 5);
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
             App.MyName=tbName.Text ;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSelectedItemDetails();
        }

        private void FavsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSelectedFavItemDetails();
        }
       
    }
}