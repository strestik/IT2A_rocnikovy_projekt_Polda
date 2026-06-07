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

namespace IT2A_rocnikovy_projekt_Polda
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        Menu menu;
        MainWindow main;
        //MainWindow game;, MainWindow gameWindow
        public Settings(Menu menuWindow, MainWindow gameWindow)
        {
            menu = menuWindow;
            main = gameWindow;
            InitializeComponent();
        }

        private void MusicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (main != null) main.background.Volume = (e.NewValue / 100);
        }

        private void VoiceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (main != null) main.text.Volume = (e.NewValue / 100);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            menu.Show();
            this.Hide();
        }
    }
}
