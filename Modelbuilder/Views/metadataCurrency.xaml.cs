using System.Text.RegularExpressions;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataCurrency.xaml
    /// </summary>
    public partial class metadataCurrency : Page
    {
        private readonly string DatabaseTable = "currency";

        public metadataCurrency()
        {
            InitializeComponent();
            DataContext = new CurrencyCodeViewModel();

            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            _ = new DataTable();

            DataTable dtCountryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            CurrencyCode_DataGrid.DataContext = dtCountryCodes;

            // Make sure the first row in the datagrid is selected
            CurrencyCode_DataGrid.SelectedIndex = 0;
            //CurrencyCode_DataGrid.Focus();
        }

        private void CurrencyCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedItem is not DataRowView Row_Selected)
            {
                return;
            }

            double _ConversionRate = double.Parse(Row_Selected["ConversionRate"].ToString());
            inpCurrencyId.Text = Row_Selected["Id"].ToString();
            inpCurrencyCode.Text = Row_Selected["Code"].ToString().ToUpper();
            inpCurrencyName.Text = Row_Selected["Name"].ToString();
            inpCurrencySymbol.Text = Row_Selected["Symbol"].ToString();
            inpCurrencyRate.Text = _ConversionRate.ToString("#,####0.0000");
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            if (inpCurrencyCode.Text != "")
            {
                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                //Database dbConnection = new Database();
                dbConnection.Connect();

                double tmpRate = double.Parse(inpCurrencyRate.Text);
                inpCurrencyRate.Text = tmpRate.ToString("n4");

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "Code = '" + inpCurrencyCode.Text.ToUpper() + "', " +
                    "Name = '" + inpCurrencyName.Text + "', " +
                    "Symbol = '" + inpCurrencySymbol.Text + "', " +
                    "ConversionRate = '" + inpCurrencyRate.Text.Replace(",", ".") + "' WHERE " +
                    "Id = " + inpCurrencyId.Text + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();
                DataTable dtCountryCodes = dbConnection.LoadMySqlData();

                // Load the data from the database into the datagrid
                CurrencyCode_DataGrid.DataContext = dtCountryCodes;

                // Make sure the eddited row in the datagrid is selected
                CurrencyCode_DataGrid.SelectedIndex = int.Parse(inpCurrencyId.Text) - 1;
                _ = CurrencyCode_DataGrid.Focus();
            }
        }

        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(Code,Name,Id, Symbol, ConversionRate) VALUES('*','',0, '', 0);";
            dbConnection.TableName = DatabaseTable;
            int ID = dbConnection.UpdateMySqlDataRecord();
            inpCurrencyId.Text = ID.ToString();
            DataTable dtCurrencyCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            CurrencyCode_DataGrid.DataContext = dtCurrencyCodes;
            CurrencyCode_DataGrid.SelectedIndex = dtCurrencyCodes.Rows.Count - 1;
            _ = CurrencyCode_DataGrid.Focus();
            inpCurrencyCode.Text = "";
        }

        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            // Get the selected row
            int row = int.Parse(CurrencyCode_DataGrid.SelectedIndex.ToString());

            int ID = int.Parse(inpCurrencyId.Text);
            dbConnection.SqlCommand = "DELETE FROM ";
            dbConnection.SqlCommandString = " WHERE Id = " + ID + ";";
            dbConnection.TableName = DatabaseTable;
            _ = dbConnection.UpdateMySqlDataRecord();
            _ = dbConnection.LoadMySqlData();

            DataTable dtCountryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            CurrencyCode_DataGrid.DataContext = dtCountryCodes;

            // Make sure the correct row in the datagrid is selected after deletion

            // if row is already ithe first row, it should be reselected
            if (row == 0)
            {
                CurrencyCode_DataGrid.SelectedIndex = 0;
            }
            else
            {
                CurrencyCode_DataGrid.SelectedIndex = row - 1;
            }

            _ = CurrencyCode_DataGrid.Focus();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }
    }
}