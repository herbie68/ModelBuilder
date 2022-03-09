using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

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
        Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
        openFileDlg.DefaultExt = ".csv";
        openFileDlg.Filter = "Import bestanden (.csv)|*.csv";
        openFileDlg.InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Launch OpenFileDialog by calling ShowDialog method
        Nullable<bool> result = openFileDlg.ShowDialog();
        // Get the selected file name and display in a TextBox.
        // Load content of file in a TextBlock
        if (result == true)
        {
            dispFileName.Text = openFileDlg.FileName;
            //var FileContent = System.IO.File.ReadAllText(openFileDlg.FileName);
        }

        var lines = File.ReadLines(dispFileName.Text);
        dispTotalLinesCount.Text = lines.Count().ToString();
        var l = 0;
        var error=0;
        var lineString = "";

        List<(int, int, string)> errorList = new(); // First element => Line number, Second element => Error code, Third element Line string

        /// Error codes
        /// 1 => Wrong ProjectCode
        /// 2 => Wrong WorktypeName
        /// 3 => Endtime bigger or equal then Starttime

        foreach (string line in lines)
        {
            dispLineCount.Text = l++.ToString();
            string[] lineField = line.Split(";");

            lineString = lineField[1].ToString () + "  " + lineField[4].ToString () + " : " + lineField[2].ToString().Substring(0,5) + "-" + lineField[3].ToString ().Substring ( 0, 5 ); 

            // Check if there is a record with the ProjectCode
            var projectCodeCheck = _helperGeneral.CheckForRecords ( HelperGeneral.DbProjectTable, new string[1, 3]
            {   { HelperGeneral.DbProjectTableFieldNameCode, HelperGeneral.DbProjectTableFieldTypeCode, lineField[0].ToString() } } );
            
            if(projectCodeCheck > 0)
            {
                // Existing Projectcode, get the ProjectId
                var ProjectId = _helperGeneral.GetValueFromTable ( HelperGeneral.DbProjectTable, new string[1, 3]
                {   { HelperGeneral.DbProjectTableFieldNameCode, HelperGeneral.DbProjectTableFieldTypeCode, lineField[0].ToString() } }, new string[1, 3]
                {   { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId,"" } } );

                dispProjectName.Text = lineField[0];
            }
            else
            {
                error = 1;
                errorList.Add((l, 1, lineString));
            }

            // Check if there is a record with the WorkTypeName
            var worktypeNameCheck = _helperGeneral.CheckForRecords ( HelperGeneral.DbWorktypeTable, new string[1, 3]
            {   { HelperGeneral.DbWorktypeTableFieldNameName, HelperGeneral.DbWorktypeTableFieldTypeName, lineField[4].ToString() } } );

            if (worktypeNameCheck > 0)
            {
                // Excisting WorktypeName get WorktypeId
                var WorkTypeId = _helperGeneral.GetValueFromTable ( HelperGeneral.DbWorktypeTable, new string[1, 3]
                {   {HelperGeneral.DbWorktypeTableFieldNameName, HelperGeneral.DbWorktypeTableFieldTypeName, lineField[4] } }, new string[1, 3]
                {   { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId,"" } } );
            }
            else
            {
                error = 1;
                errorList.Add ( (l, 2, lineString) );
            }

            var StartTime = (int.Parse(line[2].ToString().Substring(0,2)) * 60) + int.Parse(line[2].ToString ().Substring ( 3, 2 ));
            var EndTime = (int.Parse ( line[3].ToString ().Substring ( 0, 2 ) ) * 60) + int.Parse(line[3].ToString ().Substring ( 3, 2 ));

            // Check if Starttime s before Endtime
            if (StartTime >= EndTime)
            {
                error = 1;
                errorList.Add ( (l, 3, lineString) );
            }

            if (error == 0)
            {
                dispStatusLine.Text = "Toegevoegd: " + lineString;
            }
            else
            {
                dispStatusLine.Text = "Fout in regel " + l + " : " + lineString
;            }

            /*
            if(error == 0)
            {
                _helperGeneral.InsertInTable(HelperGeneral.DbTimeTable, new string[5, 3]
                {   { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, lineField[1].ToString() },
                    { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId,ProjectId},
                    { HelperGeneral.DbTimeTableFieldNameStartTime, HelperGeneral.DbTimeTableFieldTypeStartTime, lineField[2].ToString() },
                    { HelperGeneral.DbTimeTableFieldNameEndTime, HelperGeneral.DbTimeTableFieldTypeEndTime, lineField[3].ToString() },
                    { HelperGeneral.DbTimeTableFieldNameWorktypeId, HelperGeneral.DbTimeTableFieldTypeWorktypeId, WorkTypeId } });
            }
            */

            lineString = "";
            error = 0;
        }
    }
}
