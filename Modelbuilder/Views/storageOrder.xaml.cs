﻿using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Xml.Linq;

using static Modelbuilder.HelperOrder;

namespace Modelbuilder.Views;
public partial class storageOrder : Page
{
    private HelperOrder _helper;
    private DataTable _dt, _dtSC;
    private int _dbRowCount, _dbRowCountSC;
    private int _currentDataGridIndex;

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
        string _tempSupplier = Row_Selected["order_SupplierName"].ToString();
        cboxSupplier.Text = _tempSupplier;

        //Select the saved Supplier in the combobox by default
        foreach (Supplier supplier in cboxSupplier.Items)
        {
            if (supplier.SupplierName == _tempSupplier)
            {
                cboxSupplier.SelectedItem = supplier;
                break;
            }
        }
        #endregion Select the saved Supplier in the Supplier combobox by default

        valueOrderId.Text = Row_Selected["order_Id"].ToString();
        inpOrderNumber.Text = Row_Selected["order_OrderNumber"].ToString();
        inpOrderDate.Text = Row_Selected["order_Date"].ToString();
        inpCurrencySymbol.Text = Row_Selected["order_CurrencySymbol"].ToString();
        inpCurrencyRate.Text = string.Format("{0:#,####0.0000}", double.Parse(Row_Selected["order_CurrencyConversionRate"].ToString()));
        inpShippingCosts.Text = Row_Selected["order_ShippingCosts"].ToString();
        inpOrderCosts.Text = Row_Selected["order_OrderCosts"].ToString();

        int _TempOrderClosed = int.Parse(Row_Selected["order_Closed"].ToString());
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
        string _tempProduct = Row_Selected["orderline_ProductName"].ToString();
        cboxProduct.Text = _tempProduct;

        foreach (Product product in cboxProduct.Items)
        {
          if (product.ProductName == _tempProduct)
                {
                cboxProduct.SelectedItem = product;
                break;
            }
        }
        #endregion Select the saved Product in the Product combobox by default

        #region Select the saved Project in the Project combobox by default
        string _tempProject = Row_Selected["orderline_ProjectName"].ToString();
        if (_tempProject=="") { cboxProject.Text = " "; } else { cboxProject.Text = _tempProject; }

        foreach (Project project in cboxProject.Items)
        {
            if (project.ProjectName == _tempProject)
            {
                cboxProject.SelectedItem = project;
                break;
            }
        }
        #endregion Select the saved Project in the Project combobox by default

        #region Select the saved Category in the Category combobox by default
        string _tempCategory = Row_Selected["orderline_CategoryName"].ToString();
        cboxCategory.Text = _tempCategory;

        foreach (Category category in cboxCategory.Items)
        {
            if (category.CategoryName == _tempCategory)
            {
                cboxCategory.SelectedItem = category;
                break;
            }
        }
        #endregion Select the saved Category in the Category combobox by default

        valueOrderlineId.Text = Row_Selected["orderline_Id"].ToString();
        inpNumber.Text = string.Format("{0:#,##0.00}", double.Parse(Row_Selected["orderline_Number"].ToString()));
        inpPrice.Text = string.Format("{0:#,##0.00}", double.Parse(Row_Selected["orderline_Price"].ToString()));
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

        inpCurrencyRate.Text = string.Format("{0:#,####0.0000}", double.Parse(_helper.GetSingleData(int.Parse(valueCurrencyId.Text), "Currency", "currency_ConversionRate", "double")));
        inpShippingCosts.Text = string.Format("{0:#,##0.00}", double.Parse(_helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_ShippingCosts", "double")));
        inpOrderCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpOrderCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_OrderCosts", "double")));
        //inpCurrencyRate.Text = _helper.GetSingleData(int.Parse(valueCurrencyId.Text), "Currency", "currency_ConversionRate", "float");
        //inpShippingCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_ShippingCosts", "double");
        valueMinShippingCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_MinShippingCosts", "double");
        //inpOrderCosts.Text = _helper.GetSingleData(int.Parse(valueSupplierId.Text), "Supplier", "supplier_OrderCosts", "double");
        //inpCurrencyRate.Text = string.Format("{0:#,####0.0000}", double.Parse(inpCurrencyRate.Text));
        //inpShippingCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpShippingCosts.Text));
        //inpOrderCosts.Text = string.Format("{0:#,##0.00}", double.Parse(inpOrderCosts.Text));
    }
    #endregion Selection changed: Combobox Supplier

    #region Selection changed: Combobox Product
    private void cboxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //If no product is selected do nothing
        //if (cboxProduct.Text == String.Empty) { return; }

        foreach (HelperOrder.Product item in e.AddedItems)
        {
            valueProductId.Text = item.ProductId.ToString();
            valueProductName.Text = item.ProductName.ToString();
        }

        var SupplierHasProduct = _helper.CheckProductForSupplier(int.Parse(valueSupplierId.Text), int.Parse(valueProductId.Text));

        // If there is no price entered, retrieve the default price from the database
        if (inpPrice.Text == string.Empty || inpPrice.Text == "0,00") 
        { 
            // Check if product is in table for this supplier, if not return value will be -1
            string _tmpPrice = _helper.GetSingleDataMultiSelect("ProductSupplier", "productSupplier_ProductPrice", "Double", "productSupplier_SupplierId", int.Parse(valueSupplierId.Text), "AND", "productSupplier_ProductId", int.Parse(valueProductId.Text));

            if (SupplierHasProduct == 0)
            {
                inpPrice.Text = _helper.GetSingleDataMultiSelect("ProductSupplier", "productSupplier_ProductPrice", "Double", "productSupplier_SupplierId", int.Parse(valueSupplierId.Text), "AND", "productSupplier_ProductId", int.Parse(valueProductId.Text));
            }
            else
            {
                inpPrice.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_Price", "Double");                
            }

            inpPrice.Text = string.Format("{0:#,##0.00}", double.Parse(inpPrice.Text));
        }

        if (valueProductId.Text != string.Empty)
        {
            valueCategoryId.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_CategoryId", "int");
            valueCategoryName.Text = _helper.GetSingleData(int.Parse(valueProductId.Text), "Product", "product_CategoryName", "string");
            if (valueCategoryName.Text != string.Empty) { cboxCategory.SelectedValuePath = valueCategoryName.Text; }
        }
    }
    #endregion

    #region Selection changed: Combobox Project
    private void cboxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //If no project is selected do nothing
        //if (cboxProject.Text == String.Empty) { return; }
        
        foreach (HelperOrder.Project item in e.AddedItems)
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

        foreach (HelperOrder.Category item in e.AddedItems)
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
        var OrderlineProductId = 0;
        var OrderlineProjectId = 0;
        var OrderlineCategoryId = 0;
        var OrderlineNumber = 0.00;
        var OrderlinePrice = 0.00;
        var OrderlineProductName = "";
        var OrderlineProjectName = "";
        var OrderlineCategoryName = "";

        // if values are entered but OrderlineId is missing this means values for a new project are already filled before creating a new record
        if (valueOrderlineId.Text == string.Empty)
        {
            if (valueProductId.Text != string.Empty) { OrderlineProductId = int.Parse(valueProductId.Text); }
            if (valueProjectId.Text != string.Empty) { OrderlineProjectId = int.Parse(valueProjectId.Text); }
            if (valueCategoryId.Text != string.Empty) { OrderlineCategoryId = int.Parse(valueCategoryId.Text); }
            if (valueProjectName.Text != string.Empty) { OrderlineProjectName = valueProjectName.Text; }
            if (valueProductName.Text != string.Empty) { OrderlineProductName = valueProductName.Text; }
            if (valueCategoryName.Text != string.Empty) { OrderlineCategoryName = valueCategoryName.Text; }
            if (inpNumber.Text == String.Empty) { OrderlineNumber = 0; } else { OrderlineNumber = double.Parse(inpNumber.Text); }
            if (inpPrice.Text == String.Empty) { OrderlinePrice = 0; } else { OrderlinePrice = double.Parse(inpPrice.Text); }
        }

        InitializeHelper();
        var result = _helper.InsertTblOrderline(OrderlineOrderId, OrderlineSupplierId, OrderlineProductId, OrderlineProductName, OrderlineProjectId, OrderlineProjectName, OrderlineCategoryId, OrderlineCategoryName, OrderlineNumber, OrderlinePrice);
        UpdateStatus("row", result);

        // Get data from database
        _dtSC = _helper.GetDataTblOrderline(int.Parse(valueOrderId.Text));

        // Populate data in datagrid from datatable
        OrderDetail_DataGrid.DataContext = _dtSC;
        //OrderDetail_DataGrid.SelectedItem = OrderDetail_DataGrid.Items.Count;
        SetFocusOnGrid(OrderDetail_DataGrid, OrderDetail_DataGrid.Items.Count - 1);

        // Make orderline related cells available
        //cboxOrderRowEditable.IsChecked = true;
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
        if(index != 0)
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
        result = _helper.UpdateTblOrderline(OrderlineOrderId, OrderlineSupplierId, OrderlineProductId, OrderlineProductName, OrderlineProjectId, OrderlineProjectName, OrderlineCategoryId, OrderlineCategoryName, OrderlineNumber, OrderlinePrice, OrderlineId);
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
        var SupplierName = "";
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;

        if (valueSupplierId.Text == String.Empty) { SupplierId = 0; } else { SupplierId = int.Parse(valueSupplierId.Text); }
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
        result = _helper.InsertTblOrder(SupplierId, SupplierName, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, OrderMemo);
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
        // On save order detail information has to be saved, but also orderline_SupplierId of existing orderrows

        int rowIndex = _currentDataGridIndex;
        var OrderId = 0;
        var SupplierId = 0;
        var SupplierName = "";
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;
         
        if (valueOrderId.Text != String.Empty) {OrderId = int.Parse(valueOrderId.Text);}
        if (valueSupplierId.Text != String.Empty) { SupplierId = int.Parse(valueSupplierId.Text); }
        if (valueSupplierName.Text != String.Empty) { SupplierName = valueSupplierName.Text; }
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
            result = _helper.UpdateTblOrder(OrderId, SupplierId, SupplierName, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, memo);
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

    }
    #endregion Click Delete button (on Order toolbar)
    
    #endregion Toolbar for Orders

    #region Insert new row in table: Supplyorder
    private void InsertOrderRow(int dgIndex)
    {
        //since the DataGrid DataContext is set to the DataTable, 
        //the DataTable is updated when data is modified in the DataGrid
        //get last row
        DataRow row = _dt.Rows[_dt.Rows.Count - 1];

        var SupplierId = 0;
        var SupplierName = "";
        var OrderNumber = "";
        var OrderDate = "";
        var CurrencySymbol = "";
        var CurrencyRate = 0.0000;
        var ShippingCosts = 0.00;
        var OrderCosts = 0.00;

        if (valueSupplierId.Text == String.Empty) { SupplierId = 0; } else { SupplierId = int.Parse(valueSupplierId.Text); }
        if (valueSupplierName.Text == String.Empty) { SupplierName = ""; } else { SupplierName = valueSupplierName.Text; }
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
        result = _helper.InsertTblOrder(SupplierId, SupplierName, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, memo);
        UpdateStatus("order", result);
    }


#endregion

    #region Update Order row
    private void UpdateOrderRow(int OrderId, int SupplierId, string SupplierName, string OrderNumber, string OrderDate, string CurrencySymbol, double CurrencyRate, double ShippingCosts, double OrderCosts, string OrderMemo)
    {
        //convert RTF to string
        string memo = GetRichTextFromFlowDocument(inpOrderMemo.Document);

        InitializeHelper();

        string result = string.Empty;
        result = _helper.UpdateTblOrder(OrderId, SupplierId, SupplierName, OrderNumber, OrderDate, CurrencySymbol, CurrencyRate, ShippingCosts, OrderCosts, memo);
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
