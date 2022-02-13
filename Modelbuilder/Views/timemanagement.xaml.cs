using System.Text.RegularExpressions;

namespace Modelbuilder;

public partial class timemanagement : Page
{
    private HelperGeneral _helperGeneral;
    private HelperProduct _helperProduct;
    private DataTable _dt;
    private int _dbRowCount;
    public timemanagement()
    {
        var ProjectList = new List<HelperGeneral.Project>();
        var WorktypeList = new List<HelperGeneral.Worktype> ();
        var ProductList = new List<HelperGeneral.Product>();

        InitializeComponent();
        InitializeHelper();

        //cboxProduct.ItemsSource = _helperGeneral.GetProductList(ProductList);
        cboxProject.ItemsSource = _helperGeneral.GetProjectList(ProjectList);
        cboxWorktype.ItemsSource = _helperGeneral.GetWorktypeList ( WorktypeList );
        cboxProjectProduct.ItemsSource = _helperGeneral.GetProjectList ( ProjectList );
    }

    #region InitializeHelper (connect to database)
    private void InitializeHelper()
    {
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
        if (_helperProduct == null)
        {
            _helperProduct = new HelperProduct(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
        }
    }
    #endregion

    #region CommonCommandBinding_CanExecute
    private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }
    #endregion CommonCommandBinding_CanExecute

    #region Get the Time Entry data
    private void GetTimeEntryData()
    {
        InitializeHelper();

        // Get data from database
        _dt = _helperGeneral.GetData(HelperGeneral.DbTimeView, new string[1, 3]
        {   {HelperGeneral.DbTimeTableFieldNameDate, HelperGeneral.DbTimeTableFieldTypeDate, inpEntryDate.Text } } );

        // Populate data in datagrid from datatable
        Time_DataGrid.DataContext = _dt;

        // Set value
        _dbRowCount = _dt.Rows.Count;
        RecordCountTimeRows.Text = _dbRowCount.ToString();

        string tmpStr = "";
        //update status
        if (_dt.Rows.Count != 1) { tmpStr = Languages.Cultures.general_TimeEntries; }
        string msg = Languages.Cultures.general_Status +  ": " + _dt.Rows.Count + " " + Languages.Cultures.general_TimeEntrie + tmpStr + " " + Languages.Cultures.general_Read + ".";
        UpdateStatus("time", msg);
    }
    #endregion Get the Time Entry data

    #region Datagrid selection changed on time entries
    private void Time_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //After loading the selectedrow data
        cboxTimeEntryEditable.IsChecked = true;
    }
    #endregion Datagrid selection changed on time entries

    #region Datagrid selection changed on Product Usage
    private void ProductUsage_DataGrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        //After loading the selectedrow data
        cboxProductEntryEditable.IsChecked = true;

    }
    #endregion Datagrid selection changed on Product usage

    #region Clicked button: Add new Time entry row
    private void TimeToolbarButtonNew ( object sender, RoutedEventArgs e )
    {
        _helperGeneral.InsertInTable ( HelperGeneral.DbTimeTable, new string[2, 3] 
        {   { HelperGeneral.DbTimeTableFieldNameDate, HelperGeneral.DbTimeTableFieldTypeDate, inpEntryDate.Text },
            { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId,valueProjectId.Text} }) ;

        valueTimeId.Text = _helperGeneral.GetLatestIdFromTable(HelperGeneral.DbTimeTable).ToString();

        GetTimeEntryData ();
        Time_DataGrid.SelectedIndex = _dbRowCount - 1;
        Time_DataGrid.Focus ();
    }
    #endregion Clicked button: Add new Time entry row

    #region Clicked button: Add new Product Usage entry row
    private void ProductUsageToolbarButtonNew ( object sender, RoutedEventArgs e )
    {

    }
    #endregion Clicked button: Add new Product Usage entry row

    #region Clicked button: Save Time Entry row
    private void TimeToolbarButtonSave ( object sender, RoutedEventArgs e )
    {

    }
    #endregion Clicked button: Save Time Entry row

    #region Clicked button: Delete Time entry row
    private void TimeToolbarButtonDelete ( object sender, RoutedEventArgs e )
    {

    }
    #endregion Clicked button: Delete Time entry row

    #region Clicked button: Delete Product Usage entry row
    private void ProductUsageToolbarButtonDelete ( object sender, RoutedEventArgs e )
    {

    }
    #endregion Clicked button: Delete Product Usage entry row

    #region Update status
    private void UpdateStatus(string area, string msg)
    {
        if (!String.IsNullOrEmpty(msg))
        {
            if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
            {
                if (area == "time")
                { textBoxStatusTimeRows.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                else { textBoxStatusProductRows.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                Debug.WriteLine(String.Format("{0} - Status: {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
            }
            else
            {
                if (area == "time")
                { textBoxStatusTimeRows.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                else { textBoxStatusProductRows.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss")); }
                Debug.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
            }
        }
    }
    #endregion Update status

    #region Selection Changed: Project combobox
    private void cboxProject_SelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        foreach (HelperGeneral.Project project in e.AddedItems)
        {
            cboxProject.SelectedItem = project;
            valueProjectId.Text = project.ProjectId.ToString();
            if(inpEntryDate.Text == "")
            {
                TBAddButtonEnable.Text = "Collapsed";
                cboxTimeEntryEditable.IsChecked = false;
            }
            else
            {
                TBAddButtonEnable.Text = "Visible";
                    cboxTimeEntryEditable.IsChecked = true; ;
            }
        }


    }
    #endregion Selection Changed: Project combobox

    #region Selection changed: Date
    private void DataPickerSelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        GetTimeEntryData();
        if (valueProjectId.Text == "") 
        {
            TBAddButtonEnable.Text = "Collapsed";
            cboxTimeEntryEditable.IsChecked = false;
        }
        else
        {
            TBAddButtonEnable.Text = "Visible";
            cboxTimeEntryEditable.IsChecked = true;
        }

    }
    #endregion Selection changed: Date

    #region Selection changed on Combobox: cboxWorktype
    private void cboxWorktype_SelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        foreach (HelperGeneral.Worktype item in e.AddedItems)
        {
            valueSelectedWorktype.Text = item.WorktypeName.ToString ();
            valueWorktypeId.Text = item.WorktypeId.ToString ();
        }
    }
    #endregion Selection changed on Combobox: cboxWorktype

    #region Make Time entries Numeric Only, and validate entered hours or minutes
    #region Handle Input of hours
    private void OnlyHourInput ( object sender, TextCompositionEventArgs e )
    {
        Regex regex = new Regex ( "[^0-9]+" );
        //e.Handled = regex.IsMatch ( e.Text );
        e.Handled = !IsValidTime ( ((TextBox)sender).Text + e.Text, 0, 23 );
    }
    #endregion Handle Input of hours

    #region Handle Input of minutes
    private void OnlyMinuteInput ( object sender, TextCompositionEventArgs e )
    {
        Regex regex = new Regex ( "[^0-9]+" );
        //e.Handled = regex.IsMatch ( e.Text );
        e.Handled = !IsValidTime ( ((TextBox)sender).Text + e.Text, 0, 59 );
    }
    #endregion Handle Input of minutes

    public static bool IsValidTime(string content, int min, int max )
    {
        int i;
        return int.TryParse ( content, out i ) && i >= min && i <= max;
    }
    #endregion Make Time entries Numeric Only

    #region Calculate entered time to minutes and calculate elapsed time
    private void TimeEntryChanged ( object sender, TextChangedEventArgs e )
    {
        TBTimeErrorVisible.Text = "Collapsed";
        if (inpStartHour.Text != string.Empty && inpStartMinute.Text != string.Empty) { valueStartTime.Text = (int.Parse ( inpStartHour.Text ) * 60 + int.Parse ( inpStartMinute.Text )).ToString (); }
        if (inpEndHour.Text != string.Empty && inpEndMinute.Text != string.Empty) { valueEndTime.Text = (int.Parse ( inpEndHour.Text ) * 60 + int.Parse ( inpEndMinute.Text )).ToString (); }
        if (valueStartTime.Text != string.Empty && valueEndTime.Text != string.Empty)
        {
            var ElapsedTotalMinutes = int.Parse ( valueEndTime.Text ) - int.Parse ( valueStartTime.Text );
            var ElapsedHours = ("0" + (ElapsedTotalMinutes / 60).ToString ()).Substring ( ("0" + (ElapsedTotalMinutes / 60).ToString ()).Length - 2, 2 );
            var temp1 = (ElapsedTotalMinutes / 60).ToString ();
            var temp2 = ("0" + (ElapsedTotalMinutes / 60).ToString ());
            var temp3 = temp2.Length;
            var ElapsedMinutes = ("0" + (ElapsedTotalMinutes % 60).ToString ()).Substring ( ("0" + (ElapsedTotalMinutes % 60).ToString ()).Length - 2, 2 );
            dispElapsedTime.Text = ElapsedHours + ":" + ElapsedMinutes;
        }
    }
    #endregion Calculate entered time to minutes and calculate elapsed time

    #region Check if entered enttime is after the entered starttime
    private void CheckEndTime ( object sender, RoutedEventArgs e )
    {
        // If a end time has been entered this should be bigger then the starting time, if this is incorrect a message will be shown, fields EndHour and EndMinute will be cleared and Focus on EndHour field
       if(int.Parse(valueStartTime.Text) > int.Parse ( valueEndTime.Text ))
        {
            inpEndHour.Text = String.Empty;
            inpEndMinute.Text = String.Empty;
            TBTimeErrorVisible.Text = "Visible";
            inpEndHour.Focusable = true;
            inpEndHour.Focus ();
        }
    }
    #endregion Check if entered enttime is after the entered starttime
}
