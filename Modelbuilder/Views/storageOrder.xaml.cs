namespace Modelbuilder;
public partial class storageOrder : Page
{
    private HelperGeneral _helperGeneral;
    private HelperOrder _helper;
    private DataTable _dt, _dtSC;
    private int _dbRowCount, _dbRowCountSC;
    private int _currentDataGridIndex;
    private string DbProductSupplierTable = "productsupplier";

    public storageOrder()
    {
        var SupplierList = new List<HelperGeneral.Supplier>();
        var ProjectList = new List<HelperGeneral.Project>();
        var ProductList = new List<HelperGeneral.Product>();
        var CategoryList = new List<HelperGeneral.Category>();

        InitializeComponent();
        InitializeHelper();

        // Fill the dropdowns
        cboxSupplier.ItemsSource = _helperGeneral.GetSupplierList(SupplierList);
        cboxProject.ItemsSource = _helperGeneral.GetProjectList(ProjectList);
        cboxProduct.ItemsSource = _helperGeneral.GetProductList(ProductList);
        cboxCategory.ItemsSource = _helperGeneral.GetCategoryList(CategoryList);

        GetData();
    }

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helper == null)
        {
            _helper = new HelperOrder(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
    }
    #endregion InitializeHelper (connect to database)

    #region Get the Order data
    private void GetData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helper.GetDataTblOrder();

        // Populate data in datagrid from datatable
        OrderCode_DataGrid.DataContext = _dt;

        // Set value
        _dbRowCount = _dt.Rows.Count;
        RecordsCount.Text = _dbRowCount.ToString();

        // Clear existing memo data
        inpOrderMemo.Document.Blocks.Clear();

        string tmpStr = "";
        //update status
        if (_dt.Rows.Count != 1) { tmpStr = "s"; };
        string msg = "Status: " + _dt.Rows.Count + " Bestelling" + tmpStr + " ingelezen.";

        UpdateStatus("order", msg);
    }
    #endregion Get the Order data

    #region Get the Orderrow data
    private void GetRowData()
    {
        InitializeHelper();

        // Get data from database
        _dtSC = _helper.GetDataTblOrderline();

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;

        // Set value
        _dbRowCountSC = _dtSC.Rows.Count;
        RecordsCountSC.Text = _dbRowCountSC.ToString();

        string tmpStr = "";
        //update status
        if (_dtSC.Rows.Count != 1) { tmpStr = "s"; };
        string msgSC = "Status: " + _dtSC.Rows.Count + " Bestelregel" + tmpStr + " ingelezen.";

        UpdateStatus("row", msgSC);
    }
    #endregion Get the Orderrow data

    #region Datagrid selection changed for Order
    private void OrderCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridIndex = dg.SelectedIndex;

        GetMemo(dg.SelectedIndex);

        #region Select the saved Supplier in the Supplier combobox by default
        string _tempSupplier = Row_Selected["SupplierName"].ToString();
        cboxSupplier.Text = _tempSupplier;

        //Select the saved Supplier in the combobox by default
        foreach (HelperGeneral.Supplier supplier in cboxSupplier.Items)
        {
            if (supplier.SupplierName == _tempSupplier)
            {
                cboxSupplier.SelectedItem = supplier;
                break;
            }
        }
        #endregion Select the saved Supplier in the Supplier combobox by default

        valueOrderId.Text = Row_Selected["Id"].ToString();
        inpOrderNumber.Text = Row_Selected["OrderNumber"].ToString();
        inpOrderDate.Text = Row_Selected["OrderDate"].ToString();
        inpCurrencySymbol.Text = Row_Selected["OrderCurrencySymbol"].ToString();
        inpCurrencyRate.Text = string.Format("{0:#,####0.0000}", double.Parse(Row_Selected["OrderConversionRate"].ToString()));
        inpShippingCosts.Text = Row_Selected["ShippingCosts"].ToString();
        inpOrderCosts.Text = Row_Selected["OrderCosts"].ToString();

        int _TempOrderClosed = int.Parse(Row_Selected["Closed"].ToString());
        if (_TempOrderClosed == 1) { dispOrderClosed.IsChecked = true; } else { dispOrderClosed.IsChecked = false; }

        // Retrieve list of OrderRows for this Order from database
        _dtSC = _helper.GetDataTblOrderline(int.Parse(valueOrderId.Text));

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;
        cboxOrderRowEditable.IsChecked = true;

        // Set value
        _dbRowCountSC = _dtSC.Rows.Count;
        RecordsCountSC.Text = _dbRowCountSC.ToString();

        string tmpStr = "";
        //update status
        if (_dtSC.Rows.Count != 1) { tmpStr = "s"; };
        string msgSC = "Status: " + _dtSC.Rows.Count + " Bestelregel" + tmpStr + " ingelezen.";

        UpdateStatus("row", msgSC);
    }
    #endregion Datagrid selection changed for Order

    #region Datagrid selectionchanged for OrderRow
    private void OrderDetail_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridIndex = dg.SelectedIndex;

        #region Select the saved Product in the Product combobox by default
        string _tempProduct = Row_Selected["ProductName"].ToString();
        cboxProduct.Text = _tempProduct;

        foreach (HelperGeneral.Product product in cboxProduct.Items)
        {
            if (product.ProductName == _tempProduct)
            {
                cboxProduct.SelectedItem = product;
                break;
            }
        }
        #endregion Select the saved Product in the Product combobox by default

        #region Select the saved Project in the Project combobox by default
        string _tempProject = Row_Selected["ProjectName"].ToString();
        if (_tempProject == "") { cboxProject.Text = " "; } else { cboxProject.Text = _tempProject; }

        foreach (HelperGeneral.Project project in cboxProject.Items)
        {
            if (project.ProjectName == _tempProject)
            {
                cboxProject.SelectedItem = project;
                break;
            }
        }
        #endregion Select the saved Project in the Project combobox by default

        #region Select the saved Category in the Category combobox by default
        string _tempCategory = Row_Selected["CategoryName"].ToString();
        cboxCategory.Text = _tempCategory;

        foreach (HelperGeneral.Category category in cboxCategory.Items)
        {
            if (category.CategoryName == _tempCategory)
            {
                cboxCategory.SelectedItem = category;
                break;
            }
        }
        #endregion Select the saved Category in the Category combobox by default

        valueOrderlineId.Text = Row_Selected["Id"].ToString();
        inpNumber.Text = string.Format("{0:#,##0.00}", double.Parse(Row_Selected["Amount"].ToString()));
        inpPrice.Text = string.Format("{0:#,##0.00}", double.Parse(Row_Selected["Price"].ToString()));
    }
    #endregion Datagrid selectionchanged for OrderRow

    #region Selection changed: Combobox Supplier
    private void cboxSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Supplier item in e.AddedItems)
        {
            valueSupplierId.Text = item.SupplierId.ToString();
            valueSupplierName.Text = item.SupplierName.ToString();
            valueCurrencyId.Text = item.SupplierCurrencyId.ToString();
            inpCurrencySymbol.Text = item.SupplierCurrencySymbol.ToString();
        }

        inpCurrencyRate.Text = string.Format("{0:#,####0.0000}", double.Parse(_helper.GetSingleData(int.Parse(valueCurrencyId.Text), "currency", "ConversionRate", "double")));
        inpShippingCosts.Text = string.Format("{0:#,##0.00}", double.Parse(_helper.GetSingleData(int.Parse(valueSupplierId.Text), "supplier", "ShippingCosts", "double")));
        inpOrderCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpOrderCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "supplier", "OrderCosts", "double")));
        //inpCurrencyRate.Text = _helperGeneral.GetSingleData(int.Parse(valueCurrencyId.Text), "Currency", "ConversionRate", "float");
        //inpShippingCosts.Text = _helperGeneral.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "ShippingCosts", "double");
        valueMinShippingCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "supplier", "MinShippingCosts", "double");
        //inpOrderCosts.Text = _helperGeneral.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "OrderCosts", "double");
        //inpCurrencyRate.Text = string.Format("{0:#,####0.0000}", double.Parse(inpCurrencyRate.Text));
        //inpShippingCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpShippingCosts.Text));
        //inpOrderCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpOrderCosts.Text));
    }
    #endregion Selection changed: Combobox Supplier

    #region Selection changed: Combobox Product
    private void cboxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cboxProduct.SelectedIndex == -1) { return; }

        foreach (HelperGeneral.Product item in e.AddedItems)
        {
            valueProductId.Text = item.ProductId.ToString();
            valueProductName.Text = item.ProductName.ToString();
        }

        var SupplierHasProduct = _helper.CheckProductForSupplier(int.Parse(valueSupplierId.Text), int.Parse(valueProductId.Text));

        // If there is no price entered, retrieve the default price from the database
        if (inpPrice.Text == string.Empty || inpPrice.Text == "0,00")
        {
            // Check if product is in table for this supplier, if not return value will be -1
            string _tmpPrice = _helper.GetSingleDataMultiSelect(DbProductSupplierTable, "Price", "Double", "Supplier_Id", int.Parse(valueSupplierId.Text), "AND", "Product_Id", int.Parse(valueProductId.Text));

            if (SupplierHasProduct == 0)
            {
                inpPrice.Text = _helper.GetSingleDataMultiSelect(DbProductSupplierTable, "Price", "Double", "Supplier_Id", int.Parse(valueSupplierId.Text), "AND", "Product_Id", int.Parse(valueProductId.Text));
            }
            else
            {
                inpPrice.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "product", "Price", "Double");
            }

            inpPrice.Text = string.Format("{0:#,##0.00}", double.Parse(inpPrice.Text));
        }

        // If there is no quantity entered, retrieve the default order quantity of the item
        if (inpNumber.Text == string.Empty || inpNumber.Text == "0,00")
        {
            inpNumber.Text = string.Format("{0:#,##0.00}", int.Parse(_helper.GetSingleData(int.Parse(valueProductId.Text), "product", "StandardOrderQuantity", "double")));
        }

        if (valueProductId.Text != string.Empty)
        {
            valueCategoryId.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "product", "Category_Id", "int");
            valueCategoryName.Text = _helper.GetSingleData(int.Parse(valueCategoryId.Text), "category", "Name", "string");
            if (valueCategoryName.Text != string.Empty)
            {
                cboxCategory.Text = valueCategoryName.Text;

                foreach (HelperGeneral.Category category in cboxCategory.Items)
                {
                    if (category.CategoryName == valueCategoryName.Text)
                    {
                        cboxCategory.SelectedItem = category;
                        break;
                    }
                }
            }
        }
    }
    #endregion

    #region Selection changed: Combobox Project
    private void cboxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //If no project is selected do nothing
        //if (cboxProject.Text == String.Empty) { return; }

        foreach (HelperGeneral.Project item in e.AddedItems)
        {
            valueProjectId.Text = item.ProjectId.ToString();
            valueProjectName.Text = item.ProjectName.ToString();
        }
    }
    #endregion Selection changed: Combobox Project

    #region Selection changed: Combobox Categroy
    private void cboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //If no category is selected do nothing
        //if (cboxCategory.Text == String.Empty) { return; }
        if (cboxCategory.SelectedIndex == -1) { return; }

        foreach (HelperGeneral.Category item in e.AddedItems)
        {
            valueCategoryId.Text = item.CategoryId.ToString();
            valueCategoryName.Text = item.CategoryName.ToString();
        }
    }
    #endregion Selection changed: Combobox Category

    #region Toolbar for order rows
    #region Toolbar button for Orderlines: New
    private void OrderlinesToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        var OrderlineOrderId = int.Parse(valueOrderId.Text);
        var OrderlineSupplierId = int.Parse(valueSupplierId.Text);
        var OrderlineProductId = int.Parse(valueProductId.Text);
        var OrderlineProjectId = int.Parse(valueProjectId.Text);
        var OrderlineCategoryId = int.Parse(valueCategoryId.Text);
        var OrderlineNumber = double.Parse(inpNumber.Text);
        var OrderlinePrice = double.Parse(inpPrice.Text);

        InitializeHelper();
        var result = _helper.InsertTblOrderline(OrderlineOrderId, OrderlineSupplierId, OrderlineProductId, OrderlineProjectId, OrderlineCategoryId, OrderlineNumber, OrderlinePrice);
        UpdateStatus("row", result);

        // Get data from database
        _dtSC = _helper.GetDataTblOrderline(int.Parse(valueOrderId.Text));

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;
        //OrderDetail_DataGrid.SelectedItem = OrderDetail_DataGrid.Items.Count;
        SetFocusOnGrid(OrderDetail_DataGrid, OrderDetail_DataGrid.Items.Count - 1);

        valueProductId.Text = string.Empty;
        valueProductName.Text = string.Empty;
        valueProjectId.Text = string.Empty;
        valueProjectName.Text = string.Empty;
        valueCategoryId.Text = string.Empty;
        valueCategoryName.Text = string.Empty;
        inpPrice.Text = "0,00";
        inpNumber.Text = "0,00";

        cboxCategory.SelectedItem = null;
        cboxCategory.SelectedIndex = -1;
        cboxCategory.Text= string.Empty;
        cboxProduct.SelectedItem = null;
        cboxProduct.SelectedIndex = -1;
        cboxProduct.Text= string.Empty;
        cboxProject.SelectedItem = null;      
        cboxProject.SelectedIndex = -1;
        cboxProject.Text= string.Empty;
    }
    #endregion Toolbar button for Orderlines: New

    #region Set focus to newly added datagrid row
    private void SetFocusOnGrid(DataGrid grid, int index)
    {
        grid.ScrollIntoView(grid.Items.GetItemAt(index));
        grid.SelectionMode = DataGridSelectionMode.Single;
        grid.SelectionUnit = DataGridSelectionUnit.FullRow;
        grid.SelectedIndex = index;

        DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
        if (index != 0)
        {
            row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
    #endregion Set focus to newly added datagrid row

    #region Toolbar button for Orderlines: Save
    private void OrderlinesToolbarButtonSave(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridIndex;

        var OrderlineId = int.Parse(valueOrderlineId.Text);
        var OrderlineOrderId = int.Parse(valueOrderId.Text);
        var OrderlineSupplierId = int.Parse(valueSupplierId.Text);
        var OrderlineProductId = int.Parse(valueProductId.Text);
        var OrderlineProjectId = 0;
        var OrderlineCategoryId = 0;
        var OrderlineNumber = 0.00;
        var OrderlinePrice = 0.00;
        var OrderlineProductName = "";
        var OrderlineProjectName = "";
        var OrderlineCategoryName = "";

        if (valueProjectId.Text != string.Empty) { OrderlineProjectId = int.Parse(valueProjectId.Text); }
        if (valueCategoryId.Text != string.Empty) { OrderlineCategoryId = int.Parse(valueCategoryId.Text); }
        if (valueProjectName.Text != string.Empty) { OrderlineProjectName = valueProjectName.Text; }
        if (valueProductName.Text != string.Empty) { OrderlineProductName = valueProductName.Text; }
        if (valueCategoryName.Text != string.Empty) { OrderlineCategoryName = valueCategoryName.Text; }
        if (inpNumber.Text == String.Empty) { OrderlineNumber = 0; } else { OrderlineNumber = double.Parse(inpNumber.Text); }
        if (inpPrice.Text == String.Empty) { OrderlinePrice = 0; } else { OrderlinePrice = double.Parse(inpPrice.Text); }

        InitializeHelper();
        string result = string.Empty;
        result = _helper.UpdateTblOrderline(OrderlineOrderId, OrderlineSupplierId, OrderlineProductId, OrderlineProjectId, OrderlineCategoryId, OrderlineNumber, OrderlinePrice, OrderlineId);
        UpdateStatus("row", result);

        // Retrieve list of OrderRows for this Order from database
        _dtSC = _helper.GetDataTblOrderline(int.Parse(valueOrderId.Text));

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;

        // Set value
        _dbRowCountSC = _dtSC.Rows.Count;
        RecordsCountSC.Text = _dbRowCountSC.ToString();

        string tmpStr = "";
        //update status
        if (_dtSC.Rows.Count != 1) { tmpStr = "s"; };
        string msgSC = "Status: " + _dtSC.Rows.Count + " Bestelregel" + tmpStr + " ingelezen.";

        UpdateStatus("row", msgSC);

        // Make sure the eddited row in the datagrid is selected
        OrderDetail_DataGrid.SelectedIndex = rowIndex;
        OrderDetail_DataGrid.Focus();

    }
    #endregion Toolbar button for Orderlines: Save

    #region Toolbar button for Orderlines: Delete
    private void OrderlinesToolbarButtonDelete(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridIndex;

        InitializeHelper();

        string result = string.Empty;
        result = _helper.DeleteTblSupplyOrderline(int.Parse(valueOrderlineId.Text), "row");
        UpdateStatus("Row", result);


        GetRowData();

        if (rowIndex == 0)
        {
            OrderDetail_DataGrid.SelectedIndex = 0;
        }
        else
        {
            OrderDetail_DataGrid.SelectedIndex = rowIndex - 1;
        }

        OrderDetail_DataGrid.Focus();
    }
    #endregion Toolbar button for Orderlines: Delete
    #endregion Toolbar for order rows

    #region Toolbar for Orders
    #region Click New button (on Order toolbar)
    private void ToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        var SupplierId = 0;
        var CurrencyId = 0;
        var SupplierName = "";
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;

        if (valueSupplierId.Text == String.Empty) { SupplierId = 0; } else { SupplierId = int.Parse(valueSupplierId.Text); }
        if (valueCurrencyId.Text == String.Empty) { CurrencyId = 0; } else { CurrencyId = int.Parse(valueCurrencyId.Text); }
        if (valueSupplierName.Text == String.Empty) { SupplierName = ""; } else { SupplierName = valueSupplierName.Text; }
        if (inpOrderNumber.Text == String.Empty) { OrderNumber = ""; } else { OrderNumber = inpOrderNumber.Text; }
        if (inpOrderDate.Text == String.Empty) { OrderDate = ""; } else { OrderDate = inpOrderDate.Text; }
        if (inpCurrencySymbol.Text == String.Empty) { CurrencySymbol = ""; } else { CurrencySymbol = inpCurrencySymbol.Text; }
        if (inpCurrencyRate.Text == String.Empty) { CurrencyRate = 0.0000; } else { CurrencyRate = double.Parse(inpCurrencyRate.Text); }
        if (inpShippingCosts.Text == String.Empty) { ShippingCosts = 0.0000; } else { ShippingCosts = double.Parse(inpShippingCosts.Text); }
        if (inpOrderCosts.Text == String.Empty) { OrderCosts = 0.0000; } else { OrderCosts = double.Parse(inpOrderCosts.Text); }

        //convert RTF to string
        string OrderMemo = GetRichTextFromFlowDocument(inpOrderMemo.Document);

        if (_dt.Rows.Count != 0)
        { DataRow row = _dt.Rows[_dt.Rows.Count - 1]; }

        InitializeHelper();

        string result = string.Empty;
        result = _helper.InsertTblOrder(SupplierId, CurrencyId, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, OrderMemo);
        UpdateStatus("order", result);

        // Get data from database
        _dt = _helper.GetDataTblOrder();

        // Populate data in datagrid from datatable
        OrderCode_DataGrid.DataContext = _dt;
        if (OrderCode_DataGrid.SelectedItem is not DataRowView)
        {
            return;
        }
        cboxOrderEditable.IsChecked = true;
        cboxOrderRowEditable.IsChecked = true;
    }
    #endregion Click New button (on Order toolbar)

    #region Click Save Data button (on Order toolbar)
    private void ToolbarButtonSave(object sender, RoutedEventArgs e)
    {
        // On save order detail information has to be saved, but also SupplierId of existing orderrows

        int rowIndex = _currentDataGridIndex;
        var OrderId = 0;
        var SupplierId = 0;
        var CurrencyId = 0;
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;

        if (valueOrderId.Text != String.Empty) { OrderId = int.Parse(valueOrderId.Text); }
        if (valueSupplierId.Text != String.Empty) { SupplierId = int.Parse(valueSupplierId.Text); }
        if (valueCurrencyId.Text != String.Empty) { CurrencyId = int.Parse(valueCurrencyId.Text); }
        if (inpOrderNumber.Text != String.Empty) { OrderNumber = inpOrderNumber.Text; }
        if (inpOrderDate.Text != String.Empty) { OrderDate = inpOrderDate.Text; }
        if (inpCurrencySymbol.Text != String.Empty) { CurrencySymbol = inpCurrencySymbol.Text; }
        if (inpCurrencyRate.Text != String.Empty) { CurrencyRate = double.Parse(inpCurrencyRate.Text); }
        if (inpShippingCosts.Text != String.Empty) { ShippingCosts = double.Parse(inpShippingCosts.Text); }
        if (inpOrderCosts.Text != String.Empty) { OrderCosts = double.Parse(inpOrderCosts.Text); }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpOrderMemo.Document);

        if (valueOrderId.Text == "")
        // if (_dt.Rows.Count > _dbRowCount)
        {
            InsertOrderRow(OrderCode_DataGrid.SelectedIndex);
        }
        else
        {
            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblOrder(OrderId, SupplierId, CurrencyId, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, memo);
            string _tempresult = _helper.UpdateSupplierTblOrderline(OrderId, SupplierId);
            UpdateStatus("order", result);
        }

        GetData();

        // Make sure the eddited row in the datagrid is selected
        OrderCode_DataGrid.SelectedIndex = rowIndex;
        OrderCode_DataGrid.Focus();
    }
    #endregion Click Save button (on Order toolbar)

    #region Click Delete button (on Order toolbar)
    private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
    {
        int rowIndex = _currentDataGridIndex;

        InitializeHelper();

        string result = string.Empty;
        result = _helper.DeleteTblSupplyOrder(int.Parse(valueOrderId.Text));
        UpdateStatus("Order", result);

        //TODO: After deleting the order we have to check if there are orderrows on this orderId and if Yes delete all those lines

        var _tempOrders = _helper.CheckOrderRowsForOrder(int.Parse(valueOrderId.Text));

        if (_tempOrders != 0)
        {
            result = _helper.DeleteTblSupplyOrderline(int.Parse(valueOrderId.Text), "order");
            UpdateStatus("Order", result);
        }

        GetData();

        if (rowIndex == 0)
        {
            OrderCode_DataGrid.SelectedIndex = 0;
        }
        else
        {
            OrderCode_DataGrid.SelectedIndex = rowIndex - 1;
        }

        OrderCode_DataGrid.Focus();
    }
    #endregion Click Delete button (on Order toolbar)
    #endregion Toolbar for Orders

    #region Get content of Memofield
    private void GetMemo(int index)
    {
        string ContentOrderMemo = string.Empty;

        if (_dt != null && index >= 0 && index < _dt.Rows.Count)
        {
            //set value
            DataRow row = _dt.Rows[index];


            if (row["Memo"] != null && row["Memo"] != DBNull.Value)
            {
                //get value from DataTable
                ContentOrderMemo = row["Memo"].ToString();
            }

            if (!String.IsNullOrEmpty(ContentOrderMemo))
            {
                //clear existing data
                inpOrderMemo.Document.Blocks.Clear();

                //convert to byte[]
                byte[] dataArr = Encoding.UTF8.GetBytes(ContentOrderMemo);

                using (MemoryStream ms = new(dataArr))
                {
                    //load data
                    TextRange flowDocRange = new TextRange(inpOrderMemo.Document.ContentStart, inpOrderMemo.Document.ContentEnd);
                    flowDocRange.Load(ms, DataFormats.Rtf);
                }
            }
        }
    }
    #endregion Get content of Memofield

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

    #region Insert new row in table: Supplyorder
    private void InsertOrderRow(int dgIndex)
    {
        //since the DataGrid DataContext is set to the DataTable, 
        //the DataTable is updated when data is modified in the DataGrid
        //get last row
        DataRow row = _dt.Rows[_dt.Rows.Count - 1];

        var SupplierId = 0;
        var CurrencyId = 0;
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;

        if (valueSupplierId.Text == String.Empty) { SupplierId = 0; } else { SupplierId = int.Parse(valueSupplierId.Text); }
        if (valueCurrencyId.Text == String.Empty) { CurrencyId = 0; } else { CurrencyId = int.Parse(valueCurrencyId.Text); }
        if (inpOrderNumber.Text == String.Empty) { OrderNumber = ""; } else { OrderNumber = inpOrderNumber.Text; }
        if (inpOrderDate.Text == String.Empty) { OrderDate = ""; } else { OrderDate = inpOrderDate.Text; }
        if (inpCurrencySymbol.Text == String.Empty) { CurrencySymbol = ""; } else { CurrencySymbol = inpCurrencySymbol.Text; }
        if (inpCurrencyRate.Text == String.Empty) { CurrencyRate = 0.0000; } else { CurrencyRate = double.Parse(inpCurrencyRate.Text); }
        if (inpShippingCosts.Text == String.Empty) { ShippingCosts = 0.0000; } else { ShippingCosts = double.Parse(inpShippingCosts.Text); }
        if (inpOrderCosts.Text == String.Empty) { OrderCosts = 0.0000; } else { OrderCosts = double.Parse(inpOrderCosts.Text); }

        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpOrderMemo.Document);

        InitializeHelper();

        string result = string.Empty;
        result = _helper.InsertTblOrder(SupplierId, CurrencyId, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, memo);
        UpdateStatus("order", result);
    }


    #endregion

    #region Update Order row
    private void UpdateOrderRow(int OrderId, int SupplierId, int CurrencyId, string OrderNumber, string OrderDate, string CurrencySymbol, double CurrencyRate, double ShippingCosts, double OrderCosts, string OrderMemo)
    {
        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpOrderMemo.Document);

        InitializeHelper();

        string result = string.Empty;
        result = _helper.UpdateTblOrder(OrderId, SupplierId, CurrencyId, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, memo);
        UpdateStatus("row", result);
    }
    #endregion Update Order Row

    #region Update status
    private void UpdateStatus(string area, string msg)
    {
        if (!String.IsNullOrEmpty(msg))
        {
            if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
            {
                if (area == "order")
                { textBoxStatus.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                else { textBoxStatusSC.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                Debug.WriteLine(String.Format("{0} - Status: {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
            }
            else
            {
                if (area == "order")
                { textBoxStatus.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                else { textBoxStatusSC.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                Debug.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
            }
        }
    }
    #endregion Update status

    #region rtfToolbar actions
    // private bool dataChanged = false; // Unsaved textchanges

    private string privateText = null; // Content of RTFBox in txt-Format
    /// <summary>
    /// Gets or Sets the text.
    /// </summary>
    public string text
    {
        get
        {
            TextRange range = new TextRange(inpOrderMemo.Document.ContentStart, inpOrderMemo.Document.ContentEnd);
            return range.Text;
        }
        set
        {
            privateText = value;
        }
    }

    private string ShowRow; // aktuelle Zeile der Cursorposition
    private int _CurrentRow = 1;
    /// <summary>
    /// Gets or Sets the current row.
    /// </summary>
    public int CurrentRow
    {
        get { return _CurrentRow; }
        set
        {
            _CurrentRow = value;
            ShowRow = "Rij: " + value;
            //Uncomment when statusbar is in place
            //LabelRowNr.Content = ShowRow;
        }
    }



    private string ShowColumn; // aktuelle Spalte der Cursorposition
    private int _CurrentColumn = 1;

    /// <summary>
    /// Gets or Sets the current column.
    /// </summary>
    public int CurrentColumn
    {
        get { return _CurrentColumn; }
        set
        {
            _CurrentColumn = value;
            ShowColumn = "Kol: " + value;
            // Uncomment when statusbar is in place
            //LabelColumnNr.Content = ShowColumn;
        }
    }
    #endregion rtfToolbar actions
}
