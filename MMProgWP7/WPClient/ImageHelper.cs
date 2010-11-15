using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;

namespace WPClient
{
    public class ImageHelper
    {

        static void  Exchange(ref double a, ref double b)
        {
            double tmp = a;
            a = b;
            b = tmp;
        }

        public static int GetAngleFromJpegStream(Stream stream)
        {
            //TODO
            return 90;
        }

        public static MemoryStream ResampleRotateBitmapStream(Stream sInput, int angle, double longSide,int quality)
        {

            BitmapImage BIsource = new BitmapImage();
            BIsource.SetSource(sInput);
            double wSource=BIsource.PixelWidth;
            double hSource=BIsource.PixelHeight;

            double scale=(double)longSide/Math.Max(wSource,hSource);

            double wDest=scale*wSource;
            double hDest=scale*hSource;

            TransformGroup tGroup=new TransformGroup();
            

            ScaleTransform tScale=new ScaleTransform();
            tScale.CenterX=0;
            tScale.CenterY=0;
            tScale.ScaleX=scale;
            tScale.ScaleY=scale;

            tGroup.Children.Add(tScale);

            TranslateTransform tTranslate1=new TranslateTransform();
            tTranslate1.X=-wDest/2;
            tTranslate1.Y=-hDest/2;

             tGroup.Children.Add(tTranslate1);
            
            RotateTransform tRotate=new RotateTransform();
            tRotate.CenterX=0;
            tRotate.CenterY=0;
            tRotate.Angle=angle;

             tGroup.Children.Add(tRotate);

            TranslateTransform tTranslate2=new TranslateTransform();
            if (angle%180!=0){
                Exchange(ref wDest,ref hDest);
            }
            tTranslate2.X=wDest/2;
            tTranslate2.Y=hDest/2;
            
            tGroup.Children.Add(tTranslate2);

            Image image=new Image();
            image.Source=BIsource;
            image.Measure(new Size(wDest,hDest));
            image.Arrange(new Rect(0,0,hDest,hDest));


            WriteableBitmap WBitmap=new WriteableBitmap((int)wDest,(int)hDest);
            WBitmap.Render(image,tGroup);
            WBitmap.Invalidate();
            
            MemoryStream ms=new MemoryStream();
            WBitmap.SaveJpeg(ms,(int)wDest,(int)hDest,0,quality);

            return ms;

        }
    }
}
