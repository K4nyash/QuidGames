﻿using Microsoft.Win32;
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
    public partial class Dodawanie_Gracz : Window
    {
        public int LatestGracz;
        public Dodawanie_Gracz(int LG)
        {
            InitializeComponent();
            LatestGracz = LG;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            Content += "G;" + (LatestGracz + 1).ToString() + ";";
            Content += Imie.Text + ";";
            Content += Nazwisko.Text + ";";
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
