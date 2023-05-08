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
    public partial class Dodawanie_Gracz : Window
    {
        public int LatestGracz;
        public class Gracz
        {
            public string Nazwisko { get; set; }
            public string Imie { get; set; }

        }
        public Dodawanie_Gracz(int LG)
        {
            InitializeComponent();
            LatestGracz = LG;
            Gracz gracz1 = new();
            this.DataContext = gracz1;
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "Z")
                {
                    Druzyny.Items.Add(temp[2]);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            Content += "G;" + (LatestGracz + 1).ToString() + ";";
            Content += Imie.Text + ";";
            Content += Nazwisko.Text + ";";
            Content += (Druzyny.SelectedIndex + 1) + ";";
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
            if (Imie.Text == "" || Nazwisko.Text == "" || Druzyny.SelectedIndex < 0)
            {
                Zapisz.IsEnabled = false;
            }
            else
            {
                TextBox tb = sender as TextBox;
                Zapisz.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            }
        }

        private void Druzyny_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Imie.Text == "" || Nazwisko.Text == "" || Druzyny.SelectedIndex < 0)
            {
                Zapisz.IsEnabled = false;
            }
            else
            {
                Zapisz.IsEnabled = true;
            }
        }
    }

    public class WalImieG : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać imię gracza");
                }
                else
                {
                    string reg = (@"^[A-Z][a-z]+$");

                    if (Regex.IsMatch(value.ToString(), reg) == true)
                    {
                        return new ValidationResult(true, null);
                    }
                    else
                    {
                        return new ValidationResult(false, "Imię powinno zaczynać się od wielkiej litery i mieć więcej niż 2 znaki");
                    }
                }
            }
            else
            {
                return new ValidationResult(false, "Wpisane dane nie są typu tekstowego");
            }
        }
    }
    public class WalNazwiskoG : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać nazwisko gracza");
                }
                else
                {
                    string reg = (@"^[A-Z][a-z]+$");

                    if (Regex.IsMatch(value.ToString(), reg) == true)
                    {
                        return new ValidationResult(true, null);
                    }
                    else
                    {
                        return new ValidationResult(false, "Nazwisko powinno zaczynać się od wielkiej litery i mieć więcej niż 2 znaki");
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
