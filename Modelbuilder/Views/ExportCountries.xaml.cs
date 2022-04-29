namespace Modelbuilder;
public partial class ExportCountries : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt;
    private HelperClass helper;

    public ExportCountries()
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
        _dt = _helperGeneral.GetData(HelperGeneral.DbCountryTable);

        _dt.Columns.Remove(HelperGeneral.DbCountryTableFieldNameId);
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
        string _tempMonth = "0" + DateTime.Now.Month.ToString();
        string _tempDay = "0" + DateTime.Now.Day.ToString();
        string _tempHour = "0" + DateTime.Now.Hour.ToString();
        string _tempMinute = "0" + DateTime.Now.Minute.ToString();
        string _tempSecond = "0" + DateTime.Now.Second.ToString();

        var FileName = DateTime.Now.Year.ToString() + 
            _tempMonth.Substring(_tempMonth.Length - 2, 2) + 
            _tempDay.Substring(_tempDay.Length - 2, 2) +
            _tempHour.Substring(_tempHour.Length - 2, 2) + 
            _tempMinute.Substring(_tempMinute.Length - 2, 2) + 
            _tempSecond.Substring(_tempSecond.Length - 2, 2) +
            " - " + Languages.Cultures.ExportCountries_FileName + ".csv";

        string[] Columns = new string[] { HelperGeneral.DbCountryTableFieldNameCode, HelperGeneral.DbCountryTableFieldNameName, HelperGeneral.DbCountryTableFieldNameDefCurrencySymbol, HelperGeneral.DbCountryTableFieldNameDefCurrencyId };
     
        dispFolderName.Text = folderDialog.SelectedPath + @"\" + FileName;
        helper = new HelperClass();
        helper.ExportToCsv(_dt, folderDialog.SelectedPath + @"\" + FileName, Columns, "Header");
        btnBrowseFolder.IsEnabled = false;

        dispStatusLine.Text = _dt.Rows.Count + " " + Languages.Cultures.Export_Statusline_Status_Completed;
    }
    #endregion
}
