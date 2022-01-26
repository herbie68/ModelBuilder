namespace Modelbuilder.Views;
public partial class storageStockChanges : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt;
    private int _dbRowCount;
    public int _currentDataGridIndex;

    public storageStockChanges()
    {
        InitializeComponent();
        InitializeHelper();
        GetData();
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

    #region Get the Stock data
    private void GetData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helperGeneral.GetData(HelperGeneral.DbStockView);

        // Populate data in datagrid from datatable
        StockStorage_DataGrid.DataContext = _dt;

        // Set value
        _dbRowCount = _dt.Rows.Count;
        RecordCountStockRows.Text = _dbRowCount.ToString();

        string tmpStr = "";
        //update status
        if (_dt.Rows.Count != 1) { tmpStr = "en"; };
        string msg = "Status: " + _dt.Rows.Count + " " + Languages.Cultures.Status_Stock_Product + tmpStr + " " + Languages.Cultures.Status_Read + ".";

        UpdateStatus(msg);
    }
    #endregion Get the Stock data

    #region Datagrid selection changed
    private void StockStorage_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        if (valueRow.Text != "")
        {
            var _NewAmount = Convert.ToDouble((_helperGeneral.GetCell(dg, int.Parse(valueRow.Text), 6).Content as TextBlock).Text);
            if (_NewAmount != double.Parse(valueAmount.Text))
            {
                // Update stock record with new Stock Amount
                _helperGeneral.UpdateFieldInTable(HelperGeneral.DbStockTable, new string[1, 3]
                {   {HelperGeneral.DbStockTableFieldNameId, HelperGeneral.DbStockTableFieldTypeId , valueId.Text} }, new string[1, 3]
                {   {HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, _NewAmount.ToString()} });
                string StatusMessage = Languages.Cultures.Status_Stock_Amount +  " \"" + valueProductName.Text + "\" " + Languages.Cultures.Status_Stock_Changed + ": " + valueAmount.Text + "=>" + _NewAmount.ToString();
                UpdateStatus(StatusMessage);
            }
        }

        //set current row values
        _currentDataGridIndex = dg.SelectedIndex;
        valueId.Text = Row_Selected["Id"].ToString();
        valueAmount.Text = Row_Selected["Amount"].ToString();
        valueProductName.Text = Row_Selected["ProductName"].ToString();
        valueRow.Text = dg.SelectedIndex.ToString();
    }
    #endregion Datagrid selection changed

    #region Datagrid Cell content changed
    private void StockStorage_DataGrid_CellContentChanged(object sender, DataGridCellEditEndingEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        if (valueRow.Text != "")
        {
            var _NewAmount = Convert.ToDouble((_helperGeneral.GetCell(dg, int.Parse(valueRow.Text), 6).Content as TextBox).Text);
            if (_NewAmount != double.Parse(valueAmount.Text))
            {
                // Update stock record with new Stock Amount
                _helperGeneral.UpdateFieldInTable(HelperGeneral.DbStockTable, new string[1, 3]
                {   {HelperGeneral.DbStockTableFieldNameId, HelperGeneral.DbStockTableFieldTypeId , valueId.Text} }, new string[1, 3]
                {   {HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, _NewAmount.ToString()} });
                string StatusMessage = Languages.Cultures.Status_Stock_Amount + " \"" + valueProductName.Text + "\" " + Languages.Cultures.Status_Stock_Changed + ": " + valueAmount.Text + "=>" + _NewAmount.ToString();
                UpdateStatus(StatusMessage);
            }
        }

        //set current row values
        _currentDataGridIndex = dg.SelectedIndex;
        valueId.Text = Row_Selected["Id"].ToString();
        valueAmount.Text = Row_Selected["Amount"].ToString();
        valueProductName.Text = Row_Selected["ProductName"].ToString();
        valueRow.Text = dg.SelectedIndex.ToString();
    }
    #endregion Datagrid Cell content changed

    #region Update status
    private void UpdateStatus(string msg)
    {
        if (!String.IsNullOrEmpty(msg))
        {
            if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
            {
                textBoxStatusStockRows.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
            }
            else
            {
                textBoxStatusStockRows.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
            }
        }
    }
    #endregion Update status

}
