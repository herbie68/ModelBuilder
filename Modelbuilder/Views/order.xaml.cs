using Microsoft.Win32;
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
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Modelbuilder.Views
{
    /// <summary>
    /// Interaction logic for order.xaml
    /// </summary>
    public partial class order : Page
    {
        public order()
        {
            InitializeComponent();
        }

        private void OrderDetail_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
