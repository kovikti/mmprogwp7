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
using System.ComponentModel;

namespace WPFServer
{
    /// <summary>
    /// Interaction logic for MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl, INotifyPropertyChanged
    {
        public MessageControl()
        {
            InitializeComponent();
        }
        MyMessage mymsg=new MyMessage();
        public MyMessage myMessage
        {
            get { return mymsg; }
            set
            {
                if (value != mymsg)
                {
                    mymsg = value;
                    NotifyPropertyChanged("myMessage");
                }
            }
        }
        protected void NotifyPropertyChanged(String propertyName)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

        public event PropertyChangedEventHandler PropertyChanged;

        //string test;
        //public string Test
        //{
        //    get
        //    {
        //        return test;
        //    }
        //    set
        //    {
        //        if (test != value)
        //        {
        //            test = value;
        //            NotifyPropertyChanged("Test");
        //        }
        //    }
        //}


        public void SetMessage(MyMessage msg)
        {
            myMessage = msg;
            //Test = msg.Owner;
            //mymsg.Text = msg.Text;
            //mymsg.Owner = msg.Owner;
        }


       
    }
}
