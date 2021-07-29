using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataSupplier.xaml
    /// </summary>
    public partial class metadataSupplier : Page
    {
        /// <summary>
        /// Translated names
        /// zeilenangabe    => ShowRow
        /// privAktZeile    => _CurrentRow
        /// AktZeile        => CurrentRow
        /// LabelZeileNr    => LabelRowNr
        /// privAktSpalte   => _CurrentColumn
        //  AktSpalte       => CurrentColumn
        /// spaltenangabe   => ShowColumn
        /// LabelSpalteNr   => LabelColumnNr
        /// Zeilennummer    => Rownumber
        /// Spaltennummer   => Columnnumber
        /// </summary>
        private readonly string DatabaseTable = "supplier", DatabaseCountryTable = "country", DatabaseCurrencyTable = "currency";
        private HelperMySQL _helper;
        private DataTable _dt, _dtCountry, _dtCurrency;
        private int _dbRowCount = 0;
        private int _currentDataGridIndex = 0;

        public metadataSupplier()
        {
            InitializeComponent();
            DataContext = new SupplierCodeViewModel();

            #region Fill Currency dropdown
            Database dbCurrencyConnection = new()
            {
                TableName = DatabaseCurrencyTable
            };

            dbCurrencyConnection.SqlSelectionString = "currency_Symbol, currency_Id, currency_Code";
            dbCurrencyConnection.SqlOrderByString = "currency_Id";
            dbCurrencyConnection.TableName = DatabaseCurrencyTable;

            DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

            List<Currency> CurrencyList = new();

            for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
            {
                CurrencyList.Add(new Currency(dtCurrencySelection.Rows[i][0].ToString(),
                    dtCurrencySelection.Rows[i][1].ToString(),
                    dtCurrencySelection.Rows[i][2].ToString()));
            };

            cboxSupplierCurrency.ItemsSource = CurrencyList;
            #endregion

            #region Fill Country dropdown
            Database dbCountryConnection = new()
            {
                TableName = DatabaseCountryTable
            };

            dbCountryConnection.SqlSelectionString = "country_Name, country_Id, country_Code";
            dbCountryConnection.SqlOrderByString = "country_Id";
            dbCountryConnection.TableName = DatabaseCountryTable;

            DataTable dtCountrySelection = dbCountryConnection.LoadSpecificMySqlData();

            List<Country> CountryList = new();

            for (int i = 0; i < dtCountrySelection.Rows.Count; i++)
            {
                CountryList.Add(new Country(dtCountrySelection.Rows[i][0].ToString(),
                    dtCountrySelection.Rows[i][1].ToString(),
                    dtCountrySelection.Rows[i][2].ToString()));
            };

            cboxSupplierCountry.ItemsSource = CountryList;
            #endregion

            GetData();
        }

        #region Create object for all currencies in table for dropdown
        private class Currency
        {
            public Currency(string Symbol, string Id, string Code)
            {
                currencySymbol = Symbol;
                currencyId = Id;
                currencyCode = Code;
            }

            public string currencySymbol { get; set; }
            public string currencyId { get; set; }
            public string currencyCode { get; set; }
        }
        #endregion

        #region Create object for all countries in table for dropdown
        private class Country
        {
            public Country(string Name, string Id, string Code)
            {
                countryName = Name;
                countryId = Id;
                countryCode = Code;
            }

            public string countryName { get; set; }
            public string countryId { get; set; }
            public string countryCode { get; set; }
        }
        #endregion

        #region CommonCommandBinding_CanExecute
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region Get the data
        private void GetData()
        {
            InitializeHelper();

            // Get data from database
            _dt = _helper.GetDataTblSupplier();

            // Populate data in datagrid from datatable
            SupplierCode_DataGrid.DataContext = _dt;

            // Set value
            _dbRowCount = _dt.Rows.Count;
            RecordsCount.Text = _dbRowCount.ToString();

            // Clear existing memo data
            inpSupplierMemo.Document.Blocks.Clear();

            string tmpStr = "";
            //update status
            if (_dt.Rows.Count != 1) { tmpStr = "s"; };
            string msg = "Status: " + _dt.Rows.Count + " leverancier" + tmpStr + " ingelezen.";
            UpdateStatus(msg);

        }
        #endregion

        #region InitializeHelper (connect to database)
        private void InitializeHelper()
        {
            if (_helper == null)
            {
                _helper = new HelperMySQL("localhost", 3306, "modelbuilder", "root", "admin");
            }
        }
        #endregion

        #region Get content of Memofield
        private void GetMemo(int index)
        {
            string ContentSupplierMemo = string.Empty;

            if (_dt != null && index >= 0 && index < _dt.Rows.Count)
            {
                //set value
                DataRow row = _dt.Rows[index];


                if (row["supplier_Memo"] != null && row["supplier_Memo"] != DBNull.Value)
                {
                    //get value from DataTable
                    ContentSupplierMemo = row["supplier_Memo"].ToString();
                }

                if (!String.IsNullOrEmpty(ContentSupplierMemo))
                {
                     //clear existing data
                    inpSupplierMemo.Document.Blocks.Clear();

                    //convert to byte[]
                    byte[] dataArr = System.Text.Encoding.UTF8.GetBytes(ContentSupplierMemo);

                    using (MemoryStream ms = new MemoryStream(dataArr))
                    {
                        //load data
                        TextRange flowDocRange = new TextRange(inpSupplierMemo.Document.ContentStart, inpSupplierMemo.Document.ContentEnd);
                        flowDocRange.Load(ms, DataFormats.Rtf);
                    }
                }
            }
        }
        #endregion

        #region Selection changed
        private void SupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) {return;}
            
            //set value
            _currentDataGridIndex = dg.SelectedIndex;

            GetMemo(dg.SelectedIndex);

            valueSupplierId.Text = Row_Selected["supplier_Id"].ToString();
            valueCountryId.Text = Row_Selected["supplier_CountryId"].ToString();
            valueCountryCode.Text = Row_Selected["supplier_CountryCode"].ToString();
            valueCountryName.Text = Row_Selected["supplier_CountryName"].ToString();
            valueCurrencyId.Text = Row_Selected["supplier_CurrencyId"].ToString();
            valueCurrencyCode.Text = Row_Selected["supplier_CurrencyCode"].ToString();
            valueCurrencySymbol.Text = Row_Selected["supplier_CurrencySymbol"].ToString();
            inpSupplierCode.Text = Row_Selected["supplier_Code"].ToString();
            inpSupplierName.Text = Row_Selected["supplier_Name"].ToString();
            inpSupplierAddress1.Text = Row_Selected["supplier_Address1"].ToString();
            inpSupplierAddress2.Text = Row_Selected["supplier_Address2"].ToString();
            inpSupplierZip.Text = Row_Selected["supplier_Zip"].ToString();
            inpSupplierCity.Text = Row_Selected["supplier_City"].ToString();

            //Select the saved Country in the combobox by default
            foreach (Country country in cboxSupplierCountry.Items)
            {
                if (country.countryName == Row_Selected["supplier_CountryName"].ToString())
                {
                    cboxSupplierCountry.SelectedItem = country;
                    break;
                }
            }

            //Select the saved Currency in the combobox by default
            foreach (Currency currency in cboxSupplierCurrency.Items)
            {
                if (currency.currencySymbol == Row_Selected["supplier_CurrencySymbol"].ToString())
                {
                    cboxSupplierCurrency.SelectedItem = currency;
                    break;
                }
            }

            inpSupplierUrl.Text = Row_Selected["supplier_Url"].ToString();
            inpSupplierPhoneGeneral.Text = Row_Selected["supplier_PhoneGeneral"].ToString();
            inpSupplierPhoneSales.Text = Row_Selected["supplier_PhoneSales"].ToString();
            inpSupplierPhoneSupport.Text = Row_Selected["supplier_PhoneSupport"].ToString();
            inpSupplierMailGeneral.Text = Row_Selected["supplier_MailGeneral"].ToString();
            inpSupplierMailSales.Text = Row_Selected["supplier_MailSales"].ToString();
            inpSupplierMailSupport.Text = Row_Selected["supplier_MailSupport"].ToString();
        }
        #endregion

        #region Insert new row in table
        private void InsertRow(int dgIndex)
        {
            //since the DataGrid DataContext is set to the DataTable, 
            //the DataTable is updated when data is modified in the DataGrid
            //get last row
            DataRow row = _dt.Rows[_dt.Rows.Count - 1];

            string supplierCode = inpSupplierCode.Text;
            string supplierName = inpSupplierName.Text;
            string supplierAddress1 = inpSupplierAddress1.Text;
            string supplierAddress2 = inpSupplierAddress2.Text;
            string supplierZip = inpSupplierZip.Text;
            string supplierCity = inpSupplierCity.Text;
            string supplierUrl = inpSupplierUrl.Text;
            int supplierCountryId = int.Parse(valueCountryId.Text);
            string supplierCountryCode = valueCountryCode.Text;
            string supplierCountryName = valueCountryName.Text;
            int supplierCurrencyId = int.Parse(valueCurrencyId.Text);
            string supplierCurrencyCode = valueCurrencyCode.Text;
            string supplierCurrencySymbol = valueCurrencySymbol.Text;
            string supplierPhoneGeneral = inpSupplierPhoneGeneral.Text;
            string supplierPhoneSales = inpSupplierPhoneSales.Text;
            string supplierPhoneSupport = inpSupplierPhoneSupport.Text;
            string supplierMailGeneral = inpSupplierMailGeneral.Text;
            string supplierMailSales = inpSupplierMailSales.Text;
            string supplierMailSupport = inpSupplierMailSupport.Text;

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblSupplier(supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, memo, supplierCountryId, supplierCountryCode, supplierCountryName, supplierCurrencyId, supplierCurrencyCode, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport);
            UpdateStatus(result);
        }
        #endregion

        #region Click Save Data button (on toolbar)
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            // Update Id, Code, Name and Symbol values with the selected Country and Currency
            valueCurrencyId.Text = ((Currency)cboxSupplierCurrency.SelectedItem).currencyId.ToString();
            valueCurrencyCode.Text = ((Currency)cboxSupplierCurrency.SelectedItem).currencyCode.ToString();
            valueCurrencySymbol.Text = ((Currency)cboxSupplierCurrency.SelectedItem).currencySymbol.ToString();
            valueCountryId.Text = ((Country)cboxSupplierCountry.SelectedItem).countryId.ToString();
            valueCountryCode.Text = ((Country)cboxSupplierCountry.SelectedItem).countryCode.ToString();
            valueCountryName.Text = ((Country)cboxSupplierCountry.SelectedItem).countryName.ToString();

            //Console.WriteLine((string)_dtCountry.Rows[0][0]);

            if (valueSupplierId.Text == "")
            // if (_dt.Rows.Count > _dbRowCount)
            {
                InsertRow(SupplierCode_DataGrid.SelectedIndex);
            }
            else
            {
                UpdateRow(SupplierCode_DataGrid.SelectedIndex);
            }

            GetData();

            // Make sure the eddited row in the datagrid is selected
            SupplierCode_DataGrid.SelectedIndex = rowIndex;
            SupplierCode_DataGrid.Focus();
        }
        #endregion

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

        private string GetRichTextFromFlowDocument(FlowDocument fDoc)
        {
            string result = string.Empty;

            //convert to string
            if (fDoc != null)
            {
                TextRange tr = new TextRange(fDoc.ContentStart, fDoc.ContentEnd);

                using (MemoryStream ms = new MemoryStream())
                {
                    tr.Save(ms, DataFormats.Rtf);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            return result;
        }

        private void UpdateRow(int dgIndex)
        {
            //when DataGrid SelectionChanged occurs, the value of '_currentDataGridIndex' is set
            //to DataGrid SelectedIndex
            //get data from DataTable
            DataRow row = _dt.Rows[_currentDataGridIndex];

            int supplierId = int.Parse(valueSupplierId.Text);
            string supplierCode = inpSupplierCode.Text;
            string supplierName = inpSupplierName.Text;
            string supplierAddress1 = inpSupplierAddress1.Text;
            string supplierAddress2 = inpSupplierAddress2.Text;
            string supplierZip = inpSupplierZip.Text;
            string supplierCity = inpSupplierCity.Text;
            string supplierUrl = inpSupplierUrl.Text;
            int supplierCountryId = int.Parse(valueCountryId.Text);
            string supplierCountryCode = valueCountryCode.Text;
            string supplierCountryName = valueCountryName.Text;
            int supplierCurrencyId = int.Parse(valueCurrencyId.Text);
            string supplierCurrencyCode = valueCurrencyCode.Text;
            string supplierCurrencySymbol = valueCurrencySymbol.Text;
            string supplierPhoneGeneral = inpSupplierPhoneGeneral.Text;
            string supplierPhoneSales = inpSupplierPhoneSales.Text;
            string supplierPhoneSupport = inpSupplierPhoneSupport.Text;
            string supplierMailGeneral = inpSupplierMailGeneral.Text;
            string supplierMailSales = inpSupplierMailSales.Text;
            string supplierMailSupport = inpSupplierMailSupport.Text;

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblSupplier(supplierId, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierCountryId, supplierCountryCode, supplierCountryName, supplierCurrencyId, supplierCurrencyCode, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport, memo);
            UpdateStatus(result);
        }

        private void UpdateStatus(string msg)
        {
            if (!String.IsNullOrEmpty(msg))
            {
                if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
                {
                    textBoxStatus.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                    Debug.WriteLine(String.Format("{0} - Status: {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
                else
                {
                    textBoxStatus.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                    Debug.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
            }
        }

        #region rtfToolbar actions
        private bool dataChanged = false; // Unsaved textchanges

        private string privateText = null; // Content of RTFBox in txt-Format
        public string text
        {
            get
            {
                TextRange range = new TextRange(inpSupplierMemo.Document.ContentStart, inpSupplierMemo.Document.ContentEnd);
                return range.Text;
            }
            set
            {
                privateText = value;
            }
        }

        private string ShowRow; // aktuelle Zeile der Cursorposition
        private int _CurrentRow = 1;
        public int CurrentRow
        {
            get { return _CurrentRow; }
            set
            {
                _CurrentRow = value;
                ShowRow = "Rij: " + value;
                //Uncomment when statusbar is in place
                //LabelRowNr.Content = ShowRow;
            }
        }

        private string ShowColumn; // aktuelle Spalte der Cursorposition
        private int _CurrentColumn = 1;
        public int CurrentColumn
        {
            get { return _CurrentColumn; }
            set
            {
                _CurrentColumn = value;
                ShowColumn = "Kol: " + value;
                // Uncomment when statusbar is in place
                //LabelColumnNr.Content = ShowColumn;
            }
        }
        #endregion rtfToolbar actions
    }
}
