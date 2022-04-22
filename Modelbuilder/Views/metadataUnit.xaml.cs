namespace Modelbuilder.Views;

/// <summary>
/// Interaction logic for metadataUnit.xaml
/// </summary>
public partial class metadataUnit : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt;
    private int _dbRowCount;
    private int _currentDataGridIndex;

    public metadataUnit()
    {
        InitializeComponent();
        InitializeHelper();
        GetData();
    }

    #region Get the Data
    private void GetData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helperGeneral.GetData(HelperGeneral.DbUnitTable);

        // Populate data in datagrid from datatable
        UnitCode_DataGrid.DataContext = _dt;

        // Set value
        _dbRowCount = _dt.Rows.Count;
        RecordsCount.Text = _dbRowCount.ToString();
    }
    #endregion

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
    }
    #endregion

    #region Selection changed in Datagrid
    private void UnitCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }
        
        _currentDataGridIndex = dg.SelectedIndex;

        valueId.Text = Row_Selected["Id"].ToString();
        valueName.Text = Row_Selected["Name"].ToString();
    }
    #endregion

    #region User changed content of the selected cell, save row to database
    private void UserChangedCell(object sender, EventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set value
        _currentDataGridIndex = dg.SelectedIndex;
        if (Row_Selected["Id"].ToString() == valueId.Text && Row_Selected["Id"].ToString()!="")
        {
            if (Row_Selected["Name"].ToString() != valueName.Text)
            {
                _helperGeneral.UpdateFieldInTable(HelperGeneral.DbUnitTable, new string[1, 3]
                {   {HelperGeneral.DbUnitTableFieldNameUnitId, HelperGeneral.DbUnitTableFieldTypeUnitId, valueId.Text } }, new string[1, 3]
                {   {HelperGeneral.DbUnitTableFieldNameUnitName, HelperGeneral.DbUnitTableFieldTypeUnitName, Row_Selected["Name"].ToString() }  });
                GetData();
            }
        }
        else if (Row_Selected["Id"].ToString() == "" && Row_Selected["Name"].ToString()!="")
        {
            _helperGeneral.InsertInTable(HelperGeneral.DbUnitTable, new string[1, 3]
            {   {HelperGeneral.DbUnitTableFieldNameUnitName, HelperGeneral.DbUnitTableFieldTypeUnitName, Row_Selected["Name"].ToString() }  });
            GetData();
        }
    }
    #endregion

    #region User pressed a button on selected row, if button is delete button: delete row in database
    private void HandlePressedButton(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete)
        {
            int rowIndex = _currentDataGridIndex;

            _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbUnitTable, new string[1, 3]
            {   {HelperGeneral.DbUnitTableFieldNameUnitId, HelperGeneral.DbUnitTableFieldTypeUnitId, valueId.Text } });
            GetData();
        }
    }
    #endregion
}
