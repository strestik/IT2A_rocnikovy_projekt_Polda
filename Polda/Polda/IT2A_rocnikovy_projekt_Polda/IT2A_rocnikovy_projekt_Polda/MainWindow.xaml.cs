using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace IT2A_rocnikovy_projekt_Polda
{
    // future TODO - přidat aspoň druhou scénu (místnost s teleportem, odtud zjistí jak použít grimoair) nový xamel?
    //             - prohra na čas?
    //             - animace chození
    //             - animace sbírání věcí
    //             - animace používání věcí
    //             - animace interakce s hotspoty
    //             - konstantní animace hýbajících se věcí (např. plameny na svíčkách, vířící se lektvar atd.)
    //             - animace návodu jako rozbalujícího se svitku
    //             - přidat popisek po najetí myši na item nebo hotspot
    //             - možnsot zvíraznění klikatelných objektů, hotspotů i itemů
    public partial class MainWindow : Window
    {
        Menu menu;
        Random rnd = new Random();
        Item? heldItem;
        Pankrac pankrac = new Pankrac("Pankrác Moudrý", 0, 0, "Pankrac");
        Inventory inventory = new Inventory();
        bool canPlace = true;
        bool known = true;
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
            new Hotspot("Exit", false,  0, 1080, "exit.mp3", "Východ.", 130, 1080, 130, 715, 1, 715),
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




        public MediaPlayer background = new MediaPlayer();
        public MediaPlayer text = new MediaPlayer();
        public MediaPlayer effect = new MediaPlayer();

        public MainWindow(Menu menuWindow)
        {
            menu = menuWindow;
            InitializeComponent();
            background.MediaEnded += (s, e) => { background.Position = TimeSpan.Zero; background.Play(); };
            background.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music", "back.mp3")));
            text.MediaOpened += (s, e) => text.Play();
            effect.MediaOpened += (s, e) => effect.Play();
            Loaded += MainWindow_Loaded;
        }



        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            background.Volume = 0.04;
            text.Volume = 2.6;
            effect.Volume = 0.04;
            background.Play();

            textTimer.Tick += Text_Timed_Check;

            OverlayCanvas.Children.Add(pankrac.pankrac);
            Canvas.SetZIndex(pankrac.pankrac, 997);
            pankrac.pankrac.MouseDown += Pankrac_MouseDown;
            pankrac.pankrac.Tag = pankrac;
            Canvas.SetLeft(pankrac.pankrac, -200);
            Canvas.SetTop(pankrac.pankrac, 600);

            foreach (Item item in items)
            {
                item.ItemImage.Tag = item;
                OverlayCanvas.Children.Add(item.ItemImage);
                Canvas.SetZIndex(item.ItemImage, 1);
                if (item.Name == "Rozbitý lektvar") Canvas.SetZIndex(item.ItemImage, 0);
                if (item.Name == "Cár pláště") Canvas.SetZIndex(item.ItemImage, 0);
                item.ItemImage.MouseDown += Item_MouseDown;
            }
            OverlayCanvas.MouseUp += Item_MouseUp;

            OverlayCanvas.Children.Add(inventory.InvetoryImage);
            Canvas.SetZIndex(inventory.InvetoryImage, 2);
            inventory.InvetoryImage.MouseDown += Inventory_MouseDown;

            OverlayCanvas.MouseMove += MouseMove;

            // tutorial
            Text_Timed("Tak jsem tady. Ve věži toho zloděje. Je prázdná, ale vypadá, jako by ji někdo opustil ve spěchu.");
            Text_Timed("Nejspíš se sem vydal rovnou včera večer, hned po té krádeži. Musel to mít dobře připravené.");
            Text_Timed("Všechno tady je rozházené. To naznačuje, že jen vzal, co potřeboval. Ale zatím si nejsem jistý,");
            Text_Timed("jak se dostal ven. Přece ho tu noc pronásledovali. Byly to jen dvě minuty, ale když se sem dostali, ");
            Text_Timed("už nenašli nic. A teď je to na mě, abych zjistil, jak zpět navrátit ukradený majetek.");
            Text_Timed("No tak abych se do toho dal. Nejprv bych se měl porozhlédnout kolem a zjistit co nejvíce informací.");
            text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "tutorial.mp3")));
            text.Play();
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
            Text_Timed(point.init);

            if (point.Name == "Rozbitý lektvar" && !polygons[7].acessable)
            {
                polygons[7].acessable = true;
                Text_Timed("Kde se tady asi tak vzal? Vypadá to, že budu potřebovat nový.");
                Text_Timed("Z tohohle už kromě střepů moc nezbývá.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "brokePotion.mp3")));
                text.Play();
            }
            if (point.Name == "Alechemistická apartatura" && point.acessable)
            {
                items[7].Collectable = true;
                if (polygons.Count > 9)
                {
                    Hotspot target = polygons[9];
                    if (target?.polygon != null) OverlayCanvas.Children.Remove(target.polygon);
                }
                Canvas.SetZIndex(items[7].ItemImage, 998);
                Text_Timed("Ahhaa - tak tady se dělají lektvary!");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "aparature.mp3")));
                text.Play();
            }

            if (!point.acessable && point.Name == "Ingredience" || point.Name == "Spouštěč" || point.Name == "Zdroj" || point.Name == "Časovač" || point.Name == "Směrovač" || point.Name == "?")
            {
                point.acessable = true;
                polygons[8].init = "Rituální instrukce se znaky a popisky.";
            }
            if (!point.acessable && point.Name == "Rituální kruh" || point.Name == "Magický inkoust" || point.Name == "Cár pláště")
            {
                point.acessable = true;
            }

            if (polygons[12].acessable && polygons[13].acessable && polygons[14].acessable && polygons[15].acessable && polygons[16].acessable && known)
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

                Text_Timed("Vypadá to, že potřebuju najít všechny tyto předměty pro zprovoznění rituálu.");
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "ingredients.mp3")));
                text.Play();
                known = false;
            }
            if (polygons[0].acessable && polygons[1].acessable && polygons[17].acessable && polygons[10].acessable)
            {
                text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "contemplation.mp3")));
                text.Play();
                Text_Timed("Nejspíš musím rozestavět předměty z návodu na ten magický kruh podle jejich rozložení na návodu.");
                Text_Timed("Vypadá to, že náš zloděj se rozhodl ukrýt palantír na bezpečné místo, aby se pro něj mohl vrátit, až odejdu.");
                Text_Timed("Nejspíš použil rituální pečetící magii, aby ho poslal do vnějších prostor mimo naši existenci.");
                Text_Timed("Do těchto míst se normálně nedá dostat, jen převelice mocnými čáry.");
                Text_Timed("A vzhledem k tomu, že použil inkoust, má na odcizený předmět dosah jen on.");
                Text_Timed("Ale při psaní smlouvy bylo účinkům inkoustu vystaveno celé jejich tělo a já jsem už dříve našel kus odtrženého pláště.");
                Text_Timed("Mohl bych ho zkusit použít jako alternativu pro klíč k rituálu.");
                // zde případný časovač na vypršení spojení cáru pláště s pečetí
                Thread.Sleep(2000);
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
                Text_Timed("Vypadá to, že lektvar patří sem. Měl bych ho sem dát.");
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
                Text_Timed("Vypadá to, že grimoire patří sem. Měl bych ho sem dát.");
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
                Text_Timed("Vypadá to, že Sefirot patří sem. Měl bych ho sem dát.");
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
                Text_Timed("Vypadá to, že svitek času patří sem. Měl bych ho sem dát.");
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
                Text_Timed("Vypadá to, že svitek prostoru patří sem. Měl bych ho sem dát.");
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
                    Text_Timed("Wow... to je on, podařilo se to!!! Teď ho musím jít vrátit.");
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
                if (inventory.Items.Any(i => i != null && i.Name == "Palantír"))
                {
                    DisplayBlock.Visibility = Visibility.Collapsed;
                    DisplayText.Visibility = Visibility.Collapsed;
                    messageQueue.Clear();
                    textTimer.Stop();
                    IsActiveRunning = false;

                    Text_Timed("Podařilo se mi získat palantír a vrátit ho na místo.");
                    Text_Timed("Rituál se zřejmě povedl a já jsem se vrátil zpět do své kanceláře.");
                    Text_Timed("Jenom my vrtá hlavou, kde se asi nachází zloděj a co s ním chtěl když ho byl ochotný tak dobře schovat?");
                    //Text_Timed("");
                    text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "end.mp3")));
                    text.Play();
                    //Thread.Sleep(19000);
                    //Application.Current.Shutdown();
                }
                else
                {
                    Text_Timed("Musím sebou vzít ten palantír.");
                    text.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "interactions", "out.mp3")));
                    text.Play();
                }
            }

        }
        private void Pankrac_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is Image btn) || !(btn.Tag is Pankrac point))
                return;

            inventory.VisibilityToggle(pankrac.pankrac);
            Canvas.SetZIndex(inventory.InvetoryImage, 2);
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
            Text_Timed(point.Description);

            Mouse.Capture(OverlayCanvas);

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
                    Canvas.SetZIndex(heldItem.ItemImage, 3);
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


            //Text_Timed(""); //96

            //MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            messageQueue.Clear();
            text.Stop();
            background.Stop();
            effect.Stop();
            if (menu != null) menu.Show();
            this.Hide();
        }

        public DispatcherTimer textTimer = new DispatcherTimer();
        public DateTime startTime;
        public double Time = 10;
        public bool IsActiveRunning = false;
        Queue<string> messageQueue = new Queue<string>();

        public void Text_Timed(string text)
        {
            if (!messageQueue.Contains(text) && messageQueue.Count < 7)
            {
                messageQueue.Enqueue(text);
                string next = messageQueue.Peek();
                Time = next.Length / 12;//(10 + (next.Length * 0.03) );
            }
            if (!IsActiveRunning)
            {
                if (messageQueue.Count > 0)
                {
                    string next = messageQueue.Dequeue();
                    if (next != null)
                    {
                        Time = next.Length / 12;//(10 + (next.Length * 0.03));
                        Text_Timed_Start(next);
                    }
                }
            }
        }

        public void Text_Timed_Start(string text)
        {
            if (textTimer == null) return;
            if (IsActiveRunning) return;
            textTimer.Start();
            IsActiveRunning = true;
            effect.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music", "textAppear.mp3")));
            effect.Play();
            startTime = DateTime.Now;
            DisplayBlock.Visibility = Visibility.Visible;
            DisplayText.Visibility = Visibility.Visible;
            

            if (text.Length > 92)
            {
                DisplayText.FontSize =  (46.0 / (text.Length / 86.0)) + text.Length * 0.02;
            }

            DisplayText.Text = text;

            // // Udělat to aby to dalo vícekrát text podle délky a tké přidělilo čas
            //for (int i = 0; i < text.Length; i += 96)
            //    if (text.Length > i + 96)
            //    {
            //        DisplayText.Text = text.Substring(i, i + 96);
            //        Time = time / (text.Length / 96);
            //    }
        }

        public void Text_Timed_Check(object sender, EventArgs e)
        {
            if (!textTimer.IsEnabled) return;
            if ((DateTime.Now - startTime).TotalSeconds >= Time)
            {
                textTimer.Stop();
                DisplayBlock.Visibility = Visibility.Collapsed;
                DisplayText.Visibility = Visibility.Collapsed;
                IsActiveRunning = false;
                if (messageQueue.Count > 0)
                {
                    effect.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music", "textDisappear.mp3")));
                    effect.Play();
                    string next = messageQueue.Dequeue();
                    if (next != null)
                    {
                        Time = next.Length / 12;//(10 + (next.Length * 0.03));
                        Text_Timed_Start(next);
                    }
                }
                if (inventory.Items.Any(i => i != null && i.Name == "Palantír") && DisplayBlock.Visibility == Visibility.Collapsed)
                {
                    Thread.Sleep(5000);
                    Application.Current.Shutdown();
                }
            }
        }
    }
}