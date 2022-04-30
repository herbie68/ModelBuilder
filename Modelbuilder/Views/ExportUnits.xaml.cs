using System.Data;

namespace Modelbuilder;

/// <summary>
/// Interaction logic for ExportUnits.xaml
/// </summary>
public partial class ExportUnits : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt;
    private int _dbRowCount;
    private HelperClass _helper;

    public ExportUnits()
    {
        InitializeComponent();
        InitializeHelper();
        GetData();
    }

    #region Get the Data
    private void GetData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helperGeneral.GetData(HelperGeneral.DbUnitTable);

        // Set value
        _dbRowCount = _dt.Rows.Count;
    }
    #endregion

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

    #region Perform Export after selecting folder
    private void ClickedSelectFolderButton(object sender, RoutedEventArgs e)
    {
        var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
        System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();

        var FileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
            DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() +
            " - " + Languages.Cultures.ExportUnits_FileName + ".csv";

        _helper = new HelperClass();
        string[] Columns = _helper.GetUnitHeaders();
     
        dispFolderName.Text = folderDialog.SelectedPath + @"\" + FileName;
        _helper.ExportToCsv(_dt, folderDialog.SelectedPath + @"\" + FileName, Columns, "Header");
        btnBrowseFolder.IsEnabled = false;

        dispStatusLine.Text = _dt.Rows.Count + " " + Languages.Cultures.Export_Statusline_Status_Completed;
    }
    #endregion
}
