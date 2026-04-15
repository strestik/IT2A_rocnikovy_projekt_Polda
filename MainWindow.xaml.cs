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
using static System.Formats.Asn1.AsnWriter;

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
            Loaded += MainWindow_Loaded;
        }

        
        Random rnd = new Random();
        Hotspot activePoint;
        List<Hotspot> points = new List<Hotspot>()
        {                       
            new Hotspot() { Name = "Jumbo", XPercent = 0.9, YPercent = 0.2, init = "Jumbo" },
            new Hotspot() { Name = "Postel", XPercent = 1, YPercent = 0.5, init = "Postel" },
        };

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPoints();
        }

        void DrawPoints()
        {
            OverlayCanvas.Children.Clear();

            foreach (var point in points)
            {
                double x = MapImage.ActualWidth * point.XPercent;
                double y = MapImage.ActualHeight * point.YPercent;

                Button btn = new Button()
                {
                    Content = point.Name,
                    Tag = point
                };

                btn.Click += Btn_Click;

                Canvas.SetLeft(btn, x);
                Canvas.SetTop(btn, y);

                OverlayCanvas.Children.Add(btn);

                //Add the Polygon Element
                Polygon myPolygon = new Polygon();
                myPolygon.Stroke = System.Windows.Media.Brushes.Black;
                myPolygon.Fill = System.Windows.Media.Brushes.LightSeaGreen;
                myPolygon.StrokeThickness = 2;
                myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
                myPolygon.VerticalAlignment = VerticalAlignment.Center;
                System.Windows.Point Point1 = new System.Windows.Point(1, 50);
                System.Windows.Point Point2 = new System.Windows.Point(10, 80);
                System.Windows.Point Point3 = new System.Windows.Point(50, 50);
                PointCollection myPointCollection = new PointCollection();
                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                myPolygon.Points = myPointCollection;
                OverlayCanvas.Children.Add(myPolygon);
            }
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Hotspot point = btn.Tag as Hotspot;
            MessageBox.Show(point.init);

            //if (point == activePoint)
            //{
            //    MessageBox.Show("Správně!");
            //}
            //else
            //{
            //    MessageBox.Show($"Špatně!");
            //}

            //round++;
            //NextRound();
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }


        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);

            double xPercent = pos.X / MapImage.ActualWidth;
            double yPercent = pos.Y / MapImage.ActualHeight;

            MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }
    }
}