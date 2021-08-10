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
using static System.Net.Mime.MediaTypeNames;
using Google.Protobuf.WellKnownTypes;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataProduct.xaml
    /// </summary>
    public partial class metadataProduct : Page
    {
        private HelperMySQL _helper;
        private DataTable _dt, _dtPS;
        private int _dbRowCount;
        private int _currentDataGridIndex, _currentDataGridPSIndex;
        static string DatabaseCategoryTable = "category", DatabaseStorageTable = "storage", DatabaseSupplierTable = "supplier", DatabaseBrandTable="brand", DatabaseUnitTable = "unit";

        public metadataProduct()
        {
            InitializeComponent();

            InitializeHelper();
            cboxProductCategory.ItemsSource = CategoryList();
            cboxProductSupplier.ItemsSource = SupplierList();
            cboxProductStorage.ItemsSource = StorageList();
            cboxProductBrand.ItemsSource = BrandList();
            cboxProductUnit.ItemsSource = UnitList();

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
            public Supplier(string Name, string Currency, string Id)
            {
                supplierName = Name;
                supplierCurrency = Currency;
                supplierId = Id;
            }

            public string supplierName { get; set; }
            public string supplierCurrency { get; set; }
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

        #region Create object for all brands in table for dropdown
        private class Brand
        {
            public Brand(string Name, string Id)
            {
                brandName = Name;
                brandId = Id;
            }

            public string brandName { get; set; }
            public string brandId { get; set; }
        }
        #endregion

        #region Create object for all units in table for dropdown
        private class Unit
        {
            public Unit(string Name, string Id)
            {
                unitName = Name;
                unitId = Id;
            }

            public string unitName { get; set; }
            public string unitId { get; set; }
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
            _dtPS = _helper.GetDataTblProductSupplier();

            // Populate data in datagrid from datatable
            ProductCode_DataGrid.DataContext = _dt;
            ProductSupplierCode_DataGrid.DataContext = _dtPS;

            // Set value
            _dbRowCount = _dt.Rows.Count;
            RecordsCount.Text = _dbRowCount.ToString();

            // Clear existing memo data
            inpProductMemo.Document.Blocks.Clear();

            string tmpStr = "";
            //update status
            if (_dt.Rows.Count != 1) { tmpStr = "s"; };
            string msg = "Status: " + _dt.Rows.Count + " producten" + tmpStr + " ingelezen.";
            UpdateStatus(msg);

        }
        #endregion

        #region Get content of Memofield
        private void GetMemo(int index)
        {
            string ContentProductMemo = string.Empty;

            if (_dt != null && index >= 0 && index < _dt.Rows.Count)
            {
                //set value
                DataRow row = _dt.Rows[index];


                if (row["product_Memo"] != null && row["product_Memo"] != DBNull.Value)
                {
                    //get value from DataTable
                    ContentProductMemo = row["product_Memo"].ToString();
                }

                if (!String.IsNullOrEmpty(ContentProductMemo))
                {
                    //clear existing data
                    inpProductMemo.Document.Blocks.Clear();

                    //convert to byte[]
                    byte[] dataArr = Encoding.UTF8.GetBytes(ContentProductMemo);

                    using (MemoryStream ms = new(dataArr))
                    {
                        //load data
                        TextRange flowDocRange = new TextRange(inpProductMemo.Document.ContentStart, inpProductMemo.Document.ContentEnd);
                        flowDocRange.Load(ms, DataFormats.Rtf);
                    }
                }
            }
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

        #region Selection changed ProductCode
        private void ProductCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridIndex = dg.SelectedIndex;

            // Get data from database
            _dtPS = _helper.GetDataTblProductSupplier();

            // Populate data in datagrid from datatable
            ProductSupplierCode_DataGrid.DataContext = _dtPS;

            GetMemo(dg.SelectedIndex);

            var _Minimalstock = float.Parse(Row_Selected["product_MinimalStock"].ToString());
            var _StandardOrderQuantity = float.Parse(Row_Selected["product_StandardOrderQuantity"].ToString());
            // var _Price = float.Parse(Row_Selected["product_Price"].ToString().Replace(".", ",")) / 100;
            var _Price = float.Parse(Row_Selected["product_Price"].ToString());

            valueProductId.Text = Row_Selected["product_Id"].ToString();
            valueCategoryId.Text = Row_Selected["product_CategoryId"].ToString();
            valueCategoryName.Text = Row_Selected["product_CategoryName"].ToString();
            valueStorageId.Text = Row_Selected["product_StorageId"].ToString();
            valueStorageName.Text = Row_Selected["product_StorageName"].ToString();
            valueSupplierId.Text = Row_Selected["product_SupplierId"].ToString();
            valueSupplierName.Text = Row_Selected["product_SupplierName"].ToString();
            valueBrandId.Text = Row_Selected["product_BrandId"].ToString();
            valueBrandName.Text = Row_Selected["product_BrandName"].ToString();
            valueUnitId.Text = Row_Selected["product_UnitId"].ToString();
            valueUnitName.Text = Row_Selected["product_UnitName"].ToString();
            inpProductCode.Text = Row_Selected["product_Code"].ToString();
            inpProductName.Text = Row_Selected["product_Name"].ToString();
            inpProductMinimalStock.Text = _Minimalstock.ToString("#,##0.00;- #,##0.00");
            inpProductStandardOrderQuantity.Text = _StandardOrderQuantity.ToString("#,##0.00;- #,##0.00");
            inpProductPrice.Text = _Price.ToString("€ #,##0.00;€ - #,##0.00");
            inpSupplierProductNumber.Text = Row_Selected["product_SupplierProductNumber"].ToString();
            inpProductDimensions.Text = Row_Selected["product_Dimensions"].ToString();

            if (Row_Selected["product_ProjectCosts"].ToString() == "0")
            {
                chkProjectProjectCosts.IsChecked = false;
            }
            else
            {
                chkProjectProjectCosts.IsChecked = true;
            }

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

            //Select the saved Brand in the combobox by default
            foreach (Brand brand in cboxProductBrand.Items)
            {
                if (brand.brandName == Row_Selected["product_BrandName"].ToString())
                {
                    cboxProductBrand.SelectedItem = brand;
                    break;
                }
            }

            //Select the saved Unit in the combobox by default
            foreach (Unit unit in cboxProductUnit.Items)
            {
                if (unit.unitName == Row_Selected["product_UnitName"].ToString())
                {
                    cboxProductUnit.SelectedItem = unit;
                    break;
                }
            }

            //Select the saved Storage location in the combobox by default
            foreach (Storage storage in cboxProductStorage.Items)
            {
                if (storage.storageName == Row_Selected["product_StorageName"].ToString())
                {
                    cboxProductStorage.SelectedItem = storage;
                    break;
                }
            }
            // Get data from database
            _dtPS = _helper.GetDataTblProductSupplier(int.Parse(valueProductId.Text));

            // Populate data in datagrid from datatable
            ProductSupplierCode_DataGrid.DataContext = _dtPS;

        }
        #endregion

        #region Selection changed ProductSupplierCode
        private void ProductSupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dgPS = (DataGrid)sender;

            if (dgPS.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridPSIndex = dgPS.SelectedIndex;
            float _ProductPrice = 0;

            if (Row_Selected["productSupplier_ProductPrice"].ToString() != "") { _ProductPrice = float.Parse(Row_Selected["productSupplier_ProductPrice"].ToString()); };

            valueProductSupplierId.Text = Row_Selected["productSupplier_Id"].ToString();
            valueProductSupplierCurrencyId.Text = Row_Selected["productSupplier_CurrencyId"].ToString();
            valueProductSupplierSupplierId.Text = Row_Selected["productSupplier_SupplierId"].ToString();
            valueProductSupplierSupplierName.Text = Row_Selected["productSupplier_SupplierName"].ToString();

            inpSupplierProductNumber.Text = Row_Selected["productSupplier_ProductNumber"].ToString();
            inpSupplierProductName.Text = Row_Selected["productSupplier_ProductName"].ToString();
            dispProductSupplierCurrencySymbol.Text = Row_Selected["productSupplier_CurrencySymbol"].ToString();
            inpSupplierProductPrice.Text = _ProductPrice.ToString("€ #,##0.00;€ - #,##0.00");;
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
            var productMinimalStock = float.Parse(inpProductMinimalStock.Text.Replace(",", "."));
            var productStandardOrderQuantity = float.Parse(inpProductStandardOrderQuantity.Text.Replace(",", "."));
            var productPrice = float.Parse(inpProductPrice.Text.Replace(",", ".").Replace("€", "").Replace(" ", ""));
            var productSupplierProductNumber = inpSupplierProductNumber.Text;
            if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
            { productProjectCosts = 0; }
            var productCategoryId = int.Parse(valueCategoryId.Text);
            var productCategoryName = valueCategoryName.Text;
            var productStorageId = int.Parse(valueStorageId.Text);
            var productStorageName = valueStorageName.Text;
            var productSupplierId = int.Parse(valueSupplierId.Text);
            var productSupplierName = valueSupplierName.Text;
            var productBrandId = int.Parse(valueBrandId.Text);
            var productBrandName = valueBrandName.Text;
            var productDimensions = inpProductDimensions.Text;
            var productUnitId = int.Parse(valueUnitId.Text);
            var productUnitName = valueBrandName.Text;

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpProductMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblProduct(productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productSupplierProductNumber, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productSupplierId, productSupplierName, productBrandId, productBrandName, productDimensions, productUnitId, productUnitName, memo);
            UpdateStatus(result);
        }
        #endregion

        #region Get rich text from flow document
        private string GetRichTextFromFlowDocument(FlowDocument fDoc)
        {
            string result = string.Empty;

            //convert to string
            if (fDoc != null)
            {
                TextRange tr = new TextRange(fDoc.ContentStart, fDoc.ContentEnd);

                using (MemoryStream ms = new MemoryStream())
                {
                    tr.Save(ms, DataFormats.Rtf);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return result;
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
            valueBrandId.Text = ((Brand)cboxProductBrand.SelectedItem).brandId.ToString();
            valueBrandName.Text = ((Brand)cboxProductBrand.SelectedItem).brandName.ToString();
            valueUnitId.Text = ((Unit)cboxProductUnit.SelectedItem).unitId.ToString();
            valueUnitName.Text = ((Unit)cboxProductUnit.SelectedItem).unitName.ToString();

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

        #region Click Save Data button (on suppliertoolbar)
        private void supplierToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridPSIndex;
            valueProductSupplierSupplierId.Text = ((Supplier)cboxProductSupplier.SelectedItem).supplierId.ToString();
            valueProductSupplierSupplierName.Text = ((Supplier)cboxProductSupplier.SelectedItem).supplierName.ToString();


            if (valueProductSupplierId.Text == "")
            // if (_dt.Rows.Count > _dbRowCount)
            {
                InsertRow(ProductSupplierCode_DataGrid.SelectedIndex);
            }
            else
            {
                UpdateRow(ProductSupplierCode_DataGrid.SelectedIndex);
            }

            GetData();

            // Make sure the eddited row in the datagrid is selected
            ProductSupplierCode_DataGrid.SelectedIndex = rowIndex;
            ProductSupplierCode_DataGrid.Focus();
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

        #region Delete Data button (on SupplierToolbar)
        private void supplierToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;
            // Do whatever is necesary to delete the selected row in the datagrid of the supplier tabpage
            /*
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
            */
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
            var productPrice = float.Parse(inpProductPrice.Text.Replace("€", "").Replace(" ", ""));
            var productSupplierProductNumber = inpSupplierProductNumber.Text;
            if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
            { productProjectCosts = 0; }
            var productCategoryId = int.Parse(valueCategoryId.Text);
            var productCategoryName = valueCategoryName.Text;
            var productStorageId = int.Parse(valueStorageId.Text);
            var productStorageName = valueStorageName.Text;
            var productSupplierId = int.Parse(valueSupplierId.Text);
            var productSupplierName = valueSupplierName.Text;
            var productBrandId = int.Parse(valueBrandId.Text);
            var productBrandName = valueBrandName.Text;
            var productDimensions = inpProductDimensions.Text;
            var productUnitId = int.Parse(valueUnitId.Text);
            var productUnitName = valueUnitName.Text;

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpProductMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblProduct(productId, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productSupplierProductNumber, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productSupplierId, productSupplierName, productBrandId, productBrandName, productDimensions, productUnitId, productUnitName, memo);
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

            dbSupplierConnection.SqlSelectionString = "supplier_Name, supplier_CurrencySymbol, supplier_Id";
            dbSupplierConnection.SqlOrderByString = "supplier_Id";
            dbSupplierConnection.TableName = DatabaseSupplierTable;

            DataTable dtSupplierSelection = dbSupplierConnection.LoadSpecificMySqlData();

            List<Supplier> SupplierList = new();

            for (int i = 0; i < dtSupplierSelection.Rows.Count; i++)
            {
                SupplierList.Add(new Supplier(dtSupplierSelection.Rows[i][0].ToString(), dtSupplierSelection.Rows[i][1].ToString(),
                    dtSupplierSelection.Rows[i][2].ToString()));
            };
            return SupplierList;
        }
        #endregion

        #region Fill Brand dropdown
        static List<Brand> BrandList()
        {

            Database dbBrandConnection = new()
            {
                TableName = DatabaseBrandTable
            };

            dbBrandConnection.SqlSelectionString = "brand_Name, brand_Id";
            dbBrandConnection.SqlOrderByString = "brand_Id";
            dbBrandConnection.TableName = DatabaseBrandTable;

            DataTable dtBrandSelection = dbBrandConnection.LoadSpecificMySqlData();

            List<Brand> BrandList = new();

            for (int i = 0; i < dtBrandSelection.Rows.Count; i++)
            {
                BrandList.Add(new Brand(dtBrandSelection.Rows[i][0].ToString(),
                    dtBrandSelection.Rows[i][1].ToString()));
            };
            return BrandList;
        }
        #endregion

        #region Fill Unit dropdown
        static List<Unit> UnitList()
        {

            Database dbUnitConnection = new()
            {
                TableName = DatabaseUnitTable
            };

            dbUnitConnection.SqlSelectionString = "unit_Name, unit_Id";
            dbUnitConnection.SqlOrderByString = "unit_Id";
            dbUnitConnection.TableName = DatabaseUnitTable;

            DataTable dtUnitSelection = dbUnitConnection.LoadSpecificMySqlData();

            List<Unit> UnitList = new();

            for (int i = 0; i < dtUnitSelection.Rows.Count; i++)
            {
                UnitList.Add(new Unit(dtUnitSelection.Rows[i][0].ToString(),
                    dtUnitSelection.Rows[i][1].ToString()));
            };
            return UnitList;
        }
        #endregion
    }
}