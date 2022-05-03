namespace Modelbuilder;

/// <summary>
/// Interaction logic for ImportSuppliers.xaml
/// </summary>
public partial class ImportSuppliers : Page
{
    private HelperGeneral _helperGeneral;
    private HelperClass _helper;
    private ErrorClass _error;

    public ImportSuppliers()
    {
        InitializeComponent();
        InitializeHelper();
    }

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
    }
    #endregion

    #region CommonCommandBinding_CanExecute
    private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }
    #endregion CommonCommandBinding_CanExecute

    #region Perform Import after selecting file
    private void ClickedSelectFileButton(object sender, RoutedEventArgs e)
    {
        // Prepare the Column values for the ColumnNames, Filled with default Locations in csv file, but depending on Header this can be different
        // Variable will be anonimous so they are multiusable
        // 0 = Code, 1 = Name, 2 = Address1, 3 = Address2, 4 = Zip, 5 = City, 6 = URL, 7 = Shipping Costs, 8 = Min Shipping Costs, 9= Order Costs, 10 = CurrencyId, 11 = CountryId 
        int[] Col = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        // Create OpenFileDialog
        Microsoft.Win32.OpenFileDialog openFileDlg = new()
        {
            DefaultExt = ".csv",
            Filter = Languages.Cultures.ImportTimeEntry_FileDialog_Filtertext + " (.csv)|*.csv",
            InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        // Launch OpenFileDialog by calling ShowDialog method
        Nullable<bool> result = openFileDlg.ShowDialog();
        // Get the selected file name and display in a TextBox.
        // Load content of file in a TextBlock
        if (result == true)
        {
            dispFileName.Text = openFileDlg.FileName;
        }

        // Import the selected file
        var lines = File.ReadLines(dispFileName.Text);
        dispTotalLinesCount.Text = lines.Count().ToString();
        var l = 0;
        var error = 0;
        var errorCount = 0;
        var ImportFailed = 0;
        var ErrorCause = "";

        List<(int, int, string)> errorList = new(); // First element => Line number, Second element => Error code, Third element Line string

        _helper = new HelperClass();
        _error = new ErrorClass();

        string[] Columns = _helper.GetSupplierHeaders();

        foreach (string line in lines)
        {
            if (ImportFailed == 0)
            {
                if (line.Contains(Columns[0]))
                {
                    // Read line is header
                    // Check the order of the names and fill the ColumnVariable
                    string[] lineField = line.Split(";");
                    if (lineField.Length == Columns.Length)
                    {
                        for (int lF = 0; lF < lineField.Length; lF++)
                        {
                            for (int c = 0; c < Columns.Length; c++)
                            {
                                if (lineField[lF] == Columns[c])
                                {
                                    Col[c] = lF;
                                }
                            }
                        }
                    }
                    else
                    {
                        ImportFailed = 1;
                    }
                }
                else
                {
                    dispLineCount.Text = l++.ToString();
                    string[] lineField = line.Split(";");

                    #region Check input on errors
                    /// Error codes
                    /// 1 => Product already exists
                    /// 2 => Worktype already exists
                    /// 3 => Brand already excists
                    /// 4 => Unit Already Excists
                    /// 5 => Category already excists
                    /// 6 => Storage already exists
                    /// 7 => Project already exists
                    /// 8 => Contacttype already excists
                    /// 9 => Supplier already excists
                    /// 10 = Currency already excists
                    /// 11 => Country already excists
                    /// 21 => Not existing SupplierCode
                    /// 22 => Not existing WorktypeName
                    /// 23 => Not existing BrandId
                    /// 24 => Not existing UnitId
                    /// 25 => Not existing Category
                    /// 26 => Not existing StorageId
                    /// 27 => Not existing ProjectId
                    /// 28 => Not existing Contacttype
                    /// 29 => Not existing Supplier
                    /// 30 => Not existing CurrencyId
                    /// 31 => Not existing CountryId
                    /// 40 => Endtime bigger or equal then Starttime
                    /// 41 => Incorrect number of fields in CSV file
                    /// 

                    // Supplier must be not existing Check on SupplierCode and SupplierName
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbSupplierTable, new string[1, 3]
                    {   { HelperGeneral.DbSupplierFieldNameCode, HelperGeneral.DbSupplierFieldTypeCode, lineField[Col[0]].ToString() }    }) != 0) { error = 9; ErrorCause = lineField[Col[0]].ToString(); }
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbSupplierTable, new string[1, 3]
                    {   { HelperGeneral.DbSupplierFieldNameName, HelperGeneral.DbSupplierFieldTypeName, lineField[Col[1]].ToString() }    }) != 0) { error = 9; ErrorCause = lineField[Col[1]].ToString(); }

                    // CurrencyId must be existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbCurrencyTable, new string[1, 3]
                    {   { HelperGeneral.DbCurrencyTableFieldNameId, HelperGeneral.DbCurrencyTableFieldTypeId, lineField[Col[10]].ToString() }    }) == 0) { error = 30; ErrorCause = lineField[Col[10]].ToString(); }

                    // CountryId must be existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbUnitTable, new string[1, 3]
                    {   { HelperGeneral.DbCountryTableFieldNameId, HelperGeneral.DbCountryTableFieldTypeId, lineField[Col[11]].ToString() }    }) == 0) { error = 31; ErrorCause = lineField[Col[11]].ToString(); }
                    #endregion
 
                    if (error == 0)
                    {
                        _helperGeneral.InsertInTable(HelperGeneral.DbSupplierTable, new string[12, 3]
                        {
                        { HelperGeneral.DbSupplierFieldNameCode,                HelperGeneral.DbSupplierFieldTypeCode, lineField[Col[0]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameName,                HelperGeneral.DbSupplierFieldTypeName,lineField[Col[1]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameAddress1,            HelperGeneral.DbSupplierFieldTypeAddress1,lineField[Col[2]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameAddress2,            HelperGeneral.DbSupplierFieldTypeAddress2,lineField[Col[3]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameZip,                 HelperGeneral.DbSupplierFieldTypeZip,lineField[Col[4]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameCity,                HelperGeneral.DbSupplierFieldTypeCity,lineField[Col[5]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameUrl,                 HelperGeneral.DbSupplierFieldTypeUrl,lineField[Col[6]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameShippingCosts,       HelperGeneral.DbSupplierFieldTypeShippingCosts,lineField[Col[7]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameMinShippingCosts,    HelperGeneral.DbSupplierFieldTypeMinShippingCosts,lineField[Col[8]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameOrderCosts,          HelperGeneral.DbSupplierFieldTypeOrderCosts,lineField[Col[9]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameCurrencyId,          HelperGeneral.DbSupplierFieldTypeCurrencyId,lineField[Col[10]].ToString()},
                        { HelperGeneral.DbSupplierFieldNameCountryId,           HelperGeneral.DbSupplierFieldTypeCountryId, lineField[Col[11]].ToString()}
                        });
                    }
                    else
                    {
                        //error = 1;
                        errorCount++;
                        var Error = _error.GetErrorMessages(error);
                        errorList.Add((l, error, Error.Label + ": " + ErrorCause + " - " + Error.ErrorMessageShort));
                    }
                }
            }
        }

        if (ImportFailed == 0)
        {
            dispLineCount.Text = dispTotalLinesCount.Text;

            string Completed;
            string CompletedError;
            string CompletedOk;

            if (int.Parse(dispTotalLinesCount.Text) == 1) { Completed = Languages.Cultures.Import_Statusline_Status_Completed_Single; } else { Completed = Languages.Cultures.Import_Statusline_Status_Completed; }
            if (errorCount == 1) { CompletedError = Languages.Cultures.Import_Statusline_Status_Completed_Error_Single; } else { CompletedError = Languages.Cultures.Import_Statusline_Status_Completed_Error; }
            if (int.Parse(dispTotalLinesCount.Text) - errorCount == 1) { CompletedOk = Languages.Cultures.Import_Statusline_Status_Completed_Ok_Single; } else { CompletedOk = Languages.Cultures.Import_Statusline_Status_Completed_Ok; }

            dispStatusLine.Text = dispTotalLinesCount.Text + " " + Completed + ", " + errorCount.ToString() + " " + CompletedError + ".";
            if (errorCount > 0)
            {
                var errorMessage = (int.Parse(dispTotalLinesCount.Text) - errorCount).ToString() + " " + CompletedOk + "." + System.Environment.NewLine + errorCount.ToString() + " " + CompletedError + "." + System.Environment.NewLine + System.Environment.NewLine;

                for (int i = 0; i < errorList.Count(); i++)
                {
                    var Error = _error.GetErrorMessages(errorList[i].Item2);

                    errorMessage += Error.ErrorMessageShort;

                    errorMessage += " " + Languages.Cultures.Import_Messagebox_Error_Inline + " " + errorList[i].Item1 + System.Environment.NewLine + "  >> " + errorList[i].Item3 + System.Environment.NewLine;
                }

                MessageBox.Show(errorMessage, Languages.Cultures.Import_Messagebox_Error_Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show(Languages.Cultures.Import_Messagebox_Error_HeaderError, Languages.Cultures.Import_Messagebox_Error_HeaderError, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
