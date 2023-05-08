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
using System.Text.RegularExpressions;
using System.Globalization;

namespace QuidGames
{
    public partial class Dodawanie_Rozgrywki : Window
    {
        public int LatestRozgrywka;
        public class Rozgrywka
        {
            public string Nazwa { get; set; }

        }
        public Dodawanie_Rozgrywki(int LR)
        {
            InitializeComponent();
            LatestRozgrywka = LR;
            Rozgrywka rozgrywka1 = new();
            this.DataContext = rozgrywka1;
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

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Nazwa.Text == "" || Turniej.SelectedIndex < 0 || Druzyna1.SelectedIndex < 0 || Druzyna2.SelectedIndex < 0 || Sedzia.SelectedIndex < 0)
            {
                Zapisz.IsEnabled = false;
            }
            else
            {
                TextBox tb = sender as TextBox;
                Zapisz.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            }
        }

        private void Sedzia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Nazwa.Text == "" || Turniej.SelectedIndex < 0 || Druzyna1.SelectedIndex < 0 || Druzyna2.SelectedIndex < 0 || Sedzia.SelectedIndex < 0)
            {
                Zapisz.IsEnabled = false;
            }
            else
            {
                Zapisz.IsEnabled = true;
            }
        }
    }

    public class WalNazwaR : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać nazwę rozgrywki");
                }
                else
                {
                    string reg = (@"^[A-Z].+$");

                    if (Regex.IsMatch(value.ToString(), reg) == true)
                    {
                        return new ValidationResult(true, null);
                    }
                    else
                    {
                        return new ValidationResult(false, "Nazwa powinna zaczynać się od wielkiej litery i mieć więcej niż 2 znaki");
                    }
                }
            }
            else
            {
                return new ValidationResult(false, "Wpisane dane nie są typu tekstowego");
            }
        }
    }
}
