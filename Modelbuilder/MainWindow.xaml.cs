using System;
using System.Windows;

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

        #endregion Call Country Page

        #region Call Currency Page

        private void ShowCurrencyPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new metadataCurrency();
        }

        #endregion Call Currency Page

        #region Call Category Page

        private void ShowCategoryPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new metadataCategory();
        }

        #endregion Call Category Page

        #region Call Storage Page

        private void ShowStoragePage(object sender, RoutedEventArgs e)
        {
            Main.Content = new metadataStorage();
        }

        #endregion Call Storage Page

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

        #endregion Exit Application
    }
}