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
using System.IO;
using System.Threading;

namespace onallo0107__brawlstars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            warning.Content = "warning: ne kattingass olyan gyorsan, \nvalamiert lassu a wpf";

            feltoltes();
            
        }

        List<string> osszesbrawler = new List<string>();
        List<string> nemezerkupas = new List<string>();
        List<string> ezerkupasbrawler = new List<string>();
        List<string> droppoltbrawler = new List<string>();


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string ujbrawler = ujhozzaadas.Text;
            if (!ezerkupasbrawler.Contains(ujbrawler) && osszesbrawler.Contains(ujbrawler))
            {
                ezerkupasbrawler.Add(ujbrawler);
                nemezerkupas.Remove(ujbrawler);
                string txtbe = "";
                foreach(var b in ezerkupasbrawler)
                {
                    txtbe += (b + ";");
                }
                File.WriteAllText("ezerkupasbrawler.txt",txtbe);
                txtbe = "";
                foreach (var n in nemezerkupas)
                {
                    txtbe += (n + ";");
                }
                File.WriteAllText("nemezerkupas.txt", txtbe);

            }
            else if (!osszesbrawler.Contains(ujbrawler))
            {
                MessageBox.Show("o nemtom kicsoda???", "hiba", MessageBoxButton.OK);

            }
            else
            {
                MessageBox.Show("ot mar hozzaadtad a lsitahoz :3", "hiba", MessageBoxButton.OK);
            }
            feltoltes();
            
        }

        public void feltoltes()
        {
            //listak krealasa
            textshenanigans("ezerkupasbrawler.txt");
            textshenanigans("nemezerkupas.txt");
            textshenanigans("osszesbrawler.txt");
            textshenanigans("droppoltbrawler.txt");



            hianyzo.Items.Clear();
            ezerkupas.Items.Clear();
            droppolt.Items.Clear();

            foreach (var b in nemezerkupas)
            {
                hianyzo.Items.Add(b);
            }

            foreach (var e in ezerkupasbrawler)
            {
                ezerkupas.Items.Add(e);
            }

            foreach (var d in droppoltbrawler)
            {
                droppolt.Items.Add(d);
            }

            //statisztik update
            stat_osszesSzama.Text = osszesbrawler.Count().ToString();
            stat_ezerSzama.Text = (ezerkupasbrawler.Count() + droppoltbrawler.Count()).ToString();
            stat_droppoltSzama.Text = droppoltbrawler.Count().ToString();
            stat_hatralevoSzama.Text = ezerkupasbrawler.Count().ToString();

        }

        private void textshenanigans(string filename)
        {
            if (File.Exists(filename) && File.ReadAllText(filename) != "")
            {
                switch (filename)
                {
                    case "ezerkupasbrawler.txt":
                        ezerkupasbrawler = File.ReadAllLines(filename)[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList(); break;
                    case "osszesbrawler.txt":
                        osszesbrawler = File.ReadAllLines(filename)[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList(); break;
                    case "nemezerkupas.txt":
                        nemezerkupas = File.ReadAllLines(filename)[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList(); break;
                    case "droppoltbrawler.txt":
                        droppoltbrawler = File.ReadAllLines(filename)[0].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList(); break;
                    default:break;
                }

                
            }
            else
            {
                File.AppendAllText(filename, "");
            }
        }

        private void hianyzo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ujhozzaadas.Text=(string)hianyzo.SelectedItem;
        }
        private void ezerkupas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dropped.IsEnabled = true;
            Remove.Content = "torolnem";
            dropping.Text = (string)ezerkupas.SelectedItem;

        }
        private void droppolt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dropped.IsEnabled = false;
            Remove.Content = "visszavittem";
            dropping.Text = (string)droppolt.SelectedItem;

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            File.AppendAllText("osszesbrawler.txt",';'+osszesupdate.Text);
            File.AppendAllText("nemezerkupas.txt",';'+osszesupdate.Text);
            osszesupdate.Text = "";
            feltoltes();
        }

        private void Dropped_Click(object sender, RoutedEventArgs e)
        {
            if ( ezerkupas.SelectedIndex == -1 || string.IsNullOrWhiteSpace(dropping.Text))
            { MessageBox.Show("nincs kit hozzaadnom a listahoz", "hiba", MessageBoxButton.OK); return; }

            

                droppoltbrawler.Add(dropping.Text);
                ezerkupasbrawler.Remove(dropping.Text);

                string txtbe = "";
                foreach (var b in ezerkupasbrawler)
                {
                    txtbe += (b + ";");
                }
                File.WriteAllText("ezerkupasbrawler.txt", txtbe);
                txtbe = "";
                foreach (var n in droppoltbrawler)
                {
                    txtbe += (n + ";");
                }
                File.WriteAllText("droppoltbrawler.txt", txtbe);

            

            feltoltes();

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (droppolt.SelectedIndex == -1 || ezerkupas.SelectedIndex == -1 || string.IsNullOrWhiteSpace(dropping.Text))
            {MessageBox.Show("nincs kit kitorolnom", "hiba", MessageBoxButton.OK); return; }


            if ((string)Remove.Content == "torolnem")
            {
                nemezerkupas.Add(dropping.Text);
                ezerkupasbrawler.Remove(dropping.Text);

                string txtbe = "";
                foreach (var b in ezerkupasbrawler)
                {
                    txtbe += (b + ";");
                }
                File.WriteAllText("ezerkupasbrawler.txt", txtbe);
                txtbe = "";
                foreach (var n in nemezerkupas)
                {
                    txtbe += (n + ";");
                }
                File.WriteAllText("nemezerkupas.txt", txtbe);
            }
            else
            {
                return; // tba :(
                // szia mukocc ugye?
            }

                feltoltes();
        }

        
    }
}
