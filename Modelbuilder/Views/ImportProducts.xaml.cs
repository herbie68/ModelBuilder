namespace Modelbuilder;

/// <summary>
/// Interaction logic for ImportProducts.xaml
/// </summary>
public partial class ImportProducts : Page
{
    private HelperGeneral _helperGeneral;
    private HelperClass _helper;
    private ErrorClass _error;

    public ImportProducts()
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
        // 0 = Code, 1 = Name, 2 = Dimensions, 3 = Price, 4 = MinimalStock, 5 = StandardOrderQuantity, 6 = ProjectCosts, 7 = UnitId, 8 = BrandId, 9 = CategoryId, 10 = StorageId 
        int[] Col = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

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

        string[] Columns = _helper.GetProductHeaders();

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

                    // Check if there is a record with the discription
                    //var ExistingProducts = _helperGeneral.CheckForRecords(HelperGeneral.DbProductTable, new string[1, 3]
                    //{   { HelperGeneral.DbProductTableFieldNameName, HelperGeneral.DbProductTableFieldTypeName, line }    });

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
                    /// 21 => Not existing ProductCode
                    /// 22 => Not existing WorktypeName
                    /// 23 => Not existing BrandId
                    /// 24 => Not existing UnitId
                    /// 25 => Not existing Category
                    /// 26 => Not existing StorageId
                    /// 27 => Not existing ProjectId
                    /// 28 => Not existing Contacttype
                    /// 40 => Endtime bigger or equal then Starttime
                    /// 41 => Incorrect number of fields in CSV file
                    /// 

                    // Product must be not existing Check on ProductCode and ProductName
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbProductTable, new string[1, 3]
                    {   { HelperGeneral.DbProductTableFieldNameCode, HelperGeneral.DbProductTableFieldTypeCode, lineField[Col[0]].ToString() }    }) != 0) { error = 1; ErrorCause = lineField[Col[0]].ToString(); }
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbProductTable, new string[1, 3]
                    {   { HelperGeneral.DbProductTableFieldNameName, HelperGeneral.DbProductTableFieldTypeName, lineField[Col[1]].ToString() }    }) != 0) { error = 1; ErrorCause = lineField[Col[1]].ToString(); }

                    // BrandId must be existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbBrandTable, new string[1, 3]
                    {   { HelperGeneral.DbBrandTableFieldNameId, HelperGeneral.DbBrandTableFieldTypeId, lineField[Col[8]].ToString() }    }) == 0) { error = 23; ErrorCause = lineField[Col[8]].ToString(); }

                    // UnitId must be existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbUnitTable, new string[1, 3]
                    {   { HelperGeneral.DbUnitTableFieldNameUnitId, HelperGeneral.DbUnitTableFieldTypeUnitId, lineField[Col[7]].ToString() }    }) == 0) { error = 24; ErrorCause = lineField[Col[7]].ToString(); }

                    // CategoryId must be existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbCategoryTable, new string[1, 3]
                    {   { HelperGeneral.DbCategoryTableFieldNameId, HelperGeneral.DbCategoryTableFieldTypeId, lineField[Col[9]].ToString() }    }) == 0) { error = 25; ErrorCause = lineField[Col[9]].ToString(); }

                    // StorageId must be existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbStorageTable, new string[1, 3]
                    {   { HelperGeneral.DbStorageTableFieldNameId, HelperGeneral.DbStorageTableFieldTypeId, lineField[Col[10]].ToString() }    }) == 0) { error = 26; ErrorCause = lineField[Col[10]].ToString(); }

                    #endregion
                    // 
                    if (error == 0)
                    {
                        _helperGeneral.InsertInTable(HelperGeneral.DbProductTable, new string[11, 3]
                        {
                        { HelperGeneral.DbProductTableFieldNameCode, HelperGeneral.DbProductTableFieldTypeCode, lineField[Col[0]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameName, HelperGeneral.DbProductTableFieldTypeName,lineField[Col[1]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameDimensions, HelperGeneral.DbProductTableFieldTypeDimensions,lineField[Col[2]].ToString()},
                        { HelperGeneral.DbProductTableFieldNamePrice, HelperGeneral.DbProductTableFieldTypePrice,lineField[Col[3]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameMinimalStock, HelperGeneral.DbProductTableFieldTypeMinimalStock,lineField[Col[4]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameStandardOrderQuantity, HelperGeneral.DbProductTableFieldTypeStandardOrderQuantity,lineField[Col[5]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameProjectCosts, HelperGeneral.DbProductTableFieldTypeProjectCosts,lineField[Col[6]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameUnitId, HelperGeneral.DbProductTableFieldTypeUnitId,lineField[Col[7]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameBrandId, HelperGeneral.DbProductTableFieldTypeBrandId,lineField[Col[8]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameCategoryId, HelperGeneral.DbProductTableFieldTypeCategoryId,lineField[Col[9]].ToString()},
                        { HelperGeneral.DbProductTableFieldNameStorageId, HelperGeneral.DbProductTableFieldTypeStorageId, lineField[Col[10]].ToString()}
                        });

                        // Get the created Product Id
                        var ProductId = _helperGeneral.GetLatestIdFromTable(HelperGeneral.DbProductTable);

                        // Add the new Product allso to the stock table
                        _helperGeneral.InsertInTable(HelperGeneral.DbStockTable, new string[2, 3]
                        {   
                            { HelperGeneral.DbStockTableFieldNameProductId, HelperGeneral.DbStockTableFieldTypeProductId, ProductId },
                            { HelperGeneral.DbStockTableFieldNameAmount, HelperGeneral.DbStockTableFieldTypeAmount, "0" } 
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
