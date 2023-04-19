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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuidGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int LatestRozgrywka;
        public int LatestTurniej;
        public int LatestSedzia;
        public int LatestZespol;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Rozgrywki okno = new Dodawanie_Rozgrywki(LatestRozgrywka);
            okno.Show();
        }

        private void DodajT_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Turniej okno = new Dodawanie_Turniej(LatestTurniej);
            okno.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dodawanie_Sedziego okno = new Dodawanie_Sedziego(LatestSedzia);
            okno.Show();
        }
        private void DodajZ_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Zespolow okno = new(LatestZespol);
            okno.Show();
        }
    }
}
