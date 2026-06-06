using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IT2A_rocnikovy_projekt_Polda
{
    internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Image ItemImage { get; set; }
        public bool IsVisible { get { return ItemImage.Visibility == Visibility.Visible; } }
        public double posX { get; set; }
        public double posY { get; set; }
        public bool Collectable { get; set; }
        public bool Activation { get; set; }
        public double RelativePosX { get; set; }
        public double RelativePosY { get; set; }
        public string audioPath { get; set; }
        public Item(string name, string description, string imagePath, double positionX, double positionY, string audio = "", bool collectable = true, bool isVisible = true, bool active = true, double relativePosX = 0, double relativePosY = 0)
        {
            Name = name;
            Description = description;
            posX = positionX;
            posY = positionY;
            ItemImage = new Image();
            ItemImage.Visibility = Visibility.Visible;
            if (!isVisible) ItemImage.Visibility = Visibility.Collapsed;
            ItemImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri($"img/{imagePath}.png", UriKind.Relative));
            Canvas.SetLeft(ItemImage, posX);
            Canvas.SetTop(ItemImage, posY);
            Collectable = collectable;
            RelativePosX = relativePosX;
            RelativePosY = relativePosY;
            audioPath = audio;
            Activation = active;
        }

        public void Show() { ItemImage.Visibility = Visibility.Visible; }

        public void JumpToSpawn()
        {
            Canvas.SetLeft(ItemImage, posX);
            Canvas.SetTop(ItemImage, posY);
        }

        
    }
}
