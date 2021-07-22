using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Interaction logic for metadataSupplier.xaml
    /// </summary>
    public partial class metadataSupplier : Page
    {
        private readonly string DatabaseTable = "supplier";
        private readonly string DatabaseCountryTable = "country";
        private readonly string DatabaseCurrencyTable = "currency";
        public metadataSupplier()
        {
            InitializeComponent();
            DataContext = new SupplierCodeViewModel();

            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            _ = new DataTable();

            DataTable dtSupplierCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            SupplierCode_DataGrid.DataContext = dtSupplierCodes;

            // Make sure the first row in the datagrid is selected
            SupplierCode_DataGrid.SelectedIndex = 0;
            SupplierCode_DataGrid.Focus();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedItem is not DataRowView Row_Selected)
            {
                return;
            }

            valueSupplierId.Text = Row_Selected["supplier_Id"].ToString();
            valueCountryId.Text = Row_Selected["country_Id"].ToString();
            valueCountryCode.Text = Row_Selected["country_Code"].ToString();
            valueCurrencyId.Text = Row_Selected["currency_Id"].ToString();
            valueCurrencyCode.Text = Row_Selected["currency_Code"].ToString();
            inpSupplierCode.Text = Row_Selected["supplier_Code"].ToString();
            inpSupplierName.Text = Row_Selected["supplier_Name"].ToString();
            inpSupplierAddress1.Text = Row_Selected["supplier_Address1"].ToString();
            inpSupplierAddress2.Text = Row_Selected["supplier_Address2"].ToString();
            inpSupplierZip.Text = Row_Selected["supplier_Zip"].ToString();
            inpSupplierCity.Text = Row_Selected["supplier_City"].ToString();
            cboxSupplierCountry.Text = Row_Selected["country_Name"].ToString();
            cboxSupplierCurrency.Text = Row_Selected["currency_Symbol"].ToString();
            inpSupplierUrl.Text = Row_Selected["supplier_Url"].ToString();
            inpSupplierPhoneGeneral.Text = Row_Selected["supplier_PhoneGeneral"].ToString();
            inpSupplierPhoneSales.Text = Row_Selected["supplier_PhoneSales"].ToString();
            inpSupplierPhoneSupport.Text = Row_Selected["supplier_PhoneSupport"].ToString();
            inpSupplierMailGeneral.Text = Row_Selected["supplier_MailGeneral"].ToString();
            inpSupplierMailSales.Text = Row_Selected["supplier_MailSales"].ToString();
            inpSupplierMailSupport.Text = Row_Selected["supplier_MailSupport"].ToString();
            inpSupplierMemo.Document.Blocks.Clear();

            // Read formatted ritch text from table and store in field
            string rtfText = Row_Selected["supplier_Memo"].ToString();
            byte[] byteArray = Encoding.ASCII.GetBytes(rtfText);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new(inpSupplierMemo.Document.ContentStart, inpSupplierMemo.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
            
            //inpSupplierMemo.Document.Blocks.Add(new Paragraph(new Run(Row_Selected["supplier_Memo"].ToString())));

        }

        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            if (inpSupplierCode.Text != "")
            {
                //string ContenSupplierMemo = new TextRange(inpSupplierMemo.Document.ContentStart, inpSupplierMemo.Document.ContentEnd).Text;
                // Prepare ritch text to be stored in database 
                string ContenSupplierMemo;
                TextRange tr = new(inpSupplierMemo.Document.ContentStart, inpSupplierMemo.Document.ContentEnd);
                using (MemoryStream ms = new MemoryStream())
                {
                    // tr.Save(ms, DataFormats.Rtf);
                    tr.Save(ms, DataFormats.Rtf);
                    ContenSupplierMemo = Encoding.ASCII.GetString(ms.ToArray());
                }

                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                dbConnection.Connect();

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "supplier_Code = '" + inpSupplierCode.Text + "', " +
                    "supplier_Name = '" + inpSupplierName.Text + "', " +
                    "supplier_Address1 = '" + inpSupplierAddress1.Text + "', " +
                    "supplier_Address2 = '" + inpSupplierAddress2.Text + "', " +
                    "supplier_Zip = '" + inpSupplierZip.Text + "', " +
                    "supplier_City = '" + inpSupplierCity.Text + "', " +
                    "Country_Id = '" + valueCountryId.Text + "', " +
                    "Country_Code = '" + valueCountryCode.Text + "', " +
                    "Country_Name = '" + cboxSupplierCountry.Text + "', " +
                    "Currency_Id = '" + valueCurrencyId.Text + "', " +
                    "Currency_Code = '" + valueCurrencyCode.Text + "', " +
                    "Currency_Symbol = '" + cboxSupplierCurrency.Text + "', " +
                    "supplier_Url = '" + inpSupplierUrl.Text + "', " +
                    "supplier_PhoneGeneral = '" + inpSupplierPhoneGeneral.Text + "', " +
                    "supplier_PhoneSales = '" + inpSupplierPhoneSales.Text + "', " +
                    "supplier_PhoneSupport = '" + inpSupplierPhoneSupport.Text + "', " +
                    "supplier_MailGeneral = '" + inpSupplierMailGeneral.Text + "', " +
                    "supplier_MailSales = '" + inpSupplierMailSales.Text + "', " +
                    "supplier_Memo = '" + ContenSupplierMemo + "', " +
                    "supplier_MailSupport = '" + inpSupplierMailSupport.Text + "' WHERE " + 
                    "supplier_Id = " + valueSupplierId.Text + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();
                DataTable dtSupplierCodes = dbConnection.LoadMySqlData();

                // Load the data from the database into the datagrid
                SupplierCode_DataGrid.DataContext = dtSupplierCodes;

                // Make sure the eddited row in the datagrid is selected
                SupplierCode_DataGrid.SelectedIndex = int.Parse(valueSupplierId.Text) - 1;
                SupplierCode_DataGrid.Focus();
            }
        }

        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(supplier_Code, supplier_Name, supplier_Address1, supplier_Address2, supplier_Zip, supplier_City, Country_Id, Country_Code, Country_Name, Currency_Id, Currency_Code, Currency_Symbol, supplier_Url, supplier_PhoneGeneral, supplier_PhoneSales, supplier_PhoneSupport, supplier_MailGeneral, supplier_MailSales, supplier_MailSupport, supplier_Memo) VALUES('*','','','','','','','','','','','','','','','','','','');";
            dbConnection.TableName = DatabaseTable;
            int ID = dbConnection.UpdateMySqlDataRecord();
            valueSupplierId.Text = ID.ToString();
            DataTable dtCountryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            SupplierCode_DataGrid.DataContext = dtCountryCodes;
            SupplierCode_DataGrid.SelectedIndex = dtCountryCodes.Rows.Count - 1;
            SupplierCode_DataGrid.Focus();
            inpSupplierCode.Text = "";
        }

        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            // Get the selected row
            int row = int.Parse(SupplierCode_DataGrid.SelectedIndex.ToString());

            int ID = int.Parse(valueSupplierId.Text);
            dbConnection.SqlCommand = "DELETE FROM ";
            dbConnection.SqlCommandString = " WHERE supplier_Id = " + ID + ";";
            dbConnection.TableName = DatabaseTable;
            _ = dbConnection.UpdateMySqlDataRecord();
            _ = dbConnection.LoadMySqlData();

            DataTable dtSupplierCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            SupplierCode_DataGrid.DataContext = dtSupplierCodes;

            // Make sure the correct row in the datagrid is selected after deletion

            // if row is already ithe first row, it should be reselected
            if (row == 0)
            {
                SupplierCode_DataGrid.SelectedIndex = 0;
            }
            else
            {
                SupplierCode_DataGrid.SelectedIndex = row - 1;
            }

            SupplierCode_DataGrid.Focus();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }
    }
}
