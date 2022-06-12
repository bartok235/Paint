using System;

using System.Windows;

using System.Windows.Media;

using System.Windows.Shapes;

namespace Zadanie1_Lista2
{
    /// <summary>
    /// Logika interakcji dla klasy color.xaml
    /// </summary>
    public partial class color : Window
    {
        private Double my_R = 0;
        private Double my_G = 0;
        private Double my_B = 0;
        private Color myColor = new Color();
        MainWindow mainWindow;
      
        public color(MainWindow mw)
        {
            InitializeComponent();
            myColor = Color.FromRgb(0, 0, 0);
            mainWindow = mw;
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            my_R = slider1.Value;
            myCanvas.Background = new SolidColorBrush(createColor());
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            my_G = slider2.Value;
            myCanvas.Background = new SolidColorBrush(createColor());
        }

        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            my_B = slider3.Value;
            myCanvas.Background = new SolidColorBrush(createColor());
        }

        private Color createColor()
        {
            Color color = new Color()
            {
                A = 255,
                R = (byte)my_R,
                G = (byte)my_G,
                B = (byte)my_B
            };
            return color;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            myColor = createColor();
            Rectangle rec = mainWindow.tworzRec();
            Ellipse ellipse = mainWindow.tworzEllipse();
            mainWindow.canvas.Children.Clear();
            if (mainWindow.radioRadialny.IsChecked==true)
            {
                mainWindow.wypelnienieRadialny.pedzelWypelnienia.GradientStops.Add(new GradientStop(myColor, (mainWindow.lastClick.X * 100) / mainWindow.canvas.ActualWidth / 100));
                ellipse.Fill = mainWindow.wypelnienieRadialny.pedzelWypelnienia;
                mainWindow.canvas.Children.Add(ellipse);
            }
            if (mainWindow.radioLiniowy.IsChecked == true)
            {
                mainWindow.wypelnienieLiniowy.pedzelWypelnienia.GradientStops.Add(new GradientStop(myColor, (mainWindow.lastClick.X * 100) / mainWindow.canvas.ActualWidth / 100));
                rec.Fill = mainWindow.wypelnienieLiniowy.pedzelWypelnienia;
                mainWindow.canvas.Children.Add(rec);
            }
            this.Close();
        }

        public Color getColor()
        {
            return myColor;
        }

    }
}
