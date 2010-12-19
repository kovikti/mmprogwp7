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
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using WPClient.MMProgService;
using Microsoft.Phone.Shell;

namespace WPClient
{
    public partial class PreviewPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        private string previewType;
        private string textSource;
        private string userName;
        private static bool fullScreen;
        private WriteableBitmap img;


        //ApplicationBarIconButton btnSend;
        //ApplicationBarIconButton btnFavs;
        //ApplicationBarIconButton btnCancel;

        public WriteableBitmap Img
        {
            get { return img; }
            set { img = value; NotifyPropertyChanged("Img"); }
        }

        public string PreviewType
        {
            get { return previewType; }
            set { previewType = value; NotifyPropertyChanged("PreviewType"); }
        }


        public string UserName
        {
            get { return userName; }
            set { userName = value; NotifyPropertyChanged("UserName"); }
        }


        public string TextSource
        {
            get { return textSource; }
            set { textSource = value; NotifyPropertyChanged("TextSource"); }
        }

        public PreviewPage()
        {
            InitializeComponent();

            ////Creating an application bar and then setting visibility and menu properties.
            //ApplicationBar = new ApplicationBar();
            //ApplicationBar.IsVisible = true;
            //ApplicationBar.IsMenuEnabled =false;

            ////This code creates the application bar icon buttons.
            //btnSend = new ApplicationBarIconButton(new Uri("/Images/check.white.png", UriKind.Relative));
            //btnFavs = new ApplicationBarIconButton(new Uri("/Images/favs.white.png", UriKind.Relative));
            //btnCancel = new ApplicationBarIconButton(new Uri("/Images/back.white.png", UriKind.Relative));
       
            ////Labels for the application bar buttons.
            //btnSend.Text = "Send";
            //btnFavs.Text = "Add To Favs";
            //btnCancel.Text = "Cancel";

            ////This code adds buttons to application bar.
            //ApplicationBar.Buttons.Add(btnCancel);
            //ApplicationBar.Buttons.Add(btnFavs);
            //ApplicationBar.Buttons.Add(btnSend);



            ////This code will create event handlers for buttons.
            //btnSend.Click += new EventHandler(SendPictureButton_Click);
            //btnFavs.Click += new EventHandler(AddToFavsButton_Click);
            //btnCancel.Click += new EventHandler(CancelButton_Click);

            //btnSend.IsEnabled = true;
            //btnFavs.IsEnabled = true;
            //btnCancel.IsEnabled = true;

            Loaded += (object sender, RoutedEventArgs e) =>
            {
                if (NavigationContext.QueryString.Count > 0)
                {
                    previewType = NavigationContext.QueryString.Values.First();
                    userName = NavigationContext.QueryString.Values.ToArray()[1];
                    TextSource = NavigationContext.QueryString.Values.ToArray()[2];
                    switch (previewType)
                    {
                        case "Preview": 
                             int angle = ImageHelper.GetAngleFromJpegImage(App.CapturedImage);//HACK HERE!!!
                             img = ImageHelper.ResampleRotateBitmapToBitmap(App.CapturedImage, angle, 640, 70);
                            break;
                        case "Favorite":
                            AddTofavsButton.IsEnabled = false;
                            img = App.CapturedImage;
                            break;
                        case "Details":
                            TextPreview.IsReadOnly = true;
                            SendButton.IsEnabled = false;
                            img = App.CapturedImage;
                            break;
                    }
                    //if (previewType == "Preview")
                    //{
                    //    int angle = ImageHelper.GetAngleFromJpegImage(App.CapturedImage);//HACK HERE!!!
                    //    img = ImageHelper.ResampleRotateBitmapToBitmap(App.CapturedImage, angle, 640, 70);
                    //}
                    //else
                    //    img = App.CapturedImage;
                    LayoutRoot.DataContext = this;
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ImagePreview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (fullScreen == false)
            {
                VisualStateManager.GoToState(this, "FullScreenImage", true);
                fullScreen = true;
            }
            else
            {
                VisualStateManager.GoToState(this, "LittleImage", true);
                fullScreen = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            
        }

        private void AddToFavsButton_Click(object sender, EventArgs e)
        {

            App.Favs.Add(new MyMessageSL(UserName , TextPreview.Text, img));
            MessageBox.Show("This Message has been added to favorites!");
            //TODO actually add it to favorites
        }

        private void SendPictureButton_Click(object sender, EventArgs e)
        {
            //int angle = ImageHelper.GetAngleFromJpegImage(App.CapturedImage);//HACK HERE!!!
            //MemoryStream s = ImageHelper.ResampleRotateBitmap(App.CapturedImage, angle, 640, 70);
            MemoryStream s = ImageHelper.BitmapToMS(img, 640, 70);
            byte[] data = s.ToArray();
            s.Close();
            MMProgServiceClient client = new MMProgServiceClient();
            client.SendMessageToServerCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_SendMessageToServerCompleted);
            MyMessageDTO dto = new MyMessageDTO() { Owner = UserName, Text = this.TextPreview.Text , ImageData = data };
            dto.Id = Guid.NewGuid();
            
            client.SendMessageToServerAsync(dto);
            
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        void client_SendMessageToServerCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Message sent!");
            //TODO error handling?
            (sender as MMProgServiceClient).CloseAsync();
        }

        private void TextPreview_GotFocus(object sender, RoutedEventArgs e)
        {
            TextPreview.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextPreview_LostFocus(object sender, RoutedEventArgs e)
        {
            TextPreview.Foreground = new SolidColorBrush(Colors.White);
        }

    }
}