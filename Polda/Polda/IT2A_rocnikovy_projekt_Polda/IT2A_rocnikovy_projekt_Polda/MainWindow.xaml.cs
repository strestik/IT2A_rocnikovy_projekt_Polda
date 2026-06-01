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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace IT2A_rocnikovy_projekt_Polda
{
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        Item? heldItem;
        Pankrac pankrac = new Pankrac("Pankrác Moudrý", 300, 900, "Pankrac");
        Inventory inventory = new Inventory();


        List<Item> items = new List<Item>()
        {
            new Item("magická věcička", "Description of wand", "mahou", 862, 758, false, false),
            new Item("sefirot", "Description of sefirot", "sefirot", 93, 138, true),
            new Item("space scroll", "Description of scroll", "scrollS", 1384, 164, true),
            new Item("time scroll", "Description of scroll", "scrollT", 1674, 360, true),
            new Item("potion", "Description of potion", "potion", 484, 523, true),
            new Item("grimoire", "Description of grimoire", "grimoire", 1195, 638, true),
            new Item("textile", "Description of textile", "textil", 1770, 941, false), // gets collectable after player becomes aware of key logic
            new Item("broken potion", "Description of broken potion", "broken", 1108, 893, false),
        };

        private List<Hotspot> polygons = new List<Hotspot>()
        {
            new Hotspot("Jumbo", 1, 1, "Jumbo"),
            new Hotspot("Postel", 0.5, 1.5, "Postel")
        };
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();

            OverlayCanvas.Children.Add(pankrac.pankrac);
            pankrac.pankrac.MouseDown += Pankrac_MouseDown;
            pankrac.pankrac.Tag = pankrac;

            OverlayCanvas.Children.Add(inventory.InvetoryImage);

            foreach (Item item in items)
            {
                item.ItemImage.Tag = item;
                OverlayCanvas.Children.Add(item.ItemImage);
                item.ItemImage.MouseDown += Item_MouseDown;
            }
            inventory.InvetoryImage.MouseDown += Inventory_MouseDown;
            OverlayCanvas.MouseUp += Item_MouseUp;

            OverlayCanvas.MouseMove += MouseMove;
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(MapImage);
            if (heldItem != null)
            {
                Canvas.SetLeft(heldItem.ItemImage, pos.X);
                Canvas.SetTop(heldItem.ItemImage, pos.Y);
            }
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }


        void Draw()
        {
            OverlayCanvas.Children.Clear();

            foreach (var poly in polygons)
            {
                poly.polygon.MouseDown -= Polygon_MouseDown;
                poly.polygon.MouseDown += Polygon_MouseDown;
                poly.polygon.Tag = poly;

                System.Windows.Point Point1 = new System.Windows.Point(770 * poly.XPercent, 210 * poly.YPercent);
                System.Windows.Point Point2 = new System.Windows.Point(742 * poly.XPercent, 310 * poly.YPercent);
                System.Windows.Point Point3 = new System.Windows.Point(746 * poly.XPercent, 513 * poly.YPercent);
                System.Windows.Point Point4 = new System.Windows.Point(814 * poly.XPercent, 573 * poly.YPercent);
                System.Windows.Point Point5 = new System.Windows.Point(814 * poly.XPercent, 313 * poly.YPercent);
                PointCollection myPointCollection = new PointCollection();
                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                myPointCollection.Add(Point4);
                myPointCollection.Add(Point5);
                poly.polygon.Points = myPointCollection;
                OverlayCanvas.Children.Add(poly.polygon);
            }
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon btn = sender as Polygon;
            Hotspot point = btn.Tag as Hotspot;
            MessageBox.Show(point.init);

        }
        private void Pankrac_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image btn = sender as Image;
            Pankrac point = btn.Tag as Pankrac;

            inventory.VisibilityToggle(pankrac.pankrac);
        }


        public DateTime timer;
        public void Timer_Check()
        {
            if ((DateTime.Now - timer).TotalSeconds > 2 && heldItem != null)
            {
                heldItem.ItemImage.IsHitTestVisible = true;
                Mouse.Capture(null);
                heldItem.JumpToSpawn();
                heldItem = null;

            }
        }
        private void Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //var pos = e.GetPosition(MapImage);

            //double xPercent = pos.X;
            //double yPercent = pos.Y;


            if (heldItem != null) heldItem.ItemImage.IsHitTestVisible = true;
            Image btn = sender as Image;
            Item point = btn.Tag as Item;

            Mouse.Capture(OverlayCanvas);
            heldItem = point;
            if (heldItem.Collectable)
            {
                Canvas.SetZIndex(heldItem.ItemImage, 999);
                inventory.RemoveItem(heldItem);
                if (heldItem != null) heldItem.ItemImage.IsHitTestVisible = false;
                timer = DateTime.Now;
            }
            else
            {
                heldItem = null;
                Mouse.Capture(null);
            }
            

        }

        private void Item_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Timer_Check();
            timer = DateTime.Now;
            var pos = e.GetPosition(MapImage);

            //double xPercent = pos.X;
            //double yPercent = pos.Y;
            //Image btn = sender as Image;
            //Item point = btn.Tag as Item;


            double invLeft = Canvas.GetLeft(inventory.InvetoryImage);
            double invTop = Canvas.GetTop(inventory.InvetoryImage);
            if (pos.X > invLeft && pos.X < invLeft + inventory.InvetoryImage.ActualWidth &&
                pos.Y > invTop && pos.Y < invTop + inventory.InvetoryImage.ActualHeight)
            {
                if (heldItem != null && heldItem.Collectable)
                {
                    if (heldItem != null) heldItem.ItemImage.IsHitTestVisible = true;
                    Mouse.Capture(null);
                    inventory.AddItem(heldItem);
                    Canvas.SetZIndex(heldItem.ItemImage, 0);
                    heldItem = null;
                }
            }
        }
        private void Inventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);

            double xPercent = pos.X;
            double yPercent = pos.Y;
            
        }
        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            inventory.VisibilityOff((pankrac.pankrac));
            var pos = e.GetPosition(MapImage);  

            double xPercent = pos.X;
            double yPercent = pos.Y;

            pankrac.move(xPercent, yPercent);

            MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }
    }
}