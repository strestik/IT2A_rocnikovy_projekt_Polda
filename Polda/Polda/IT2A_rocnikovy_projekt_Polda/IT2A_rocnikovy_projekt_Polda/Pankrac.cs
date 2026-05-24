using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
        public double targetX { get; set; }
        public double targetY { get; set; }
        public double centerOffsetX { get; set; }
        public double centerOffsetY { get; set; }

        public Pankrac(string name, double xPercent, double yPercent, string item = "Empty")
        {
            Name = name;
            posX = xPercent;
            posY = yPercent;
            pankrac = new Image();
            init = item;
            pankrac.Source = new BitmapImage(new Uri("img/lowPolda.png", UriKind.Relative));
            pankrac.Loaded += (s, e) =>
            {
                centerOffsetX = pankrac.ActualWidth / 2;
                centerOffsetY = pankrac.ActualHeight / 2;
            };
        }

        //public void refresh()
        //{
        //    Canvas.SetLeft(pankrac, posX - centerOffsetX);
        //    Canvas.SetTop(pankrac, posY - centerOffsetY);
        //}

        //public DispatcherTimer timer = new DispatcherTimer();

        //public void Timer_Tick(object sender, EventArgs e)
        //{
        //    timer.Interval = TimeSpan.FromMilliseconds(16);
        //    double deltaX = targetX - posX;
        //    double deltaY = targetY - posY;
        //    double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

        //    if (distance < 3)
        //    {
        //        posX = targetX;
        //        posY = targetY;
        //        refresh();
        //        timer.Stop();
        //        return;
        //    }
        //    double speed = 6; // pixels per tick
        //    posX += (deltaX / distance) * speed;
        //    posY += (deltaY / distance) * speed;
        //    refresh();
        //}

        //public void move(double targX, double targY)
        //{
        //    timer.Stop();
        //    targetX = targX;
        //    targetY = targY;
        //    timer.Tick -= Timer_Tick;
        //    timer.Tick += Timer_Tick;
        //    timer.Start();
        //}
        public void move(double targetX, double targetY, double speed = 2.5)
        {
            if (!double.IsNaN(Canvas.GetLeft(pankrac)))
                posX = Canvas.GetLeft(pankrac);
            if (!double.IsNaN(Canvas.GetTop(pankrac)))
                posY = Canvas.GetTop(pankrac);
            double deltaX = targetX - posX;
            double deltaY = targetY - posY;
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            DoubleAnimation animX = new DoubleAnimation(posX, targetX - centerOffsetX, TimeSpan.FromSeconds(speed * distance / 1000)); 
            DoubleAnimation animY = new DoubleAnimation(posY, targetY - centerOffsetY, TimeSpan.FromSeconds(speed * distance / 1000));
            pankrac.BeginAnimation(Canvas.LeftProperty, animX);
            pankrac.BeginAnimation(Canvas.TopProperty, animY);
        }
    }
}
