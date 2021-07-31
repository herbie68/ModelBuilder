using System;
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

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataProduct.xaml
    /// </summary>
    public partial class metadataProduct : Page
    {
        private readonly string DatabaseTable = "product", DatabaseCategoryTable = "category", DatabaseStorageTable = "storage", DatabaseProductTable="product";
        private HelperMySQL _helper;
        private DataTable _dt, _dtCategory, _dtStorage, _dtProduct;
        private int _dbRowCount = 0;
        private int _currentDataGridIndex = 0;

        public metadataProduct()
        {
            InitializeComponent();
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
            inpProductCode.Text = Row_Selected["product_Code"].ToString();
            inpProductName.Text = Row_Selected["product_Name"].ToString();


            //Select the saved Country in the combobox by default
            foreach (Supplier supplier in cboxProductSupplierName.Items)
            {
                if (supplier.supplierName == Row_Selected["product_SupplierName"].ToString())
                {
                    cboxProductSupplierName.SelectedItem = supplier;
                    break;
                }
            }

            //Select the saved Category in the combobox by default
            foreach (Category category in cboxProductCategoryName.Items)
            {
                if (category.categoryName == Row_Selected["product_CategoryName"].ToString())
                {
                    cboxProductCategoryName.SelectedItem = category;
                    break;
                }
            }

            //Select the saved Storage location in the combobox by default
            foreach (Storage storage in cboxProductStorageName.Items)
            {
                if (storage.storageName == Row_Selected["product_StorageName"].ToString())
                {
                    cboxProductCategoryName.SelectedItem = storage;
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

            string productCode = inpProductCode.Text;
            string productName = inpProductName.Text;

            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblProduct(productCode, productName);
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
            valueCategoryId.Text = ((Category)cboxProductCategoryName.SelectedItem).categoryId.ToString();
            valueCategoryName.Text = ((Category)cboxProductCategoryName.SelectedItem).categoryName.ToString();
            valueStorageId.Text = ((Storage)cboxProductStorageName.SelectedItem).storageId.ToString();
            valueStorageName.Text = ((Storage)cboxProductStorageName.SelectedItem).storageName.ToString();
            valueSupplierId.Text = ((Supplier)cboxProductSupplierName.SelectedItem).supplierId.ToString();
            valueSupplierName.Text = ((Supplier)cboxProductSupplierName.SelectedItem).supplierName.ToString();

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

            int productId = int.Parse(valueProductId.Text);
            string productCode = inpProductCode.Text;
            string productName = inpProductName.Text;

            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblProduct(productId, productCode, productName);
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
    }
}