﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataProduct.xaml
    /// </summary>
    public partial class metadataProduct : Page
    {
        private HelperMySQL _helper;
        private DataTable _dt;
        private int _dbRowCount;
        private int _currentDataGridIndex;
        static string DatabaseCategoryTable = "category", DatabaseStorageTable = "storage", DatabaseSupplierTable = "supplier";

        public metadataProduct()
        {
            InitializeComponent();

            InitializeHelper();
            //cboxProductCategory.ItemsSource = _helper.CategoryList();
            //cboxProductSupplier.ItemsSource = _helper.SupplierList();
            //cboxProductStorage.ItemsSource = _helper.StorageList();
            cboxProductCategory.ItemsSource = CategoryList();
            cboxProductSupplier.ItemsSource = SupplierList();
            cboxProductStorage.ItemsSource = StorageList();


            GetData();
        }

        #region Create object for all categories in table for dropdown
        private class Category
        {
            public Category(string Name, string Id)
            {
                categoryName = Name;
                categoryId = Id;
            }

            public string categoryName { get; set; }
            public string categoryId { get; set; }
        }
        #endregion

        #region Create object for all Suppliers in table for dropdown
        private class Supplier
        {
            public Supplier(string Name, string Id)
            {
                supplierName = Name;
                supplierId = Id;
            }

            public string supplierName { get; set; }
            public string supplierId { get; set; }
        }
        #endregion

        #region Create object for all storagelocations in table for dropdown
        private class Storage
        {
            public Storage(string Name, string Id)
            {
                storageName = Name;
                storageId = Id;
            }

            public string storageName { get; set; }
            public string storageId { get; set; }
        }
        #endregion

        #region Create object for all countries in table for dropdown
        private class Country
        {
            public Country(string Name, string Id)
            {
                countryName = Name;
                countryId = Id;
            }

            public string countryName { get; set; }
            public string countryId { get; set; }
        }
        #endregion

        #region CommonCommandBinding_CanExecute
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region Get the data
        private void GetData()
        {
            InitializeHelper();

            // Get data from database
            _dt = _helper.GetDataTblProduct();

            // Populate data in datagrid from datatable
            ProductCode_DataGrid.DataContext = _dt;

            // Set value
            _dbRowCount = _dt.Rows.Count;
            RecordsCount.Text = _dbRowCount.ToString();

            string tmpStr = "";
            //update status
            if (_dt.Rows.Count != 1) { tmpStr = "s"; };
            string msg = "Status: " + _dt.Rows.Count + " producten" + tmpStr + " ingelezen.";
            UpdateStatus(msg);

        }
        #endregion

        #region InitializeHelper (connect to database)
        private void InitializeHelper()
        {
            if (_helper == null)
            {
                _helper = new HelperMySQL("localhost", 3306, "modelbuilder", "root", "admin");
            }
        }
        #endregion

        #region Selection changed
        private void ProductCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridIndex = dg.SelectedIndex;

            valueProductId.Text = Row_Selected["product_Id"].ToString();
            valueCategoryId.Text = Row_Selected["product_CategoryId"].ToString();
            valueCategoryName.Text = Row_Selected["product_CategoryName"].ToString();
            valueStorageId.Text = Row_Selected["product_StorageId"].ToString();
            valueStorageName.Text = Row_Selected["product_StorageName"].ToString();
            valueSupplierId.Text = Row_Selected["product_SupplierId"].ToString();
            valueSupplierName.Text = Row_Selected["product_SupplierName"].ToString();
            inpProductCode.Text = Row_Selected["product_Code"].ToString();
            inpProductName.Text = Row_Selected["product_Name"].ToString();
            inpProductMinimalStock.Text = Row_Selected["product_MinimalStock"].ToString().Replace(".", ",");
            inpProductStandardOrderQuantity.Text = Row_Selected["product_StandardOrderQuantity"].ToString().Replace(".", ",");
            inpProductPrice.Text = Row_Selected["product_Price"].ToString().Replace(".", ",");
            inpSupplierProductNumber.Text = Row_Selected["product_SupplierProductNumber"].ToString();
            chkProjectProjectCosts.IsChecked = Row_Selected["product_ProjectCosts"].ToString() == "0";


            //Select the saved Category in the combobox by default
            foreach (Category category in cboxProductCategory.Items)
            {
                if (category.categoryName == Row_Selected["product_CategoryName"].ToString())
                {
                    cboxProductCategory.SelectedItem = category;
                    break;
                }
            }

            //Select the saved Supplier in the combobox by default
            foreach (Supplier supplier in cboxProductSupplier.Items)
            {
                if (supplier.supplierName == Row_Selected["product_SupplierName"].ToString())
                {
                    cboxProductSupplier.SelectedItem = supplier;
                    break;
                }
            }
            

            //Select the saved Storage location in the combobox by default
            foreach (Storage storage in cboxProductStorage.Items)
            {
                if (storage.storageName == Row_Selected["product_StorageName"].ToString())
                {
                    cboxProductCategory.SelectedItem = storage;
                    break;
                }
            }
        }
        #endregion

        #region Insert new row in table
        private void InsertRow(int dgIndex)
        {
            //since the DataGrid DataContext is set to the DataTable, 
            //the DataTable is updated when data is modified in the DataGrid
            //get last row
            DataRow row = _dt.Rows[_dt.Rows.Count - 1];
            int productProjectCosts = 0;

            var productCode = inpProductCode.Text;
            var productName = inpProductName.Text;
            var productMinimalStock = float.Parse(inpProductMinimalStock.Text);
            var productStandardOrderQuantity = float.Parse(inpProductStandardOrderQuantity.Text);
            var productPrice = float.Parse(inpProductPrice.Text);
            var productSupplierProductNumber = inpSupplierProductNumber.Text;
            if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
            { productProjectCosts = 0; }
            //byte productProjectCosts = byte.Parse(chkProjectProjectCosts.IsChecked);
            var productCategoryId = int.Parse(valueCategoryId.Text);
            var productCategoryName = valueCategoryName.Text;
            var productStorageId = int.Parse(valueStorageId.Text);
            var productStorageName = valueStorageName.Text;
            var productSupplierId = int.Parse(valueSupplierId.Text);
            var productSupplierName = valueSupplierName.Text;
            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblProduct(productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productSupplierProductNumber, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productSupplierId, productSupplierName);
            UpdateStatus(result);
        }
        #endregion

        #region Delete row in table
        private void DeleteRow(int dgIndex)
        {
            //since the DataGrid DataContext is set to the DataTable, 
            //the DataTable is updated when data is modified in the DataGrid
            //get last row
            DataRow row = _dt.Rows[_dt.Rows.Count - 1];

            int productId = int.Parse(valueProductId.Text);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.DeleteTblProduct(productId);
            UpdateStatus(result);
        }
        #endregion

        #region Click Save Data button (on toolbar)
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            // Update Id, Code, Name and Symbol values with the selected Country and Currency
            valueCategoryId.Text = ((Category)cboxProductCategory.SelectedItem).categoryId.ToString();
            valueCategoryName.Text = ((Category)cboxProductCategory.SelectedItem).categoryName.ToString();
            valueStorageId.Text = ((Storage)cboxProductStorage.SelectedItem).storageId.ToString();
            valueStorageName.Text = ((Storage)cboxProductStorage.SelectedItem).storageName.ToString();
            valueSupplierId.Text = ((Supplier)cboxProductSupplier.SelectedItem).supplierId.ToString();
            valueSupplierName.Text = ((Supplier)cboxProductSupplier.SelectedItem).supplierName.ToString();

            if (valueProductId.Text == "")
            // if (_dt.Rows.Count > _dbRowCount)
            {
                InsertRow(ProductCode_DataGrid.SelectedIndex);
            }
            else
            {
                UpdateRow(ProductCode_DataGrid.SelectedIndex);
            }

            GetData();

            // Make sure the eddited row in the datagrid is selected
            ProductCode_DataGrid.SelectedIndex = rowIndex;
            ProductCode_DataGrid.Focus();
        }
        #endregion

        #region Delete Data button (on toolbar)
        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            DeleteRow(ProductCode_DataGrid.SelectedIndex);

            GetData();

            if (rowIndex == 0)
            {
                ProductCode_DataGrid.SelectedIndex = 0;
            }
            else
            {
                ProductCode_DataGrid.SelectedIndex = rowIndex - 1;
            }

            ProductCode_DataGrid.Focus();
        }
        #endregion

        #region Update row
        private void UpdateRow(int dgIndex)
        {
            //when DataGrid SelectionChanged occurs, the value of '_currentDataGridIndex' is set
            //to DataGrid SelectedIndex
            //get data from DataTable
            DataRow row = _dt.Rows[_currentDataGridIndex];
            var productProjectCosts = 0;



            var productId = int.Parse(valueProductId.Text);
            string productCode = inpProductCode.Text;
            string productName = inpProductName.Text;
            var productMinimalStock = float.Parse(inpProductMinimalStock.Text);
            var productStandardOrderQuantity = float.Parse(inpProductStandardOrderQuantity.Text);

            if (inpProductPrice.Text == "") { inpProductPrice.Text = "0,00"; }
            { inpProductPrice.Text.Replace(".", ","); }
            var productPrice = float.Parse(inpProductPrice.Text);
            var productSupplierProductNumber = inpSupplierProductNumber.Text;
            if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
            { productProjectCosts = 0; }
            //byte productProjectCosts = byte.Parse(chkProjectProjectCosts.IsChecked);
            var productCategoryId = int.Parse(valueCategoryId.Text);
            var productCategoryName = valueCategoryName.Text;
            var productStorageId = int.Parse(valueStorageId.Text);
            var productStorageName = valueStorageName.Text;
            var productSupplierId = int.Parse(valueSupplierId.Text);
            var productSupplierName = valueSupplierName.Text;

            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblProduct(productId, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productSupplierProductNumber, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productSupplierId, productSupplierName);
            UpdateStatus(result);
        }
        #endregion

        #region Update status
        private void UpdateStatus(string msg)
        {
            if (!String.IsNullOrEmpty(msg))
            {
                if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
                {
                    textBoxStatus.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                    Debug.WriteLine(String.Format("{0} - Status: {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
                else
                {
                    textBoxStatus.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                    Debug.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
            }
        }
        #endregion

        #region Fill Category dropdown
        static List<Category> CategoryList()
        {

            Database dbCategoryConnection = new()
            {
                TableName = DatabaseCategoryTable
            };

            dbCategoryConnection.SqlSelectionString = "category_Name, category_Id";
            dbCategoryConnection.SqlOrderByString = "category_Id";
            dbCategoryConnection.TableName = DatabaseCategoryTable;

            DataTable dtCategorySelection = dbCategoryConnection.LoadSpecificMySqlData();

            List<Category> CategoryList = new();

            for (int i = 0; i < dtCategorySelection.Rows.Count; i++)
            {
                CategoryList.Add(new Category(dtCategorySelection.Rows[i][0].ToString(),
                    dtCategorySelection.Rows[i][1].ToString()));
            };
            return CategoryList;
        }
        #endregion

        #region Fill Storage dropdown
        static List<Storage> StorageList()
        {

            Database dbStorageConnection = new()
            {
                TableName = DatabaseStorageTable
            };

            dbStorageConnection.SqlSelectionString = "storage_Name, storage_Id";
            dbStorageConnection.SqlOrderByString = "storage_Id";
            dbStorageConnection.TableName = DatabaseStorageTable;

            DataTable dtStorageSelection = dbStorageConnection.LoadSpecificMySqlData();

            List<Storage> StorageList = new();

            for (int i = 0; i < dtStorageSelection.Rows.Count; i++)
            {
                StorageList.Add(new Storage(dtStorageSelection.Rows[i][0].ToString(),
                    dtStorageSelection.Rows[i][1].ToString()));
            };
            return StorageList;
        }
        #endregion

        #region Fill Supplier dropdown
        static List<Supplier> SupplierList()
        {

            Database dbSupplierConnection = new()
            {
                TableName = DatabaseSupplierTable
            };

            dbSupplierConnection.SqlSelectionString = "supplier_Name, supplier_Id";
            dbSupplierConnection.SqlOrderByString = "supplier_Id";
            dbSupplierConnection.TableName = DatabaseSupplierTable;

            DataTable dtSupplierSelection = dbSupplierConnection.LoadSpecificMySqlData();

            List<Supplier> SupplierList = new();

            for (int i = 0; i < dtSupplierSelection.Rows.Count; i++)
            {
                SupplierList.Add(new Supplier(dtSupplierSelection.Rows[i][0].ToString(),
                    dtSupplierSelection.Rows[i][1].ToString()));
            };
            return SupplierList;
        }
        #endregion

    }
}