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
            hoster = new Hoster();
            hoster.MessageReceivedEvent += new Action<MyMessageDTO>(hoster_MessageReceivedEvent);

        }
        Hoster hoster;

        BitmapSource GetBitmapOfVisual(Visual vis)
        {
            try
            {

                Rect r = new Rect(0, 0, 300, 100);//
                //Rect r = VisualTreeHelper.GetDescendantBounds(vis);
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
        }

        void SetNewMessageView(MyMessage msg)
        {
            if (msg != null)
            {
                MessageControl msgc = new MessageControl();
                msgc.SetMessage(msg);
                //msgc.Measure(new Size(300, 300));
                //msgc.Width = animatedViewControl1.ActualWidth;
                //msgc.Height = animatedViewControl1.ActualHeight;
                
                msgc.MyMeasure();
                VisualBrush vb = new VisualBrush(msgc);
                vb.Stretch = Stretch.Fill;
                vb.AlignmentX = AlignmentX.Center;
                vb.AlignmentY = AlignmentY.Center;
                
                //animatedViewControl1.SetNewBrush(vb, msgc.Width, msgc.Height);
                animatedViewControl1.SetNewBrush(vb, msgc.ActualWidth, msgc.ActualHeight);
            }
        }

        void hoster_MessageReceivedEvent(MyMessageDTO obj)
        {
            
            //KV: Is this actually they way it is usually done?
            Dispatcher.Invoke(
                new Action(() =>
                {
                    MyMessage msg = new MyMessage(obj);
                    lvMessages.Items.Add(msg);
                    lvMessages.ScrollIntoView(msg);
                    //MessageControl msgc = new MessageControl();
                    //msgc.SetMessage(msg);
                    //animatedViewControl1.SetNewBrush(new VisualBrush(msgc), msgc.Width, msgc.Height);
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


    }
}
