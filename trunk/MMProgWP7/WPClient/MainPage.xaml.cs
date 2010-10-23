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
using WPClient.MMProgServiceReference;



namespace WPClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MMProgServiceClient client = new MMProgServiceClient();
            client.SendMessageToServerCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_SendMessageToServerCompleted);
            client.SendMessageToServerAsync(new MyMessage() { Owner = "Me", Text = "Message text" });
            
        }

        void client_SendMessageToServerCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            (sender as MMProgServiceClient).CloseAsync();
        }
    }
}