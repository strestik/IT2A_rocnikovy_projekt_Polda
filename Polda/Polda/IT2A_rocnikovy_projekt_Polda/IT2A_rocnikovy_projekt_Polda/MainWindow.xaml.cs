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
    // TODO
    //      - menu
    //      - návod
    //      - zprávy
    //      - go back tlačítko
    //      - přidat aspoň druhou scénu (místnost s teleportem, odtud zjistí jak použít grimoair) nový xamel?
    //      - prohra na čas?

    // future TODO - animace chození
    //             - animace sbírání věcí
    //             - animace používání věcí
    //             - animace interakce s hotspoty
    //             - konstantní animace hýbajících se věcí (např. plameny na svíčkách, vířící se lektvar atd.)
    //             - animace návodu jako rozbalujícího se svitku
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        Item? heldItem;
        Pankrac pankrac = new Pankrac("Pankrác Moudrý", 0, 0, "Pankrac");
        Inventory inventory = new Inventory();
        bool canPlace = true;
        int itemsToBePlaced = 6;

        private List<Hotspot> polygons = new List<Hotspot>()
        {
            new Hotspot("Rituální kruh", false,  524, 921, "ritual.mp3", "Kruh obsahuje pečetící magii. Pečeť je pasivní kouzlo schopné schraňovt vybraný objekt mimo dosah našich smyslů.", 629, 819, 838, 763, 1077, 763, 1357, 870, 1361, 980, 1192, 1075, 707, 1077),
            new Hotspot("Magický inkoust", false, 966, 902, "inkoust.mp3", "Magický inkoust je běžně užit u smluv vázaných poutací magií. Tvoří vazbu na jeho uživatele, která je prakticky nezlomitelná, ale prý se dá obejít.", 963, 872, 946, 872, 921, 885, 920, 901),
            new Hotspot("Místo pro lektvar", false,  1157, 1050, "lektvarMisto.mp3", "Místo pro lektvar", 1209, 1035, 1183, 1007, 1130, 1019),
            new Hotspot("Místo pro kouzlo z Grimoireu", false,  702, 993, "grimoireMisto.mp3", "Místo pro kouzlo z Grimoireu", 756, 1003, 762, 1054, 696, 1042),
            new Hotspot("Místo pro Sefirot", false,  625, 817, "sefirotMisto.mp3", "Místo pro Sefirot", 671, 830, 670, 856, 610, 857),
            new Hotspot("Místo pro Svitek času", false,  916, 763, "svitekTMisto.mp3", "Místo pro Svitek času", 976, 762, 979, 802, 917, 799),
            new Hotspot("Místo pro Svitek prostoru", false,  1219, 816, "svitekSMisto.mp3", "Místo pro Svitek prostoru", 1285, 821, 1293, 856, 1227, 855),
            new Hotspot("Alechemistická apartatura", false,  5, 476, "aparatura.mp3", "Alechemická apartatura", 2, 698, 498, 655, 472, 462, 292, 346),
            new Hotspot("Instrukce rituálu", true,  861, 626, "instrukce.mp3", "Instrukce rituálu. Jsou zde nakresleny nějaké symboly a znaky, které mohou být důležité pro provedení rituálu. Jsou u nich nějaké malé texty. Měl bych se podívat.", 1272, 626, 1303, 128, 886, 126),
            new Hotspot("Rozbitý lektvar", false,  1120, 1057, "rozbit.mp3", "Rozbitý lektvar. Měl bych ho odebrat, než se někdo zraní. Odkud se tak asi vzal?", 1228, 1054, 1240, 1012, 1198, 926, 1127, 994),
            new Hotspot("Cár pláště", true,  1912, 939, "car.mp3", "Cár pláště", 1817, 944, 1570, 1769, 1916, 1069),
            new Hotspot("Podezřelé stopy", true,  1384, 988, "stopy.mp3", "Nejspíše úniková cesta hledaného zloděje. Vedou do prázdna, takže se musel teleportovat pryč.", 1554, 854, 1874, 859, 1735, 999),
            new Hotspot("Ingredience", false, 1072, 546, "ingredience.mp3", "Kombinace fantastických matérií tvořící bájnou substanci schopnou dočasného spojení našeho světa se světem magickým.", 1083, 459, 1243, 451, 1242, 542),
            new Hotspot("Spouštěč", false, 1035, 538, "spoustec.mp3", "Silný výboj magické moci schopný uvést do pohybu ty nejnáročnější procesí.", 888, 543, 943, 423, 1004, 459, 993, 480),
            new Hotspot("Zdroj", false, 947, 386, "zdroj.mp3", "Baterie plná magické energie, schopná pohánět nekonečné čarovné inkantace.", 883, 324, 945, 257, 1010, 323),
            new Hotspot("Časovač", false, 1053, 213, "casovac.mp3", "Nástroj s mocí ovládnout čas a schopnost volně jím proplouvat.", 1054, 301, 1119, 302, 1119, 212),
            new Hotspot("Směrovač", false, 1150, 269, "smerovac.mp3", "Instrument schonpný ovládání prostoru a nemožného přenosu v něm.", 1158, 390, 1261, 385, 1262, 273),
            new Hotspot("?", false, 1007, 469, "spojeni.mp3", "Spojení všech instrukcí v rituálu tvořící nepředstavitelně mocné zakletí.", 995, 345, 1085, 307, 1153, 359, 1164, 453),
            new Hotspot("Exit", false,  0, 1080, "exit.mp3", "Východ.", 130, 1080, 130, 715, 0, 715),
            new Hotspot("Druhá místnost", true, 1920, 900, "mistnost.mp3", "Vchod do druhé místnosti.", 1920, 100, 1820, 100, 1820, 900),
            // add empty props
        };

        List<Item> items = new List<Item>()
        {
            new Item("Palantír", "Nepředstavitelně magický vidoucí kámen, který ruší všechnu aktivní magii.", "mahou", 862, 758, "palatir.mp3", false, false),
            new Item("Sefirot", "Sefirot je mocné magické jádro překypující přírodní magií.", "sefirot", 79, 143, "sefirot.mp3", true),
            new Item("Svitek prostoru", "Text schopen ovládnutí prostoru v malém okolí.", "scrollS", 1425, 186, "svitekS.mp3", true),
            new Item("Svitek času", "Text s mocí ovládnout čas určeného objektu.", "scrollT", 1727, 377, "svitekT.mp3", true),
            new Item("Lektvar divoké magie", "Tento jektvar je vytvořen smícháním kočičího chlupu a baziliščího jedu, těch nejmagičtějších látek vůbec.", "potion", 484, 523, "lektvar.mp3", true),
            new Item("Grimoire", "Mocný magický svazek obsahující významná kouzla schopná rozpoutat i ty nejsilnější bouře kouzel.", "grimoire", 1195, 638, "grimoire.mp3", true, true, false),
            new Item("Cár pláště", "Kus mágova pláště, utrženém ve spěchu.", "textil", 1770, 941, "plast.mp3", false),
            new Item("Rozbitý lektvar", "Vypadá to jako čerstvě uvařený a ještě čerstvěji roztříštěný lektvar.", "broken", 1108, 893, "rozbity.mp3", false),
        };


        MediaPlayer background = new MediaPlayer() { Volume = 0.02 };
        MediaPlayer text = new MediaPlayer() { Volume = 1.4 };

        public MainWindow()
        {
            InitializeComponent();
            background.MediaEnded += (s, e) => { background.Position = TimeSpan.Zero; background.Play(); };
            background.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music", "back.mp3")));
            text.MediaOpened += (s, e) => text.Play();
            Loaded += MainWindow_Loaded;
        }



        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            background.Play();

            OverlayCanvas.Children.Add(pankrac.pankrac);
            Canvas.SetZIndex(pankrac.pankrac, 997);
            pankrac.pankrac.MouseDown += Pankrac_MouseDown;
            pankrac.pankrac.Tag = pankrac;
            Canvas.SetLeft(pankrac.pankrac, -200);
            Canvas.SetTop(pankrac.pankrac, 600);

            OverlayCanvas.Children.Add(inventory.InvetoryImage);
            Canvas.SetZIndex(inventory.InvetoryImage, 2);

            foreach (Item item in items)
            {
                item.ItemImage.Tag = item;
                OverlayCanvas.Children.Add(item.ItemImage);
                Canvas.SetZIndex(item.ItemImage, 2);
                if (item.Name == "Rozbitý lektvar") Canvas.SetZIndex(item.ItemImage, 0);
                if (item.Name == "Cár pláště") Canvas.SetZIndex(item.ItemImage, 0);
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
                if (poly?.polygon == null)
                    continue;

                poly.polygon.MouseDown -= Polygon_MouseDown;
                poly.polygon.MouseDown += Polygon_MouseDown;
                poly.polygon.Tag = poly;

                System.Windows.Point Point0 = new System.Windows.Point(poly.XPercent0, poly.YPercent0);
                System.Windows.Point Point1 = new System.Windows.Point(poly.XPercent1, poly.YPercent1);
                System.Windows.Point Point2 = new System.Windows.Point(poly.XPercent2, poly.YPercent2);
                System.Windows.Point Point3 = new System.Windows.Point(poly.XPercent3, poly.YPercent3);
                System.Windows.Point Point4 = new System.Windows.Point(poly.XPercent4, poly.YPercent4);
                System.Windows.Point Point5 = new System.Windows.Point(poly.XPercent5, poly.YPercent5);
                System.Windows.Point Point6 = new System.Windows.Point(poly.XPercent6, poly.YPercent6);
                System.Windows.Point Point7 = new System.Windows.Point(poly.XPercent7, poly.YPercent7);
                PointCollection myPointCollection = new PointCollection();
                myPointCollection.Add(Point0);
                myPointCollection.Add(Point1);
                myPointCollection.Add(Point2);
                myPointCollection.Add(Point3);
                myPointCollection.Add(Point4);
                myPointCollection.Add(Point5);
                myPointCollection.Add(Point6);
                myPointCollection.Add(Point7);
                poly.polygon.Points = myPointCollection;
                if (poly == polygons[2] || poly == polygons[3] || poly == polygons[4] || poly == polygons[5] || poly == polygons[6] || poly == polygons[18]) continue;
                OverlayCanvas.Children.Add(poly.polygon);
                Canvas.SetZIndex(poly.polygon, 1);
            }
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Polygon btn) || !(btn.Tag is Hotspot point))
                return;

            var pos = e.GetPosition(MapImage);
            pankrac.move(pos.X, pos.Y);

            text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", point.audioPath)));
            text.Play();
            MessageBox.Show(point.init);

            if (point.Name == "Rozbitý lektvar" && !polygons[7].acessable )
            {
                polygons[7].acessable = true;
                MessageBox.Show("Kde se tady asi tak vzal? Vypadá to, že budu potřebovat nový. Z tohohle už kromě střepů moc nezbývá.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "brokePotion.mp3")));
                text.Play();
            }
            if (point.Name == "Alechemistická apartatura" && point.acessable)
            {
                items[7].Collectable = true;
                if (polygons.Count > 9)
                {
                    Hotspot target = polygons[9];
                    if (target?.polygon != null)  OverlayCanvas.Children.Remove(target.polygon);
                }
                Canvas.SetZIndex(items[7].ItemImage, 998);
                MessageBox.Show("Ahhaa - tak tady se dělají lektvary!");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "aparature.mp3")));
                text.Play();
            }

            if (!point.acessable && point.Name == "Ingredience" || point.Name == "Spouštěč" || point.Name == "Zdroj" || point.Name == "Časovač" || point.Name == "Směrovač" || point.Name == "?")
            {
                point.acessable = true;
                polygons[8].init = "Rituální instrukce se znaky a popisky.";
            }
            if (!point.acessable && point.Name == "Rituální kruh" || point.Name == "Magický inkust" || point.Name == "Cár pláště") 
            {
                point.acessable = true;
            }

            if (polygons[12].acessable && polygons[13].acessable && polygons[14].acessable && polygons[15].acessable && polygons[16].acessable)
            {
                polygons[2].acessable = true;
                Canvas.SetZIndex(polygons[2].polygon, 2);
                polygons[3].acessable = true;
                Canvas.SetZIndex(polygons[3].polygon, 2);
                polygons[4].acessable = true;
                Canvas.SetZIndex(polygons[4].polygon, 2);
                polygons[5].acessable = true;
                Canvas.SetZIndex(polygons[5].polygon, 2);
                polygons[6].acessable = true;
                Canvas.SetZIndex(polygons[6].polygon, 2);
                MessageBox.Show("Vypadá to, že potřebuju najít všechny tyto předměty pro zprovoznění rituálu.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "ingredients.mp3")));
                text.Play();

            }
            if (polygons[0].acessable && polygons[1].acessable && polygons[17].acessable && polygons[10].acessable)
            {
                MessageBox.Show("Nejspíš musím rozestavět předměty z návodu na ten magický kruh podle jejich rozložení na návodu.");
                MessageBox.Show("Vypadá to, že náš zloděj se rozhodl ukrýt palantír na bezpečné místo, aby se pro něj mohl vrátit, až odejdu.");
                MessageBox.Show("Nejspíš použil rituální pečetící magii, aby ho poslal do vnějších prostor mimo naši existenci.");
                MessageBox.Show("Do těchto míst se normálně nedá dostat, jen převelice mocnými čáry.");
                MessageBox.Show("A vzhledem k tomu, že použil inkoust, má na odcizený předmět dosah jen on.");
                MessageBox.Show("Ale při psaní smlouvy bylo účinkům inkoustu vystaveno celé jejich tělo a já jsem už dříve našel kus odtrženého pláště.");
                MessageBox.Show("Mohl bych ho zkusit použít jako alternativu pro klíč k rituálu.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "contemplation.mp3")));
                text.Play();
                // zde případný časovač na vypršení spojení cáru pláště s pečetí
                if (polygons.Count > 10)
                {
                    Hotspot target = polygons[10];
                    if (target?.polygon != null) OverlayCanvas.Children.Remove(target.polygon);
                }
                items[6].Collectable = true;
                Canvas.SetZIndex(items[6].ItemImage, 2);
                polygons[0].acessable = false;
                polygons[1].acessable = false;
            }
            if (polygons[2].acessable && polygons[3].acessable && polygons[4].acessable && polygons[5].acessable && polygons[6].acessable && canPlace)
            {
                canPlace = false;
                OverlayCanvas.Children.Add(polygons[2].polygon);
                OverlayCanvas.Children.Add(polygons[3].polygon);
                OverlayCanvas.Children.Add(polygons[4].polygon);
                OverlayCanvas.Children.Add(polygons[5].polygon);
                OverlayCanvas.Children.Add(polygons[6].polygon);
            }

            if (point.Name == "Místo pro lektvar" && inventory.Items.Any(i => i != null && i.Name == "Lektvar divoké magie"))
            {
                inventory.RemoveItem(inventory.Items.First(i => i != null && i.Name == "Lektvar divoké magie"));
                polygons[2].acessable = false;
                Canvas.SetZIndex(polygons[2].polygon, 2);
                MessageBox.Show("Vypadá to, že lektvar patří sem. Měl bych ho sem dát."); 
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "potionMisto.mp3")));
                text.Play();

                items[4].ItemImage.Visibility = Visibility.Visible;
                Canvas.SetLeft(items[4].ItemImage, polygons[2].XPercent0 - 80);
                Canvas.SetTop(items[4].ItemImage, polygons[2].YPercent0 - 160);
                itemsToBePlaced--;
            }
            if (point.Name == "Místo pro kouzlo z Grimoireu" && inventory.Items.Any(i => i != null && i.Name == "Grimoire"))
            {
                inventory.RemoveItem(inventory.Items.First(i => i != null && i.Name == "Grimoire"));
                polygons[3].acessable = false;
                Canvas.SetZIndex(polygons[3].polygon, 2);
                MessageBox.Show("Vypadá to, že grimoire patří sem. Měl bych ho sem dát.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "grimoireMisto.mp3")));
                text.Play();

                items[5].ItemImage.Visibility = Visibility.Visible;
                Canvas.SetLeft(items[5].ItemImage, polygons[3].XPercent0 - 70);
                Canvas.SetTop(items[5].ItemImage, polygons[3].YPercent0 - 90);
                itemsToBePlaced--;
            }
            if (point.Name == "Místo pro Sefirot" && inventory.Items.Any(i => i != null && i.Name == "Sefirot"))
            {
                inventory.RemoveItem(inventory.Items.First(i => i != null && i.Name == "Sefirot"));
                polygons[4].acessable = false;
                Canvas.SetZIndex(polygons[4].polygon, 2);
                MessageBox.Show("Vypadá to, že Sefirot patří sem. Měl bych ho sem dát.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "sefirotMisto.mp3")));
                text.Play();

                items[1].ItemImage.Visibility = Visibility.Visible;
                Canvas.SetLeft(items[1].ItemImage, polygons[4].XPercent0 - 80);
                Canvas.SetTop(items[1].ItemImage, polygons[4].YPercent0 - 70);
                itemsToBePlaced--;
            }
            if ((point.Name == "Místo pro Svitek času") && inventory.Items.Any(i => i != null && i.Name == "Svitek času")) 
            {
                inventory.RemoveItem(inventory.Items.First(i => i != null && i.Name == "Svitek času"));
                polygons[5].acessable = false;
                Canvas.SetZIndex(polygons[5].polygon, 2);
                MessageBox.Show("Vypadá to, že svitek času patří sem. Měl bych ho sem dát.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "svitekTMisto.mp3")));
                text.Play();

                items[2].ItemImage.Visibility = Visibility.Visible;
                Canvas.SetLeft(items[2].ItemImage, polygons[5].XPercent0 - 60);
                Canvas.SetTop(items[2].ItemImage, polygons[5].YPercent0 - 80);
                itemsToBePlaced--;
            }
            if (point.Name == "Místo pro Svitek prostoru" && inventory.Items.Any(i => i != null && i.Name == "Svitek prostoru")) 
            {
                inventory.RemoveItem(inventory.Items.First(i => i != null && i.Name == "Svitek prostoru"));
                polygons[6].acessable = false;
                Canvas.SetZIndex(polygons[6].polygon, 2);
                MessageBox.Show("Vypadá to, že svitek prostoru patří sem. Měl bych ho sem dát.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "svitekSMisto.mp3")));
                text.Play();

                items[3].ItemImage.Visibility = Visibility.Visible;
                Canvas.SetLeft(items[3].ItemImage, polygons[6].XPercent0 - 60);
                Canvas.SetTop(items[3].ItemImage, polygons[6].YPercent0 - 70);
                itemsToBePlaced--;
            }

            if (itemsToBePlaced == 1)
            {
                if (inventory.Items.Any(i => i != null && i.Name == "Cár pláště"))
                {
                    items[0].Collectable = true;
                    items[0].ItemImage.Visibility = Visibility.Visible;
                    Canvas.SetZIndex(items[0].ItemImage, 997);
                    MessageBox.Show("Wow... to je on, podařilo se to!!!");
                    MessageBox.Show("Teď ho musím jít vrátit.");
                    text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "aquired.mp3")));
                    text.Play();

                    OverlayCanvas.Children.Add(polygons[18].polygon);
                    Canvas.SetZIndex(polygons[18].polygon, 997);
                    polygons[18].acessable = true;
                    itemsToBePlaced--;
                    // možnost přidání animace odpečetění a teleportace palantíru zpět do světa
                }
            }
            if (point.Name == "Exit" && polygons[18].acessable)
            {
                if (!inventory.Items.Any(i => i != null && i.Name == "Palantír"))
                {
                    MessageBox.Show("Musím sebou vzít ten palantír.");
                    text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "out.mp3")));
                    text.Play();
                }
                MessageBox.Show("Podařilo se mi získat palantír a vrátit ho na místo. Rituál se zřejmě povedl a já jsem se vrátil zpět do své kanceláře. Jenom my vrtá hlavou, kde se asi nachází zloděj a co s ním chtěl když ho byl ochotný tak dobře schovat?");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "end.mp3")));
                text.Play();
                Application.Current.Shutdown();
            }

        }
        private void Pankrac_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Image btn) || !(btn.Tag is Pankrac point))
                return;

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
                Canvas.SetZIndex(heldItem.ItemImage, 2);
                heldItem.JumpToSpawn();
                heldItem = null;
            }
        }
        private void Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Image btn) || !(btn.Tag is Item point))
                return;

            var pos = e.GetPosition(MapImage);
            pankrac.move(pos.X, pos.Y);
            text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "itemlogs", point.audioPath)));
            text.Play();

            Mouse.Capture(OverlayCanvas);

            // safely clear previous heldItem visuals if any
            if (heldItem != null)
                heldItem.ItemImage.IsHitTestVisible = true;

            heldItem = point;

            if (heldItem.Collectable)
            {
                Canvas.SetZIndex(heldItem.ItemImage, 999);
                inventory.RemoveItem(heldItem);
                heldItem.ItemImage.IsHitTestVisible = false;
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
                    Canvas.SetZIndex(heldItem.ItemImage, 997);
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