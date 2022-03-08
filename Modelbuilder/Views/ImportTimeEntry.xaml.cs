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

        foreach (string line in lines)
        {
            string[] lineField = line.Split(";");
            var ProjectId = _helperGeneral.GetValueFromTable(HelperGeneral.DbProjectTable, new string[1, 3]
            {   {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeName, lineField[0] } }, new string[1, 3]
            {   { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId,"" } });

            var WorkTypeId = _helperGeneral.GetValueFromTable(HelperGeneral.DbWorktypeTable, new string[1, 3]
            {   {HelperGeneral.DbWorktypeTableFieldNameName, HelperGeneral.DbWorktypeTableFieldTypeName, lineField[4] } }, new string[1, 3]
            {   { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId,"" } });

            _helperGeneral.InsertInTable(HelperGeneral.DbTimeTable, new string[5, 3]
            {   { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, lineField[1] },
                { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId,ProjectId},
                { HelperGeneral.DbTimeTableFieldNameStartTime, HelperGeneral.DbTimeTableFieldTypeStartTime, lineField[2] },
                { HelperGeneral.DbTimeTableFieldNameEndTime, HelperGeneral.DbTimeTableFieldTypeEndTime, lineField[3] },
                { HelperGeneral.DbTimeTableFieldNameWorktypeId, HelperGeneral.DbTimeTableFieldTypeWorktypeId, WorkTypeId } });

            Console.WriteLine(line, lineField[0]);
        }
    }
}
