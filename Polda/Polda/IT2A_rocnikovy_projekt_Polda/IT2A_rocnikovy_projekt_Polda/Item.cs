using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IT2A_rocnikovy_projekt_Polda
{
    internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Image ItemImage { get; set; }
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
            ItemImage = new Image();
        }
    }
}
