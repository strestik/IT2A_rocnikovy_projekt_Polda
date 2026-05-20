using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace IT2A_rocnikovy_projekt_Polda
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
            pankrac.Source = new BitmapImage(new Uri("img/lowPolda.png", UriKind.Relative));
        }

        public void refresh()
        {
            Canvas.SetLeft(pankrac, posX);
            Canvas.SetTop(pankrac, posY);
        }

        public void move(double targetX, double targetY)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(.0001); 
            timer.Tick += (s, e) =>
            {

                double deltaX = targetX - posX;
                double deltaY = targetY - posY;
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                if (distance < 1)
                {
                    posX = targetX;
                    posY = targetY;
                    refresh();
                    timer.Stop();
                    return;
                }
                double speed = 0.03; // pixels per tick
                posX += (deltaX / distance) * speed;
                posY += (deltaY / distance) * speed;
                refresh();
            };
            timer.Start();
        }
    }
}
