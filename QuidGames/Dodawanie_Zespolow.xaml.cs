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
    public partial class Dodawanie_Zespolow : Window
    {
        public int LatestZespol;

        public class Zespol
        {
            public string Nazwa { get; set; }

        }
        public Dodawanie_Zespolow(int ZR)
        {
            InitializeComponent();
            LatestZespol = ZR;
            Zespol zespol1 = new();
            this.DataContext = zespol1;

        }


            private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            Content += "Z;" + (LatestZespol + 1).ToString() + ";";
            Content += Nazwa.Text + ";";
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
        }


    }

    public class WalNazwaZ : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult(false, "Proszę wpisać nazwę zespołu");
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
                            if (line.StartsWith("Z;")) {
                                string[] temp = line.Split(";");
                                if (temp[2] == value.ToString()) {
                                    return new ValidationResult(false, "Zespół o tej nazwie już istnieje");
                                }
                            }
                        }
                        return new ValidationResult(true, null);
                        
                    }
                    else
                    {
                        return new ValidationResult(false, "Nazwa zespołu powinna zaczynać się od wielkiej litery i mieć więcej niż 2 znaki");
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
