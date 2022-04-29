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
    #endregion

    #region Export Time Entries
    private void ShowTimeEntryExportPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportTimeEntries();
    }
    #endregion
    #endregion TimeManegement

    #region ProjectManagement
    #region Call Project Page
    private void ShowProjectPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataProject();
    }
    #endregion

    #region Call Export Projects
    private void ShowExportProjectPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportProjects();
    }
    #endregion

    #endregion

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

    private void storageOrderPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new storageOrder();
    }

    #endregion Maintain orders and deliveries

    #region Metadata pages
    #region Brands

    #region Call Export Brands
    private void ShowExportBrandsPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportBrands();
    }
    #endregion
    #endregion

    #region Country
    #region Call Country Page
    private void ShowCountryPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataCountry();
    }
    #endregion

    #region Call Export Countries
    private void ShowExportCountriesPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportCountries();
    }
    #endregion
    #endregion

    #region Currency
    #region Call Currency Page
    private void ShowCurrencyPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataCurrency();
    }
    #endregion

    #region Call Export Currencies
    private void ShowExportCurrenciesPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportCurrencies();
    }
    #endregion
    #endregion

    #region Categories
    #region Call Category Page
    private void ShowCategoryPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataCategory();
    }
    #endregion

    #region Call Export Categories
    private void ShowExportCategoriesPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportCategories();
    }
    #endregion
    #endregion

    #region Storage
    #region Call Storage Page
    private void ShowStoragePage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataStorage();
    }
    #endregion

    #region Call Export Storages
    private void ShowExportStoragePage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportStorage();
    }
    #endregion
    #endregion

    #region Worktypes
    #region Call Worktype Page
    private void ShowWorktypePage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataWorktype();
    }
    #endregion

    #region Call Export Worktypes
    private void ShowExportWorktypesPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportWorkTypes();
    }
    #endregion
    #endregion

    #region Supplier
    #region Call Supplier Page
    private void ShowSupplierPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataSupplier();
    }
    #endregion

    #region Call Export Suppliers
    private void ShowExportSupplierPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportSupplier();
    }
    #endregion

    #region Call Export SuppliersContacts
    private void ShowExportSupplierContactsPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportSupplierContacts();
    }
    #endregion

    #region Call Export Contacttypes
    private void ShowExportContactTypesPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportContacttypes();
    }
    #endregion
    #endregion

    #region Products
    #region Call Product Page
    private void ShowProductPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new metadataProduct();
    }
    #endregion

    #region Call Export Products
    private void ShowExportProductsPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportProducts();
    }
    #endregion

    #region Call Export Products per Supplier
    private void ShowExportProductsSupplierPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportProductsSupplier();
    }
    #endregion
    #endregion

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

    #region Export Units
    private void ShowUnitsExportPage(object sender, RoutedEventArgs e)
    {
        Main.Content = new ExportUnits();
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
