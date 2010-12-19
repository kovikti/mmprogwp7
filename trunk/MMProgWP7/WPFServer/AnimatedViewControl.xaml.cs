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

        //Calculate vertexes for image W, H, and defined Z
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
                NewImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -0.0002);
            }

        }
        double BrushWidth;
        double BrushHeight;

        //Update brush
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
                 //sb = (Storyboard)(list[list.Count-1]);//Test last effect
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




        private void AddOldGeometryToList_()
        {
            MeshGeometry3D geo = new MeshGeometry3D();
            geo.Positions = CalcXY(BrushWidth, BrushHeight, -1);


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
            mat.Brush.Opacity = 0.4;




            GeometryModel3D model = new GeometryModel3D(geo, mat);

            for (int i = 0; i < model3DGroup.Children.Count;i++ )
            {
                GeometryModel3D m = model3DGroup.Children[i] as GeometryModel3D;
                if (m != null)
                {
                    if (m.Transform == null)
                    {
                        m.Transform = new Transform3DGroup();
                    }
                    Transform3DGroup tg = m.Transform as Transform3DGroup;
                    //animation: move 1..n back

                    //animation, move 1. back

                    //add continous moving animation to the 1.
                }
            }


           
            //OldImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -100);
            //NewImageMesh.Positions = CalcXY(BrushWidth, BrushHeight, -100);


        }

        //Update history, etc
        private void AddOldGeometryToList()
        {
            MeshGeometry3D geo = new MeshGeometry3D();
            geo.Positions = CalcXY(BrushWidth, BrushHeight, -1);
            
           
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
            mat.Brush.Opacity =0.4;
            //mat.Brush = new SolidColorBrush(Colors.Wheat);
         
           

            GeometryModel3D model = new GeometryModel3D(geo, mat);
           


             historylist.Insert(0,model);
            
            //model3DGroup.Children.Add(model);
            while (historylist.Count > 10)
            {
                historylist.RemoveAt(historylist.Count - 1);
            }
            Random r = new Random();
            int animNum = r.Next(8);
            for (int i = 0; i < historylist.Count; i++)
            {
                double ofset = 2.1;

                RotateTransform3D rot = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), -15));//+30
                TranslateTransform3D tt1 = new TranslateTransform3D(0, 0, -0.1 - i * ofset);//1, 0.5+i*ofset, +2+i*ofset); -0.5-i*ofset
                Transform3DGroup trg = new Transform3DGroup();
                TranslateTransform3D tt2 = new TranslateTransform3D(+0.4, -0.5 + i / 2.0, 0);//1.2  -0.4+i/4.0
                trg.Children.Add(tt1);
                trg.Children.Add(rot);
                trg.Children.Add(tt2);


                

                
                //if (r.NextDouble() > 0.8)
                //if (i==2)
                if (i==animNum)
                {
                    TranslateTransform3D tt3 = new TranslateTransform3D();
                    DoubleAnimation day = new DoubleAnimation(0, 1 , new Duration(TimeSpan.FromSeconds(2)));
                    //day.BeginTime = TimeSpan.FromSeconds(Math.Cos(i) / 3);
                    day.BeginTime = TimeSpan.FromSeconds(0.5);
                    day.AutoReverse = true;
                    //day.RepeatBehavior = RepeatBehavior.Forever;
                    day.AccelerationRatio = 0.1;
                    day.DecelerationRatio = 0.1;
                    tt3.BeginAnimation(TranslateTransform3D.OffsetYProperty, day);
                    //model.Transform = tt3;
                    trg.Children.Add(tt3);
                    /*if (model.Transform==null || (!((model.Transform is Transform3DGroup)))){
                        model.Transform=new Transform3DGroup();
                    }

                    if (model.Transform is Transform3DGroup)
                    {
                        ((Transform3DGroup)model.Transform).Children.Add( tt3);
                    }*/
                }

                /*
                DoubleAnimation day = new DoubleAnimation(-0.5 * Math.Cos(i) - i / 5.0, 0.5 * Math.Cos(i) + i / 5.0, new Duration(TimeSpan.FromSeconds(5)));
                //day.BeginTime = TimeSpan.FromSeconds(Math.Cos(i) / 3);
                day.AutoReverse = true;
                day.RepeatBehavior = RepeatBehavior.Forever;
                day.AccelerationRatio = 0.2;
                day.DecelerationRatio = 0.2;
                DoubleAnimation daz = new DoubleAnimation(-0.2 * Math.Cos(i), 0.2 * Math.Cos(i), new Duration(TimeSpan.FromSeconds(5)));
                daz.AutoReverse = true;
                daz.RepeatBehavior = RepeatBehavior.Forever;
                daz.AccelerationRatio = 0.2;
                daz.DecelerationRatio = 0.2;
                DoubleAnimation dax = new DoubleAnimation(-0.2 * Math.Cos(i), 0.2 * Math.Cos(i), new Duration(TimeSpan.FromSeconds(5)));
                dax.AutoReverse = true;
                dax.RepeatBehavior = RepeatBehavior.Forever;
                dax.AccelerationRatio = 0.2;
                dax.DecelerationRatio = 0.2;
                tt3.BeginAnimation(TranslateTransform3D.OffsetYProperty,day);
                tt3.BeginAnimation(TranslateTransform3D.OffsetZProperty, daz);
                tt3.BeginAnimation(TranslateTransform3D.OffsetZProperty, dax);*/



               

                historylist[i].Transform = trg;

            }
            model3DGroup.Children.Clear();
            for (int i = 0; i < historylist.Count; i++)
            {
                if (i < 8)
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
