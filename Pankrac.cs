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
using System.Windows.Threading;

namespace Polda
{
    internal class Pankrac
    {
        public string Name { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }
        public string init { get; set; }
        public Image pankrac { get; set; }


        public Pankrac(string name, double xPercent, double yPercent, Image img, string item = "Empty")
        {
            Name = name;
            posX = xPercent;
            posY = yPercent;
            pankrac = img;
            init = item;
        }

        public void refresh()
        {
            Canvas.SetLeft(pankrac, posX);
            Canvas.SetTop(pankrac, posY);
        }

        public void move(double targetX, double targetY)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16); // ~60fps
            timer.Tick += (s, e) =>
            {
                
                double deltaX = targetX - posX;
                double deltaY = targetY - posY;
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                if (distance < 3)
                {
                    posX = targetX;
                    posY = targetY;
                    refresh();
                    timer.Stop();
                    return;
                }
                double speed = 3; // pixels per tick
                posX += (deltaX / distance) * speed;
                posY += (deltaY / distance) * speed;
                refresh();
            };
            timer.Start();
        }
    }
}