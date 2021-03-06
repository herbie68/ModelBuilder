namespace Modelbuilder;
public partial class storageOrderReceipt : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt, _dtSC;
    private int _dbRowCount, _dbRowCountSC;
    private int _currentDataGridIndex;

    public storageOrderReceipt()
    {
        var StorageList = new List<HelperGeneral.Storage>();

        InitializeComponent();
        InitializeHelper();

        // Fill the dropdown
        cboxStorage.ItemsSource = _helperGeneral.GetStorageList(StorageList);

        GetOrderData();
    }

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
    }
    #endregion InitializeHelper (connect to database)

    #region Get the Order data
    private void GetOrderData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helperGeneral.GetData(HelperGeneral.DbOpenOrderView);

        // Populate data in datagrid from datatable
        OrderCode_DataGrid.DataContext = _dt;

        // Set value
        _dbRowCount = _dt.Rows.Count;
        RecordCountOrderRows.Text = _dbRowCount.ToString();

        string tmpStr = "";
        //update status
        if (_dt.Rows.Count != 1) { tmpStr = "s"; }
        string msg = "Status: " + _dt.Rows.Count + " Bestelling" + tmpStr + " ingelezen.";

        UpdateStatus("order", msg);
    }
    #endregion Get the Order data

    #region Get the Orderrow data
    private void GetOrderRowData()
    {
        InitializeHelper();

        // Get data from database
        _dtSC = _helperGeneral.GetData(HelperGeneral.DbOpenOrderLineView, HelperGeneral.DbOpenOrderLineFieldNameSupplyOrderId, int.Parse(valueOrderId.Text));

        // Populate data in datagrid from datatable
        OrderlineCode_DataGrid.DataContext = _dtSC;
        cboxReceiptRowEditable.IsChecked = true;

        // Set value
        _dbRowCountSC = _dtSC.Rows.Count;
        RecordsCountOrderRecRows.Text = _dbRowCountSC.ToString();

        string tmpStr = "";
        //update status
        if (_dtSC.Rows.Count != 1) { tmpStr = "s"; }
        string msg = "Status: " + _dtSC.Rows.Count + " Bestelregels" + tmpStr + " ingelezen.";

        UpdateStatus("row", msg);
    }
    #endregion Get the Orderrow data

    #region Selection changed of Storage Combobox
    private void cboxStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //Select the saved Storage location in the combobox by default
        foreach (HelperGeneral.Storage storage in cboxStorage.Items)
        {
            if (storage.StorageName == cboxStorage.Text)
            {
                cboxStorage.SelectedItem = storage;
                break;
            }
        }
    }
    #endregion Selection changed of Storage Combobox

    #region Button Cancel Orderrow receipt pressed
    private void ButtonCancel(object sender, RoutedEventArgs e)
    {
        // If No button is pressed, do nothing otherwise take all actions to remove OpenAmount on orderling
        if (MessageBox.Show(Languages.Cultures.Receipt_ConfirmCancel_Message, Languages.Cultures.Receipt_ConfirmCancel_Title, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            // Get Total Order Amount
            var Amount = double.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbOrderLineTable, new string[1, 3]
            {   {HelperGeneral.DbOrderLineFieldNameId,  HelperGeneral.DbOrderLineFieldTypeId, valueOrderlineId.Text} }, new string[1, 3]
            {   { HelperGeneral.DbOrderLineFieldNameAmount, HelperGeneral.DbOrderLineFieldTypeAmount, "" }}));

            // Get OpenAmount
            var OpenAmount = double.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbOrderLineTable, new string[1, 3]
            {   {HelperGeneral.DbOrderLineFieldNameId,  HelperGeneral.DbOrderLineFieldTypeId, valueOrderlineId.Text} }, new string[1, 3]
            {   { HelperGeneral.DbOrderLineFieldNameOpenAmount, HelperGeneral.DbOrderLineFieldTypeOpenAmount, "" }}));

            if (Amount == OpenAmount)
            {
                // Open amount is equal to ordered amount, so no excisting receipts, orderline can be deleted
                _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbOrderLineTable, new string[1, 3] { { HelperGeneral.DbOrderLineFieldNameId, HelperGeneral.DbOrderLineFieldTypeId, valueOrderlineId.Text } });
            }
            else
            {
                // Set OpenAmount to 0, Modify Originaly Ordered Amount and Orderline to closed (do not modify the closed date)
                _helperGeneral.UpdateFieldInTable(HelperGeneral.DbOrderLineTable, new string[1, 3]
                {   {HelperGeneral.DbOrderLineFieldNameId, HelperGeneral.DbOrderLineFieldTypeId , valueOrderlineId.Text} }, new string[3, 3]
                {   {HelperGeneral.DbOrderLineFieldNameOpenAmount, HelperGeneral.DbOrderLineFieldTypeOpenAmount, "0"},
                        {HelperGeneral.DbOrderLineFieldNameAmount, HelperGeneral.DbOrderLineFieldTypeAmount, (Amount - OpenAmount).ToString()},
                        {HelperGeneral.DbOrderLineFieldNameClosed, HelperGeneral.DbOrderLineFieldTypeClosed, "1"} });
            }

            // check if order has open orderlines No: Close Order
            var _tempOpenOrderLinesCount = _helperGeneral.CheckForRecords(HelperGeneral.DbOrderLineTable, new string[2, 3]
            {   {HelperGeneral.DbOrderLineFieldNameOrderId, HelperGeneral.DbOrderLineFieldTypeOrderId, valueOrderId.Text},
                {HelperGeneral.DbOrderLineFieldNameClosed, HelperGeneral.DbOrderLineFieldTypeClosed, "0" } });

            if (_tempOpenOrderLinesCount == 0)
            {
                // No open Orderlines check if order has closed orderlines
                var _tempClosedOrderLinesCount = _helperGeneral.CheckForRecords(HelperGeneral.DbOrderLineTable, new string[2, 3]
                {   {HelperGeneral.DbOrderLineFieldNameOrderId, HelperGeneral.DbOrderLineFieldTypeOrderId, valueOrderId.Text},
                        {HelperGeneral.DbOrderLineFieldNameClosed, HelperGeneral.DbOrderLineFieldTypeClosed, "1" } });

                if (_tempClosedOrderLinesCount == 0)
                {
                    // No open orderlines and no already closed orderlines, so order completely canceled, can be deleted
                    _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbOrderTable, new string[1, 3] { { HelperGeneral.DbOrderTableFieldNameId, HelperGeneral.DbOrderTableFieldTypeId, valueOrderId.Text } });
                }
                else
                {
                    // All other orderlines are closed, close order. Closed Date will be the Closed Date of the last Orderlne received
                    // Get the last receie date for the order
                    //SELECT MAX(ClosedDate) FROM supplyorderline WHERE supplyOrder_Id = 2
                    var ClosedDate = _helperGeneral.GetMaxValueFromTable(HelperGeneral.DbOrderLineTable, new string[1, 3]
                    {   {HelperGeneral.DbOrderLineFieldNameOrderId,  HelperGeneral.DbOrderLineFieldTypeOrderId, valueOrderId.Text} }, new string[1, 3]
                    {   { HelperGeneral.DbOrderLineFieldNameClosedDate, HelperGeneral.DbOrderLineFieldTypeClosedDate, "" }});


                    _helperGeneral.UpdateFieldInTable(HelperGeneral.DbOrderTable, new string[1, 3]
                    {   {HelperGeneral.DbOrderTableFieldNameId, HelperGeneral.DbOrderTableFieldTypeId , valueOrderId.Text} }, new string[2, 3]
                    {   {HelperGeneral.DbOrderTableFieldNameClosed, HelperGeneral.DbOrderTableFieldTypeClosed, "1"},
                        {HelperGeneral.DbOrderTableFieldNameClosedDate, HelperGeneral.DbOrderTableFieldTypeClosedDate, ClosedDate}});
                }
            }

            GetOrderRowData();
            inpDeliveryDate.Text = String.Empty;
            inpProductName.Text = String.Empty;
            inpNumber.Text = String.Empty;
            cboxStorage.SelectedIndex = 1;
            GetOrderData();
        }
    }
    #endregion Button Cancel Orderrow receipt pressed

    #region Button Apply receipt pressed
    private void ButtonApply(object sender, RoutedEventArgs e)
    {
        var LineClosed = 0;
        var (ProductName, ReceivedDate) = ("", "");
        var (AmountReceived, AmountRest) = (0.00, 0.00);

        AmountRest = double.Parse(valueOpenAmount.Text) - double.Parse(inpNumber.Text);

        if (AmountRest <= 0)
        {
            AmountRest = 0.00;
            LineClosed = 1;
        }

        _helperGeneral.UpdateFieldInTable(HelperGeneral.DbOrderLineTable, new string[1, 3]
        {   {HelperGeneral.DbOrderLineFieldNameId, HelperGeneral.DbOrderLineFieldTypeId , valueOrderlineId.Text} }, new string[3, 3]
        {   {HelperGeneral.DbOrderLineFieldNameOpenAmount, HelperGeneral.DbOrderLineFieldTypeOpenAmount, AmountRest.ToString()},
                {HelperGeneral.DbOrderLineFieldNameClosed, HelperGeneral.DbOrderLineFieldTypeClosed, LineClosed.ToString()},
                {HelperGeneral.DbOrderLineFieldNameClosedDate, HelperGeneral.DbOrderLineFieldTypeClosedDate, inpDeliveryDate.Text} });

        // Check if Product Id excists in stock table if yes, Amount has to be changed, if no record has to be added
        var _tempRecordCount = _helperGeneral.CheckForRecords ( HelperGeneral.DbStockTable, new string[1, 3]
        {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId, valueProductId.Text} } );

        if (_tempRecordCount > 0)
        {
            // Get Stock Id
            var StockId = int.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbStockView, new string[1, 3]
            {   {HelperGeneral.DbStockViewFieldNameProductId, HelperGeneral.DbStockViewFieldTypeProductId, valueProductId.Text} }, new string[1, 3]
            {   {HelperGeneral.DbStockViewFieldNameId, HelperGeneral.DbStockViewFieldTypeId, ""} }));

            // Get Currunt Stock Amount
            var Amount = double.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbStockTable, new string[1, 3]
            {   {HelperGeneral.DbStockTableFieldNameProductId,  HelperGeneral.DbStockTableFieldNameProductId, valueProductId.Text} }, new string[1, 3]
            {   { HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, "" }}));

            // Calculate new Stock Amount
            var AmountNew = Amount + double.Parse(inpNumber.Text);

            // Update stock record with new Stock Amount
            _helperGeneral.UpdateFieldInTable(HelperGeneral.DbStockTable, new string[1, 3]
            {   {HelperGeneral.DbStockTableFieldNameId, HelperGeneral.DbStockTableFieldTypeId , StockId.ToString()} }, new string[2, 3]
            {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId, valueProductId.Text},
                    {HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, AmountNew.ToString()} });
        }
        else
        {
            _helperGeneral.InsertInTable(HelperGeneral.DbStockTable, new string[2, 3]
            {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId , valueProductId.Text},
                    {HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, inpNumber.Text } });
        }

        // Add record to the StockLog table
        _helperGeneral.InsertInTable(HelperGeneral.DbStocklogTable, new string[5, 3]
        {   {HelperGeneral.DbStocklogTableFieldNameProductId, HelperGeneral.DbStocklogTableFieldTypeProductId , valueProductId.Text},
                {HelperGeneral.DbStocklogTableFieldNameSupplyOrderId, HelperGeneral.DbStocklogTableFieldTypeSupplyOrderId, valueOrderId.Text},
                {HelperGeneral.DbStocklogTableFieldNameSupplyOrderlineId, HelperGeneral.DbStocklogTableFieldTypeSupplyOrderlineId, valueOrderlineId.Text },
                {HelperGeneral.DbStocklogTableFieldNameAmountReceived, HelperGeneral.DbStocklogTableFieldTypeAmountReceived, inpNumber.Text },
                {HelperGeneral.DbStocklogTableFieldNameDate, HelperGeneral.DbStocklogTableFieldTypeDate, inpDeliveryDate.Text } });

        // Finaly check if All orderlines are closed If Yes close the Order
        var _tempOpenOrderLinesCount = _helperGeneral.CheckForRecords(HelperGeneral.DbOrderLineTable, new string[2, 3]
        {   {HelperGeneral.DbOrderLineFieldNameOrderId, HelperGeneral.DbOrderLineFieldTypeOrderId, valueOrderId.Text},
                {HelperGeneral.DbOrderLineFieldNameClosed, HelperGeneral.DbOrderLineFieldTypeClosed, "0" } });

        if (_tempOpenOrderLinesCount == 0)
        {
            // No open orderlines left for this order
            _helperGeneral.UpdateFieldInTable(HelperGeneral.DbOrderTable, new string[1, 3]
            {   {HelperGeneral.DbOrderTableFieldNameId, HelperGeneral.DbOrderTableFieldTypeId , valueOrderId.Text} }, new string[2, 3]
            {   {HelperGeneral.DbOrderTableFieldNameClosed, HelperGeneral.DbOrderTableFieldTypeClosed, "1"},
                    {HelperGeneral.DbOrderTableFieldNameClosedDate, HelperGeneral.DbOrderTableFieldTypeClosedDate, inpDeliveryDate.Text} });
            GetOrderData();
        }
        GetOrderRowData();
        inpDeliveryDate.Text = String.Empty;
        inpProductName.Text = String.Empty;
        inpNumber.Text = String.Empty;
        cboxStorage.SelectedIndex = 1;
    }
    #endregion Button Apply receipt pressed

    #region Datagrid Selection Changed: Order
    private void OrderCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridIndex = dg.SelectedIndex;

        valueOrderId.Text = Row_Selected["Id"].ToString();
        valueSupplierId.Text = Row_Selected["Supplier_Id"].ToString();

        GetOrderRowData();
    }
    #endregion Datagrid Selection Changed: Order

    #region Datagrid Selection Changed: Orderline
    private void OrderlineCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridIndex = dg.SelectedIndex;
        inpProductName.Text = Row_Selected["ProductName"].ToString();
        inpNumber.Text = Row_Selected["OpenAmount"].ToString();
        valueOpenAmount.Text = Row_Selected["OpenAmount"].ToString();
        inpDeliveryDate.SelectedDate = DateTime.Now.Date;
        displUnit.Text = Row_Selected["UnitName"].ToString();
        valueOrderlineId.Text = Row_Selected["Id"].ToString();
        valueProductId.Text = Row_Selected["Product_Id"].ToString();

        #region Select the saved Storage location in the Storage combobox by default
        string _tempStorage = Row_Selected["StorageName"].ToString();
        cboxStorage.Text = _tempStorage;

        //Select the saved Storage location in the combobox by default
        foreach (HelperGeneral.Storage storage in cboxStorage.Items)
        {
            if (storage.StorageName == _tempStorage)
            {
                cboxStorage.SelectedItem = storage;
                break;
            }
        }
        #endregion Select the saved Storage location in the Storage combobox by default


    }
    #endregion Datagrid Selection Changed: Orderline

    #region Update status
    private void UpdateStatus(string area, string msg)
    {
        if (!String.IsNullOrEmpty(msg))
        {
            if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
            {
                if (area == "order")
                { textBoxStatusOrderRows.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                else { textBoxStatusOrderRecRows.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                Debug.WriteLine(String.Format("{0} - Status: {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
            }
            else
            {
                if (area == "order")
                { textBoxStatusOrderRows.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                else { textBoxStatusOrderRecRows.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                Debug.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
            }
        }
    }
    #endregion Update status

}
