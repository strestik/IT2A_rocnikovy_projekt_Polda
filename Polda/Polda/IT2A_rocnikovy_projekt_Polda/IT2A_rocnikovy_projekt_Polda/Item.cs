using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT2A_rocnikovy_projekt_Polda
{
    internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Item(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
        }
    }
}
