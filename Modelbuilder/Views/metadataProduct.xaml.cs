using Org.BouncyCastle.Asn1.Crmf;
using System.Windows.Controls;

namespace Modelbuilder;
public partial class metadataProduct : Page
{
    private HelperGeneral _helperGeneral;
    private HelperProduct _helper;
    private DataTable _dt, _dtPS;
    private int _dbRowCount;
    private int _currentDataGridIndex, _currentDataGridPSIndex;

    public metadataProduct()
    {
        var BrandList = new List<HelperGeneral.Brand>();
        var SupplierList = new List<HelperGeneral.Supplier>();
        var CategoryList = new List<HelperGeneral.Category>();
        var StorageList =new List<HelperGeneral.Storage>();
        var UnitList = new List<HelperGeneral.Unit>();
        InitializeComponent();

        InitializeHelper();
        cboxProductCategory.ItemsSource = _helperGeneral.GetCategoryList(CategoryList);
        cboxProductSupplier.ItemsSource = _helperGeneral.GetSupplierList(SupplierList);
        cboxProductStorage.ItemsSource = _helperGeneral.GetStorageList(StorageList);
        cboxProductBrand.ItemsSource = _helperGeneral.GetBrandList(BrandList);
        cboxProductUnit.ItemsSource = _helperGeneral.GetUnitList(UnitList);

        //By default  the toolbarbuttons have to be disabled, this doesn't work from within the xaml
        btnNewProduct.IsEnabled = false;
        btnSaveProduct.IsEnabled = false;
        btnDeleteProduct.IsEnabled = false;

        GetData();
    }

    #region CommonCommandBinding_CanExecute
    private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }
    #endregion CommonCommandBinding_CanExecute

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

        // Clear existing memo data
        inpProductMemo.Document.Blocks.Clear();

        string tmpStr = "";
        //update status
        if (_dt.Rows.Count != 1) { tmpStr = "s"; }
        string msg = "Status: " + _dt.Rows.Count + " producten" + tmpStr + " ingelezen.";
        UpdateStatus(msg);
    }
    #endregion Get the data

    #region Get content of Memofield
    private void GetMemo(int index)
    {
        string ContentProductMemo = string.Empty;

        if (_dt != null && index >= 0 && index < _dt.Rows.Count)
        {
            //set value
            DataRow row = _dt.Rows[index];


            if (row["Memo"] != null && row["Memo"] != DBNull.Value)
            {
                //get value from DataTable
                ContentProductMemo = row["Memo"].ToString();
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
    #endregion Get content of Memofield

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral("localhost", 3306, "modelbuilder", "root", "admin");
            //_helperGeneral = new HelperGeneral("db4free.net", 3306, "modelbuilder", "herbie68", "9b9749c1");
        }
        if (_helper == null)
        {
            _helper = new HelperProduct("localhost", 3306, "modelbuilder", "root", "admin");
            //_helper = new HelperProduct("db4free.net", 3306, "modelbuilder", "herbie68", "9b9749c1");
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

        GetMemo(dg.SelectedIndex);

        var _Minimalstock = double.Parse(Row_Selected["MinimalStock"].ToString().Replace(".", ","));
        var _StandardOrderQuantity = double.Parse(Row_Selected["StandardOrderQuantity"].ToString().Replace(".", ","));
        var _Price = double.Parse(Row_Selected["Price"].ToString().Replace(".", ","));

        valueProductId.Text = Row_Selected["Id"].ToString();
        valueCategoryId.Text = Row_Selected["Category_Id"].ToString();
        valueStorageId.Text = Row_Selected["Storage_Id"].ToString();
        valueBrandId.Text = Row_Selected["Brand_Id"].ToString();
        valueUnitId.Text = Row_Selected["Unit_Id"].ToString();
        valueImageRotationAngle.Text = Row_Selected["ImageRotationAngle"].ToString();
        inpProductCode.Text = Row_Selected["Code"].ToString();
        inpProductName.Text = Row_Selected["Name"].ToString();
        inpProductMinimalStock.Text = _Minimalstock.ToString("#,##0.00;- #,##0.00");
        inpProductStandardOrderQuantity.Text = _StandardOrderQuantity.ToString("#,##0.00;- #,##0.00");
        inpProductPrice.Text = _Price.ToString("#,##0.00;- #,##0.00");
        inpProductDimensions.Text = Row_Selected["Dimensions"].ToString();
        inpSupplierProductName.Text = Row_Selected["Name"].ToString();
        inpSupplierProductPrice.Text = _Price.ToString("#,##0.00;- #,##0.00");

        // Retrieve Product Image            
        if (Row_Selected["Image"].ToString() != "")
        {
            byte[] productImageByte = (byte[])Row_Selected["Image"];
            var stream = new MemoryStream(productImageByte);
            PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            BitmapSource source = decoder.Frames[0];

            imgProductImage.Source = source;
            imgProductImage.LayoutTransform = new RotateTransform(int.Parse(valueImageRotationAngle.Text));
        }
        else
        {
            imgProductImage.Source = new BitmapImage(new Uri("..\\Resources\\noImage.png", UriKind.Relative));
        }

        // When there is an existing Product selected the supplier tabpage can be activated
        SupplierTab.IsEnabled = inpProductCode.Text != "";

        if (Row_Selected["ProjectCosts"].ToString() == "0")
        {
            chkProjectProjectCosts.IsChecked = false;
        }
        else
        {
            chkProjectProjectCosts.IsChecked = true;
        }

        //Select the saved Category in the combobox by default
        foreach (HelperGeneral.Category category in cboxProductCategory.Items)
        {
            if (category.CategoryName == Row_Selected["CategoryName"].ToString())
            {
                cboxProductCategory.SelectedItem = category;
                break;
            }
        }

        //Select the saved Brand in the combobox by default
        foreach (HelperGeneral.Brand brand in cboxProductBrand.Items)
        {
            if (brand.BrandName == Row_Selected["BrandName"].ToString())
            {
                cboxProductBrand.SelectedItem = brand;
            }
        }


        //Select the saved Unit in the combobox by default
        foreach (HelperGeneral.Unit unit in cboxProductUnit.Items)
        {
            if (unit.UnitName == Row_Selected["UnitName"].ToString())
            {
                cboxProductUnit.SelectedItem = unit;
                break;
            }
        }

        //Select the saved Storage location in the combobox by default
        foreach (HelperGeneral.Storage storage in cboxProductStorage.Items)
        {
            if (storage.StorageName == Row_Selected["StorageName"].ToString())
            {
                cboxProductStorage.SelectedItem = storage;
                break;
            }
        }

        // Retrieve list of suppliers for this product from database
        _dtPS = _helper.GetDataTblProductSupplier(int.Parse(valueProductId.Text));

        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;

    }
    #endregion Selection changed ProductCode

    #region Selection changed ProductSupplierCode
    private void ProductSupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // When a row in the datagrid is selected, all fields can be enablen
        //inpSupplierProductName.IsEnabled = true;
        //inpSupplierProductNumber.IsEnabled = true;
        //inpSupplierProductPrice.IsEnabled = true;
        //cboxProductSupplier.IsEnabled = true;
        //chkSupplierDefault.IsEnabled = true;

        DataGrid dgPS = (DataGrid)sender;

        if (dgPS.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridPSIndex = dgPS.SelectedIndex;

        var _ProductPrice = 0.00;
        if (Row_Selected["Price"].ToString() != "") { _ProductPrice = double.Parse(Row_Selected["Price"].ToString().Replace(".", ",")); };

        valueProductSupplierId.Text = Row_Selected["Id"].ToString();
        valueProductSupplierSupplierId.Text = Row_Selected["Supplier_Id"].ToString();
        valueProductSupplierCurrencyId.Text = Row_Selected["Currency_Id"].ToString();
        inpSupplierProductNumber.Text = Row_Selected["ProductNumber"].ToString();
        inpSupplierProductName.Text = Row_Selected["Name"].ToString();
        //dispProductSupplierCurrencySymbol.Text = Row_Selected["CurrencySymbol"].ToString();
        inpSupplierProductPrice.Text = _ProductPrice.ToString("#,##0.00;- #,##0.00");
        if (Row_Selected["DefaultSupplier"].ToString() == "*")
        {
            chkSupplierDefault.IsChecked = true;
        }
        else
        {
            chkSupplierDefault.IsChecked = false;
        }


        //Select the saved Supplier in the combobox by default
        foreach (HelperGeneral.Supplier supplier in cboxProductSupplier.Items)
        //foreach (Supplier supplier in cboxProductSupplier.Items)
        {
            if (supplier.SupplierName == Row_Selected["SupplierName"].ToString())
            {
                cboxProductSupplier.SelectedItem = supplier;
                valueProductSupplierSupplierId.Text = Row_Selected["Supplier_Id"].ToString();
                valueProductSupplierCurrencyId.Text = Row_Selected["Currency_Id"].ToString();
                dispProductSupplierCurrencySymbol.Text = Row_Selected["CurrencySymbol"].ToString();
                break;
            }
        }
    }
    #endregion

    #region The Selection in the Brand combobox has changed
    private void cboxBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Brand item in e.AddedItems)
        {
            valueBrandId.Text = item.BrandId.ToString();
        }
    }
    #endregion The Selection in the Brand combobox has changed

    #region The Selection in the Category combobox has changed
    private void cboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Category item in e.AddedItems)
        {
            valueCategoryId.Text = item.CategoryId.ToString();
        }
    }
    #endregion The Selection in the Category combobox has changed

    #region The Selection in the Storage combobox has changed
    private void cboxStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Storage item in e.AddedItems)
        {
            valueStorageId.Text = item.StorageId.ToString();
        }
    }
    #endregion The Selection in the Category combobox has changed

    #region The Selection in the Unit combobox has changed
    private void cboxUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Unit item in e.AddedItems)
        {
            valueUnitId.Text = item.UnitId.ToString();
        }
    }
    #endregion The Selection in the Category combobox has changed

    #region The Selection in the ProductSupplier combobox has changed
    private void cboxSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Supplier item in e.AddedItems)
        {
            valueProductSupplierSupplierId.Text = item.SupplierId.ToString();
            valueProductSupplierCurrencyId.Text = item.SupplierCurrencyId.ToString();
            dispProductSupplierCurrencySymbol.Text = item.SupplierCurrencySymbol.ToString();
        }
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

    #region Click Save Data button (on toolbar)
    private void ToolbarButtonSave(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridIndex;

        valueBrandId.Text = ((HelperGeneral.Brand)cboxProductBrand.SelectedItem).BrandId.ToString();
        valueCategoryId.Text = ((HelperGeneral.Category)cboxProductCategory.SelectedItem).CategoryId.ToString();
        valueStorageId.Text = ((HelperGeneral.Storage)cboxProductStorage.SelectedItem).StorageId.ToString();
        valueUnitId.Text = ((HelperGeneral.Unit)cboxProductUnit.SelectedItem).UnitId.ToString();

        // When there is an existing Prduct selected the supplier tabpage can be activated
        SupplierTab.IsEnabled = inpProductCode.Text != "";

        if (valueProductId.Text != "")
        {
            UpdateRowProduct(ProductCode_DataGrid.SelectedIndex);
        }

        GetData();

        // Make sure the eddited row in the datagrid is selected
        ProductCode_DataGrid.SelectedIndex = rowIndex;
        ProductCode_DataGrid.Focus();
    }
    #endregion Click Save Data button (on toolbar)

    #region Click New data row button (on toolbar)
    private void ToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        // When the ProductId is not empty, a excisting row is selected when the add new row button is hit. 
        // In this case a new row with blank values should be added to the dtable and selected.
        // Otherwise it can be that fields are already filled with data befor the add new row button was hit.
        // In this case the existing value should be used instead of emptying all the data from the form.

        var productProjectCosts = 0;
        var productCode = "";
        var productName = "";
        var productMinimalStock = 0.00;
        var productStandardOrderQuantity = 0.00;
        var productPrice = 0.00;
        var productCategoryId = 1;
        var productStorageId = 1;
        var productBrandId = 1;
        var productUnitId = 1;
        var productImageRotationAngle = "0";
        var productDimensions = "";
        byte[] productImage = null;

        if (valueProductId.Text == "")
        {
            // No existing product selected, use formdata if entered
            // check on entered data on formated field because they throw an error on adding a new row
            productCode = inpProductCode.Text;
            productName = inpProductName.Text;
            productDimensions = inpProductDimensions.Text;

            if (inpProductMinimalStock.Text != "")
            { productMinimalStock = double.Parse(inpProductMinimalStock.Text.Replace(",", ".")); }

            if (inpProductStandardOrderQuantity.Text != "")
            { productStandardOrderQuantity = double.Parse(inpProductStandardOrderQuantity.Text.Replace(",", ".")); }

            if (inpProductPrice.Text != "")
            { productPrice = double.Parse(inpProductPrice.Text.Replace(",", ".").Replace("€", "").Replace(" ", "")); }

            if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }

            if (valueCategoryId.Text != "")
            {
                productCategoryId = int.Parse(valueCategoryId.Text);
            }

            if (valueStorageId.Text != "")
            {
                productStorageId = int.Parse(valueStorageId.Text);
            }

            if (valueBrandId.Text != "")
            {
                productBrandId = int.Parse(valueBrandId.Text);
            }

            if (valueUnitId.Text != "")
            {
                productUnitId = int.Parse(valueUnitId.Text);
            }

            productImageRotationAngle = valueImageRotationAngle.Text;

            var bitmap = imgProductImage.Source as BitmapSource;
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using var stream = new MemoryStream();
            encoder.Save(stream);
            productImage = stream.ToArray();
        }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpProductMemo.Document);

        if (_dt.Rows.Count != 0)
        { DataRow row = _dt.Rows[_dt.Rows.Count - 1]; }

        InitializeHelper();

        string result = string.Empty;
        result = _helper.InsertTblProduct(productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productStorageId, productBrandId, productUnitId, memo, productImageRotationAngle, productImage, productDimensions);
        UpdateStatus(result);

        // Get data from database
        _dt = _helper.GetDataTblProduct();

        // Populate data in datagrid from datatable
        ProductCode_DataGrid.DataContext = _dt;
        //dataGridView1.Rows[e.RowIndex].Selected = true;
        if (ProductCode_DataGrid.SelectedItem is not DataRowView Row_Selected)
        {
            return;
        }
    }
    #endregion Click New data row button (on toolbar)

    #region Click New data row button (on suppliertoolbar)
    private void supplierToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        var ProductId = int.Parse(valueProductId.Text);
        var SupplierId = int.Parse(valueProductSupplierSupplierId.Text);
        var CurrencyId = int.Parse(valueProductSupplierCurrencyId.Text);
        var Number = "";
        var Name = inpProductName.Text;
        var Default = "";
        var Price = 0.00;

        if (valueProductSupplierId.Text == "")
        {
            // No existng supplier selected, use formdata if entered
            // check on entered data on formated field because they throw an error on adding a new row
            if (valueProductSupplierSupplierId.Text == "")
            {
                // There is no supplier selected Yet, But is Price is entered
                // Use default value, to cause no error if the save button is pressed without changing the default supplier (1)
                valueProductSupplierSupplierId.Text = SupplierId.ToString();
            }

            // Get the currency from the supplier and add the Id and Symbol to the form

            Number = inpSupplierProductNumber.Text;

            // If no supplier specific description has been entered, use the General Product descreprion
            if(inpSupplierProductName.Text == "") { inpSupplierProductName.Text = Name; } else { Name = inpSupplierProductName.Text; }

            if (inpSupplierProductPrice.Text != "")
            { Price = double.Parse(inpSupplierProductPrice.Text.Replace("€", "").Replace(" ", "")); }

            if ((bool)chkSupplierDefault.IsChecked) { Default = "*"; }

        }

        InitializeHelper();

        var result = _helper.InsertTblProductSupplier(ProductId, SupplierId, CurrencyId, Number, Name, Price, Default);
        UpdateStatus(result);

        // Get data from database
        _dtPS = _helper.GetDataTblProductSupplier(int.Parse(valueProductId.Text));

        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;
        //DataGrid dg = (DataGrid)sender;

        //if (dg.SelectedItem is not DataRowView Row_Selected)
        //{
        //    return;
        //}

    }
    #endregion Click New data row button (on suppliertoolbar)

    #region Click Save Data button (on suppliertoolbar)
    private void supplierToolbarButtonSave(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridPSIndex;

        if (valueProductSupplierId.Text == "")
        //if (_dtPS.Rows.Count > _dbRowCount)
        {
            //InsertRowProductSupplier(ProductSupplierCode_DataGrid.SelectedIndex);
        }
        else
        {
            UpdateRowProductSupplier(ProductSupplierCode_DataGrid.SelectedIndex);
        }

        GetData();

        // Make sure the eddited row in the datagrid is selected
        ProductSupplierCode_DataGrid.SelectedIndex = rowIndex;
        ProductSupplierCode_DataGrid.Focus();
    }
    #endregion Click Save Data button (on suppliertoolbar)

    #region Delete Data button (on toolbar)
    private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridIndex;

        DeleteRowProduct(ProductCode_DataGrid.SelectedIndex);

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
    #endregion Delete Data button (on toolbar)

    #region Delete Data button (on SupplierToolbar)
    private void SupplierToolbarButtonDelete(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridPSIndex;

        DeleteRowProductSupplier(ProductSupplierCode_DataGrid.SelectedIndex);

        GetData();

        if (rowIndex == 0)
        {
            ProductSupplierCode_DataGrid.SelectedIndex = 0;
        }
        else
        {
            ProductSupplierCode_DataGrid.SelectedIndex = rowIndex - 1;
        }

        ProductSupplierCode_DataGrid.Focus();
    }
    #endregion Delete Data button (on SupplierToolbar)

    #region Update row Product Table
    private void UpdateRowProduct(int dgIndex)
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
        var productStorageId = int.Parse(valueStorageId.Text);
        var productBrandId = int.Parse(valueBrandId.Text);
        var productUnitId = int.Parse(valueUnitId.Text);
        var productDimensions = inpProductDimensions.Text;
        var productImageRotationAngle = valueImageRotationAngle.Text;

        byte[] productImage;
        var bitmap = imgProductImage.Source as BitmapSource;
        var encoder = new PngBitmapEncoder();

        encoder.Frames.Add(BitmapFrame.Create(bitmap));

        using (var stream = new MemoryStream())
        {
            encoder.Save(stream);
            productImage = stream.ToArray();
        }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpProductMemo.Document);

        InitializeHelper();

        string result = string.Empty;
        result = _helper.UpdateTblProduct(productId, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productStorageId, productBrandId, productUnitId, memo, productImageRotationAngle, productImage, productDimensions);
        UpdateStatus(result);
    }
    #endregion Update row Product Table

    #region Update row ProductSupplier Table
    private void UpdateRowProductSupplier(int dgIndex)
    {
        //var productSupplierId = int.Parse(valueProductSupplierId.Text);
        var productSupplierId = int.Parse(valueProductSupplierId.Text);                     // Unique Id for the recrd in the ProductSupplier Table
        var productSupplierProductId = int.Parse(valueProductId.Text);                      // Id of the selected Product
        var productSupplierSupplierId = int.Parse(valueProductSupplierSupplierId.Text);     // Id of the Supplier selected from the Supplier ComboBox
        var productSupplierCurrencyId = int.Parse(valueProductSupplierCurrencyId.Text);     // Default Currency Id of the Supplier selected from the Supplier ComboBox
        var productSupplierCurrencySymbol = dispProductSupplierCurrencySymbol.Text;         // Default Currency Symbol of the Supplier selected from the Supplier ComboBox
        var productSupplierProductNumber = inpSupplierProductNumber.Text;                   // Product number used by the selected supplier
        var productSupplierProductName = inpSupplierProductName.Text;                       // Product description used by the selected supplier
        var productSupplierProductPrice = float.Parse(inpSupplierProductPrice.Text.Replace("€", "").Replace(" ", "")); // Product price used by the selected supplier
        var productSupplierDefault = "";                                                    // Is the selected supplier/product the default product to be used?
        if ((bool)chkSupplierDefault.IsChecked) { productSupplierDefault = "*"; }

        InitializeHelper();

        // If the checkbox that for default supplier is selected, the eventual other default supplier should be set to '', because there can only be one default
        if ((bool)chkSupplierDefault.IsChecked)
        {
            _ = _helper.UncheckDefaultSupplierTblProductSupplier(productSupplierId, productSupplierProductId);
        }

        string result = string.Empty;
        result = _helper.UpdateTblProductSupplier(productSupplierId, productSupplierProductId, productSupplierSupplierId, productSupplierCurrencyId, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice, productSupplierDefault);
        UpdateStatus(result);

        // Get data from database
        _dtPS = _helper.GetDataTblProductSupplier();

        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;
    }
    #endregion Update row ProductSupplier Tabble

    #region Insert new row in Product Table
    private void InsertRowProduct(int dgIndex)
    {
        //since the DataGrid DataContext is set to the DataTable, 
        //the DataTable is updated when data is modified in the DataGrid
        //get last row
        DataRow row = _dt.Rows[_dt.Rows.Count - 1];
        int productProjectCosts = 0;

        var productCode = inpProductCode.Text;
        var productName = inpProductName.Text;
        var productDimensions = inpProductDimensions.Text;
        var productMinimalStock = double.Parse(inpProductMinimalStock.Text.Replace(",", "."));
        var productStandardOrderQuantity = double.Parse(inpProductStandardOrderQuantity.Text.Replace(",", "."));
        var productPrice = double.Parse(inpProductPrice.Text.Replace(",", ".").Replace("€", "").Replace(" ", ""));
        var productSupplierProductNumber = inpSupplierProductNumber.Text;
        if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
        { productProjectCosts = 0; }
        var productCategoryId = int.Parse(valueCategoryId.Text);
        var productStorageId = int.Parse(valueStorageId.Text);
        var productBrandId = int.Parse(valueBrandId.Text);
        var productUnitId = int.Parse(valueUnitId.Text);
        var productImageRotationAngle = valueImageRotationAngle.Text;

        byte[] productImage;
        var bitmap = imgProductImage.Source as BitmapSource;
        var encoder = new PngBitmapEncoder();

        encoder.Frames.Add(BitmapFrame.Create(bitmap));

        using (var stream = new MemoryStream())
        {
            encoder.Save(stream);
            productImage = stream.ToArray();
        }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpProductMemo.Document);

        InitializeHelper();

        string result = string.Empty;
        result = _helper.InsertTblProduct(productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productStorageId, productBrandId, productUnitId, memo, productImageRotationAngle, productImage, productDimensions);
        UpdateStatus(result);
    }
    #endregion Insert new row in Product Table

    #region Delete row from Product table
    private void DeleteRowProduct(int dgIndex)
    {
        if (valueProductId.Text != "")
        {
            var productId = int.Parse(valueProductId.Text);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.DeleteTblProduct(productId);
            UpdateStatus(result);
        }
    }
    #endregion Delete row from Product table

    #region Delete row in ProductSupplier table
    private void DeleteRowProductSupplier(int dgIndex)
    {
        int productSupplierId = int.Parse(valueProductSupplierSupplierId.Text);

        InitializeHelper();

        string result = string.Empty;
        result = _helper.DeleteTblProductSupplier(productSupplierId);
        UpdateStatus(result);
    }
    #endregion Delete row in ProductSupplier table

    #region Add a product Image
    private void ImageAdd(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ImageDialog = new OpenFileDialog();
        ImageDialog.Title = "Selecteer een afbeelding voor dit product";
        ImageDialog.Filter = "Afbeeldingen (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
        if (ImageDialog.ShowDialog() == true)
        {
            imgProductImage.Source = new BitmapImage(new Uri(ImageDialog.FileName));
        }
    }
    #endregion Add a product Image

    #region Delete a product Image
    private void ImageDelete(object sender, RoutedEventArgs e)
    {
        imgProductImage.Source = null;
        valueImageRotationAngle.Text = "0";
    }
    #endregion Delete a product Image

    #region Rotate a product Image
    private void ImageRotate(object sender, RoutedEventArgs e)
    {
        var _tempValue = (int.Parse(valueImageRotationAngle.Text) + 90);
        if (_tempValue == 360)
        {
            _tempValue = 0;
        }
        valueImageRotationAngle.Text = _tempValue.ToString();
        imgProductImage.LayoutTransform = new RotateTransform(int.Parse(valueImageRotationAngle.Text));

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

    #region Validation if Product Code and Name are filled in order to add/save the record
    private void ProductAllowNewSaveDeleteValidation(object sender, TextChangedEventArgs e)
    {
        EnableButtons();
    }
    private void EnableButtons()
    {
        #region Enable or Disable buttons on Product Toolbar
        // Add new product button can be selected if No Product_Id exists and a ProductCode and ProductName are filled
        if (valueProductId.Text == string.Empty && inpProductCode.Text != string.Empty && inpProductName.Text != string.Empty)
        { 
            btnNewProduct.IsEnabled = true; 
        }
        else
        {
            btnNewProduct.IsEnabled = false;
        }

        // Save existing product button can be selected if ProductId exists and a ProductCode and ProductName are filled
        if (valueProductId.Text != string.Empty && inpProductCode.Text != string.Empty && inpProductName.Text != string.Empty)
        {
            btnSaveProduct.IsEnabled = true;
        }
        else
        {
            btnSaveProduct.IsEnabled = false;
        }

        // Delete existing product button can be selected if ProductId 
        if (valueProductId.Text != string.Empty)
        {
            btnDeleteProduct.IsEnabled = true;
        }
        else
        {
            btnDeleteProduct.IsEnabled = false;
        }
        #endregion Enable or Disable buttons on Product Toolbar

        #region Enable or Disable buttons on ProductSupplier Toolbar
        // Add new product button can be selected if No Product_Id exists and a ProductCode and ProductName are filled
        if (valueProductSupplierId.Text == string.Empty && valueProductSupplierSupplierId.Text != string.Empty)
        {
            btnNewProductSupplier.IsEnabled = true;
        }
        else
        {
            btnNewProductSupplier.IsEnabled = false;
        }

        // Save existing product button can be selected if ProductId exists and a ProductCode and ProductName are filled
        if (valueProductSupplierId.Text != string.Empty && valueProductSupplierSupplierId.Text != string.Empty)
        {
            btnSaveProductSupplier.IsEnabled = true;
        }
        else
        {
            btnSaveProductSupplier.IsEnabled = false;
        }

        // Delete existing product button can be selected if ProductId 
        if (valueProductSupplierId.Text != string.Empty)
        {
            btnDeleteProductSupplier.IsEnabled = true;
        }
        else
        {
            btnDeleteProductSupplier.IsEnabled = false;
        }
        #endregion Enable or Disable buttons on ProductSupplier Toolbar
    }
    #endregion Validation if Product Code and Name are filled in order to add/save the record
}
