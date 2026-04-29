using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static System.Formats.Asn1.AsnWriter;

namespace Polda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private List<Hotspot> polygons = new List<Hotspot>()
        {
            new Hotspot("Jumbo", 0.9, 0.2, "Jumbo"),
            new Hotspot("Postel", 1.0, 0.5, "Postel")
        };

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPoints();
        }

        void DrawPoints()
        {
            OverlayCanvas.Children.Clear();

            foreach (var poly in polygons)
            {
                poly.polygon.MouseDown += Polygon_MouseDown;
                poly.polygon.Tag = poly;

                System.Windows.Point Point1 = new System.Windows.Point(456 * poly.XPercent, 870 * poly.YPercent);
                System.Windows.Point Point2 = new System.Windows.Point(545 * poly.XPercent, 570 * poly.YPercent);
                System.Windows.Point Point3 = new System.Windows.Point(50 * poly.XPercent, 550 * poly.YPercent);
                PointCollection myPointCollection = new PointCollection();
                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                poly.polygon.Points = myPointCollection;
                OverlayCanvas.Children.Add(poly.polygon);//
            }
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon btn = sender as Polygon;
            Hotspot point = btn.Tag as Hotspot;
            MessageBox.Show("aaaaaaaaaaaaaaaa");

        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }


        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);

            double xPercent = pos.X;
            double yPercent = pos.Y;

            MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
            MessageBox.Show($"Tam nic není.");
        }
    }
}