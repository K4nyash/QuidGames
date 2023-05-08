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
    public partial class Dodawanie_Turniej : Window
    {
        public int LatestTurniej;
        public int x;

        public class Turniej
        {
            public string Nazwa { get; set; }
            public string Miejsce { get; set; }

        }
        public Dodawanie_Turniej(int TR)
        {
            InitializeComponent();
            LatestTurniej = TR;
            x = 1;
            Turniej turniej1 = new();
            this.DataContext = turniej1;
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
            if (Miejsce.Text == "" || Nazwa.Text == "")
            {
                Zapisz.IsEnabled = false;
            }
        }

    }

    public class WalNazwaT : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać nazwę turnieju");
                }
                else
                {
                    string reg = (@"^[A-Z].+$");

                    if (Regex.IsMatch(value.ToString(), reg) == true)
                    {
                        string Content;
                        Content = File.ReadAllText("../../../../Baza.txt");
                        foreach (string line in Content.Split("\n"))
                        {
                            if (line.StartsWith("T;"))
                            {
                                string[] temp = line.Split(";");
                                if (temp[2] == value.ToString())
                                {
                                    return new ValidationResult(false, "Turniej o tej nazwie już istnieje");
                                }
                            }
                        }
                        return new ValidationResult(true, null);

                    }
                    else
                    {
                        return new ValidationResult(false, "Nazwa turnieju powinna zaczynać się od wielkiej litery i mieć więcej niż 2 znaki");
                    }
                }
            }
            else
            {
                return new ValidationResult(false, "Wpisane dane nie są typu tekstowego");
            }
        }
    }

    public class WalMiejsce : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać miejsce");
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
                        return new ValidationResult(false, "Nazwa miejsca powinna zaczynać się od wielkiej litery i mieć więcej niż 2 znaki");
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
