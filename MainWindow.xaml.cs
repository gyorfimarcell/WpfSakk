﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSakk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Jatszma> jatszmak;
        public MainWindow()
        {
            InitializeComponent();
            jatszmak = File.ReadAllLines("jatszmak.txt").Select(sor => new Jatszma(sor)).ToList();

            MessageBox.Show($"A huszárok ennyi mezőt haladtak: {jatszmak.Sum(jatszma => jatszma.HuszarokLepesszama) * 4}");
            MessageBox.Show($"A futók ennyiszer léptek: {jatszmak.Sum(jatszma => jatszma.TisztLepesszama('F'))}");

            MessageBox.Show($"A játékok amiben vezért ütöttek: {string.Join(';', jatszmak.Where(x => x.VezertUttotek).Select(x => jatszmak.IndexOf(x) + 1))}");

            MessageBox.Show($"A vezérek {jatszmak.Sum(x => x.VezerLepett)} mezőt léptek.");

            MessageBox.Show($"A világos király {jatszmak.Count(x => !x.VilagosKiralyMozgott)} játékban nem mozgott.");

            MessageBox.Show($"{jatszmak.Count(x => x.MegmaradtBabuk > 20)} játékban maradt több mint 20 bábu.");

            Tabla aktualisAllapot = new Tabla();
            aktualisAllapot.NyitoAllapotLetrehozasa();
            aktualisAllapot.Lep("Hx45");
            //... sok sok lépés

            //Hogyan néz ki ez vizuálisan
            Tablakep winTablakep = new Tablakep(aktualisAllapot);
            winTablakep.ShowDialog();
        }
    }
}