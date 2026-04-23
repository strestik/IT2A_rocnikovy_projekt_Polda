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
    internal class Hotspot
    {
        public string Name { get; set; }
        public 
        public double XPercent { get; set; }
        public double YPercent { get; set; }
        public string init { get; set; }
        public Polygon polygon { get; set; } = new Polygon();


        public Hotspot(string name, double xPercent, double yPercent, string item = "Empty",
                   System.Windows.Media.Brush stroke = null, double strokeThickness = 1)
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
    }
}
