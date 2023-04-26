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
using System.IO;

namespace QuidGames
{
    /// <summary>
    /// Logika interakcji dla klasy Dodawanie_Sedziego.xaml
    /// </summary>
    public partial class Dodawanie_Sedziego : Window
    {
        public int LatestRozgrywka;
        public Dodawanie_Sedziego(int LR)
        {
            InitializeComponent();
            LatestRozgrywka = LR;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            Content += "S;" + (LatestRozgrywka + 1).ToString() + ";";
            Content += Imie.Text + ";";
            Content += Nazwisko.Text + ";";
            Content += "\n";
            File.WriteAllText("../../../../Baza.txt", Content);
            DialogResult = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
