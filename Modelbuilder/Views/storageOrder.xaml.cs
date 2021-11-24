using static Modelbuilder.HelperOrder;

namespace Modelbuilder.Views;
public partial class storageOrder : Page
{
    private HelperOrder _helper;
    private DataTable _dt, _dtSC;
    private int _dbRowCount, _dbRowCountSC;
    private int _currentDataGridIndex, _currentDataGridPSIndex;
    // static string DatabaseSupplierTable = "supplier", DatabaseProductTable = "product", DatabaseProjectTable = "project";

    public storageOrder()
    {
        var SupplierList = new List<HelperOrder.Supplier>();
        var ProjectList = new List<HelperOrder.Project>();
        var ProductList = new List<HelperOrder.Product>();
        var CategoryList = new List<HelperOrder.Category>();

        InitializeComponent();
        InitializeHelper();

        // Fill the dropdowns
        cboxSupplier.ItemsSource = _helper.GetSupplierList(SupplierList);
        cboxProject.ItemsSource = _helper.GetProjectList(ProjectList);
        cboxProduct.ItemsSource = _helper.GetProductList(ProductList);
        cboxCategory.ItemsSource = _helper.GetCategoryList(CategoryList);

        valRowTotal.Text = "0,00";
        valShippingCost.Text = "0,00";
        valOrderCost.Text = "0,00";
        valGrandTotal.Text = "0,00";
        valTotal.Text = "0,00";

        GetData();
    }

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helper == null)
        {
            _helper = new HelperOrder("localhost", 3306, "modelbuilder", "root", "admin");
        }
    }
    #endregion InitializeHelper (connect to database)

    #region Get the Order data
    private void GetData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helper.GetDataTblOrder();
        //_dtSC = _helper.GetDataTblOrderline();

        // Populate data in datagrid from datatable
        OrderCode_DataGrid.DataContext = _dt;
        //OrderDetail_DataGrid.DataContext = _dtSC;

        // Set value
        _dbRowCount = _dt.Rows.Count;
        //_dbRowCountSC = _dtSC.Rows.Count;
        RecordsCount.Text = _dbRowCount.ToString();
        //RecordsCountSC.Text = _dbRowCountSC.ToString();

        // Clear existing memo data
        inpOrderMemo.Document.Blocks.Clear();

        string tmpStr = "";
        //update status
        if (_dt.Rows.Count != 1) { tmpStr = "s"; };
        string msg = "Status: " + _dt.Rows.Count + " Bestelling" + tmpStr + " ingelezen.";
        //if (_dtSC.Rows.Count != 1) { tmpStr = "s"; };
        //string msgSC = "Status: " + _dtSC.Rows.Count + " Bestelregel" + tmpStr + " ingelezen.";

        UpdateStatus(msg);
        //UpdateStatus(msgSC);
    }
    #endregion

    #region Datagrid selection changed for Order
    private void OrderCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridIndex = dg.SelectedIndex;

        GetMemo(dg.SelectedIndex);

        valueOrderId.Text = Row_Selected["order_Id"].ToString();
        valueSupplierId.Text = Row_Selected["order_SupplierId"].ToString();
        valueSupplierName.Text = Row_Selected["order_SupplierName"].ToString();
        inpOrderNumber.Text = Row_Selected["order_OrderNumber"].ToString();
        inpOrderDate.Text = Row_Selected["order_Date"].ToString();
        inpCurrencySymbol.Text = Row_Selected["order_CurrencySymbol"].ToString();
        inpCurrencyRate.Text = Row_Selected["order_CurrencyConversionRate"].ToString();
        inpShippingCosts.Text = Row_Selected["order_ShippingCosts"].ToString();
        inpOrderCosts.Text = Row_Selected["order_OrderCosts"].ToString();
        int _TempOrderClosed = int.Parse(Row_Selected["order_Closed"].ToString());
        if (_TempOrderClosed == 1) { dispOrderClosed.IsChecked = true; } else { dispOrderClosed.IsChecked = false; }

        cboxSupplier.Text = Row_Selected["order_SupplierName"].ToString();

        //Select the saved Supplier in the combobox by default
        foreach (Supplier supplier in cboxSupplier.Items)
        {
            if (supplier.SupplierName == Row_Selected["order_SupplierName"].ToString())
            {
                cboxSupplier.SelectedItem = supplier;
                break;
            }
        }

        // Retrieve list of OrderRows for this Order from database
        _dtSC = _helper.GetDataTblOrderline(int.Parse(valueOrderId.Text));

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;
        cboxOrderRowEditable.IsChecked = true;
    }
    #endregion Datagrid selection changed for Order

    #region Datagrid selectionchanged for OrderRow
    private void OrderDetail_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
    #endregion Datagrid selectionchanged for OrderRow

    #region Selection changed: Combobox Supplier
    private void cboxSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperOrder.Supplier item in e.AddedItems)
        {
            valueSupplierId.Text = item.SupplierId.ToString();
            valueSupplierName.Text = item.SupplierName.ToString();
            valueCurrencyId.Text = item.SupplierCurrencyId.ToString();
            inpCurrencySymbol.Text = item.SupplierCurrencySymbol.ToString();
        }
        // textBox1.Text = string.Format("{0:#,##0.00}", double.Parse(textBox1.Text));
        inpCurrencyRate.Text = _helper.GetSingleData(int.Parse(valueCurrencyId.Text), "Currency", "currency_ConversionRate", "float");
        inpShippingCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_ShippingCosts", "double");
        valueMinShippingCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_MinShippingCosts", "double");
        inpOrderCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_OrderCosts", "double");
        inpCurrencyRate.Text = string.Format("{0:#,###0.000}", double.Parse(inpCurrencyRate.Text));
        inpShippingCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpShippingCosts.Text));
        inpOrderCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpOrderCosts.Text));

        // Totals should not be empty
        if (string.IsNullOrWhiteSpace(valRowTotal.Text))
        {
            valRowTotal.Text = "0,00";
            valShippingCost.Text = "0,00";
            valOrderCost.Text = "0,00";
            valGrandTotal.Text = "0,00";
            valTotal.Text = "0,00";
        }

        if (double.Parse(valRowTotal.Text) < double.Parse(valueMinShippingCosts.Text))
        {
            valShippingCost.Text = inpShippingCosts.Text;
        }
        else
        {
            valShippingCost.Text = "0,00";
        }

        // For order costs there is now minimal valur
        valOrderCost.Text = inpOrderCosts.Text;
    }
    #endregion

    #region Selection changed: Combobox Product
    private void cboxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperOrder.Product item in e.AddedItems)
        {
            valueProductId.Text = item.ProductId.ToString();
            valueProductName.Text = item.ProductName.ToString();
        }

        // Check if product is in table for this supplier, if not return value will be -1
        string _tmpPrice = _helper.GetSingleDataMultiSelect("ProductSupplier", "productSupplier_ProductPrice", "Double", "productSupplier_SupplierId", int.Parse(valueSupplierId.Text), "AND", "productSupplier_ProductId", int.Parse(valueProductId.Text));

        if (_tmpPrice != "-1")
        {
            inpPrice.Text = _tmpPrice.ToString();
        }
        else
        {
            inpPrice.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_Price", "Double");
        }

        inpPrice.Text = string.Format("{0:#,##0.00}", double.Parse(inpPrice.Text));

        if (inpNumber.Text == string.Empty)
        {
            valTotal.Text = "0";
        }
        else
        {
            valTotal.Text = (double.Parse(inpNumber.Text) * double.Parse(inpPrice.Text)).ToString();
        }

        valTotal.Text = string.Format("{0:#,##0.00}", double.Parse(valTotal.Text));

        valueCategoryId.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_CategoryId", "int");

        cboxCategory.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_CategoryName", "string");
        //cboxCategory.SelectedItem= _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_CategoryName", "string");
    }
    #endregion

    #region Selection changed: Combobox Project
    private void cboxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperOrder.Project item in e.AddedItems)
        {
            valueProjectId.Text = item.ProjectId.ToString();
            //valueProjectName.Text = item.ProjectName.ToString();
        }

    }
    #endregion Selection changed: Combobox Project

    #region Selection changed: Combobox Categroy
    private void cboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperOrder.Category item in e.AddedItems)
        {
            valueCategoryId.Text = item.CategoryId.ToString();
            //valueCategoryName.Text = item.CategoryName.ToString();
        }

    }
    #endregion Selection changed: Combobox Category

    #region Selection changed: Number of items
    private void inpNumber_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (inpNumber.Text == string.Empty)
        {
            valTotal.Text = "0";
        }
        else
        {
            valTotal.Text = (double.Parse(inpNumber.Text) * double.Parse(inpPrice.Text)).ToString();
        }

        valTotal.Text = string.Format("{0:#,##0.00}", double.Parse(valTotal.Text));

    }
    #endregion Selection changed: Number of items

    #region Selection changed: Price of single item
    private void inpPrice_SelectionChanged(object sender, RoutedEventArgs e)
    {
        if (inpNumber.Text == string.Empty)
        {
            valTotal.Text = "0";
        }
        else
        {
            valTotal.Text = (double.Parse(inpNumber.Text) * double.Parse(inpPrice.Text)).ToString();
        }
        valTotal.Text = string.Format("{0:#,##0.00}", double.Parse(valTotal.Text));
    }
    #endregion Selection changed: Price of single item

    #region UpdateEvent after changing OrderCosts
    private void OrderCostsChanged(object sender, TextChangedEventArgs e)
    {
        // Values should not be empty
        if (string.IsNullOrWhiteSpace(valRowTotal.Text))
        {
            valRowTotal.Text = "0,00";
        }
        if (string.IsNullOrWhiteSpace(valGrandTotal.Text))
        {
            valGrandTotal.Text = "0,00";
        }

        // Change the order costs
        valOrderCost.Text = inpOrderCosts.Text;
    }
    #endregion UpdateEvent after changing OrderCosts

    #region UpdateEvent after changing ShippingCosts
    private void ShippingCostsChanged(object sender, TextChangedEventArgs e)
    {
        // Values should not be empty
        if (string.IsNullOrWhiteSpace(valRowTotal.Text))
        {
            valRowTotal.Text = "0,00";
        }
        if (string.IsNullOrWhiteSpace(valGrandTotal.Text))
        {
            valGrandTotal.Text = "0,00";
        }

        if (string.IsNullOrWhiteSpace(valueMinShippingCosts.Text))
        {
            valueMinShippingCosts.Text = "0,00";
        }

        // update shipping Costs depending on the minimal order value
        if (double.Parse(valRowTotal.Text) < double.Parse(valueMinShippingCosts.Text))
        {
            valShippingCost.Text = inpShippingCosts.Text;
        }
        else
        {
            valShippingCost.Text = "0,00";
        }
    }
    #endregion UpdateEvent after changing OrderCosts

    #region Toolbar for order rows
    #region Toolbar button for Orderlines: New
    private void OrderlinesToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        var OrderlineOrderId = int.Parse(valueOrderId.Text);
        var OrderlineProductId = int.Parse(valueProductId.Text);
        var OrderlineProjectId = 0;
        var OrderlineCategoryId = 0;
        var OrderlineNumber = 0.00;
        var OrderlinePrice = 0.00;
        var OrderlineRealRowTotal = 0.00;  //RealRowTotal Will be calculated and updated/inserted on Save

        if (valueProjectId.Text != string.Empty) { OrderlineProjectId = int.Parse(valueProjectId.Text); }
        if (valueCategoryId.Text != string.Empty) { OrderlineCategoryId = int.Parse(valueCategoryId.Text); }
        if (inpNumber.Text == String.Empty) { OrderlineNumber = 0; } else { OrderlineNumber = double.Parse(inpNumber.Text); }
        if (inpPrice.Text == String.Empty) { OrderlinePrice = 0; } else { OrderlinePrice = double.Parse(inpPrice.Text); }

        InitializeHelper();
        var result = _helper.InsertTblOrderline(OrderlineOrderId, OrderlineProductId, OrderlineProjectId, OrderlineCategoryId, OrderlineNumber, OrderlinePrice, OrderlineRealRowTotal);
        UpdateStatus(result);

        // Get data from database
        _dtSC = _helper.GetDataTblOrderline(int.Parse(valueSupplierId.Text));

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;
        OrderDetail_DataGrid.SelectedItem = OrderDetail_DataGrid.Items.Count - 1;
        _ = OrderDetail_DataGrid.Focus();

        // Make orderline related cells available
        cboxOrderRowEditable.IsChecked = true;
    }
    #endregion Toolbar button for Orderlines: New

    #region Toolbar button for Orderlines: Save
    private void OrderlinesToolbarButtonSave(object sender, RoutedEventArgs e)
    {

    }
    #endregion Toolbar button for Orderlines: Save

    #region Toolbar button for Orderlines: Delete
    private void OrderlinesToolbarButtonDelete(object sender, RoutedEventArgs e)
    {

    }
    #endregion Toolbar button for Orderlines: Delete
    #endregion Toolbar for order rows

    #region Get content of Memofield
    private void GetMemo(int index)
    {
        string ContentOrderMemo = string.Empty;

        if (_dt != null && index >= 0 && index < _dt.Rows.Count)
        {
            //set value
            DataRow row = _dt.Rows[index];


            if (row["order_Memo"] != null && row["order_Memo"] != DBNull.Value)
            {
                //get value from DataTable
                ContentOrderMemo = row["order_Memo"].ToString();
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

    #region Toolbar for Orders
    #region Click New button (on Order toolbar)
    private void ToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        var SupplierId = 0;
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;

        if (valueSupplierId.Text == String.Empty) { SupplierId = 0; } else { SupplierId = int.Parse(valueSupplierId.Text); }
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
        result = _helper.InsertTblOrder(SupplierId, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, OrderMemo);
        UpdateStatus(result);

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

    }
    #endregion Click Save button (on Order toolbar)

    #region Click Delete button (on Order toolbar)
    private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
    {

    }
    #endregion Click Delete button (on Order toolbar)
    #endregion Toolbar for Orders

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
    #endregion Update status

    #region rtfToolbar actions
    // private bool dataChanged = false; // Unsaved textchanges

    private string privateText = null; // Content of RTFBox in txt-Format
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
