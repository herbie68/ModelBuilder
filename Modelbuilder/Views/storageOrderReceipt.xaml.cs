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
            if (_helperOrder == null)
            {
                _helperOrder = new HelperOrder(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
            }
            if (_helperGeneral == null)
            {
                _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
            }

            if (_helperReceipt == null)
            {
                _helperReceipt= new HelperReceipt(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
            }
        }
        #endregion InitializeHelper (connect to database)

        #region Get the Order data
        private void GetOrderData()
        {
            InitializeHelper();

            // Get data from database
            _dt = _helperReceipt.GetDataTblOrderOpen();

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
            _dtSC = _helperReceipt.GetDataTblOrderlineOpen(int.Parse(valueOrderId.Text));

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


        private void cboxStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // No action yet
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            // No action yet
        }

        private void ButtonApply(object sender, RoutedEventArgs e)
        {
            // No action yet
        }

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
            inpDeliveryDate.SelectedDate = DateTime.Now.Date;
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
}
