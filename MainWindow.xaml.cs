using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Zadanie1_Lista2
{

    public class GradientLiniowy
    {
        public double niePrzezroczystosc = 1;
        public LinearGradientBrush pedzelWypelnienia = new LinearGradientBrush();
        public LinearGradientBrush Liniowy(Point p1, Point p2, double canvasWidth, double canvasHeight)
        {
            Point punktPoczatkowy = new Point((p1.X * 100) / canvasWidth / 100, (p1.Y * 100) / canvasHeight / 100);
            Point punktKoncowy = new Point((p2.X * 100) / canvasWidth / 100, (p2.Y * 100) / canvasHeight / 100);
            pedzelWypelnienia.StartPoint = punktPoczatkowy;
            pedzelWypelnienia.EndPoint = punktKoncowy;
            pedzelWypelnienia.Opacity = niePrzezroczystosc;
            return pedzelWypelnienia;
        }
    }


    public class GradientRadialny
    {
        public double zaokragleniePoX = 0.5;
        public double zaokragleniePoy = 0.5;
        private double niePrzezroczystosc = 1;
        public RadialGradientBrush pedzelWypelnienia = new RadialGradientBrush();

        public RadialGradientBrush Radialny(Point punkt1, Point punkt2, double canvasWidth, double canvasHeight)
        {
            Point punktCentralny = new Point((punkt1.X * 100) / canvasWidth / 100, (punkt1.Y * 100) / canvasHeight / 100);
            Point punktodniesienia = new Point((punkt2.X * 100) / canvasWidth / 100, (punkt2.Y * 100) / canvasHeight / 100);
            pedzelWypelnienia.Center = punktCentralny;
            pedzelWypelnienia.GradientOrigin = punktodniesienia;
            pedzelWypelnienia.RadiusX = zaokragleniePoX;
            pedzelWypelnienia.RadiusY = zaokragleniePoy;
            pedzelWypelnienia.Opacity = niePrzezroczystosc;
            return pedzelWypelnienia;
        }
    }


    public partial class MainWindow : Window
    {
        public GradientLiniowy wypelnienieLiniowy = new GradientLiniowy();
        public GradientRadialny wypelnienieRadialny = new GradientRadialny();
        public int licznik = 0;
        public Point lastClick;
        Point punkt1 = new Point();
        Point punkt2 = new Point();
      
        public MainWindow()
        {
            InitializeComponent();
            canvas.ClipToBounds = true;
            radioLiniowy_Checked(this, new RoutedEventArgs());
        }
      

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouseClick = e.MouseDevice.GetPosition(canvas);

            if (licznik == 0) punkt1 = mouseClick;
            if (licznik == 1)
            {
                punkt2 = mouseClick;
                if (radioLiniowy.IsChecked == true)
                {
                    Rectangle rec = tworzRec();
                    canvas.Children.Clear();
                    wypelnienieLiniowy = new GradientLiniowy();
                    rec.Fill = wypelnienieLiniowy.Liniowy(punkt1, punkt2,canvas.ActualWidth,canvas.ActualHeight);
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromRgb(100, 100, 100);
                    rec.Fill = brush;
                    canvas.Children.Add(rec);
                }

                if (radioRadialny.IsChecked == true)
                {
                    Ellipse ellipse = tworzEllipse();
                    canvas.Children.Clear();
                    wypelnienieRadialny = new GradientRadialny();
                    ellipse.Fill = wypelnienieRadialny.Radialny(punkt1, punkt2, canvas.ActualWidth, canvas.ActualHeight);
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromRgb(100, 100, 100);
                    ellipse.Fill = brush;
                    canvas.Children.Add(ellipse);
                }


            }
            if (licznik >= 2)
            {
              lastClick = mouseClick;
              color window = new color(this);
              window.Show();
            }
            licznik++;
        }

        public Rectangle tworzRec()
        {
            Rectangle r = new Rectangle();
            if (canvas.ActualWidth < 1) r.Width = 2500;
            else r.Width = canvas.ActualWidth;
            if(canvas.ActualHeight < 1) r.Height = canvas.Height;
            else r.Height = canvas.ActualWidth;
            return r;
        }

        public Ellipse tworzEllipse()
        {
            Ellipse el = new Ellipse();
            if (canvas.ActualWidth < 1) el.Width = 2500;
            else el.Width = canvas.ActualWidth;
            el.Height = 189;

            return el;
        }

        public void rysuj()
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(100, 100, 100);

            if (radioLiniowy.IsChecked == true)
            {

                Rectangle r = tworzRec();
                if (wypelnienieLiniowy.pedzelWypelnienia.GradientStops.Count == 0) r.Fill = brush;
                else r.Fill = wypelnienieLiniowy.pedzelWypelnienia;
                canvas.Children.Clear();
                canvas.Children.Add(r);
            }
            else
            {
                Ellipse e = tworzEllipse();
                if (wypelnienieRadialny.pedzelWypelnienia.GradientStops.Count == 0) e.Fill = brush;
                else e.Fill = wypelnienieRadialny.pedzelWypelnienia;
                canvas.Children.Clear();
                canvas.Children.Add(e);
            }
        }

        private void radioRadialny_Checked(object sender, RoutedEventArgs e)
        {
            licznik = 0;
            canvas.Children.Clear();
            Ellipse ellipse = tworzEllipse(); 
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(100, 100, 100);
            ellipse.Fill = brush;
            canvas.Children.Add(ellipse);
        }

        private void radioLiniowy_Checked(object sender, RoutedEventArgs e)
        {
            licznik = 0;
            canvas.Children.Clear();
            Rectangle rec = tworzRec();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromRgb(100, 100, 100);
            rec.Fill = brush;
            canvas.Children.Add(rec);
        }

        private void radiusX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                wypelnienieRadialny.pedzelWypelnienia.RadiusX = double.Parse(radiusX.Text);
            }
            catch (FormatException) { }
        }

        private void RadiusY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                wypelnienieRadialny.pedzelWypelnienia.RadiusY = double.Parse(radiusY.Text);
            }
              catch (FormatException) { }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                wypelnienieRadialny.pedzelWypelnienia.Opacity = double.Parse(opacityTxt.Text);
                wypelnienieLiniowy.pedzelWypelnienia.Opacity = double.Parse(opacityTxt.Text);
            }
            catch (FormatException) { }
        }

    }
}
