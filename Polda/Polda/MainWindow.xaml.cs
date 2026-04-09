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

namespace Polda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Object> points = new List<Object>()
        {
            new Object() { Name = "Jumbo", XPercent = 0.365, YPercent = 0.39, init = "Praha" },
            new Object() { Name = "Postel", XPercent = 0.365, YPercent = 0.39, init = "Praha" },
        };

        //void DrawPoints()
        //{
        //    OverlayCanvas.Children.Clear();

        //    foreach (var point in points)
        //    {
        //        double x = MapImage.ActualWidth * point.XPercent;
        //        double y = MapImage.ActualHeight * point.YPercent;

        //        Button btn = new Button()
        //        {
        //            Content = point.Name,
        //            Tag = point
        //        };

        //        btn.Click += Btn_Click;

        //        Canvas.SetLeft(btn, x);
        //        Canvas.SetTop(btn, y);

        //        OverlayCanvas.Children.Add(btn);
        //    }
        //}

        //private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    DrawPoints();
        //}


        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);

            double xPercent = pos.X / MapImage.ActualWidth;
            double yPercent = pos.Y / MapImage.ActualHeight;

            MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }
    }
}