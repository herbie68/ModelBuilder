using K4os.Compression.LZ4.Internal;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

using static Modelbuilder.HelperMySQL;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataSupplier.xaml
    /// </summary>
    public partial class metadataSupplier : Page
    {
        private HelperMySQL _helper;
        private DataTable _dt, _dtSC;
        private int _dbRowCount;
        private int _currentDataGridIndex;
        private static string DatabaseCountryTable = "country", DatabaseCurrencyTable = "currency";

        public metadataSupplier()
        {
            var ContactTypeList = new List<HelperMySQL.ContactType>();
            InitializeComponent();

            InitializeHelper();
            cboxSupplierCurrency.ItemsSource = CurrencyList();
            cboxSupplierCountry.ItemsSource = CountryList();
            cboxSupplierContactType.ItemsSource = _helper.GetContactTypeList(ContactTypeList);

            GetData();
        }

        #region Create object for all currencies in table for dropdown
        private class Currency
        {
            public Currency(string Symbol, string Id)
            {
                currencySymbol = Symbol;
                currencyId = Id;
            }

            public string currencySymbol { get; set; }
            public string currencyId { get; set; }
        }
        #endregion

        #region Create object for all countries in table for dropdown
        private class Country
        {
            public Country(string Name, string Id)
            {
                countryName = Name;
                countryId = Id;
            }

            public string countryName { get; set; }
            public string countryId { get; set; }
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
            //_dtSC = _helper.GetDataTblSupplierContact();

            // Populate data in datagrid from datatable
            SupplierCode_DataGrid.DataContext = _dt;
            //SupplierContact_DataGrid.DataContext = _dtSC;

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

        #region Selection changed: Supplier
        private void SupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridIndex = dg.SelectedIndex;

            //Clear memo field
            inpSupplierMemo.Document.Blocks.Clear();

            GetMemo(dg.SelectedIndex);

            double _MinimalOrderCosts = 0, _OrderCosts = 0;

            if (Row_Selected["supplier_MinOrderCosts"].ToString() != "") { _MinimalOrderCosts = double.Parse(Row_Selected["supplier_MinOrderCosts"].ToString()); }
            if (Row_Selected["supplier_OrderCosts"].ToString() != "") { _OrderCosts = double.Parse(Row_Selected["supplier_OrderCosts"].ToString()); }

            valueSupplierId.Text = Row_Selected["supplier_Id"].ToString();
            valueCountryId.Text = Row_Selected["supplier_CountryId"].ToString();
            valueCountryName.Text = Row_Selected["supplier_CountryName"].ToString();
            valueCurrencyId.Text = Row_Selected["supplier_CurrencyId"].ToString();
            valueCurrencySymbol.Text = Row_Selected["supplier_CurrencySymbol"].ToString();
            inpSupplierCode.Text = Row_Selected["supplier_Code"].ToString();
            inpSupplierName.Text = Row_Selected["supplier_Name"].ToString();
            inpSupplierAddress1.Text = Row_Selected["supplier_Address1"].ToString();
            inpSupplierAddress2.Text = Row_Selected["supplier_Address2"].ToString();
            inpSupplierZip.Text = Row_Selected["supplier_Zip"].ToString();
            inpSupplierCity.Text = Row_Selected["supplier_City"].ToString();
            inpSupplierOrderCosts.Text = _OrderCosts.ToString("€ #,##0.00;€ - #,##0.00");
            inpSupplierMinOrderCosts.Text = _MinimalOrderCosts.ToString("€ #,##0.00;€ - #,##0.00");
            inpSupplierUrl.Text = Row_Selected["supplier_Url"].ToString();

            // Empty the fields on the SupplierContact tab
            inpSupplierContactMail.Text = "";
            inpSupplierContactName.Text = "";
            inpSupplierContactPhone.Text = "";
            valueContactTypeId.Text = "1";
            valueContactTypeName.Text = "";

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

            tabSupplierContact.IsEnabled = true;

            // Retrieve list of contacts for this supplier from database
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Populate data in datagrid from datatable after clearing the current gatagrid
            SupplierContact_DataGrid.DataContext = _dtSC;

        }
        #endregion

        #region Selection changed: SupplierContacts
        private void SupplierContact_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When a row in the datagrid is selected, all fields can be enablen
            inpSupplierContactName.IsEnabled = true;
            cboxSupplierContactType.IsEnabled = true;
            inpSupplierContactPhone.IsEnabled = true;
            inpSupplierContactMail.IsEnabled = true;

            DataGrid dgSCT = (DataGrid)sender;

            if (dgSCT.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            int _currentDataGridSCTIndex = dgSCT.SelectedIndex;

            valueSupplierContactId.Text = Row_Selected["suppliercontact_Id"].ToString();
            inpSupplierContactName.Text = Row_Selected["suppliercontact_Name"].ToString();
            inpSupplierContactPhone.Text = Row_Selected["suppliercontact_Phone"].ToString();
            inpSupplierContactMail.Text = Row_Selected["suppliercontact_Mail"].ToString();


            //Select the saved Contacttype in the combobox by default
            foreach (ContactType contacttype in cboxSupplierContactType.Items)
            {
                if (contacttype.ContactTypeName == Row_Selected["suppliercontact_TypeName"].ToString())
                {
                    cboxSupplierContactType.SelectedItem = contacttype;
                    valueContactTypeId.Text = contacttype.ContactTypeId.ToString();
                    valueContactTypeName.Text = contacttype.ContactTypeName;
                    break;
                }
            }
        }
        #endregion

        #region Selection changed: Combobox Contacts
        private void cboxSupplierContactType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (HelperMySQL.ContactType item in e.AddedItems)
            {
                valueContactTypeId.Text = item.ContactTypeId.ToString();
                valueContactTypeName.Text = item.ContactTypeName.ToString();
            }
        }
        #endregion

        #region Insert new row in table: Supplier
        private void InsertRowSupplier(int dgIndex)
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
            string supplierCountryName = valueCountryName.Text;
            int supplierCurrencyId = int.Parse(valueCurrencyId.Text);
            string supplierCurrencySymbol = valueCurrencySymbol.Text;
            var supplierOrderCosts = double.Parse(inpSupplierOrderCosts.Text.Replace("€", "").Replace(" ", ""));
            var supplierMinOrderCosts = double.Parse(inpSupplierMinOrderCosts.Text.Replace("€", "").Replace(" ", ""));

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblSupplier(supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, memo, supplierCountryId, supplierCountryName, supplierCurrencyId, supplierCurrencySymbol, supplierOrderCosts, supplierMinOrderCosts);
            UpdateStatus(result);
        }
        #endregion

        #region Delete row in table Supplier
        private void DeleteRow(int dgIndex)
        {
            //since the DataGrid DataContext is set to the DataTable, 
            //the DataTable is updated when data is modified in the DataGrid
            //get last row
            DataRow row = _dt.Rows[_dt.Rows.Count - 1];

            int supplierId = int.Parse(valueSupplierId.Text);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.DeleteTblSupplier(supplierId);
            UpdateStatus(result);
        }
        #endregion

        #region Delete row in table SupplierContact
        private void DeleteRowSupplierContact(int dgIndex)
        {
            //since the DataGrid DataContext is set to the DataTable, 
            //the DataTable is updated when data is modified in the DataGrid
            //get last row
            DataRow row = _dt.Rows[_dt.Rows.Count - 1];

            int SupplierContactId = int.Parse(valueSupplierContactId.Text);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.DeleteTblSupplierContact(SupplierContactId);
            UpdateStatus(result);
        }
        #endregion

        #region Click New Contact button (on supplier contacts toolbar)
        private void SupplierContactToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            var supplierId = int.Parse(valueSupplierId.Text);
            var supplierContactContactTypeId = 1;
            var supplierContactContactTypeName = "";
            var supplierContactContactName = "";
            var supplierContactContactPhone = "";
            var supplierContactContactMail = "";

            // if (_dt.Rows.Count != 0)
            // { DataRow row = _dt.Rows[_dt.Rows.Count - 1]; }

            InitializeHelper();

            var result = _helper.InsertTblSupplierContact(supplierId,  supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail);
            UpdateStatus(result);

            // Get data from database
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Populate data in datagrid from datatable
            SupplierContact_DataGrid.DataContext = _dtSC;
            SupplierContact_DataGrid.SelectedItem = SupplierContact_DataGrid.Items.Count - 1;
            _ = SupplierContact_DataGrid.Focus();
        }
        #endregion

        #region Click Save Contact button (on supplier contacts toolbar)
        private void SupplierContactToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = SupplierContact_DataGrid.SelectedIndex;

            if (valueSupplierContactId.Text != "")
            {
                UpdateRowSupplierContact(SupplierContact_DataGrid.SelectedIndex);
            }

            //GetData();
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Make sure the eddited row in the datagrid is selected
            SupplierContact_DataGrid.DataContext = _dtSC;
            SupplierContact_DataGrid.SelectedIndex = rowIndex;
            _ = SupplierContact_DataGrid.Focus();
        }
        #endregion

        #region Click Delete Contact button (on supplier contacts toolbar)
        private void SupplierContactToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            int rowIndex = SupplierContact_DataGrid.SelectedIndex;

            DeleteRowSupplierContact(SupplierContact_DataGrid.SelectedIndex);

            GetData();

            if (rowIndex == 0)
            {
                SupplierContact_DataGrid.SelectedIndex = 0;
            }
            else
            {
                SupplierContact_DataGrid.SelectedIndex = rowIndex - 1;
            }

            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Make sure the eddited row in the datagrid is selected
            SupplierContact_DataGrid.DataContext = _dtSC;
            SupplierContact_DataGrid.SelectedIndex = rowIndex - 1;
            _ = SupplierContact_DataGrid.Focus();
        }
        #endregion

        #region Click New Supplier button (on toolbar)
        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            InsertRowSupplier(SupplierCode_DataGrid.SelectedIndex);

            // Get data from database
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Populate data in datagrid from datatable
            SupplierContact_DataGrid.DataContext = _dtSC;
            SupplierContact_DataGrid.SelectedItem = SupplierContact_DataGrid.Items.Count - 1;
            _ = SupplierContact_DataGrid.Focus();
        }
        #endregion

        #region Click Save Data button (on toolbar)
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            // Update Id, Code, Name and Symbol values with the selected Country and Currency
            valueCurrencyId.Text = ((Currency)cboxSupplierCurrency.SelectedItem).currencyId.ToString();
            valueCurrencySymbol.Text = ((Currency)cboxSupplierCurrency.SelectedItem).currencySymbol.ToString();
            valueCountryId.Text = ((Country)cboxSupplierCountry.SelectedItem).countryId.ToString();
            valueCountryName.Text = ((Country)cboxSupplierCountry.SelectedItem).countryName.ToString();

            if (valueSupplierId.Text == "")
            // if (_dt.Rows.Count > _dbRowCount)
            {
                InsertRowSupplier(SupplierCode_DataGrid.SelectedIndex);
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

        #region Delete Data button (on toolbar)
        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            DeleteRow(SupplierCode_DataGrid.SelectedIndex);

            GetData();

            if (rowIndex == 0)
            {
                SupplierCode_DataGrid.SelectedIndex = 0;
            }
            else
            {
                SupplierCode_DataGrid.SelectedIndex = rowIndex - 1;
            }

            SupplierCode_DataGrid.Focus();
        }
        #endregion

        #region Get rich text from flow document
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
        #endregion

        #region Update row
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
            string supplierCountryName = valueCountryName.Text;
            int supplierCurrencyId = int.Parse(valueCurrencyId.Text);
            string supplierCurrencySymbol = valueCurrencySymbol.Text;
            var supplierOrderCosts = double.Parse(inpSupplierOrderCosts.Text.Replace("€", "").Replace(" ", ""));
            var supplierMinOrderCosts = double.Parse(inpSupplierMinOrderCosts.Text.Replace("€", "").Replace(" ", ""));

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblSupplier(supplierId, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierCountryId, supplierCountryName, supplierCurrencyId, supplierCurrencySymbol, memo, supplierOrderCosts, supplierMinOrderCosts);
            UpdateStatus(result);
        }
        #endregion

        #region Update row SupplierContact
        private void UpdateRowSupplierContact(int dgIndex)
        {
            DataRow row = _dt.Rows[_currentDataGridIndex];
            var supplierContactContactName = "";
            var supplierContactContactTypeId = 1;
            var supplierContactContactTypeName = "";
            var supplierContactContactPhone = "";
            var supplierContactContactMail = "";

            var supplierContactContactId = int.Parse(valueSupplierContactId.Text);
            var supplierId = int.Parse(valueSupplierId.Text);
            if (inpSupplierContactName.Text != "")
            {
                supplierContactContactName = inpSupplierContactName.Text;
            }

            if (valueContactTypeId.Text != "")
            {
                supplierContactContactTypeId = int.Parse(valueContactTypeId.Text);
                supplierContactContactTypeName = valueContactTypeName.Text;
            }

            if(inpSupplierContactPhone.Text != "")
            { 
                supplierContactContactPhone = inpSupplierContactPhone.Text; 
            }

            if (inpSupplierContactMail.Text != "")
            { 
                supplierContactContactMail = inpSupplierContactMail.Text; 
            }

            InitializeHelper();
            string result = string.Empty;
            result = _helper.UpdateTblSupplierContact(supplierContactContactId, supplierId, supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail);
            UpdateStatus(result);
}

#endregion

        #region Fill Country dropdown
        static List<Country> CountryList()
        {
        Database dbCountryConnection = new()
        {
        TableName = DatabaseCountryTable
        };

            dbCountryConnection.SqlSelectionString = "country_Name, country_Id";
            dbCountryConnection.SqlOrderByString = "country_Id";
            dbCountryConnection.TableName = DatabaseCountryTable;

            DataTable dtCountrySelection = dbCountryConnection.LoadSpecificMySqlData();

            List<Country> CountryList = new();

            for (int i = 0; i < dtCountrySelection.Rows.Count; i++)
            {
                CountryList.Add(new Country(dtCountrySelection.Rows[i][0].ToString(),
                    dtCountrySelection.Rows[i][1].ToString()));
            };
            return CountryList;
        }
        #endregion

        #region Fill Currency dropdown
        static List<Currency> CurrencyList()
        {

            Database dbCurrencyConnection = new()
            {
                TableName = DatabaseCurrencyTable
            };

            dbCurrencyConnection.SqlSelectionString = "currency_Symbol, currency_Id";
            dbCurrencyConnection.SqlOrderByString = "currency_Id";
            dbCurrencyConnection.TableName = DatabaseCurrencyTable;

            DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

            List<Currency> CurrencyList = new();

            for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
            {
                CurrencyList.Add(new Currency(dtCurrencySelection.Rows[i][0].ToString(),
                    dtCurrencySelection.Rows[i][1].ToString()));
            };
            return CurrencyList;
        }
        #endregion

        #region Update status
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
        #endregion

        #region rtfToolbar actions
        // private bool dataChanged = false; // Unsaved textchanges

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
