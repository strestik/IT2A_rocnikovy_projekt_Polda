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
        Pankrac pankrac = new Pankrac("Pankrác Moudrý", 0, 0, "Pankrac");
        Inventory inventory = new Inventory();


        List<Item> items = new List<Item>()
        {
            new Item("Palantír", "Nepředstavitelně magický vidoucí kámen který ruší všechnu aktivní magii.", "mahou", 862, 758, false, false),
            new Item("Sefirot", "Sefirot, je mocné magické jádro překypující přírodní magií.", "sefirot", 79, 143, true),
            new Item("Svitek prostoru", "Text schopen ovládnutí prostoru v malém okolí.", "scrollS", 1425, 186, true),
            new Item("Svitek času", "Text s mocí ovládnout čas určeného objektu.", "scrollT", 1727, 377, true),
            new Item("Lektvar divoké magie.", "Tento jektvar je vytvořen smícháním kočičího chlupu a baziliščího jedu, těch nejmagičtějších látek.", "potion", 484, 523, true),
            new Item("Grimoire", "Mocný magický svazek obsahující významná kouzla schopná rozpoutat i ty nejsilnější procesy.", "grimoire", 1195, 638, true),
            new Item("Cár pláště", "Kus mágova pláště, utrženém ve spěchu.", "textil", 1770, 941, false), // gets collectable after player becomes aware of key logic
            new Item("Rozbitý lektvar", "Vipadá to jako čerstvě uvařený a ještě čerstvěji roztříštěný lektvar.", "broken", 1108, 893, false),
        };

        private List<Hotspot> polygons = new List<Hotspot>()
        {
            new Hotspot("Magický inkust", true, 966, 902, "Magický inkust je běžně užit u smluv vázaných poutací magií.", 963, 872, 946, 872, 921, 885, 920, 901),
            new Hotspot("Alechemistická apartatura", false,  5, 476, "Alechemická apartatura", 2, 635, 498, 655, 472, 462, 292, 346),
            new Hotspot("Instrukce rituálu", true,  861, 626, "Instrukce rituálu", 1272, 626, 1303, 128, 886, 126),
            new Hotspot("Rozbitý lektvar", true,  1120, 1057, "Rozbitý lektvar", 1228, 1054, 1240, 1012, 1198, 926, 1127, 994),
            new Hotspot("Cár pláště", true,  1912, 939, "Cár pláště", 1817, 944, 1570, 1769, 1916, 1069),
            new Hotspot("Podezřelé stopy", true,  1384, 988, "Nejspíše úniková cesta hledaného zloděje. Vedou do prázdna takže se musel teleportovat pryč.", 1554, 854, 1874, 859, 1735, 999),
            // magický kruh Pečeť je pasivní kouzlo schopné schraňovt vybraný objekt mimo dosah našich protor.
            // add hotspot for every thing on návod
            // add hotspot for all five ingredients of ritual
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
            Canvas.SetZIndex(pankrac.pankrac, 997);
            pankrac.pankrac.MouseDown += Pankrac_MouseDown;
            pankrac.pankrac.Tag = pankrac;
            Canvas.SetLeft(pankrac.pankrac, -200);
            Canvas.SetTop(pankrac.pankrac, 600);

            OverlayCanvas.Children.Add(inventory.InvetoryImage);
            Canvas.SetZIndex(inventory.InvetoryImage, 1);

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

                System.Windows.Point Point0 = new System.Windows.Point(poly.XPercent0, poly.YPercent0);
                System.Windows.Point Point1 = new System.Windows.Point(poly.XPercent1, poly.YPercent1);
                System.Windows.Point Point2 = new System.Windows.Point(poly.XPercent2, poly.YPercent2);
                System.Windows.Point Point3 = new System.Windows.Point(poly.XPercent3, poly.YPercent3);
                System.Windows.Point Point4 = new System.Windows.Point(poly.XPercent4, poly.YPercent4);
                PointCollection myPointCollection = new PointCollection();
                myPointCollection.Add(Point0);
                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                myPointCollection.Add(Point4);
                poly.polygon.Points = myPointCollection;
                OverlayCanvas.Children.Add(poly.polygon);
                Canvas.SetZIndex(poly.polygon, 2);
            }
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon btn = sender as Polygon;
            Hotspot point = btn.Tag as Hotspot;

            if (point.Name == "Rozbitý lektvar" && !polygons[1].acessable )
            {
                polygons[1].acessable = true;
                MessageBox.Show("Kde se tady asi tak vzal? Vipadá to že budu potřebovat nový, z tohohle už moc nezbývá.");
            }
            if (point.Name == "Alechemistická apartatura" && point.acessable)
            {
                items[7].Collectable = true;
                if (polygons.Count > 3)
                {
                    Hotspot target = polygons[3];
                    if (target?.polygon != null)  OverlayCanvas.Children.Remove(target.polygon);
                }
                MessageBox.Show("Ahhaa - tak tady se dělají lektvary!");
            }


            MessageBox.Show(point.init);
        }
        private void Pankrac_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image btn = sender as Image;
            Pankrac point = btn.Tag as Pankrac;

            inventory.VisibilityToggle(pankrac.pankrac);
            Canvas.SetZIndex(inventory.InvetoryImage, 1);
        }


        public DateTime timer;
        public void Timer_Check()
        {
            if ((DateTime.Now - timer).TotalSeconds > 2 && heldItem != null)
            {
                heldItem.ItemImage.IsHitTestVisible = true;
                Mouse.Capture(null);
                Canvas.SetZIndex(heldItem.ItemImage, 0);
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
                    Canvas.SetZIndex(heldItem.ItemImage, 1);
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

            //MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }
    }
}