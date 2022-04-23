global using ConnectionNamespace;

global using Microsoft.Win32;

global using MySql.Data.MySqlClient;

global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Diagnostics;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Text;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Documents;
global using System.Windows.Input;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
using Modelbuilder.Views;



namespace Modelbuilder;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    #region TimeManagement
    #region Show TimeManagement Page
    private void ShowTimeManagementPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new timemanagement();
    }
    #endregion Show TimeManagement Page

    #region Show TimeReport Page
    private void ShowTimeReportPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ReportingTime();
    }
    #endregion Show TimeReport Page

    #region Import Time Entries
    private void ShowTimeEntryImportPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ImportTimeEntry();
    }
    #endregion Import Time Entries
    #endregion TimeManegement

    #region Maintain orders and deliveries
    #region Call Order Page
    private void ShowOrderPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new storageOrder();
    }

    #endregion Call Order Page

    #region Call Order RecievePage
    private void ShowOrderReceiptPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new storageOrderReceipt();
    }
    #endregion Call Order Recieve Page

    #region Call Stock Changes Page
    private void ShowStockChangesPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new storageStockChanges();
    }
    #endregion Call Stock Changes Page
    #endregion Maintain orders and deliveries

    #region Metadata pages
    #region Call Country Page


    private void storageOrderPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new storageOrder();
    }
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

    #region Call Supplier Page
    private void ShowSupplierPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataSupplier();
    }
    #endregion Call Supplier Page

    #region Call Product Page
    private void ShowProductPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataProduct();
    }
    #endregion Call Product Page

    #region Call Project Page
    private void ShowProjectPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataProject();
    }
    #endregion Call Product Page

    #region Units
    #region Call Unit Page
    private void ShowUnitPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataUnit();
    }
    #endregion

    #region Import Units
    private void ShowUnitsImportPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ImportUnits();
    }
    #endregion
    #endregion
    #endregion

    #region Exit Application

    private void ApplicationExit_Click(object sender, EventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }

    #endregion Exit Application

}
