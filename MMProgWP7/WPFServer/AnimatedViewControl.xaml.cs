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
                 //sb = (Storyboard)(list[list.Count-1]);
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

            

            OldImageMesh.Positions = CalcXY(0, 0, 1000000);
            AddOldGeometryToList();
        }

        List<GeometryModel3D> historylist = new List<GeometryModel3D>();

        private void AddOldGeometryToList()
        {

            
            MeshGeometry3D geo = new MeshGeometry3D();
            //geo.Positions = new Point3DCollection();
            //geo.Positions.Add(new Point3D(0, 0, 0));
            //geo.Positions.Add(new Point3D(0, 1, 0));
            //geo.Positions.Add(new Point3D(1, 1, 0));
            //geo.Positions.Add(new Point3D(1, 0, 0));
            geo.Positions = CalcXY(BrushWidth, BrushHeight, -1);
            //geo.TriangleIndices = new Int32Collection();
            geo.TriangleIndices.Add(2);
            geo.TriangleIndices.Add(1);
            geo.TriangleIndices.Add(0);
            geo.TriangleIndices.Add(3);
            geo.TriangleIndices.Add(2);
            geo.TriangleIndices.Add(0);
            geo.Normals.Add(new Vector3D(0, 0, 1));
            geo.Normals.Add(new Vector3D(0, 0, 1));
            geo.TextureCoordinates.Add(new Point(0, 1));
            geo.TextureCoordinates.Add(new Point(0, 0));
            geo.TextureCoordinates.Add(new Point(1, 0));
            geo.TextureCoordinates.Add(new Point(1, 1));



            DiffuseMaterial mat = new DiffuseMaterial(OldDiffuseMaterial.Brush);
            mat.Brush.Opacity =0.5;
            //mat.Brush = new SolidColorBrush(Colors.Wheat);
         
           

            GeometryModel3D model = new GeometryModel3D(geo, mat);
           


             historylist.Insert(0,model);
            
            //model3DGroup.Children.Add(model);
            while (historylist.Count > 5)
            {
                historylist.RemoveAt(historylist.Count - 1);
            }
            for (int i = 0; i < historylist.Count; i++)
            {
                double ofset = 1.2;
                
                RotateTransform3D rot = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), +30));//-20
                TranslateTransform3D tt1 = new TranslateTransform3D(0,0, -0.1-i*ofset);//1, 0.5+i*ofset, +2+i*ofset); -0.5-i*ofset
                Transform3DGroup trg = new Transform3DGroup();
                TranslateTransform3D tt2 = new TranslateTransform3D(+1.2, 0, 0);//-0.5
                trg.Children.Add(tt1);
                trg.Children.Add(rot);
                trg.Children.Add(tt2);
                
                

                historylist[i].Transform=trg;
                
                //model3DGroup.Children.RemoveAt(2+i);//.Remove((Model3D)historylist[i]);

                

            }
            //model3DGroup.Children.Add(historylist[0]);
            model3DGroup.Children.Clear();
            for (int i = 0; i < historylist.Count; i++)
            {
                if (i < 5)
                {
                    model3DGroup.Children.Add(historylist[i]);
                }
            }
            model3DGroup.Children.Add(new AmbientLight(Colors.White));
            model3DGroup.Children.Add(OldModel);
            model3DGroup.Children.Add(NewModel);
            //OldImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -100);
            //NewImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -100);
            
            
        }

        private void viewPort_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetImagePoints();
        }
    }
}
