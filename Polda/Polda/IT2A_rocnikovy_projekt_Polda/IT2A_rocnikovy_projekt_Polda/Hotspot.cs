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
        public double XPercent0 { get; set; }
        public double YPercent0 { get; set; }
        public double XPercent1 { get; set; }
        public double YPercent1 { get; set; }
        public double XPercent2 { get; set; }
        public double YPercent2 { get; set; }
        public double XPercent3 { get; set; }
        public double YPercent3 { get; set; }
        public double XPercent4 { get; set; }
        public double YPercent4 { get; set; }
        public double XPercent5 { get; set; }
        public double YPercent5 { get; set; }
        public double XPercent6 { get; set; }
        public double YPercent6 { get; set; }
        public double XPercent7 { get; set; }
        public double YPercent7 { get; set; }
        public string init { get; set; }
        public bool acessable { get; set; }
        public string audioPath { get; set; }
        public Polygon polygon { get; set; } = new Polygon();


        public Hotspot(string name, bool acess, double xPercent0, double yPercent0, string audio = "", string item = "Empty", double xPercent1 = 0, double yPercent1 = 0, 
            double xPercent2 = 0, double yPercent2 = 0, double xPercent3 = 0, double yPercent3 = 0, double xPercent4 = 0, double yPercent4 = 0, double xPercent5 = 0, 
            double yPercent5 = 0, double xPercent6 = 0, double yPercent6 = 0, double xPercent7 = 0, double yPercent7 = 0,
             System.Windows.Media.Brush stroke = null, double strokeThickness = 1)
        {
            Name = name;
            XPercent0 = xPercent0;
            YPercent0 = yPercent0;
            XPercent1 = xPercent1 != 0 ? xPercent1 : XPercent0;
            YPercent1 = yPercent1 != 0 ? yPercent1 : YPercent0;
            XPercent2 = xPercent2 != 0 ? xPercent2 : XPercent1;
            YPercent2 = yPercent2 != 0 ? yPercent2 : YPercent1;
            XPercent3 = xPercent3 != 0 ? xPercent3 : XPercent2;
            YPercent3 = yPercent3 != 0 ? yPercent3 : YPercent2;
            XPercent4 = xPercent4 != 0 ? xPercent4 : XPercent3;
            YPercent4 = yPercent4 != 0 ? yPercent4 : YPercent3;
            XPercent5 = xPercent5 != 0 ? xPercent5 : XPercent4;
            YPercent5 = yPercent5 != 0 ? yPercent5 : YPercent4;
            XPercent6 = xPercent6 != 0 ? xPercent6 : XPercent5;
            YPercent6 = yPercent6 != 0 ? yPercent6 : YPercent5;
            XPercent7 = xPercent7 != 0 ? xPercent7 : XPercent6;
            YPercent7 = yPercent7 != 0 ? yPercent7 : YPercent6;
            init = item;
            acessable = acess;
            audioPath = audio;

            polygon.Stroke = stroke ?? System.Windows.Media.Brushes.Red;
            polygon.Fill = System.Windows.Media.Brushes.Transparent;
            polygon.StrokeThickness = strokeThickness;
            polygon.HorizontalAlignment = HorizontalAlignment.Left;
            polygon.VerticalAlignment = VerticalAlignment.Center;
            // nastavení vrstvy polygonu, aby byl kliknutelný
        }

        //void Draw()
        //{
        //    OverlayCanvas.Children.Clear();

        //    foreach (var poly in polygons)
        //    {
        //        poly.polygon.MouseDown -= Polygon_MouseDown;
        //        poly.polygon.MouseDown += Polygon_MouseDown;
        //        poly.polygon.Tag = poly;

        //        System.Windows.Point Point1 = new System.Windows.Point(poly.XPercent, poly.YPercent);
        //        System.Windows.Point Point2 = new System.Windows.Point(poly.XPercent, poly.YPercent);
        //        System.Windows.Point Point3 = new System.Windows.Point(poly.XPercent, poly.YPercent);
        //        System.Windows.Point Point4 = new System.Windows.Point(poly.XPercent, poly.YPercent);
        //        System.Windows.Point Point5 = new System.Windows.Point(poly.XPercent, poly.YPercent);
        //        PointCollection myPointCollection = new PointCollection();
        //        myPointCollection.Add(Point1);
        //        myPointCollection.Add(Point2);
        //        myPointCollection.Add(Point3);
        //        myPointCollection.Add(Point4);
        //        myPointCollection.Add(Point5);
        //        poly.polygon.Points = myPointCollection;
        //        OverlayCanvas.Children.Add(poly.polygon);
        //    }
        //}

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
