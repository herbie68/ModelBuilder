namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataCountry.xaml
    /// </summary>
    public partial class metadataCountry : Page
    {
        private readonly string DatabaseTable = "country";
        private readonly string DatabaseCurrencyTable = "currency";

        public metadataCountry()
        {
            InitializeComponent();
            DataContext = new CountryCodeViewModel();

            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            _ = new DataTable();

            DataTable dtCountryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            CountryCode_DataGrid.DataContext = dtCountryCodes;

            // Make sure the first row in the datagrid is selected
            CountryCode_DataGrid.SelectedIndex = 0;
            CountryCode_DataGrid.Focus();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CountryCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedItem is not DataRowView Row_Selected)
            {
                return;
            }
            inpCountryId.Text = Row_Selected["Id"].ToString();
            inpCountryCode.Text = Row_Selected["Code"].ToString();
            inpCountryName.Text = Row_Selected["Name"].ToString();
            inpCountryCurrencyId.Text = Row_Selected["Defaultcurrency_Id"].ToString();
            cboxCountryCurrency.Text = Row_Selected["Defaultcurrency_Symbol"].ToString();
        }

        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            if (inpCountryCode.Text != "")
            {
                Database dbCurrencyConnection = new()
                {
                    TableName = DatabaseCurrencyTable
                };

                dbCurrencyConnection.Connect();
                dbCurrencyConnection.SqlCommandString = "Id";
                dbCurrencyConnection.SqlWhereString = "Symbol = '" + cboxCountryCurrency.Text + "'";

                long currencyId = dbCurrencyConnection.RetrieveSpecificIdFromMySqlData();
                inpCountryCurrencyId.Text = currencyId.ToString();

                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                //Database dbConnection = new Database();
                dbConnection.Connect();

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "Code = '" + inpCountryCode.Text + "', " +
                    "Name = '" + inpCountryName.Text + "', " +
                    "Defaultcurrency_Symbol = '" + cboxCountryCurrency.Text + "', " +
                    "Defaultcurrency_Id = '" + currencyId + "' WHERE " +
                    "Id = " + inpCountryId.Text + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();
                DataTable dtCountryCodes = dbConnection.LoadMySqlData();

                // Load the data from the database into the datagrid
                CountryCode_DataGrid.DataContext = dtCountryCodes;

                // Make sure the eddited row in the datagrid is selected
                CountryCode_DataGrid.SelectedIndex = int.Parse(inpCountryId.Text) - 1;
                CountryCode_DataGrid.Focus();
            }
        }

        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(Code,Name,Defaultcurrency_Id, Defaultcurrency_Symbol) VALUES('*','',0, '');";
            dbConnection.TableName = DatabaseTable;
            int ID = dbConnection.UpdateMySqlDataRecord();
            inpCountryId.Text = ID.ToString();
            DataTable dtCountryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            CountryCode_DataGrid.DataContext = dtCountryCodes;
            CountryCode_DataGrid.SelectedIndex = dtCountryCodes.Rows.Count - 1;
            CountryCode_DataGrid.Focus();
            inpCountryCode.Text = "";
        }

        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            // Get the selected row
            int row = int.Parse(CountryCode_DataGrid.SelectedIndex.ToString());

            int ID = int.Parse(inpCountryId.Text);
            dbConnection.SqlCommand = "DELETE FROM ";
            dbConnection.SqlCommandString = " WHERE Id = " + ID + ";";
            dbConnection.TableName = DatabaseTable;
            _ = dbConnection.UpdateMySqlDataRecord();
            _ = dbConnection.LoadMySqlData();

            DataTable dtCountryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            CountryCode_DataGrid.DataContext = dtCountryCodes;

            // Make sure the correct row in the datagrid is selected after deletion

            // if row is already ithe first row, it should be reselected
            if (row == 0)
            {
                CountryCode_DataGrid.SelectedIndex = 0;
            }
            else
            {
                CountryCode_DataGrid.SelectedIndex = row - 1;
            }

            CountryCode_DataGrid.Focus();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }
    }
}