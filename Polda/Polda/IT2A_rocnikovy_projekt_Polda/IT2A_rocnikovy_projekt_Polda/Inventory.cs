using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IT2A_rocnikovy_projekt_Polda
{
    internal class Inventory
    {
        public Image InvetoryImage { get; set; }
        public List<Item?> Items { get; set; }
        //public Item? _item { get; set; }
        public int[] ImagePositionsX = new int[] { -240, -80, 80, 240 };

        public Inventory()
        {
            Items = new List<Item?> { null, null, null, null };
            InvetoryImage = new Image();
            InvetoryImage.Source = new BitmapImage(new Uri("img/inventary.png", UriKind.Relative));
            // Set invisible at spawn
            InvetoryImage.Visibility = Visibility.Collapsed;
        }

        public void AddItem(Item item)
        {
            if (Items.Count(i => i != null) >= 4)
            {
                return;
            }
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] == null)
                {
                    Items[i] = item;
                    break;
                }
            }

            double x = Canvas.GetLeft(InvetoryImage);
            double y = Canvas.GetTop(InvetoryImage);
            for(int i = 0; i < ImagePositionsX.Length; i++)
            {
                if (Items[i] != null)
                {
                    Canvas.SetLeft(Items[i].ItemImage,x + ImagePositionsX[i]);
                    Canvas.SetTop(Items[i].ItemImage, y);
                    Console.WriteLine(Items[i].Name);
                }
            }
        }
        
        public void RemoveItem(Item item) {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] != null && Items[i].Name == item.Name)
                {
                    Items[i] = null;
                    break;
                }
            }
        }


        public void VisibilityToggle(Image pos)
        {
            double x = Canvas.GetLeft(pos);
            double y = Canvas.GetTop(pos);

            if (InvetoryImage.Visibility == Visibility.Visible)
                InvetoryImage.Visibility = Visibility.Collapsed;
            else
            {
                // add posible positioning so it doesnt go out of bounds
                Canvas.SetLeft(InvetoryImage, x + 100);
                Canvas.SetTop(InvetoryImage, y - 200);
                InvetoryImage.Visibility = Visibility.Visible;
            }
        }

        public void VisibilityOff()
        {
            InvetoryImage.Visibility = Visibility.Collapsed;
        }   
    }
}
