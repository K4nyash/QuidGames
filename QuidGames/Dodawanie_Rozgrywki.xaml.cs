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
    public partial class Dodawanie_Rozgrywki : Window
    {
        public int LatestRozgrywka;
        public Dodawanie_Rozgrywki(int LR)
        {
            InitializeComponent();
            LatestRozgrywka = LR;
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "T")
                {
                    Turniej.Items.Add(temp[2]);
                }
                else if (temp[0] == "S")
                {
                    Sedzia.Items.Add(temp[2]+" " + temp[3]);
                }
                else if (temp[0] == "Z")
                {
                    Druzyna1.Items.Add(temp[2]);
                    Druzyna2.Items.Add(temp[2]);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            Content += "R;" + (LatestRozgrywka + 1).ToString() + ";";
            Content += Nazwa.Text + ";";
            Content += (Turniej.SelectedIndex + 1).ToString() + ";";
            Content += (Druzyna1.SelectedIndex + 1).ToString() + ";";
            Content += (Druzyna2.SelectedIndex + 1).ToString() + ";";
            Content += (Sedzia.SelectedIndex + 1).ToString() + ";";
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
