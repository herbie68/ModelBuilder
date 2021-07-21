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

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for dialogWorktype.xaml
    /// </summary>
    public partial class dialogWorktype : Window
    {
        public static string diaLogWorktypeValue = String.Empty;
        public dialogWorktype()
        {
            InitializeComponent();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            diaLogWorktypeValue = NewWorktypeName.Text;
            this.Close();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
