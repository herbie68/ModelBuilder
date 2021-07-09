using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            CurrencyCode_DataGrid.Focus();

        }
        private void CurrencyCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedItem is not DataRowView Row_Selected)
            {
                return;
            }

            LocalCurrencyData filldata = new LocalCurrencyData()
            {
                localCurrencyId = int.Parse(Row_Selected["currency_Id"].ToString()),
                localCurrencyCode = Row_Selected["currency_Code"].ToString().ToUpper(),
                localCurrencyName= Row_Selected["currency_Name"].ToString(),
                localCurrencySymbol = Row_Selected["currency_Symbol"].ToString(),
                localCurrencyRate = float.Parse(Row_Selected["currency_ConversionRate"].ToString().Replace(",","."))
        };

            inpCurrencyId.Text = Row_Selected["currency_Id"].ToString();
            inpCurrencyCode.Text = Row_Selected["currency_Code"].ToString().ToUpper();
            inpCurrencyName.Text = Row_Selected["currency_Name"].ToString();
            inpCurrencySymbol.Text = Row_Selected["currency_Symbol"].ToString();
            inpCurrencyRate.Text = Row_Selected["currency_ConversionRate"].ToString().Replace(",", ".");
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

                string tmpCurrencyRate = inpCurrencyRate.Text.Replace(",", ".");
                if (tmpCurrencyRate.Contains("."))
                {
                    // Make ure stored number of decimals is 4
                    // Count No of 0 to be added to the current decimals in the CurrencyRate

                    // Do Nothing if NoOfZero's = 0 and trim the string if NoOfZeros <0
                    int noOfZero = 4 - (tmpCurrencyRate.Length - (tmpCurrencyRate.IndexOf('.') + 1));
                    
                    tmpCurrencyRate += string.Concat(Enumerable.Repeat("0", noOfZero));
                }
                else
                {
                    tmpCurrencyRate += ".0000";
                }
                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "currency_Code = '" + inpCurrencyCode.Text.ToUpper() + "', " +
                    "currency_Name = '" + inpCurrencyName.Text + "', " +
                    "currency_Symbol = '" + inpCurrencySymbol.Text + "', " +
                    "currency_Id = '" + inpCurrencyId + "' WHERE " +
                    "currency_ConversionRate = " + inpCurrencyRate.Text.Replace(",",".") + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();
                DataTable dtCountryCodes = dbConnection.LoadMySqlData();

                // Load the data from the database into the datagrid
                CurrencyCode_DataGrid.DataContext = dtCountryCodes;

                // Make sure the eddited row in the datagrid is selected
                CurrencyCode_DataGrid.SelectedIndex = int.Parse(inpCurrencyId.Text) - 1;
                CurrencyCode_DataGrid.Focus();
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
            CurrencyCode_DataGrid.Focus();
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

            CurrencyCode_DataGrid.Focus();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

    }
    public class LocalCurrencyData
    {
        public int localCurrencyId { get; set; }
        public string localCurrencyCode { get; set; }
        public string localCurrencyName { get; set; }
        public string localCurrencySymbol { get; set; }
        public float localCurrencyRate { get; set; }
    }
}
