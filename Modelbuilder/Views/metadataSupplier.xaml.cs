﻿namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataSupplier.xaml
    /// </summary>
    public partial class metadataSupplier : Page
    {
        private HelperGeneral _helpergeneral;
        private HelperSupplier _helper;
        private DataTable _dt, _dtSC;
        private int _dbRowCount;
        private int _currentDataGridIndex;
        private static readonly string DatabaseCountryTable = "country", DatabaseCurrencyTable = "currency";

        /// <summary>
        /// Initializes a new instance of the <see cref="metadataSupplier"/> class.
        /// </summary>
        public metadataSupplier()
        {
            var ContactTypeList = new List<HelperGeneral.ContactType>();
            var CurrencyList = new List<HelperGeneral.Currency>();
            var CountryList = new List<HelperGeneral.Country>();
            InitializeComponent();

            InitializeHelper();
            cboxSupplierCurrency.ItemsSource = _helpergeneral.GetCurrencyList(CurrencyList);
            cboxSupplierCountry.ItemsSource = _helpergeneral.GetCountryList(CountryList);
            cboxSupplierContactType.ItemsSource = _helpergeneral.GetContactTypeList(ContactTypeList);

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

            /// <summary>
            /// Gets or Sets the currency symbol.
            /// </summary>
            public string currencySymbol { get; set; }
            /// <summary>
            /// Gets or Sets the currency id.
            /// </summary>
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

            /// <summary>
            /// Gets or Sets the country name.
            /// </summary>
            public string countryName { get; set; }
            /// <summary>
            /// Gets or Sets the country id.
            /// </summary>
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
            //_dtSC = _helperGeneral.GetDataTblSupplierContact();

            // Populate data in datagrid from datatable
            SupplierCode_DataGrid.DataContext = _dt;
            //DataGrid.DataContext = _dtSC;

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
        /// <summary>
        /// Initializes the helper.
        /// </summary>
        private void InitializeHelper()
        {
            if (_helpergeneral == null)
            {
                _helpergeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
                //_helpergeneral = new HelperGeneral("db4free.net", int.Parse(Connection_Query.port), Connection_Query.database, "herbie68", "9b9749c1");
            }
            if (_helper == null)
            {
                //_helper = new HelperSupplier("db4free.net", int.Parse(Connection_Query.port), Connection_Query.database, "herbie68", "9b9749c1");
                _helper = new HelperSupplier(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
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


                if (row["Memo"] != null && row["Memo"] != DBNull.Value)
                {
                    //get value from DataTable
                    ContentSupplierMemo = row["Memo"].ToString();
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
        /// <summary>
        /// Suppliers the code_ data grid_ selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SupplierCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridIndex = dg.SelectedIndex;

            //Clear memo field
            inpSupplierMemo.Document.Blocks.Clear();

            GetMemo(dg.SelectedIndex);

            double _MinimalShippingCosts = 0, _ShippingCosts = 0, _OrderCosts = 0;

            if (Row_Selected["MinShippingCosts"].ToString() != "") { _MinimalShippingCosts = double.Parse(Row_Selected["MinShippingCosts"].ToString()); }
            if (Row_Selected["ShippingCosts"].ToString() != "") { _ShippingCosts = double.Parse(Row_Selected["ShippingCosts"].ToString()); }
            if (Row_Selected["OrderCosts"].ToString() != "") { _OrderCosts = double.Parse(Row_Selected["OrderCosts"].ToString()); }

            valueSupplierId.Text = Row_Selected["Id"].ToString();
            valueCountryId.Text = Row_Selected["CountryId"].ToString();
            valueCurrencyId.Text = Row_Selected["CurrencyId"].ToString();
            inpSupplierCode.Text = Row_Selected["Code"].ToString();
            inpSupplierName.Text = Row_Selected["Name"].ToString();
            inpSupplierAddress1.Text = Row_Selected["Address1"].ToString();
            inpSupplierAddress2.Text = Row_Selected["Address2"].ToString();
            inpSupplierZip.Text = Row_Selected["Zip"].ToString();
            inpSupplierCity.Text = Row_Selected["City"].ToString();
            inpSupplierShippingCosts.Text = _ShippingCosts.ToString("€ #,##0.00;€ - #,##0.00");
            inpSupplierMinShippingCosts.Text = _MinimalShippingCosts.ToString("€ #,##0.00;€ - #,##0.00");
            inpSupplierOrderCosts.Text = _OrderCosts.ToString("€ #,##0.00;€ - #,##0.00");
            inpSupplierUrl.Text = Row_Selected["Url"].ToString();

            // Empty the fields on the SupplierContact tab
            inpSupplierContactMail.Text = "";
            inpSupplierContactName.Text = "";
            inpSupplierContactPhone.Text = "";
            valueContactTypeId.Text = "1";

            //Select the saved Country in the combobox by default
            foreach (HelperGeneral.Country country in cboxSupplierCountry.Items)
            {
                if (country.CountryName == Row_Selected["CountryName"].ToString())
                {
                    cboxSupplierCountry.SelectedItem = country;
                    break;
                }
            }

            //Select the saved Currency in the combobox by default
            foreach (HelperGeneral.Currency currency in cboxSupplierCurrency.Items)
            {
                if (currency.CurrencySymbol == Row_Selected["CurrencySymbol"].ToString())
                {
                    cboxSupplierCurrency.SelectedItem = currency;
                    break;
                }
            }

            tabSupplierContact.IsEnabled = true;

            // Retrieve list of contacts for this supplier from database
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Populate data in datagrid from datatable after clearing the current gatagrid
            DataGrid.DataContext = _dtSC;

        }
        #endregion

        #region Selection changed: SupplierContacts
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

            valueSupplierContactId.Text = Row_Selected["Id"].ToString();
            inpSupplierContactName.Text = Row_Selected["Name"].ToString();
            inpSupplierContactPhone.Text = Row_Selected["Phone"].ToString();
            inpSupplierContactMail.Text = Row_Selected["Mail"].ToString();


            //Select the saved Contacttype in the combobox by default
            foreach (HelperGeneral.ContactType contacttype in cboxSupplierContactType.Items)
            {
                if (contacttype.ContactTypeName == Row_Selected["TypeName"].ToString())
                {
                    cboxSupplierContactType.SelectedItem = contacttype;
                    valueContactTypeId.Text = contacttype.ContactTypeId.ToString();
                    break;
                }
            }
        }
        #endregion

        #region Selection changed: Combobox Contacttype
        private void cboxSupplierSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (HelperGeneral.ContactType item in e.AddedItems)
            {
                valueContactTypeId.Text = item.ContactTypeId.ToString();
            }
        }
        #endregion

        #region Selection changed: Combobox Country
        private void cboxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (HelperGeneral.Country item in e.AddedItems)
            {
                valueCountryId.Text = item.CountryId.ToString();
            }
        }
        #endregion

        #region Selection changed: Combobox Currency
        private void cboxCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (HelperGeneral.Currency item in e.AddedItems)
            {
                valueCurrencyId.Text = item.CurrencyId.ToString();
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
            int supplierCurrencyId = int.Parse(valueCurrencyId.Text);
            var supplierShippingCosts = double.Parse(inpSupplierShippingCosts.Text.Replace("€", "").Replace(" ", ""));
            var supplierMinShippingCosts = double.Parse(inpSupplierMinShippingCosts.Text.Replace("€", "").Replace(" ", ""));
            if (inpSupplierOrderCosts.Text == String.Empty) { inpSupplierOrderCosts.Text = "0.00"; }
            var supplierOrderCosts = double.Parse(inpSupplierOrderCosts.Text.Replace("€", "").Replace(" ", ""));

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblSupplier(supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, memo, supplierCountryId, supplierCurrencyId, supplierShippingCosts, supplierMinShippingCosts, supplierOrderCosts);
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

            var result = _helper.InsertTblSupplierContact(supplierId, supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail);
            UpdateStatus(result);

            // Get data from database
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Populate data in datagrid from datatable
            DataGrid.DataContext = _dtSC;
            DataGrid.SelectedItem = DataGrid.Items.Count - 1;
            _ = DataGrid.Focus();
        }
        #endregion

        #region Click Save Contact button (on supplier contacts toolbar)
        private void SupplierContactToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = DataGrid.SelectedIndex;

            if (valueSupplierContactId.Text != "")
            {
                UpdateRowSupplierContact(DataGrid.SelectedIndex);
            }

            //GetData();
            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Make sure the eddited row in the datagrid is selected
            DataGrid.DataContext = _dtSC;
            DataGrid.SelectedIndex = rowIndex;
            _ = DataGrid.Focus();
        }
        #endregion

        #region Click Delete Contact button (on supplier contacts toolbar)
        private void SupplierContactToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            int rowIndex = DataGrid.SelectedIndex;

            DeleteRowSupplierContact(DataGrid.SelectedIndex);

            GetData();

            if (rowIndex == 0)
            {
                DataGrid.SelectedIndex = 0;
            }
            else
            {
                DataGrid.SelectedIndex = rowIndex - 1;
            }

            _dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Make sure the eddited row in the datagrid is selected
            DataGrid.DataContext = _dtSC;
            DataGrid.SelectedIndex = rowIndex - 1;
            _ = DataGrid.Focus();
        }
        #endregion

        #region Click New Supplier button (on toolbar)
        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            // When the SupplierId is not empty, a excisting row is selected when the add new row button is hit. 
            // In this case a new row with blank values should be added to the dtable and selected.
            // Otherwise it can be that fields are already filled with data befor the add new row button was hit.
            // In this case the existing value should be used instead of emptying all the data from the form.

            var supplierCode = "";
            var supplierName = "";
            var supplierAddress1 = "";
            var supplierAddress2 = "";
            var supplierZip = "";
            var supplierCity = "";
            var supplierCurrencyId = 1;
            var supplierCountryId = 1;
            var supplierUrl = "";
            var supplierShippingCosts = 0.00;
            var supplierMinShippingCosts = 0.00;
            var supplierOrderCosts = 0.00;

            if (valueSupplierId.Text == "")
            {
                // No existing supplier selected, use formdata if entered
                // check on entered data on formated field because they throw an error on adding a new row

                supplierCode = inpSupplierCode.Text;
                supplierName = inpSupplierName.Text;
                supplierAddress1 = inpSupplierAddress1.Text;
                supplierAddress2 = inpSupplierAddress1.Text;
                supplierZip = inpSupplierZip.Text;
                supplierCity = inpSupplierCity.Text;

                if (valueCountryId.Text != "")
                {
                    supplierCountryId = int.Parse(valueCountryId.Text);
                }

                if (valueCurrencyId.Text != "")
                {
                    supplierCurrencyId = int.Parse(valueCurrencyId.Text);
                }

                supplierUrl = inpSupplierUrl.Text;

                if (inpSupplierShippingCosts.Text != "")
                { supplierShippingCosts = double.Parse(inpSupplierShippingCosts.Text.Replace(",", ".").Replace("€", "").Replace(" ", "")); }

                if (inpSupplierMinShippingCosts.Text != "")
                { supplierMinShippingCosts = double.Parse(inpSupplierMinShippingCosts.Text.Replace(",", ".").Replace("€", "").Replace(" ", "")); }

                if (inpSupplierOrderCosts.Text != "")
                { supplierOrderCosts = double.Parse(inpSupplierOrderCosts.Text.Replace(",", ".").Replace("€", "").Replace(" ", "")); }

            }

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            if (_dt.Rows.Count != 0)
            { DataRow row = _dt.Rows[_dt.Rows.Count - 1]; }

            InitializeHelper();

            string result = string.Empty;
            result = _helper.InsertTblSupplier(supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, memo, supplierCountryId, supplierCurrencyId, supplierShippingCosts, supplierMinShippingCosts, supplierOrderCosts);
            UpdateStatus(result);

            // Get data from database
            _dt = _helper.GetDataTblSupplier();

            // Populate data in datagrid from datatable
            SupplierCode_DataGrid.DataContext = _dt;
            //dataGridView1.Rows[e.RowIndex].Selected = true;
            if (SupplierCode_DataGrid.SelectedItem is not DataRowView Row_Selected)
            {
                return;
            }
            //InsertRowSupplier(SupplierCode_DataGrid.SelectedIndex);

            // Get data from database
            //_dtSC = _helper.GetDataTblSupplierContact(int.Parse(valueSupplierId.Text));

            // Populate data in datagrid from datatable
            //DataGrid.DataContext = _dtSC;
            //DataGrid.SelectedItem = DataGrid.Items.Count - 1;
            //_ = DataGrid.Focus();
        }
        #endregion

        #region Click Save Data button (on toolbar)
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            // Update Id, Code, Name and Symbol values with the selected Country and Currency
            valueCurrencyId.Text = ((HelperGeneral.Currency)cboxSupplierCurrency.SelectedItem).CurrencyId.ToString();
            valueCountryId.Text = ((HelperGeneral.Country)cboxSupplierCountry.SelectedItem).CountryId.ToString();

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
            int supplierCurrencyId = int.Parse(valueCurrencyId.Text);
            var supplierShippingCosts = double.Parse(inpSupplierShippingCosts.Text.Replace("€", "").Replace(" ", ""));
            var supplierMinShippingCosts = double.Parse(inpSupplierMinShippingCosts.Text.Replace("€", "").Replace(" ", ""));
            var supplierOrderCosts = double.Parse(inpSupplierOrderCosts.Text.Replace("€", "").Replace(" ", ""));

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpSupplierMemo.Document);

            InitializeHelper();

            string result = string.Empty;
            result = _helper.UpdateTblSupplier(supplierId, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierCountryId, supplierCurrencyId, memo, supplierShippingCosts, supplierMinShippingCosts, supplierOrderCosts);
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
            }

            if (inpSupplierContactPhone.Text != "")
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

            dbCountryConnection.SqlSelectionString = "Name, Id";
            dbCountryConnection.SqlOrderByString = "Id";
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

            dbCurrencyConnection.SqlSelectionString = "Symbol, Id";
            dbCurrencyConnection.SqlOrderByString = "Id";
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
        /// <summary>
        /// Gets or Sets the text.
        /// </summary>
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
        /// <summary>
        /// Gets or Sets the current row.
        /// </summary>
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

        /// <summary>
        /// Gets or Sets the current column.
        /// </summary>
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
