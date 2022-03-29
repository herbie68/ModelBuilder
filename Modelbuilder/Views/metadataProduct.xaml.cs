using System;

using K4os.Compression.LZ4.Internal;

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
        _dt = _helperGeneral.GetData(HelperGeneral.DbProductView);

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

    #region Get the ProductSupplier data
    private void GetProductSupplierData()
    {
        InitializeHelper();

        // Get data from database
        _dtPS = _helperGeneral.GetData(HelperGeneral.DbProductSupplierView, "Product_Id", Id: int.Parse(valueProductId.Text));

        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;
    }
    #endregion Get the ProductSupplier data

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
            _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
        if (_helper == null)
        {
            _helper = new HelperProduct(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
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
        var _tempCategory = _helperGeneral.GetValueFromTable(HelperGeneral.DbCategoryTable, new string[1, 3]
        {
            { HelperGeneral.DbCategoryTableFieldNameCategoryId, HelperGeneral.DbCategoryTableFieldTypeCategoryId, valueCategoryId.Text }
        }, new string[1, 3] 
        {
            { HelperGeneral.DbCategoryTableFieldNameCategoryName, HelperGeneral.DbCategoryTableFieldTypeCategoryName, "" }
        });
        cboxProductCategory.Text = _tempCategory;

        foreach (HelperGeneral.Category category in cboxProductCategory.Items)
        {
            if (category.CategoryName == _tempCategory)
            {
                cboxProductCategory.SelectedItem = category;
                break;
            }
        }

        //Select the saved Brand in the combobox by default
        var _tempBrand = _helperGeneral.GetValueFromTable(HelperGeneral.DbBrandTable, new string[1, 3]
        {
            { HelperGeneral.DbBrandTableFieldNameBrandId, HelperGeneral.DbBrandTableFieldTypeBrandId, valueBrandId.Text }
        }, new string[1, 3]
        {
            { HelperGeneral.DbBrandTableFieldNameBrandName, HelperGeneral.DbBrandTableFieldTypeBrandName, "" }
        });

        foreach (HelperGeneral.Brand brand in cboxProductBrand.Items)
        {
            if (brand.BrandName == _tempBrand)
            {
                cboxProductBrand.SelectedItem = brand;
            }
        }

        //Select the saved Unit in the combobox by default
        var _tempUnit = _helperGeneral.GetValueFromTable(HelperGeneral.DbUnitTable, new string[1, 3]
        {
            { HelperGeneral.DbUnitTableFieldNameUnitId, HelperGeneral.DbUnitTableFieldTypeUnitId, valueBrandId.Text }
        }, new string[1, 3]
        {
            { HelperGeneral.DbUnitTableFieldNameUnitName, HelperGeneral.DbUnitTableFieldTypeUnitName, "" }
        });
        foreach (HelperGeneral.Unit unit in cboxProductUnit.Items)
        {
            if (unit.UnitName == _tempUnit)
            {
                cboxProductUnit.SelectedItem = unit;
                break;
            }
        }

        //Select the saved Storage location in the combobox by default
        var _tempStorage = _helperGeneral.GetValueFromTable(HelperGeneral.DbStorageTable, new string[1, 3]
        {
            { HelperGeneral.DbStorageTableFieldNameStorageId, HelperGeneral.DbStorageTableFieldTypeStorageId, valueStorageId.Text }
        }, new string[1, 3]
        {
            { HelperGeneral.DbStorageTableFieldNameStorageName, HelperGeneral.DbStorageTableFieldTypeStorageName, "" }
        });
        foreach (HelperGeneral.Storage storage in cboxProductStorage.Items)
        {
            if (storage.StorageName == _tempStorage)
            {
                cboxProductStorage.SelectedItem = storage;
                break;
            }
        }

        // Retrieve list of suppliers for this product from database
        _dtPS = _helperGeneral.GetData ( HelperGeneral.DbProductSupplierView, "Product_Id", Id: int.Parse ( valueProductId.Text ) );

        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;

        TBResetButtonEnable.Text = "Visible";
    }
    #endregion Selection changed ProductCode

    #region Selection changed ProductSupplierCode
    private void ProductSupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dgPS = (DataGrid)sender;

        if (dgPS.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridPSIndex = dgPS.SelectedIndex;

        var _ProductPrice = 0.00;
        if (Row_Selected["Price"].ToString() != "") { _ProductPrice = double.Parse(Row_Selected["Price"].ToString().Replace(".", ",")); }

        valueProductSupplierId.Text = Row_Selected["Id"].ToString();
        valueProductSupplierSupplierId.Text = Row_Selected["Supplier_Id"].ToString();
        valueProductSupplierCurrencyId.Text = Row_Selected["Currency_Id"].ToString();
        inpSupplierProductNumber.Text = Row_Selected["ProductNumber"].ToString();
        inpSupplierProductName.Text = Row_Selected["Name"].ToString();
        inpSupplierProductUrl.Text = Row_Selected["Url"].ToString();
        inpSupplierProductPrice.Text = _ProductPrice.ToString("#,##0.00;- #,##0.00");
        inpSupplierProductUrl.Text = Row_Selected["Url"].ToString();
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
            UpdateRowProduct ();
        }

        ClearAllFields ();

        GetData();

        // Make sure the eddited row in the datagrid is selected
        ProductCode_DataGrid.SelectedIndex = rowIndex;
        ProductCode_DataGrid.Focus();

        TBResetButtonEnable.Text = "Visible";
    }
    #endregion Click Save Data button (on toolbar)

    #region Click New data row button (on toolbar)
    private void ToolbarButtonNew ( object sender, RoutedEventArgs e )
    {
        // When the ProductId is not empty, a excisting row is selected when the add new row button is hit. 
        // In this case a new row with blank values should be added to the dtable and selected.
        // Otherwise it can be that fields are already filled with data befor the add new row button was hit.
        // In this case the existing value should be used instead of emptying all the data from the form.

        var productProjectCosts = 0;
        byte[] productImage = null;

        if (valueProductId.Text == "")
        {
            if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }
            var bitmap = imgProductImage.Source as BitmapSource;
            var encoder = new PngBitmapEncoder ();

            encoder.Frames.Add ( BitmapFrame.Create ( bitmap ) );

            using var stream = new MemoryStream ();
            encoder.Save ( stream );
            productImage = stream.ToArray ();
        }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument ( inpProductMemo.Document );

        InitializeHelper ();

        _helperGeneral.InsertInTable ( HelperGeneral.DbProductTable, new string[13, 3]
        {
            {HelperGeneral.DbProductTableFieldNameCode, HelperGeneral.DbProductTableFieldTypeCode, inpProductCode.Text },
            {HelperGeneral.DbProductTableFieldNameName, HelperGeneral.DbProductTableFieldTypeName, inpProductName.Text },
            {HelperGeneral.DbProductTableFieldNameMinimalStock, HelperGeneral.DbProductTableFieldTypeMinimalStock, inpProductMinimalStock.Text.Replace ( ",", "." ) },
            {HelperGeneral.DbProductTableFieldNameStandardOrderQuantity, HelperGeneral.DbProductTableFieldTypeStandardOrderQuantity,inpProductStandardOrderQuantity.Text.Replace ( ",", "." ) },
            {HelperGeneral.DbProductTableFieldNamePrice, HelperGeneral.DbProductTableFieldTypePrice,inpProductPrice.Text.Replace ( "€", "" ).Replace ( " ", "" ) },
            {HelperGeneral.DbProductTableFieldNameProjectCosts, HelperGeneral.DbProductTableFieldTypeProjectCosts, productProjectCosts.ToString() },
            {HelperGeneral.DbProductTableFieldNameCategoryId, HelperGeneral.DbProductTableFieldTypeCategoryId, valueCategoryId.Text },
            {HelperGeneral.DbProductTableFieldNameStorageId, HelperGeneral.DbProductTableFieldTypeStorageId, valueStorageId.Text },
            {HelperGeneral.DbProductTableFieldNameBrandId, HelperGeneral.DbProductTableFieldTypeBrandId, valueBrandId.Text },
            {HelperGeneral.DbProductTableFieldNameUnitId, HelperGeneral.DbProductTableFieldTypeUnitId, valueUnitId.Text },
            {HelperGeneral.DbProductTableFieldNameMemo, HelperGeneral.DbProductTableFieldTypeMemo, memo },
            {HelperGeneral.DbProductTableFieldNameImageRotationAngle, HelperGeneral.DbProductTableFieldTypeImageRotationAngle, valueImageRotationAngle.Text },
            {HelperGeneral.DbProductTableFieldNameDimensions, HelperGeneral.DbProductTableFieldTypeDimensions, inpProductDimensions.Text }
        }, productImage, HelperGeneral.DbProductTableFieldNameImage );
        valueProductId.Text = _helperGeneral.GetLatestIdFromTable(HelperGeneral.DbProductTable);
        _helperGeneral.InsertInTable(HelperGeneral.DbStockTable, new string[1, 3]
        {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId , valueProductId.Text} });

        // Get data from database
        _dt = _helperGeneral.GetData ( HelperGeneral.DbProductTable );

        // Populate data in datagrid from datatable
        ProductCode_DataGrid.DataContext = _dt;
        if (ProductCode_DataGrid.SelectedItem is not DataRowView Row_Selected)
        {
            return;
        }
        TBResetButtonEnable.Text = "Visible";
    }
    #endregion Click New data row button (on toolbar)

    #region Click New data row button (on suppliertoolbar)
    private void supplierToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        var Default = "";

        if (valueProductSupplierId.Text == "")
        {
            // If no supplier specific description has been entered, use the General Product descreprion
            if(inpSupplierProductName.Text == "") { inpSupplierProductName.Text = inpProductName.Text; } else { inpProductName.Text = inpSupplierProductName.Text; }

            if ((bool)chkSupplierDefault.IsChecked) { Default = "*"; }
        }

        InitializeHelper();

        _helperGeneral.InsertInTable ( HelperGeneral.DbProductSupplierTable, new string[8, 3]
            {   { HelperGeneral.DbProductSupplierTableFieldNameProductId, HelperGeneral.DbProductSupplierTableFieldTypeProductId, valueProductId.Text},
                { HelperGeneral.DbProductSupplierTableFieldNameSupplierId, HelperGeneral.DbProductSupplierTableFieldTypeSupplierId, valueProductSupplierSupplierId.Text},
                { HelperGeneral.DbProductSupplierTableFieldNameCurrencyId, HelperGeneral.DbProductSupplierTableFieldTypeCurrencyId, valueProductSupplierCurrencyId.Text},
                { HelperGeneral.DbProductSupplierTableFieldNameProductNumber, HelperGeneral.DbProductSupplierTableFieldTypeProductNumber, inpSupplierProductNumber.Text},
                { HelperGeneral.DbProductSupplierTableFieldNameProductName, HelperGeneral.DbProductSupplierTableFieldTypeProductName, inpSupplierProductName.Text},
                { HelperGeneral.DbProductSupplierTableFieldNameProductUrl, HelperGeneral.DbProductSupplierTableFieldTypeProductUrl, inpSupplierProductUrl.Text},
                { HelperGeneral.DbProductSupplierTableFieldNamePrice, HelperGeneral.DbProductSupplierTableFieldTypePrice, inpSupplierProductPrice.Text.Replace ( "€", "" ).Replace ( " ", "" )},
                { HelperGeneral.DbProductSupplierTableFieldNameDefaultSupplier, HelperGeneral.DbProductSupplierTableFieldTypeDefaultSupplier, Default} } );

        // Get data from database
        _dtPS = _helperGeneral.GetData ( HelperGeneral.DbProductSupplierTable, Id: int.Parse ( valueProductId.Text ) );


        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;
    }
    #endregion Click New data row button (on suppliertoolbar)

    #region Click Save Data button (on suppliertoolbar)
    private void SupplierToolbarButtonSave(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridPSIndex;

        if (valueProductSupplierId.Text != "") { UpdateRowProductSupplier(); }

        GetProductSupplierData();

        // Make sure the eddited row in the datagrid is selected
        ProductSupplierCode_DataGrid.SelectedIndex = rowIndex;
        ProductSupplierCode_DataGrid.Focus();
    }
    #endregion Click Save Data button (on suppliertoolbar)

    #region Delete Data button (on toolbar)
    private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridIndex;

        // Delete product from Stocklog Table
        _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbStocklogTable, new string[1, 3]
        {   { HelperGeneral.DbStocklogTableFieldNameProductId, HelperGeneral.DbStocklogTableFieldTypeProductId, valueProductId.Text } });

        // Delete product from Stock Table
        _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbStockTable, new string[1, 3]
        {   { HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId, valueProductId.Text } });
        // Delete product from product supplierTable
        _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbProductSupplierTable, new string[1, 3]
        {   { HelperGeneral.DbProductSupplierTableFieldNameProductId, HelperGeneral.DbProductSupplierTableFieldTypeId, valueProductId.Text } });

        // Delete product from product Table
        _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbProductTable, new string[1, 3]
        {   { HelperGeneral.DbProductTableFieldNameId, HelperGeneral.DbProductTableFieldTypeId, valueProductId.Text } });

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

        DeleteRowProductSupplier();

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

    #region Toolbar Reset button pressed 
    private void ToolbarButtonReset(object sender, RoutedEventArgs e)
    {
        ClearAllFields();
        InitializeComponent();
    }
    #endregion Toolbar Reset button pressed

    #region Goto Web browser
    private void Button2Web(object sender, RoutedEventArgs e)
    {
        var browserwindow = new System.Diagnostics.ProcessStartInfo();
        browserwindow.UseShellExecute = true;
        browserwindow.FileName = inpSupplierProductUrl.Text;
        System.Diagnostics.Process.Start(browserwindow);
    }
    #endregion Goto Web browser

    #region Update row Product Table
    private void UpdateRowProduct()
    {
        var productProjectCosts = 0;

        if ((bool)chkProjectProjectCosts.IsChecked) { productProjectCosts = 1; }

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

        _helperGeneral.UpdateFieldInTable(HelperGeneral.DbProductTable, new string[1, 3]
        {   {HelperGeneral.DbProductTableFieldNameId, HelperGeneral.DbProductTableFieldTypeId, valueProductId.Text } }, new string[12, 3]
        {   {HelperGeneral.DbProductTableFieldNameCode, HelperGeneral.DbProductTableFieldTypeCode, inpProductCode.Text },
            {HelperGeneral.DbProductTableFieldNameName, HelperGeneral.DbProductTableFieldTypeName, inpProductName.Text },
            {HelperGeneral.DbProductTableFieldNameMinimalStock, HelperGeneral.DbProductTableFieldTypeMinimalStock, inpProductMinimalStock.Text },
            {HelperGeneral.DbProductTableFieldNameStandardOrderQuantity, HelperGeneral.DbProductTableFieldTypeStandardOrderQuantity,inpProductStandardOrderQuantity.Text },
            {HelperGeneral.DbProductTableFieldNamePrice, HelperGeneral.DbProductTableFieldTypePrice,inpProductPrice.Text },
            {HelperGeneral.DbProductTableFieldNameProjectCosts, HelperGeneral.DbProductTableFieldTypeProjectCosts, productProjectCosts.ToString() },
            {HelperGeneral.DbProductTableFieldNameCategoryId, HelperGeneral.DbProductTableFieldTypeCategoryId, valueCategoryId.Text },
            {HelperGeneral.DbProductTableFieldNameStorageId, HelperGeneral.DbProductTableFieldTypeStorageId, valueStorageId.Text },
            {HelperGeneral.DbProductTableFieldNameBrandId, HelperGeneral.DbProductTableFieldTypeBrandId, valueBrandId.Text },
            {HelperGeneral.DbProductTableFieldNameUnitId, HelperGeneral.DbProductTableFieldTypeUnitId, valueUnitId.Text },
            {HelperGeneral.DbProductTableFieldNameImageRotationAngle, HelperGeneral.DbProductTableFieldTypeImageRotationAngle, valueImageRotationAngle.Text },
            {HelperGeneral.DbProductTableFieldNameDimensions, HelperGeneral.DbProductTableFieldTypeDimensions, inpProductDimensions.Text }});

        _helperGeneral.UpdateMemoFieldInTable ( HelperGeneral.DbProductTable, new string[1, 3]
        {   {HelperGeneral.DbProductTableFieldNameId, HelperGeneral.DbProductTableFieldTypeId, valueProductId.Text } }, HelperGeneral.DbProductTableFieldNameMemo, memo );

        _helperGeneral.UpdateImageFieldInTable(HelperGeneral.DbProductTable, new string[1, 3]
        {   {HelperGeneral.DbProductTableFieldNameId, HelperGeneral.DbProductTableFieldTypeId, valueProductId.Text } }, productImage, HelperGeneral.DbProductTableFieldNameImage);
    }
    #endregion Update row Product Table

    #region Update row ProductSupplier Table
    private void UpdateRowProductSupplier ()
    {
        var productSupplierDefault = "";                                                    // Is the selected supplier/product the default product to be used?
        if ((bool)chkSupplierDefault.IsChecked) { productSupplierDefault = "*"; }

        InitializeHelper ();

        // If the checkbox that for default supplier is selected, the eventual other default supplier should be set to '', because there can only be one default
        if ((bool)chkSupplierDefault.IsChecked)
        {
            _ = _helper.UncheckDefaultSupplierTblProductSupplier ( int.Parse ( valueProductSupplierId.Text ), int.Parse ( valueProductId.Text ) );
        }

        string result = string.Empty;
        _helperGeneral.UpdateFieldInTable ( HelperGeneral.DbProductSupplierTable, new string[1, 3]
        {   { HelperGeneral.DbProductSupplierTableFieldNameId, HelperGeneral.DbProductSupplierTableFieldTypeId, valueProductSupplierId.Text} }, new string[9, 3]
        {   { HelperGeneral.DbProductSupplierTableFieldNameProductId, HelperGeneral.DbProductSupplierTableFieldTypeProductId, valueProductId.Text},
            { HelperGeneral.DbProductSupplierTableFieldNameSupplierId, HelperGeneral.DbProductSupplierTableFieldTypeSupplierId, valueProductSupplierSupplierId.Text},
            { HelperGeneral.DbProductSupplierTableFieldNameCurrencyId, HelperGeneral.DbProductSupplierTableFieldTypeCurrencyId, valueProductSupplierCurrencyId.Text},
            { HelperGeneral.DbProductSupplierTableFieldNameProductNumber, HelperGeneral.DbProductSupplierTableFieldTypeProductNumber, inpSupplierProductNumber.Text},
            { HelperGeneral.DbProductSupplierTableFieldNameProductName, HelperGeneral.DbProductSupplierTableFieldTypeProductName, inpSupplierProductName.Text},
            { HelperGeneral.DbProductSupplierTableFieldNameProductUrl, HelperGeneral.DbProductSupplierTableFieldTypeProductUrl, inpSupplierProductUrl.Text},
            { HelperGeneral.DbProductSupplierTableFieldNamePrice, HelperGeneral.DbProductSupplierTableFieldTypePrice, inpSupplierProductPrice.Text.Replace ( "€", "" ).Replace ( " ", "" )},
            { HelperGeneral.DbProductSupplierTableFieldNameProductUrl, HelperGeneral.DbProductSupplierTableFieldTypeProductUrl, inpSupplierProductUrl.Text},
            { HelperGeneral.DbProductSupplierTableFieldNameDefaultSupplier, HelperGeneral.DbProductSupplierTableFieldTypeDefaultSupplier, productSupplierDefault} } );

        UpdateStatus (result);

        // Get data from database
        _dtPS = _helperGeneral.GetData ( HelperGeneral.DbProductSupplierTable);


        // Populate data in datagrid from datatable
        ProductSupplierCode_DataGrid.DataContext = _dtPS;
    }
    #endregion Update row ProductSupplier Tabble

    #region Delete row in ProductSupplier table
    private void DeleteRowProductSupplier()
    {
        InitializeHelper();

        _helperGeneral.DeleteRecordFromTable ( HelperGeneral.DbProductSupplierTable, new string[1, 3]
        {   {HelperGeneral.DbProductSupplierTableFieldNameSupplierId, HelperGeneral.DbProductSupplierTableFieldTypeSupplierId, valueProductSupplierSupplierId.Text} } );
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

    private void ButtonWeb(object sender, RoutedEventArgs e)
    {
        var browserwindow = new System.Diagnostics.ProcessStartInfo();
        browserwindow.UseShellExecute = true;
        browserwindow.FileName = inpSupplierProductUrl.Text;
        System.Diagnostics.Process.Start(browserwindow);
    }

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

    #region Reset all values to empty
    private void ClearAllFields ()
    {
        valueProductId.Clear ();
        valueCategoryId.Clear ();
        valueStorageId.Clear ();
        valueBrandId.Clear ();
        valueUnitId.Clear ();
        valueProductSupplierId.Clear ();
        valueProductSupplierSupplierId.Clear ();
        valueProductSupplierCurrencyId.Clear ();
        valueImageRotationAngle.Clear ();
        inpProductCode.Clear ();
        inpProductName.Clear ();
        inpProductMinimalStock.Clear ();
        inpProductStandardOrderQuantity.Clear ();
        inpProductDimensions.Clear ();
        inpProductPrice.Clear ();
        inpSupplierProductNumber.Clear ();
        inpSupplierProductName.Clear ();
        inpSupplierProductPrice.Clear ();
        inpProductMemo.Document.Blocks.Clear ();
        chkProjectProjectCosts.IsChecked = false;
        chkSupplierDefault.IsChecked = false;
        cboxProductBrand.SelectedItem = null;
        cboxProductCategory.SelectedItem = null;
        cboxProductUnit.SelectedItem = null;
        cboxProductSupplier.SelectedItem = null;
        imgProductImage.Source = null;
        TBResetButtonEnable.Text = "Collapsed";
    }
    #endregion
}
