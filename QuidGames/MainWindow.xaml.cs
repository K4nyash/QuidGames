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
using System.ComponentModel;

namespace QuidGames
{
    public partial class MainWindow : Window
    {
        public int LatestRozgrywka = 0;
        public int LatestTurniej = 0;
        public int LatestSedzia = 0;
        public int LatestZespol = 0;
        public int LatestGracz = 0;

        public List<Turniej> ListT = new List<Turniej>();
        public List<Rozgrywka> ListR = new List<Rozgrywka>();
        public List<Sedzia> ListS = new List<Sedzia>();
        public List<Zespol> ListZ = new List<Zespol>();
        public List<Zawodnik> ListG = new List<Zawodnik>();

        public List<int> SzukajD1 = new List<int>();
        public List<int> SzukajD2 = new List<int>();
        public List<int> SzukajS = new List<int>();
        public List<int> SzukajT = new List<int>();

        public string pTurniej;
        public string pZespol;
        public string Content;
        public MainWindow()
        {
            InitializeComponent();

            Content = File.ReadAllText("../../../../Baza.txt");
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "R")
                {
                    if (LatestRozgrywka < Convert.ToInt32(temp[1])){
                        LatestRozgrywka = Convert.ToInt32(temp[1]);
                    }
                    if (Convert.ToInt32(temp[3]) == 1)
                    {
                        SzukajD1.Add(Convert.ToInt32(temp[4]));
                        SzukajD2.Add(Convert.ToInt32(temp[5]));
                        SzukajS.Add(Convert.ToInt32(temp[6]));
                        ListR.Add(new Rozgrywka(temp[2], "", "", "", ""));
                    }
                }
                else if (temp[0] == "T")
                {
                    if (LatestTurniej < Convert.ToInt32(temp[1]))
                    {
                        LatestTurniej = Convert.ToInt32(temp[1]);
                    }
                    if (ListT.Count == 0)
                    {
                        pTurniej = temp[2];
                    }
                    RozgrywkaWobor.Items.Add(temp[2]);
                    ListT.Add(new Turniej( temp[2], temp[3]));
                }
                else if (temp[0] == "S")
                {
                    if (LatestSedzia < Convert.ToInt32(temp[1]))
                    {
                        LatestSedzia = Convert.ToInt32(temp[1]);
                    }
                    ListS.Add(new Sedzia(temp[2], temp[3]));
                }
                else if (temp[0] == "Z")
                {
                    if (LatestZespol < Convert.ToInt32(temp[1]))
                    {
                        LatestZespol = Convert.ToInt32(temp[1]);
                    }
                    if (ListZ.Count == 0)
                    {
                        pZespol = temp[2];
                    }
                    GraczWobor.Items.Add(temp[2]);
                    ListZ.Add(new Zespol(temp[2]));
                }
                else if (temp[0] == "G")
                {
                    if (LatestGracz < Convert.ToInt32(temp[1]))
                    {
                        LatestGracz = Convert.ToInt32(temp[1]);
                    }
                    if (Convert.ToInt32(temp[4]) == 1)
                    {
                        ListG.Add(new Zawodnik(temp[2], temp[3], ""));
                    }
                }
            }
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "S")
                {
                    for (int i = 0; i < SzukajS.Count; i++)
                    {
                        if (Convert.ToInt32(temp[1]) == SzukajS[i])
                        {
                            ListR[i].Sedzia = temp[2];
                        }
                    }
                }
                else if (temp[0] == "Z")
                {
                    for (int i = 0; i < SzukajD1.Count; i++)
                    {
                        if (Convert.ToInt32(temp[1]) == SzukajD1[i])
                        {
                            ListR[i].Zespol1 = temp[2];
                        }
                        else if (Convert.ToInt32(temp[1]) == SzukajD2[i])
                        {
                            ListR[i].Zespol2 = temp[2];
                        }
                    }
                }
            }
            foreach(Rozgrywka r in ListR)
            {
                r.Turniej = pTurniej;
            }
            foreach(Zawodnik z in ListG)
            {
                z.Zespol = pZespol;
            }
            RozgrywkaDG.ItemsSource = ListR;
            TurniejDG.ItemsSource = ListT;
            SedziaDG.ItemsSource = ListS;
            ZespolDG.ItemsSource = ListZ;
            GraczDG.ItemsSource = ListG;

            RozgrywkaWobor.SelectedIndex = 0;
            GraczWobor.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Rozgrywki okno = new(LatestRozgrywka);
            if (okno.ShowDialog()==true)
            {
                RozgrywkaUpdate();
            }
            LatestRozgrywka++;
        }

        private void DodajT_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Turniej okno = new(LatestTurniej);
            if (okno.ShowDialog() == true)
            {
                Content = File.ReadAllText("../../../../Baza.txt");
                string temp1 = Content.Split("\n")[Content.Split("\n").Length - 2];
                string[] temp2 = temp1.Split(";");
                RozgrywkaWobor.Items.Add(temp2[2]);
                ListT.Add(new Turniej(temp2[2], temp2[3]));
                TurniejDG.ItemsSource = null;
                TurniejDG.ItemsSource = ListT;
            }
            LatestTurniej++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dodawanie_Sedziego okno = new(LatestSedzia);
            if (okno.ShowDialog() == true)
            {
                Content = File.ReadAllText("../../../../Baza.txt");
                string temp1 = Content.Split("\n")[Content.Split("\n").Length-2];
                string[] temp2 = temp1.Split(";");
                ListS.Add(new Sedzia(temp2[2], temp2[3]));
                SedziaDG.ItemsSource = null;
                SedziaDG.ItemsSource = ListS;
            }
            
            LatestSedzia++;
        }
        private void DodajZ_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Zespolow okno = new(LatestZespol);
            if (okno.ShowDialog() == true)
            {
                Content = File.ReadAllText("../../../../Baza.txt");
                string temp1 = Content.Split("\n")[Content.Split("\n").Length - 2];
                string[] temp2 = temp1.Split(";");
                GraczWobor.Items.Add(temp2[2]);
                ListZ.Add(new Zespol(temp2[2]));
                ZespolDG.ItemsSource = null;
                ZespolDG.ItemsSource = ListZ;
            }
            LatestZespol++;
        }
        private void DodajG_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie_Gracz okno = new(LatestGracz);
            if (okno.ShowDialog() == true)
            {
                GraczUpdate();
            }
            LatestGracz++;
        }
        private void RozgrywkaUpdate()
        {
            ComboBox obj = RozgrywkaWobor;

            ListR = new List<Rozgrywka>();
            SzukajD1 = new List<int>();
            SzukajD2 = new List<int>();
            SzukajS = new List<int>();
            SzukajT = new List<int>();

            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "R")
                {
                    if (obj.SelectedIndex + 1 == Convert.ToInt32(temp[4]))
                    {
                        SzukajD1.Add(Convert.ToInt32(temp[4]));
                        SzukajD2.Add(Convert.ToInt32(temp[5]));
                        SzukajS.Add(Convert.ToInt32(temp[6]));
                        SzukajT.Add(Convert.ToInt32(temp[3]));
                        ListR.Add(new Rozgrywka(temp[2], "", "", "", ""));
                    }
                }
            }
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "S")
                {
                    for (int i = 0; i < SzukajS.Count; i++)
                    {
                        if (Convert.ToInt32(temp[1]) == SzukajS[i])
                        {
                            ListR[i].Sedzia = temp[2];
                        }
                    }
                }
                else if (temp[0] == "Z")
                {
                    for (int i = 0; i < SzukajD1.Count; i++)
                    {
                        if (Convert.ToInt32(temp[1]) == SzukajD1[i])
                        {
                            ListR[i].Zespol1 = temp[2];
                        }
                        else if (Convert.ToInt32(temp[1]) == SzukajD2[i])
                        {
                            ListR[i].Zespol2 = temp[2];
                        }
                    }
                }
                else if (temp[0] == "T")
                {
                    for (int i = 0; i < SzukajT.Count; i++)
                    {
                        if (Convert.ToInt32(temp[1]) == SzukajT[i])
                        {
                            ListR[i].Turniej = temp[2];
                        }
                    }
                }
            }
            RozgrywkaDG.ItemsSource = null;
            RozgrywkaDG.ItemsSource = ListR;
        }
        private void RozgrywkaWobor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RozgrywkaUpdate();
        }
        private void GraczUpdate()
        {
            ComboBox obj = GraczWobor;

            ListG = new List<Zawodnik>();
            List<int> SzukajZ = new List<int>();

            string Content;
            Content = File.ReadAllText("../../../../Baza.txt");
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "G")
                {
                    if (obj.SelectedIndex + 1 == Convert.ToInt32(temp[4]))
                    {
                        SzukajZ.Add(Convert.ToInt32(temp[4]));
                        ListG.Add(new Zawodnik(temp[2], temp[3], ""));
                    }
                }
            }
            foreach (string line in Content.Split("\n"))
            {
                string[] temp = line.Split(";");
                if (temp[0] == "Z")
                {
                    for (int i = 0; i < SzukajZ.Count; i++)
                    {
                        if (Convert.ToInt32(temp[1]) == SzukajZ[i])
                        {
                            ListG[i].Zespol = temp[2];
                        }
                    }
                }
            }
            GraczDG.ItemsSource = null;
            GraczDG.ItemsSource = ListG;
        }
        private void GraczWobor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GraczUpdate();
        }
    }
    public class Turniej
    {
        public string Nazwa { get; set; }
        public string Miejsce { get; set; }
        public Turniej(string n, string m)
        {
            Nazwa = n;
            Miejsce = m;
        }
    }
    public class Rozgrywka
    {
        public string Nazwa { get; set; }
        public string Turniej { get; set; }
        public string Zespol1{ get; set; }
        public string Zespol2 { get; set; }
        public string Sedzia { get; set; }
        public Rozgrywka(string n, string t, string z1, string z2, string s)
        {
            Nazwa = n;
            Turniej = t;
            Zespol1 = z1;
            Zespol2 = z2;
            Sedzia = s;
        }
    }
    public class Sedzia
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Sedzia(string im, string n)
        {
            Imie = im;
            Nazwisko = n;

        }
    }
    public class Zespol
    {
        public string Nazwa { get; set; }
        public Zespol(string n)
        {
            Nazwa = n;

        }
    }
    public class Zawodnik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Zespol { get; set; }
        public Zawodnik(string im, string n, string z)
        {
            Imie = im;
            Nazwisko = n;
            Zespol = z; 
        }
    }
}
