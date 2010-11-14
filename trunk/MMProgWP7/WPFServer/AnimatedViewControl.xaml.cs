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
using System.Collections;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace WPFServer
{
    /// <summary>
    /// Interaction logic for AnimatedViewControl.xaml
    /// </summary>
    public partial class AnimatedViewControl : UserControl
    {
        public AnimatedViewControl()
        {
            InitializeComponent();
        }
        Storyboard sb;

        Point3DCollection CalcXY(double imw, double imh, double z)
        {

            Point3DCollection points = new Point3DCollection();
            Point3D a, b, c, d;


            double imasp = imw / imh;
            double vpasp = viewPort.ActualWidth / viewPort.ActualHeight;
            double wres, hres;

            if (imasp >= vpasp)
            {
                wres = 1;
                hres = 1 / imasp;
            }
            else
            {
                hres = 1 / vpasp;
                wres = imasp * hres;
            }

            double xres, yres;

            xres = wres * -0.5;
            yres = hres * -0.5;
            a = new Point3D(xres, yres, z);
            b = new Point3D(xres, yres + hres, z);
            c = new Point3D(xres + wres, yres + hres, z);
            d = new Point3D(xres + wres, yres, z);

            points.Add(a);
            points.Add(b);
            points.Add(c);
            points.Add(d);
            return points;
        }


        void SetImagePoints()
        {
            //double w, h;
            if (OldDiffuseMaterial.Brush!=null)
            //if (OldImageBrush.ImageSource != null)
            {

                OldImageMesh.Positions = CalcXY(0, 0, -1000000);
            }
            //if (NewImageBrush.ImageSource != null)
            if (NewDiffuseMaterial.Brush != null)
            {

                //w = (NewImageBrush.ImageSource as BitmapSource).Width;
                //h = (NewImageBrush.ImageSource as BitmapSource).Height;
                //w=(NewDiffuseMaterial.Brush as VisualBrush).Visual.
                //w = 400;
                //h = 300;
                NewImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -0.0002);
            }

        }
        double BrushWidth;
        double BrushHeight;
        public void SetNewBrush(Brush brush, double w, double h)
        {
            OldDiffuseMaterial.Brush = NewDiffuseMaterial.Brush;
            NewDiffuseMaterial.Brush = brush;
            BrushWidth = w;
            BrushHeight = h;
            //OldImageBrush.ImageSource = NewImageBrush.ImageSource;

            //NewImageBrush.ImageSource = image;

            SetImagePoints();
           
            if (OldDiffuseMaterial.Brush != null)
            {
                
                OldImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -0.0001);
            }
             Random r = new Random();



             ArrayList list = (Resources["Storyboards"] as ArrayList);

             if (sb == null)
             {
                 sb = (Storyboard)(list[r.Next(0, list.Count)]);

                 if (sb != null)
                     sb.Begin(this);
             }



        }


        private void Storyboard_Completed(object sender, EventArgs e)
        {
            if (sb != null)
            {
                sb.SkipToFill();
                sb.Stop();
                sb = null;
            }

            OldImageMesh.Positions = CalcXY(0, 0, -1000000);

        }

        private void viewPort_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetImagePoints();
        }
    }
}
