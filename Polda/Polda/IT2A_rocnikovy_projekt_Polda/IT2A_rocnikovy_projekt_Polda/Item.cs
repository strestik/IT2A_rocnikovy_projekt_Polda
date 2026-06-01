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
        public Item(string name, string description, string imagePath, double positionX, double positionY, bool collectable, bool isVisible = true)
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
        }

        public void Show() { ItemImage.Visibility = Visibility.Visible; }

        public void JumpToSpawn()
        {
            Canvas.SetLeft(ItemImage, posX);
            Canvas.SetTop(ItemImage, posY);
        }

        
    }
}
