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

namespace WpfSakk
{
    /// <summary>
    /// Interaction logic for Tablakep.xaml
    /// </summary>
    public partial class Tablakep : Window
    {
        public Tablakep(Tabla tablakep)
        {
            InitializeComponent();

            for (int i = 0; i < tablakep.tabla.GetLength(0); i++)
            {
                grTabla.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < tablakep.tabla.GetLength(1); i++)
            {
                grTabla.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < tablakep.tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tablakep.tabla.GetLength(1); j++)
                {
                    Button gomb = new Button();
                    gomb.Content = $"{Convert.ToChar('a' + j)}{8 - i}";
                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb, j);

                    grTabla.Children.Add(gomb);
                }
            }
        }
    }
}
