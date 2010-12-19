using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using MMProgServiceLib;
using System.Windows.Threading;

namespace WPFServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //instantiate service hoster
            hoster = new Hoster();
            hoster.MessageReceivedEvent += new Action<MyMessageDTO>(hoster_MessageReceivedEvent);
            
            //history slideshow timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(6);
            timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
        }
        


        Hoster hoster;
        DispatcherTimer timer;

        /*BitmapSource GetBitmapOfVisual(Visual vis)
        {
            try
            {

                Rect r = new Rect(0, 0, 300, 100);
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)r.Width, (int)r.Height, 96, 96, PixelFormats.Pbgra32);

                DrawingVisual visual = new DrawingVisual();
                DrawingContext context = visual.RenderOpen();
                VisualBrush brush = new VisualBrush(vis);
                context.DrawText(new FormattedText("ppp", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, new SolidColorBrush(Colors.Red)), new Point(0, 0));
                context.DrawRectangle(brush, null, new Rect(new Point(), r.Size));
                bmp.Render(visual);
                bmp.Freeze();
                return bmp;
            }
            catch { }
            return null;
        }*/

        MyMessage lastMessage = null;

        //Show a new message in the 3D viewport
        void SetNewMessageView(MyMessage msg)
        {
            if (msg != null && lastMessage!=msg)
            {
                MessageControl msgc = new MessageControl();
                msgc.SetMessage(msg);       
                msgc.MyMeasure();
                VisualBrush vb = new VisualBrush(msgc);
                vb.Stretch = Stretch.Fill;
                vb.AlignmentX = AlignmentX.Center;
                vb.AlignmentY = AlignmentY.Center;         
                animatedViewControl1.SetNewBrush(vb, msgc.ActualWidth, msgc.ActualHeight);
                lastMessage = msg;
            }
        }

        void hoster_MessageReceivedEvent(MyMessageDTO obj)
        {
            
            Dispatcher.Invoke(
                new Action(() =>
                {
                    MyMessage msg = new MyMessage(obj);
                    lvMessages.Items.Add(msg);
                    lvMessages.ScrollIntoView(msg);
                    //enable slideshow after 10 secs
                    DisableRandomMessages();
                    EnableRandomMessagesAfter(10);
                    SetNewMessageView(msg);

                   
                }), System.Windows.Threading.DispatcherPriority.Normal);
        
        }

        private void lvMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMessages.SelectedItem != null)
            {
                SetNewMessageView(lvMessages.SelectedItem as MyMessage);
            }
        }


        void timer_Tick(object sender, EventArgs e)
        {
            SetNewMessageView(GetRandomMessage());
        }
        //MyMessage lastRandomMessage = null;
        MyMessage GetRandomMessage()
        {
            Random r = new Random();
            int i = (int)(r.NextDouble() * lvMessages.Items.Count);
            MyMessage selected=(MyMessage)lvMessages.Items[i];
            return selected;
        }

        DispatcherTimer enableTimer;
        void EnableRandomMessagesAfter(int seconds)
        {
            DisableRandomMessages();
            enableTimer = new DispatcherTimer();
            enableTimer.Interval = TimeSpan.FromSeconds(seconds);
            enableTimer.Tick += new EventHandler(enableTimer_Tick);
            enableTimer.Start();
        }
        void DisableRandomMessages()
        {
            timer.Stop();
            if (enableTimer != null)
            {
                enableTimer.Stop();
            }
        }
        void enableTimer_Tick(object sender, EventArgs e)
        {
            if (enableTimer != null)
            {
                enableTimer.Stop();
            }
            timer.Start();
        }

    }
}
