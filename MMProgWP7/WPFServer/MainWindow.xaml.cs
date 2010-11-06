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
            hoster.MessageReceivedEvent += new Action<MyMessage>(hoster_MessageReceivedEvent);

        }
        Hoster hoster;


        void hoster_MessageReceivedEvent(MyMessage obj)
        {
            //KV: Is this actually they way it is usually done?
            Dispatcher.Invoke(
                new Action(() =>
                {
                    lvMessages.Items.Add(obj);
                }), System.Windows.Threading.DispatcherPriority.Normal);
        
        }


    }
}
