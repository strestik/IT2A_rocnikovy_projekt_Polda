using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace IT2A_rocnikovy_projekt_Polda
{
    internal class Inventory
    {
        public Image invetory { get; set; }
        public List<Item?> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item?> { null, null, null, null };
            invetory.Source = new BitmapImage(new Uri("img/inventary.png", UriKind.Relative));
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
    }
}
