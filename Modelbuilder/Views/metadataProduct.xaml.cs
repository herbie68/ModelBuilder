namespace Modelbuilder;
public partial class metadataProduct : Page
{
    private HelperGeneral _helperGeneral;
    private HelperProduct _helper;
    private DataTable _dt, _dtPS;
    private int _dbRowCount;
    private int _currentDataGridIndex, _currentDataGridPSIndex;
    static string DatabaseCategoryTable = "category", DatabaseStorageTable = "storage", DatabaseSupplierTable = "supplier", DatabaseBrandTable = "brand", DatabaseUnitTable = "unit";

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

        var _Minimalstock = double.Parse(Row_Selected["MinimalStock"].ToString());
        var _StandardOrderQuantity = double.Parse(Row_Selected["StandardOrderQuantity"].ToString());
        var _Price = double.Parse(Row_Selected["Price"].ToString());

        valueProductId.Text = Row_Selected["Id"].ToString();
        valueCategoryId.Text = Row_Selected["CategoryId"].ToString();
        valueCategoryName.Text = Row_Selected["CategoryName"].ToString();
        valueStorageId.Text = Row_Selected["StorageId"].ToString();
        valueStorageName.Text = Row_Selected["StorageName"].ToString();
        valueBrandId.Text = Row_Selected["BrandId"].ToString();
        valueBrandName.Text = Row_Selected["BrandName"].ToString();
        valueUnitId.Text = Row_Selected["UnitId"].ToString();
        valueUnitName.Text = Row_Selected["UnitName"].ToString();
        valueImageRotationAngle.Text = Row_Selected["ImageRotationAngle"].ToString();
        inpProductCode.Text = Row_Selected["Code"].ToString();
        inpProductName.Text = Row_Selected["Name"].ToString();
        inpProductMinimalStock.Text = _Minimalstock.ToString("#,##0.00;- #,##0.00");
        inpProductStandardOrderQuantity.Text = _StandardOrderQuantity.ToString("#,##0.00;- #,##0.00");
        inpProductPrice.Text = _Price.ToString("€ #,##0.00;€ - #,##0.00");

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
        foreach (Category category in cboxProductCategory.Items)
        {
            if (category.categoryName == Row_Selected["CategoryName"].ToString())
            {
                cboxProductCategory.SelectedItem = category;
                break;
            }
        }

        //Select the saved Brand in the combobox by default
        foreach (HelperMySql.Brand brand in cboxProductBrand.Items)
        {
            if (brand.BrandName == Row_Selected["BrandName"].ToString())
            {
                cboxProductBrand.SelectedItem = brand;
            }
        }


        //Select the saved Unit in the combobox by default
        foreach (Unit unit in cboxProductUnit.Items)
        {
            if (unit.unitName == Row_Selected["UnitName"].ToString())
            {
                cboxProductUnit.SelectedItem = unit;
                break;
            }
        }

        //Select the saved Storage location in the combobox by default
        foreach (Storage storage in cboxProductStorage.Items)
        {
            if (storage.storageName == Row_Selected["StorageName"].ToString())
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
        inpSupplierProductName.IsEnabled = true;
        inpSupplierProductNumber.IsEnabled = true;
        inpSupplierProductPrice.IsEnabled = true;
        cboxProductSupplier.IsEnabled = true;
        chkSupplierDefault.IsEnabled = true;

        DataGrid dgPS = (DataGrid)sender;

        if (dgPS.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridPSIndex = dgPS.SelectedIndex;

        float _ProductPrice = 0;

        if (Row_Selected["ProductPrice"].ToString() != "") { _ProductPrice = float.Parse(Row_Selected["ProductPrice"].ToString()); };

        valueProductSupplierId.Text = Row_Selected["Id"].ToString();
        valueProductSupplierSupplierId.Text = Row_Selected["SupplierId"].ToString();
        valueProductSupplierSupplierName.Text = Row_Selected["SupplierName"].ToString();
        valueProductSupplierCurrencyId.Text = Row_Selected["CurrencyId"].ToString();
        inpSupplierProductNumber.Text = Row_Selected["ProductNumber"].ToString();
        inpSupplierProductName.Text = Row_Selected["ProductName"].ToString();
        dispProductSupplierCurrencySymbol.Text = Row_Selected["CurrencySymbol"].ToString();
        inpSupplierProductPrice.Text = _ProductPrice.ToString("#,##0.00;- #,##0.00");
        if (Row_Selected["Default"].ToString() == "*")
        {
            chkSupplierDefault.IsChecked = true;
        }
        else
        {
            chkSupplierDefault.IsChecked = false;
        }


        //Select the saved Supplier in the combobox by default
        foreach (HelperMySql.Supplier supplier in cboxProductSupplier.Items)
        //foreach (Supplier supplier in cboxProductSupplier.Items)
        {
            if (supplier.SupplierName == Row_Selected["SupplierName"].ToString())
            {
                cboxProductSupplier.SelectedItem = supplier;
                valueProductSupplierSupplierId.Text = Row_Selected["SupplierId"].ToString();
                valueProductSupplierSupplierName.Text = Row_Selected["SupplierName"].ToString();
                valueProductSupplierCurrencyId.Text = Row_Selected["CurrencyId"].ToString();
                dispProductSupplierCurrencySymbol.Text = Row_Selected["CurrencySymbol"].ToString();
                break;
            }
        }
    }
    #endregion

    #region The Selection in the ProductBrand combobox has changed
    private void cboxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperMySql.Brand item in e.AddedItems)
        {
            valueBrandId.Text = item.BrandId.ToString();
            valueBrandName.Text = item.BrandName;
        }
    }
    #endregion

    #region The Selection in the ProductSupplier combobox has changed
    private void cboxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperMySql.Supplier item in e.AddedItems)
        {
            valueProductSupplierSupplierId.Text = item.SupplierId.ToString();
            valueProductSupplierSupplierName.Text = item.SupplierName;
            valueProductSupplierCurrencyId.Text = item.SupplierCurrencyId.ToString();
            dispProductSupplierCurrencySymbol.Text = item.SupplierCurrencySymbol;
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

        valueCategoryId.Text = ((Category)cboxProductCategory.SelectedItem).categoryId.ToString();
        valueCategoryName.Text = ((Category)cboxProductCategory.SelectedItem).categoryName.ToString();
        valueStorageId.Text = ((Storage)cboxProductStorage.SelectedItem).storageId.ToString();
        valueStorageName.Text = ((Storage)cboxProductStorage.SelectedItem).storageName.ToString();
        valueUnitId.Text = ((Unit)cboxProductUnit.SelectedItem).unitId.ToString();
        valueUnitName.Text = ((Unit)cboxProductUnit.SelectedItem).unitName.ToString();

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
        var productCategoryId = 0;
        var productCategoryName = "";
        var productStorageId = 0;
        var productStorageName = "";
        var productBrandId = 0;
        var productBrandName = "";
        var productUnitId = 0;
        var productUnitName = "";
        var productImageRotationAngle = "0";
        byte[] productImage = null;

        if (valueProductId.Text == "")
        {
            // No existing product selected, use formdata if entered
            // check on entered data on formated field because they throw an error on adding a new row
            productCode = inpProductCode.Text;
            productName = inpProductName.Text;

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

            productCategoryName = valueCategoryName.Text;

            if (valueStorageId.Text != "")
            {
                productStorageId = int.Parse(valueStorageId.Text);
            }

            productStorageName = valueStorageName.Text;

            if (valueBrandId.Text != "")
            {
                productBrandId = int.Parse(valueBrandId.Text);
            }

            productBrandName = valueBrandName.Text;

            if (valueUnitId.Text != "")
            {
                productUnitId = int.Parse(valueUnitId.Text);
            }

            productUnitName = valueBrandName.Text;

            productImageRotationAngle = valueImageRotationAngle.Text;

            var bitmap = imgProductImage.Source as BitmapSource;
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                productImage = stream.ToArray();
            }
        }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpProductMemo.Document);

        if (_dt.Rows.Count != 0)
        { DataRow row = _dt.Rows[_dt.Rows.Count - 1]; }

        InitializeHelper();

        string result = string.Empty;
        result = _helper.InsertTblProduct(productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productBrandId, productBrandName, productUnitId, productUnitName, memo, productImageRotationAngle, productImage);
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
        var productSupplierProductId = int.Parse(valueProductId.Text);
        var productSupplierSupplierId = 0;
        var productSupplierSupplierName = "";
        var productSupplierCurrencyId = 1;
        var productSupplierCurrencySymbol = "€";
        var productSupplierProductNumber = "";
        var productSupplierProductName = "";
        var productSupplierDefault = "";
        float productSupplierProductPrice = 0;

        InitializeHelper();

        var result = _helper.InsertTblProductSupplier(productSupplierProductId, productSupplierSupplierId, productSupplierSupplierName, productSupplierCurrencyId, productSupplierCurrencySymbol, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice, productSupplierDefault);
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
        var productCategoryName = valueCategoryName.Text;
        var productStorageId = int.Parse(valueStorageId.Text);
        var productStorageName = valueStorageName.Text;
        var productBrandId = int.Parse(valueBrandId.Text);
        var productBrandName = valueBrandName.Text;
        var productUnitId = int.Parse(valueUnitId.Text);
        var productUnitName = valueUnitName.Text;

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
        result = _helper.UpdateTblProduct(productId, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productBrandId, productBrandName, productUnitId, productUnitName, memo, productImageRotationAngle, productImage);
        UpdateStatus(result);
    }
    #endregion Update row Product Table

    #region Update row ProductSupplier Tabble
    private void UpdateRowProductSupplier(int dgIndex)
    {
        //var productSupplierId = int.Parse(valueProductSupplierId.Text);
        var productSupplierId = int.Parse(valueProductSupplierId.Text);                     // Unique Id for the recrd in the ProductSupplier Table
        var productSupplierProductId = int.Parse(valueProductId.Text);                      // Id of the selected Product
        var productSupplierSupplierId = int.Parse(valueProductSupplierSupplierId.Text);     // Id of the Supplier selected from the Supplier ComboBox
        var productSupplierSupplierName = valueProductSupplierSupplierName.Text;            // Name of the Supplier selected from the Supplier ComboBox
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
        result = _helper.UpdateTblProductSupplier(productSupplierId, productSupplierProductId, productSupplierSupplierId, productSupplierSupplierName, productSupplierCurrencyId, productSupplierCurrencySymbol, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice, productSupplierDefault);
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
        var productMinimalStock = double.Parse(inpProductMinimalStock.Text.Replace(",", "."));
        var productStandardOrderQuantity = double.Parse(inpProductStandardOrderQuantity.Text.Replace(",", "."));
        var productPrice = double.Parse(inpProductPrice.Text.Replace(",", ".").Replace("€", "").Replace(" ", ""));
        var productSupplierProductNumber = inpSupplierProductNumber.Text;
        if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
        { productProjectCosts = 0; }
        var productCategoryId = int.Parse(valueCategoryId.Text);
        var productCategoryName = valueCategoryName.Text;
        var productStorageId = int.Parse(valueStorageId.Text);
        var productStorageName = valueStorageName.Text;
        var productBrandId = int.Parse(valueBrandId.Text);
        var productBrandName = valueBrandName.Text;
        var productUnitId = int.Parse(valueUnitId.Text);
        var productUnitName = valueBrandName.Text;
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
        result = _helper.InsertTblProduct(productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productBrandId, productBrandName, productUnitId, productUnitName, memo, productImageRotationAngle, productImage);
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

    #region Category dropdown
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
    #endregion Create object for all categories in table for dropdown

    #region Fill Category dropdown
    static List<Category> CategoryList()
    {

        Database dbCategoryConnection = new()
        {
            TableName = DatabaseCategoryTable
        };

        dbCategoryConnection.SqlSelectionString = "Name, Id";
        dbCategoryConnection.SqlOrderByString = "Id";
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
    #endregion Fill Category dropdown
    #endregion Category dropdown

    #region Storage dropdown
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
    #endregion Create object for all storagelocations in table for dropdown

    #region Fill Storage dropdown
    static List<Storage> StorageList()
    {

        Database dbStorageConnection = new()
        {
            TableName = DatabaseStorageTable
        };

        dbStorageConnection.SqlSelectionString = "Name, Id";
        dbStorageConnection.SqlOrderByString = "Id";
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
    #endregion Fill Storage dropdown
    #endregion Storage dropdown

    #region Supplier dropdown
    #region Create object for all Suppliers in table for dropdown
    private class Supplier
    {
        public Supplier(string Name, string Currency, string CurrencyId, string Id)
        {
            supplierName = Name;
            supplierCurrency = Currency;
            supplierCurrencyId = CurrencyId;
            supplierId = Id;
        }

        public string supplierName { get; set; }
        public string supplierCurrency { get; set; }
        public string supplierCurrencyId { get; set; }
        public string supplierId { get; set; }
    }
    #endregion

    #region Fill Supplier dropdown
    static List<Supplier> SupplierList()
    {

        Database dbSupplierConnection = new()
        {
            TableName = DatabaseSupplierTable
        };

        //dbSupplierConnection.SqlSelectionString = "Name, CurrencySymbol, Id";
        dbSupplierConnection.SqlSelectionString = "Name, CurrencySymbol, CurrencyId, Id";
        dbSupplierConnection.SqlOrderByString = "Id";
        dbSupplierConnection.TableName = DatabaseSupplierTable;

        DataTable dtSupplierSelection = dbSupplierConnection.LoadSpecificMySqlData();

        List<Supplier> SupplierList = new();

        for (int i = 0; i < dtSupplierSelection.Rows.Count; i++)
        {
            //SupplierList.Add(new Supplier(dtSupplierSelection.Rows[i][0].ToString(), dtSupplierSelection.Rows[i][1].ToString(), dtSupplierSelection.Rows[i][2].ToString()));
            SupplierList.Add(new Supplier(dtSupplierSelection.Rows[i][0].ToString(), dtSupplierSelection.Rows[i][1].ToString(), dtSupplierSelection.Rows[i][2].ToString(), dtSupplierSelection.Rows[i][3].ToString()));
        };
        return SupplierList;
    }
    #endregion
    #endregion

    #region Brand dropdown
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

    #region Fill Brand dropdown
    static List<Brand> BrandList()
    {

        Database dbBrandConnection = new()
        {
            TableName = DatabaseBrandTable
        };

        dbBrandConnection.SqlSelectionString = "Name, Id";
        dbBrandConnection.SqlOrderByString = "Id";
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
    #endregion

    #region Units dropdown
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

    #region Fill Unit dropdown
    static List<Unit> UnitList()
    {

        Database dbUnitConnection = new()
        {
            TableName = DatabaseUnitTable
        };

        dbUnitConnection.SqlSelectionString = "Name, Id";
        dbUnitConnection.SqlOrderByString = "Id";
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
    #endregion
}
