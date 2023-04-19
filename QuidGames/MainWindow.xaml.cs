using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
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
    public partial class MainWindow : Window
    {
        public int LatestRozgrywka = 0;
        public int LatestTurniej = 0;
        public int LatestSedzia = 0;
        public int LatestZespol = 0;
        public int LatestGracz = 0;
        public MainWindow()
        {
            InitializeComponent();
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "R")
                {
                    if (LatestRozgrywka < Convert.ToInt32(temp[1])){
                        LatestRozgrywka = Convert.ToInt32(temp[1]);
                    }
                }
                else if (temp[0] == "T")
                {
                    if (LatestTurniej < Convert.ToInt32(temp[1]))
                    {
                        LatestTurniej = Convert.ToInt32(temp[1]);
                    }
                }
                else if (temp[0] == "S")
                {
                    if (LatestSedzia < Convert.ToInt32(temp[1]))
                    {
                        LatestSedzia = Convert.ToInt32(temp[1]);
                    }
                }
                else if (temp[0] == "Z")
                {
                    if (LatestZespol < Convert.ToInt32(temp[1]))
                    {
                        LatestZespol = Convert.ToInt32(temp[1]);
                    }
                }
                else if (temp[0] == "G")
                {
                    if (LatestGracz < Convert.ToInt32(temp[1]))
                    {
                        LatestGracz = Convert.ToInt32(temp[1]);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Rozgrywki okno = new Dodawanie_Rozgrywki(LatestRozgrywka);
            okno.Show();
            LatestRozgrywka++;
        }

        private void DodajT_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Turniej okno = new Dodawanie_Turniej(LatestTurniej);
            okno.Show();
            LatestTurniej++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dodawanie_Sedziego okno = new Dodawanie_Sedziego(LatestSedzia);
            okno.Show();
            LatestSedzia++;
        }
        private void DodajZ_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Zespolow okno = new(LatestZespol);
            okno.Show();
            LatestZespol++;
        }
        private void DodajG_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Gracz okno = new(LatestGracz);
            okno.Show();
            LatestGracz++;
        }
    }
}
