using static Modelbuilder.HelperGeneral;
using System.Security.Policy;

namespace Modelbuilder;

/// <summary>
/// Interaction logic for ImportCategories.xaml
/// </summary>
public partial class ImportCategories : Page
{
    private HelperGeneral _helperGeneral;
    private HelperClass _helper;
    private ErrorClass _error;

    public ImportCategories()
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
        // 0 = Id, 1 = ParentId, 2 = Name, 3 = FullPath 
        int[] Col = { 0, 1, 2, 3};

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

        string[] Columns = _helper.GetCurrencyHeaders();

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
                    // Id must be non existing
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbCategoryTable, new string[1, 3]
                    {   { HelperGeneral.DbCategoryTableFieldNameId, HelperGeneral.DbCategoryTableFieldTypeId, lineField[Col[0]].ToString() }    }) != 0) { error = 5; ErrorCause = lineField[Col[0]].ToString(); }

                    // Parent Id has to exist
                    if (_helperGeneral.CheckForRecords(HelperGeneral.DbCurrencyTable, new string[1, 3]
                    {   { HelperGeneral.DbCategoryTableFieldNameParentId, HelperGeneral.DbCategoryTableFieldTypeParentId, lineField[Col[1]].ToString() }    }) == 0) { error = 32; ErrorCause = lineField[Col[1]].ToString(); }
                    #endregion

                    if (error == 0)
                    {
                        _helperGeneral.InsertInTable(HelperGeneral.DbCategoryTable, new string[4, 3]
                        {
                            { HelperGeneral.DbCategoryTableFieldNameId,    HelperGeneral.DbCategoryTableFieldTypeId, lineField[Col[0]].ToString()},
                            { HelperGeneral.DbCategoryTableFieldNameParentId,  HelperGeneral.DbCategoryTableFieldTypeParentId, lineField[Col[1]].ToString()},
                            { HelperGeneral.DbCategoryTableFieldNameName,    HelperGeneral.DbCategoryTableFieldTypeName, lineField[Col[2]].ToString()},
                            { HelperGeneral.DbCategoryTableFieldNameFullpath,    HelperGeneral.DbCategoryTableFieldTypeFullpath, lineField[Col[3]].ToString()}
                        });
                    }
                    else
                    {
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
    }
    #endregion

    #region Write empty CSV File
    private void PrepareEmptyCSV(object sender, RoutedEventArgs e)
    {
        var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
        System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();

        _helper = new HelperClass();
        var FileName = _helper.GetFilePrefix() + Languages.Cultures.ExportCategories_FileName + ".csv";
        string[] Columns = _helper.GetCurrencyHeaders();

        _helper.PrepareCsv(folderDialog.SelectedPath + @"\" + FileName, Columns);

        dispStatusLine.Text = Languages.Cultures.Import_Statusline_Status_Completed_PreparedCSV + " " + folderDialog.SelectedPath + @"\" + FileName;
    }
    #endregion
}
