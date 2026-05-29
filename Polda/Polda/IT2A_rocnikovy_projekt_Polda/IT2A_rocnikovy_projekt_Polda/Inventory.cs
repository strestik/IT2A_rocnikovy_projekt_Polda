using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int[] ImagePositionsX = new int[] { 0, 160, 320, 480 };

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

            UpdateItemPositions();
        }
        private void UpdateItemPositions()
        {
            double x = Canvas.GetLeft(InvetoryImage);
            double y = Canvas.GetTop(InvetoryImage);
            ////Trace.WriteLine($"x:{x} y:{y}");
            for (int i = 0; i < ImagePositionsX.Length; i++)
            {
                if (Items[i] != null)
                {
                    Canvas.SetLeft(Items[i].ItemImage, x + ImagePositionsX[i]);
                    Canvas.SetTop(Items[i].ItemImage, y);
                    //Trace.WriteLine(Items[i].Name);
                    //Trace.WriteLine(string.Join(", ", Items.Select((item, index) => item != null ? $"Slot {index}: {item.Name}" : $"Slot {index}: Empty")));
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
            for (int i = 0; i < ImagePositionsX.Length; i++)
            {
                if (Items[i] != null)
                {
                    if (Items[i].IsVisible)
                    {
                        Items[i].ItemImage.Visibility = Visibility.Collapsed;
                    }
                    else if (!Items[i].IsVisible)
                    {
                        Items[i].ItemImage.Visibility = Visibility.Visible;
                    }
                }
            }

                
            double x = Canvas.GetLeft(pos);
            double y = Canvas.GetTop(pos);

            if (InvetoryImage.Visibility == Visibility.Visible)
                InvetoryImage.Visibility = Visibility.Collapsed;
            else
            {
                if (x > 1200)
                {
                    Canvas.SetLeft(InvetoryImage, x - 700);
                    Canvas.SetTop(InvetoryImage, y - 200);
                }
                else
                {
                    Canvas.SetLeft(InvetoryImage, x + 100);
                    Canvas.SetTop(InvetoryImage, y - 200);
                }
                

                UpdateItemPositions();
                InvetoryImage.Visibility = Visibility.Visible;
            }
        }

        public void VisibilityOff(Image pos)
        {
            InvetoryImage.Visibility = Visibility.Collapsed;
            for (int i = 0; i < ImagePositionsX.Length; i++)
            {
                if (Items[i] != null)
                {
                    if (Items[i].IsVisible)
                    {
                        Items[i].ItemImage.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }   
    }
}
