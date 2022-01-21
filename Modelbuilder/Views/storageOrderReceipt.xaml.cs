using MySqlX.XDevAPI.Common;

namespace Modelbuilder
{
    public partial class storageOrderReceipt : Page
    {
        private HelperGeneral _helperGeneral;
        private HelperOrder _helperOrder;
        private HelperReceipt _helperReceipt;
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
            if (_dt.Rows.Count != 1) { tmpStr = "s"; };
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
            if (_dtSC.Rows.Count != 1) { tmpStr = "s"; };
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

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            // No action yet
        }

        #region Button Apply receipt pressed
        private void ButtonApply(object sender, RoutedEventArgs e)
        {
            var (OrderId, OrderlineId, SupplierId, StorageId, SupplyOrderlineId, ProductId, LineClosed) = (0, 0, 0, 0, 0, 0, 0);
            var (ProductName, ReceivedDate) = ("", "");
            var (AmountReceived, AmountRest) = (0.00, 0.00);

            if (valueOrderId.Text == String.Empty) { OrderId = 0; } else { OrderId = int.Parse(valueOrderId.Text); }
            if (valueOrderlineId.Text == String.Empty) { OrderlineId = 0; } else { OrderlineId = int.Parse(valueOrderlineId.Text); }
            if (valueSupplierId.Text == String.Empty) { SupplierId = 0; } else { SupplierId = int.Parse(valueSupplierId.Text); }
            if (valueProductId.Text == String.Empty) { ProductId = 0; } else { ProductId = int.Parse(valueProductId.Text); }
            if (valueStorageId.Text == String.Empty) { StorageId = 0; } else { StorageId = int.Parse(valueStorageId.Text); }
            if (inpDeliveryDate.Text == String.Empty) { ReceivedDate = ""; } else { ReceivedDate = inpDeliveryDate.Text; }
            if (inpProductName.Text == String.Empty) { ProductName = ""; } else { ProductName = inpProductName.Text; }
            if (inpNumber.Text == String.Empty) { AmountReceived = 0.00; } else { AmountReceived = double.Parse(inpNumber.Text); }

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
            var _tempRecordCount = _helperGeneral.CheckForRecords(HelperGeneral.DbStockTable, new string[2, 3]
            {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId, valueProductId.Text},
                {HelperGeneral.DbStockTableFieldNameStorageId, HelperGeneral.DbStockTableFieldTypeStorageId, valueStorageId.Text} }); 

            if (_tempRecordCount > 0)
            {
                // Get Stock Id
                var StockId = int.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbStockView, new string[2, 3]
                {   {HelperGeneral.DbStockViewFieldNameProductId, HelperGeneral.DbStockViewFieldTypeProductId, valueProductId.Text},
                    {HelperGeneral.DbStockViewFieldNameStorageId, HelperGeneral.DbStockViewFieldTypeStorageId, valueStorageId.Text} }, new string[1, 3]
                {   {HelperGeneral.DbStockViewFieldNameId, HelperGeneral.DbStockViewFieldTypeId, ""} }));

                // Get Currunt Stock Amount
                var Amount = double.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbStockTable, new string[2, 3]
                {   {HelperGeneral.DbStockTableFieldNameProductId,  HelperGeneral.DbStockTableFieldNameProductId, valueProductId.Text},
                    {HelperGeneral.DbStockTableFieldNameStorageId, HelperGeneral.DbStockTableFieldTypeStorageId, valueStorageId.Text } }, new string[1, 3]
                {   { HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, "" }}));
                
                // Calculate new Stock Amount
                var AmountNew = Amount + double.Parse(inpNumber.Text);

                // Update stock record with new Soch Amount
                //_helperReceipt.UpdateFieldInTable(HelperGeneral.DbStockTable, "Id", StockId.ToString(), "int", "product_Id", valueProductId.Text, "int", "storage_Id", valueStorageId.Text, "int", "Amount", AmountNew.ToString(), "double");
                _helperGeneral.UpdateFieldInTable(HelperGeneral.DbStockTable, new string[1, 3]
                {   {HelperGeneral.DbStockTableFieldNameId, HelperGeneral.DbStockTableFieldTypeId , StockId.ToString()} }, new string[3, 3]
                {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId, valueProductId.Text},
                    {HelperGeneral.DbStockTableFieldNameStorageId, HelperGeneral.DbStockTableFieldTypeStorageId, valueStorageId.Text},
                    {HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, AmountNew.ToString()} });
            }
            else
            {
                _helperGeneral.InsertInTable(HelperGeneral.DbStockTable, new string[3, 3]
                {   {HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId , valueProductId.Text},
                    {HelperGeneral.DbStockTableFieldNameStorageId, HelperGeneral.DbStockTableFieldTypeStorageId, valueStorageId.Text },
                    {HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, inpNumber.Text } });
            }

            // Add record to the StockLog table
            _helperGeneral.InsertInTable(HelperGeneral.DbStocklogTable, new string[6, 3]
            {   {HelperGeneral.DbStocklogTableFieldNameProductId, HelperGeneral.DbStocklogTableFieldTypeProductId , valueProductId.Text},
                {HelperGeneral.DbStocklogTableFieldNameStorageId, HelperGeneral.DbStocklogTableFieldTypeStorageId, valueStorageId.Text },
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
            valueStorageId.Text = Row_Selected["Storage_Id"].ToString();
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
}
