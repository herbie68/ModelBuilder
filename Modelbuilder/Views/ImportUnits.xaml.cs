namespace Modelbuilder;

/// <summary>
/// Interaction logic for ImportUnits.xaml
/// </summary>
public partial class ImportUnits : Page
{
    private HelperGeneral _helperGeneral;

    public ImportUnits()
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

        var lines = File.ReadLines(dispFileName.Text);
        dispTotalLinesCount.Text = lines.Count().ToString();
        var l = 0;
        var error = 0;
        var errorCount = 0;

        List<(int, int, string)> errorList = new(); // First element => Line number, Second element => Error code, Third element Line string

        /// Error codes
        /// 1 => Unit already exists

        foreach (string line in lines)
        {
            dispLineCount.Text = l++.ToString();

            // Check if there is a record with the Unit discription
            var ExistingUnits = _helperGeneral.CheckForRecords(HelperGeneral.DbUnitTable, new string[1, 3]
            {   { HelperGeneral.DbUnitTableFieldNameUnitName, HelperGeneral.DbUnitTableFieldTypeUnitName, line }    });

            if (ExistingUnits == 0)
            {
                _helperGeneral.InsertInTable(HelperGeneral.DbUnitTable, new string[1, 3]
                {   { HelperGeneral.DbUnitTableFieldNameUnitName, HelperGeneral.DbUnitTableFieldTypeUnitName, line }    });
            }
            else
            {
                error = 1;
                errorCount++;
                errorList.Add((l, 1, Languages.Cultures.ImportTimeEntry_Unit_Label + ": " + line.ToString() + " - " + Languages.Cultures.ImportUnits_Messagebox_Error_AlreadyExists));
            }

            dispLineCount.Text = dispTotalLinesCount.Text;

            string Completed;
            string CompletedError;
            string CompletedOk;

            if(int.Parse(dispTotalLinesCount.Text) == 1) { Completed = Languages.Cultures.Import_Statusline_Status_Completed_Single; } else { Completed = Languages.Cultures.Import_Statusline_Status_Completed; }
            if (errorCount == 1) { CompletedError = Languages.Cultures.Import_Statusline_Status_Completed_Error_Single; } else { CompletedError = Languages.Cultures.Import_Statusline_Status_Completed_Error; }
            if (int.Parse(dispTotalLinesCount.Text) - errorCount == 1) { CompletedOk = Languages.Cultures.Import_Statusline_Status_Completed_Ok_Single; } else { CompletedOk = Languages.Cultures.Import_Statusline_Status_Completed_Ok; }

            dispStatusLine.Text = dispTotalLinesCount.Text + " " + Completed + ", " + errorCount.ToString() + " " + CompletedError + ".";
            if (errorCount > 0)
            {
                var errorMessage = (int.Parse(dispTotalLinesCount.Text) - errorCount).ToString() + " " + CompletedOk + "." + System.Environment.NewLine + errorCount.ToString() + " " + CompletedError + "." + System.Environment.NewLine + System.Environment.NewLine;

                for (int i = 0; i < errorList.Count(); i++)
                {
                    switch (errorList[i].Item2)
                    {
                        case 1:
                            errorMessage += Languages.Cultures.ImportUnits_Messagebox_Error_ExistingUnit;
                            break;
                        default:
                            errorMessage += Languages.Cultures.Import_Messagebox_Error_Unknown;
                            break;
                    }
                    errorMessage += " " + Languages.Cultures.Import_Messagebox_Error_Inline + " " + errorList[i].Item1 + System.Environment.NewLine + "  >> " + errorList[i].Item3 + System.Environment.NewLine;
                }
                MessageBox.Show(errorMessage, Languages.Cultures.Import_Messagebox_Error_Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
