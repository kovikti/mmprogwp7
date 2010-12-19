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



        public void SetMessage(MyMessage msg)
        {
            myMessage = msg;

        }

        public void MyMeasure()
        {
            Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Arrange(new Rect(0, 0, Width, Height));
        }
       
    }


}
