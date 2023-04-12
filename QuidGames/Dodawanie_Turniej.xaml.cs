using Microsoft.Win32;
using System;
using System.IO;
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

namespace QuidGames
{
    /// <summary>
    /// Logika interakcji dla klasy Dodawanie_Turniej.xaml
    /// </summary>
    public partial class Dodawanie_Turniej : Window
    {
        public int LatestTurniej;
        public Dodawanie_Turniej(int TR)
        {
            InitializeComponent();
            LatestTurniej = TR;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            Content += "T;" + (LatestTurniej + 1).ToString() + ";";
            Content += Nazwa.Text + ";";
            Content += Miejsce.Text + ";";
            Content += "\n";
            File.WriteAllText("../../../../Baza.txt", Content);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
