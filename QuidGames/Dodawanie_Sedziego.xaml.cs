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
using System.Text.RegularExpressions;
using System.Globalization;

namespace QuidGames
{
    /// <summary>
    /// Logika interakcji dla klasy Dodawanie_Sedziego.xaml
    /// </summary>
    public partial class Dodawanie_Sedziego : Window
    {
        public int LatestRozgrywka;

        public class Sedzia
        {
            public string Nazwisko { get; set; }
            public string Imie { get; set; }

        }
        public Dodawanie_Sedziego(int LR)
        {
            InitializeComponent();
            LatestRozgrywka = LR;
            Sedzia sedzia1 = new();
            this.DataContext = sedzia1;
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

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            Zapisz.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (Imie.Text == "" || Nazwisko.Text == "")
            {
                Zapisz.IsEnabled = false;
            }
        }
    }
    public class WalImieS : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać imię sędziego");
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
    public class WalNazwiskoS : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać nazwisko sędziego");
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
