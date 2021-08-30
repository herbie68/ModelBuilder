using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            float _ConversionRate = float.Parse(Row_Selected["currency_ConversionRate"].ToString());
            inpCurrencyId.Text = Row_Selected["currency_Id"].ToString();
            inpCurrencyCode.Text = Row_Selected["currency_Code"].ToString().ToUpper();
            inpCurrencyName.Text = Row_Selected["currency_Name"].ToString();
            inpCurrencySymbol.Text = Row_Selected["currency_Symbol"].ToString();
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

                float tmpRate = float.Parse(inpCurrencyRate.Text);
                inpCurrencyRate.Text = tmpRate.ToString("n4");

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "currency_Code = '" + inpCurrencyCode.Text.ToUpper() + "', " +
                    "currency_Name = '" + inpCurrencyName.Text + "', " +
                    "currency_Symbol = '" + inpCurrencySymbol.Text + "', " +
                    "currency_ConversionRate = '" + inpCurrencyRate.Text.Replace(",", ".") + "' WHERE " +
                    "currency_Id = " + inpCurrencyId.Text + ";";

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
            dbConnection.SqlCommandString = "(currency_Code,currency_Name,currency_Id, currency_Symbol, currency_ConversionRate) VALUES('*','',0, '', 0);";
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
            dbConnection.SqlCommandString = " WHERE currency_Id = " + ID + ";";
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