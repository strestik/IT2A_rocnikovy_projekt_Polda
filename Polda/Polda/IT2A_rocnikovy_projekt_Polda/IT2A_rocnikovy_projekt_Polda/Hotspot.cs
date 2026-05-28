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

namespace IT2A_rocnikovy_projekt_Polda
{
    internal class Hotspot
    {
        public string Name { get; set; }
        public double XPercent { get; set; }
        public double YPercent { get; set; }
        public string init { get; set; }
        public Polygon polygon { get; set; } = new Polygon();


        public Hotspot(string name, double xPercent, double yPercent, string item = "Empty", System.Windows.Media.Brush stroke = null, double strokeThickness = 1)
        {
            Name = name;
            XPercent = xPercent;
            YPercent = yPercent;
            init = item;

            polygon.Stroke = stroke ?? System.Windows.Media.Brushes.Black;
            polygon.Fill = System.Windows.Media.Brushes.Transparent;
            polygon.StrokeThickness = strokeThickness;
            polygon.HorizontalAlignment = HorizontalAlignment.Left;
            polygon.VerticalAlignment = VerticalAlignment.Center;
        }

        //public void refresh()
        //{
        //    System.Windows.Point Point1 = new System.Windows.Point(770 * XPercent, 210 * YPercent);
        //    System.Windows.Point Point2 = new System.Windows.Point(742 * XPercent, 310 * YPercent);
        //    System.Windows.Point Point3 = new System.Windows.Point(746 * XPercent, 513 * YPercent);
        //    System.Windows.Point Point4 = new System.Windows.Point(814 * XPercent, 573 * YPercent);
        //    System.Windows.Point Point5 = new System.Windows.Point(814 * XPercent, 313 * YPercent);
        //    PointCollection myPointCollection = new PointCollection();
        //    myPointCollection.Add(Point1);
        //    myPointCollection.Add(Point2);
        //    myPointCollection.Add(Point3);
        //    myPointCollection.Add(Point4);
        //    myPointCollection.Add(Point5);
        //    polygon.Points = myPointCollection;
        //}

        public void GiveItem(Pankrac pankrac, Inventory inventory)
        {
            if (pankrac.init == init)
            {
                //Item item = new Item(init, "Description of " + init);
                //inventory.AddItem(item);
                pankrac.init = "Empty";
            }
        }
    }
}
