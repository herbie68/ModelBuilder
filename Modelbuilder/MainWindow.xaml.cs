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
using Modelbuilder;
using System.Windows.Controls.Ribbon;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Call Country Page
        private void ShowCountryPage(object sender, RoutedEventArgs e)
        {

            Main.Content = new metadataCountry();
        }
        #endregion

        #region Call Currency Page
        private void ShowCurrencyPage(object sender, RoutedEventArgs e)
        {

            Main.Content = new metadataCurrency();
        }
        #endregion

        #region Call Category Page
        private void ShowCategoryPage(object sender, RoutedEventArgs e)
        {

            Main.Content = new metadataCategory();
        }
        #endregion

        #region Call Storage Page
        private void ShowStoragePage(object sender, RoutedEventArgs e)
        {

            Main.Content = new metadataStorage();
        }
        #endregion

        #region Call Worktype Page
        private void ShowWorktypePage(object sender, RoutedEventArgs e)
        {

            Main.Content = new metadataWorktype();
        }
        #endregion
        #region Exit Application
        private void ApplicationExit_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
    }
}
