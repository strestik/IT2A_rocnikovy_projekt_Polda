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
        public Image invetory { get; set; }
        public List<Item?> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item?> { null, null, null, null };
            invetory = new Image();
            invetory.Source = new BitmapImage(new Uri("img/inventary.png", UriKind.Relative));
            // Set invisible at spawn
            invetory.Visibility = Visibility.Collapsed;
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

            if (invetory.Visibility == Visibility.Visible)
                invetory.Visibility = Visibility.Collapsed;
            else
            {
                // add posible positioning so it doesnt go out of bounds
                Canvas.SetLeft(invetory, x + 100);
                Canvas.SetTop(invetory, y - 200);
                invetory.Visibility = Visibility.Visible;
            }
        }

    }
}
