using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IT2A_rocnikovy_projekt_Polda
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        MainWindow main;
        Settings settings;

        public Menu()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (main == null)
            {
                main = new MainWindow(this);
                main.background.Volume = 0.08;
                main.text.Volume = 1.4;
            }
            main.background.MediaEnded += (s, e) => { main.background.Position = TimeSpan.Zero; main.background.Play(); };
            main.background.Open(new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music", "back.mp3")));
            main.background.Play();
            main.Show();
            this.Hide();
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            if (settings == null) settings = new Settings(this, main);
            settings.Show();
            this.Hide();
        }

    }
}
