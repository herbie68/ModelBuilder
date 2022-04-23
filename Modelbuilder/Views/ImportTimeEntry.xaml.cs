namespace Modelbuilder;

public partial class ImportTimeEntry : Page
{
    private HelperGeneral _helperGeneral;

    public ImportTimeEntry()
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

    private void ClickedSelectFileButton(object sender, RoutedEventArgs e)
    {
        // Create OpenFileDialog
        OpenFileDialog openFileDlg = new();
        openFileDlg.DefaultExt = ".csv";
        openFileDlg.Filter = Languages.Cultures.ImportTimeEntry_FileDialog_Filtertext + " (.csv)|*.csv";
        openFileDlg.InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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
        var error=0;
        var errorCount = 0;
        var WorkTypeId = "";
        var ProjectId = "";

        List<(int, int, string)> errorList = new(); // First element => Line number, Second element => Error code, Third element Line string

        /// Error codes
        /// 1 => Wrong ProjectCode
        /// 2 => Wrong WorktypeName
        /// 3 => Endtime bigger or equal then Starttime

        foreach (string line in lines)
        {
            dispLineCount.Text = l++.ToString();
            string[] lineField = line.Split(";");

            var lineString = lineField[1].ToString () + "  " + lineField[4].ToString () + " : " + lineField[2].ToString().Substring(0,5) + "-" + lineField[3].ToString ().Substring ( 0, 5 ); 

            // Check if there is a record with the ProjectCode
            var projectCodeCheck = _helperGeneral.CheckForRecords ( HelperGeneral.DbProjectTable, new string[1, 3]
            {   { HelperGeneral.DbProjectTableFieldNameCode, HelperGeneral.DbProjectTableFieldTypeCode, lineField[0].ToString() } } );
            
            if(projectCodeCheck > 0)
            {
                // Existing Projectcode, get the ProjectId
                ProjectId = _helperGeneral.GetValueFromTable ( HelperGeneral.DbProjectTable, new string[1, 3]
                {   { HelperGeneral.DbProjectTableFieldNameCode, HelperGeneral.DbProjectTableFieldTypeCode, lineField[0].ToString() } }, new string[1, 3]
                {   { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId,"" } } );

                dispProjectName.Text = lineField[0];
            }
            else
            {
                error = 1;
                errorCount++;
                errorList.Add((l, 1, Languages.Cultures.ImportTimeEntry_Project_Label + ": " + lineField[0].ToString() + ", " + lineString));
            }

            // Check if there is a record with the WorkTypeName
            var worktypeNameCheck = _helperGeneral.CheckForRecords ( HelperGeneral.DbWorktypeTable, new string[1, 3]
            {   { HelperGeneral.DbWorktypeTableFieldNameName, HelperGeneral.DbWorktypeTableFieldTypeName, lineField[4].ToString() } } );

            if (worktypeNameCheck > 0)
            {
                // Excisting WorktypeName get WorktypeId
                WorkTypeId = _helperGeneral.GetValueFromTable ( HelperGeneral.DbWorktypeTable, new string[1, 3]
                {   {HelperGeneral.DbWorktypeTableFieldNameName, HelperGeneral.DbWorktypeTableFieldTypeName, lineField[4] } }, new string[1, 3]
                {   { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId,"" } } );
            }
            else
            {
                error = 1;
                errorCount++;
                errorList.Add ( (l, 2, lineString) );
            }

            var StartTime = (int.Parse(lineField[2].ToString().Substring(0,2)) * 60) + int.Parse(lineField[2].ToString ().Substring ( 3, 2 ));
            var EndTime = (int.Parse ( lineField[3].ToString ().Substring ( 0, 2 ) ) * 60) + int.Parse(lineField[3].ToString ().Substring ( 3, 2 ));

            // Check if Starttime s before Endtime
            if (StartTime >= EndTime)
            {
                error = 1;
                errorCount++;
                errorList.Add ( (l, 3, lineString) );
            }

            if (error == 0)
            {
                dispStatusLine.Text = Languages.Cultures.ImportTimeEntry_Statusline_Status_Prefix_Added + lineString;

                _helperGeneral.InsertInTable(HelperGeneral.DbTimeTable, new string[5, 3]
                {   { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, lineField[1].ToString() },
                    { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId,ProjectId},
                    { HelperGeneral.DbTimeTableFieldNameStartTime, HelperGeneral.DbTimeTableFieldTypeStartTime, lineField[2].ToString() },
                    { HelperGeneral.DbTimeTableFieldNameEndTime, HelperGeneral.DbTimeTableFieldTypeEndTime, lineField[3].ToString() },
                    { HelperGeneral.DbTimeTableFieldNameWorktypeId, HelperGeneral.DbTimeTableFieldTypeWorktypeId, WorkTypeId } });
            }
            else
            { dispStatusLine.Text = Languages.Cultures.ImportTimeEntry_Statusline_Status_Prefix_Error + " " + l + " : " + lineString; }

            lineString = "";
            error = 0;
        }
        dispLineCount.Text = dispTotalLinesCount.Text;

        string Completed;
        if (int.Parse(dispTotalLinesCount.Text) == 1) { Completed = Languages.Cultures.Import_Statusline_Status_Completed_Single; } else { Completed = Languages.Cultures.Import_Statusline_Status_Completed; }
        
        string CompletedError;
        if (errorCount == 1) { CompletedError = Languages.Cultures.Import_Statusline_Status_Completed_Error_Single; } else { CompletedError = Languages.Cultures.Import_Statusline_Status_Completed_Error; }
        
        string CompletedOk;
        if (int.Parse(dispTotalLinesCount.Text) - errorCount == 1) { CompletedOk = Languages.Cultures.Import_Statusline_Status_Completed_Ok_Single; } else { CompletedOk = Languages.Cultures.Import_Statusline_Status_Completed_Ok; }

        dispStatusLine.Text = dispTotalLinesCount.Text + " " + Completed + ", " + errorCount.ToString() + " " + CompletedError + ".";
        if(errorCount > 0)
        {
            var errorMessage = (int.Parse(dispTotalLinesCount.Text) - errorCount).ToString() + " " + CompletedOk + "." + System.Environment.NewLine + errorCount.ToString() + " "+ CompletedError + "." + System.Environment.NewLine + System.Environment.NewLine;

            for (int i = 0; i < errorList.Count(); i++)
            {
                switch (errorList[i].Item2)
                {
                    case 1:
                        errorMessage += Languages.Cultures.ImportTimeEntry_Messagebox_Error_Projectcode;
                        break;
                    case 2:
                        errorMessage += Languages.Cultures.ImportTimeEntry_Messagebox_Error_Worktypename;
                        break;
                    case 3:
                        errorMessage += Languages.Cultures.ImportTimeEntry_Messagebox_Error_Endtime;
                        break;
                    default:
                        errorMessage += Languages.Cultures.ImportTimeEntry_Messagebox_Error_Unknown;
                        break;
                }
                errorMessage += " " + Languages.Cultures.Import_Messagebox_Error_Inline + " " + errorList[i].Item1 + System.Environment.NewLine + "  >> " + errorList[i].Item3 + System.Environment.NewLine;
            }
            MessageBox.Show(errorMessage, Languages.Cultures.Import_Messagebox_Error_Message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
