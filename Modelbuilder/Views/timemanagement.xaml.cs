using System.Text.RegularExpressions;
using System.Xml.Linq;

using static Modelbuilder.HelperClass;

namespace Modelbuilder;

public partial class timemanagement : Page
{
    private HelperGeneral _helperGeneral;
    private HelperProduct _helperProduct;
    private DataTable _dt;
    private int _dbRowCount;
    private HelperClass az;
    private int _currentDataGridIndex;

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
        {   {HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, inpEntryDate.Text } } );

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

        // Get the total elapsed time for this work date
        var ElapsedMinutes= _helperGeneral.GetValueFromTable(HelperGeneral.DbTimeView, new string[1, 3]
        { { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, inpEntryDate.Text } }, new string[1, 3]
        { { "SUM(ElapsedMinutes)", "double", "" }});
        var _tempElapsedHours= int.Parse(ElapsedMinutes.ToString())/60;
        var _tempElapsedMinutes=int.Parse(ElapsedMinutes.ToString())%60;
        dispElapsedTotal.Text= _tempElapsedHours.ToString() +":"+_tempElapsedMinutes.ToString();

        //Prevent possility to Change date and prodiect durung time entry edit
        inpEntryDate.IsEnabled = false;
        cboxProject.IsEnabled = false;
        TBResetButtonEnable.Text = "Visible";
    }
    #endregion Get the Time Entry data

    #region Datagrid selection changed on time entries
    private void Time_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dg = (DataGrid)sender;

        if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

        //set values
        _currentDataGridIndex = dg.SelectedIndex;
        var _tempStartTime = Row_Selected["StartTime"].ToString().Split(":");
        var _tempEndTime = Row_Selected["EndTime"].ToString().Split(":");
        inpStartHour.Text = _tempStartTime[0];
        inpStartMinute.Text = _tempStartTime[1];
        inpEndHour.Text = _tempEndTime[0];
        inpEndMinute.Text = _tempEndTime[1];
        inpComment.Text = Row_Selected["Comment"].ToString();
        valueWorktypeId.Text = Row_Selected["WorktypeId"].ToString();
        valueSelectedWorktype.Text = Row_Selected["WorktypeName"].ToString();
        valueTimeId.Text = Row_Selected["Id"].ToString();

        #region Select the saved Storage location in the Storage combobox by default
        string _tempWorktype = Row_Selected["WorktypeName"].ToString();
        cboxWorktype.Text = _tempWorktype;

        //Select the saved Worktype in the combobox by default
        foreach (HelperGeneral.Worktype worktype in cboxWorktype.Items)
        {
            if (worktype.WorktypeName == _tempWorktype)
            {
                cboxWorktype.SelectedItem = worktype;
                break;
            }
        }
        #endregion Select the saved Storage location in the Storage combobox by default
        
        cboxTimeEntryEditable.IsChecked = true;
        TBSaveButtonEnable.Text = "visible";
        TBAddButtonEnable.Text = "collapsed";
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
    private void TimeToolbarButtonNew(object sender, RoutedEventArgs e)
    {
        az = new HelperClass();
        // Save Date AS date, Project_Id AS int, Worktype_Id AS int, StartTime AS time, EndTime AS time, Comment AS varchar
        var StartTime = az.AddZeros(inpStartHour.Text.Trim(), 2) + ":" + az.AddZeros(inpStartMinute.Text.Trim(), 2) + ":00";
        var EndTime = az.AddZeros(inpEndHour.Text.Trim(), 2) + ":" + az.AddZeros(inpEndMinute.Text.Trim(), 2) + ":00";
        var Comment = "";

        if (inpComment.Text != string.Empty) { Comment = inpComment.Text; }
        _helperGeneral.InsertInTable(HelperGeneral.DbTimeTable, new string[6, 3]
        {   { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, inpEntryDate.Text },
            { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId,valueProjectId.Text},
            { HelperGeneral.DbTimeTableFieldNameStartTime, HelperGeneral.DbTimeTableFieldTypeStartTime, StartTime },
            { HelperGeneral.DbTimeTableFieldNameEndTime, HelperGeneral.DbTimeTableFieldTypeEndTime, EndTime },
            { HelperGeneral.DbTimeTableFieldNameWorktypeId, HelperGeneral.DbTimeTableFieldTypeWorktypeId, valueWorktypeId.Text },
            { HelperGeneral.DbTimeTableFieldNameComment, HelperGeneral.DbTimeTableFieldTypeComment, Comment }
        });

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
        az = new HelperClass();
        // Save Date AS date, Project_Id AS int, Worktype_Id AS int, StartTime AS time, EndTime AS time, Comment AS varchar
        var StartTime = az.AddZeros(inpStartHour.Text.Trim(), 2) + ":" + az.AddZeros(inpStartMinute.Text.Trim(), 2) + ":00";
        var EndTime = az.AddZeros(inpEndHour.Text.Trim(), 2) + ":" + az.AddZeros(inpEndMinute.Text.Trim(), 2) + ":00";
        var Comment = "";

        if (inpComment.Text != string.Empty) { Comment = inpComment.Text; }
        _helperGeneral.UpdateFieldInTable(HelperGeneral.DbTimeTable, new string[1, 3]
        { { HelperGeneral.DbTimeTableFieldNameId, HelperGeneral.DbTimeTableFieldTypeId, valueTimeId.Text }
        }, new string[6, 3]
        {   { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, inpEntryDate.Text },
            { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId,valueProjectId.Text},
            { HelperGeneral.DbTimeTableFieldNameStartTime, HelperGeneral.DbTimeTableFieldTypeStartTime, StartTime },
            { HelperGeneral.DbTimeTableFieldNameEndTime, HelperGeneral.DbTimeTableFieldTypeEndTime, EndTime },
            { HelperGeneral.DbTimeTableFieldNameWorktypeId, HelperGeneral.DbTimeTableFieldTypeWorktypeId, valueWorktypeId.Text },
            { HelperGeneral.DbTimeTableFieldNameComment, HelperGeneral.DbTimeTableFieldTypeComment, Comment }
        });

        valueTimeId.Text = _helperGeneral.GetLatestIdFromTable(HelperGeneral.DbTimeTable).ToString();

        GetTimeEntryData();
        Time_DataGrid.SelectedIndex = _dbRowCount - 1;
        Time_DataGrid.Focus();
    }
    #endregion Clicked button: Save Time Entry row

    #region Clicked button: Delete Time entry row
    private void TimeToolbarButtonDelete ( object sender, RoutedEventArgs e )
    {
        _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbTimeTable, new string[1, 3]
         { { HelperGeneral.DbTimeTableFieldNameId, HelperGeneral.DbTimeTableFieldTypeId, valueTimeId.Text }});
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
                TBAddTimeEntryEnable.Text = "Collapsed";
                TBAddButtonEnable.Text = "Collapsed";
                cboxTimeEntryEditable.IsChecked = false;
            }
            else
            {
                TBAddTimeEntryEnable.Text = "Visible";
                cboxTimeEntryEditable.IsChecked = true;
                // check time entries, if also available add button can become visible
                if(inpStartHour.Text != string.Empty && inpStartMinute.Text != string.Empty && inpEndHour.Text != string.Empty && inpEndMinute.Text != string.Empty && valueSelectedWorktype.Text != string.Empty)
                {
                    TBAddButtonEnable.Text = "Visible";
                }
                else
                {
                    TBAddButtonEnable.Text = "Collapsed";
                }
            }
        }


    }
    #endregion Selection Changed: Project combobox

    #region Selection changed: Date
    private void DataPickerSelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        if (inpEntryDate.Text == string.Empty) { return; }
        // Check if there are records for the selected date
        var _RecordCount = _helperGeneral.CheckForRecords(HelperGeneral.DbTimeView, new string[1, 3]
        { { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, inpEntryDate.Text } });
        if(_RecordCount > 0) { GetTimeEntryData(); }
        
        if (valueProjectId.Text == "") 
        {
            TBAddTimeEntryEnable.Text = "Collapsed";
            cboxTimeEntryEditable.IsChecked = false;
        }
        else
        {
            TBAddTimeEntryEnable.Text = "Visible";
            cboxTimeEntryEditable.IsChecked = true;
            // check time entries, if also available add button can become visible
            if (inpStartHour.Text != string.Empty && inpStartMinute.Text != string.Empty && inpEndHour.Text != string.Empty && inpEndMinute.Text != string.Empty && valueSelectedWorktype.Text != string.Empty)
            {
                TBAddButtonEnable.Text = "Visible";
            }
            else
            {
                TBAddButtonEnable.Text = "Collapsed";
            }
            TBResetButtonEnable.Text = "Visible";
        }

        if (_RecordCount > 0)
        {
            var ProjectId = int.Parse(_helperGeneral.GetValueFromTable(HelperGeneral.DbTimeTable, new string[1, 3]
            { { HelperGeneral.DbTimeTableFieldNameWorkDate, HelperGeneral.DbTimeTableFieldTypeWorkDate, inpEntryDate.Text } }, new string[1, 3]
            { { HelperGeneral.DbTimeTableFieldNameProjectId, HelperGeneral.DbTimeTableFieldTypeProjectId, "" }}));

            var _tempProject = _helperGeneral.GetValueFromTable(HelperGeneral.DbProjectTable, new string[1, 3]
            { { HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId, ProjectId.ToString() } }, new string[1, 3]
            { { HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeName, "" }});

            valueProductId.Text = ProjectId.ToString();

            //Select the saved project in the combobox
            foreach (HelperGeneral.Project project in cboxProject.Items)
            {
                if (project.ProjectName == _tempProject)
                {
                    cboxProject.SelectedItem = project;
                    break;
                }
            }
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
        // check time entries, if also available add button can become visible
        if (inpStartHour.Text != string.Empty && inpStartMinute.Text != string.Empty && inpEndHour.Text != string.Empty && inpEndMinute.Text != string.Empty && valueSelectedWorktype.Text != string.Empty)
        {
            TBAddButtonEnable.Text = "Visible";
        }
        else
        {
            TBAddButtonEnable.Text = "Collapsed";
        }
        TBResetButtonEnable.Text = "Visible";

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
        // check time entries, if also available add button can become visible
        if (inpStartHour.Text != string.Empty && inpStartMinute.Text != string.Empty && inpEndHour.Text != string.Empty && inpEndMinute.Text != string.Empty && valueSelectedWorktype.Text != string.Empty)
        {
            // Only enable add button if save button is not available
            if(TBSaveButtonEnable.Text == "Visible") { TBAddButtonEnable.Text = "Collapsed"; } else { TBAddButtonEnable.Text = "Visible"; }
        }
        else
        {
            TBAddButtonEnable.Text = "Collapsed";
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

    #region Toolbar Reset button pressed 
    private void TimeToolbarButtonReset(object sender, RoutedEventArgs e)
    {
        valueCategoryId.Clear();
        valueEndTime.Clear();
        valueProductId.Clear();
        valueProductUsageId.Clear();
        valueProjectId.Clear();
        valueSelectedWorktype.Clear();
        valueStartTime.Clear();
        valueStockId.Clear();
        valueStocklogId.Clear();
        valueStorageId.Clear();
        valueStoredAmount.Clear();
        valueTimeId.Clear();
        valueWorktypeId.Clear();
        valueSelectedWorktype.Clear();
        cboxProject.SelectedItem = null;
        cboxWorktype.SelectedItem = null;
        inpComment.Clear();
        inpEndHour.Clear();
        inpEndMinute.Clear();
        inpEntryDate.Text = string.Empty;
        inpStartHour.Clear();
        inpStartMinute.Clear();
        dispElapsedTime.Clear();
        dispElapsedTotal.Clear();
        cboxProductEntryEditable.IsChecked = false;
        cboxTimeEntryEditable.IsChecked = false;
        TBAddButtonEnable.Text = "collapsed";
        TBAddTimeEntryEnable.Text = "collapsed";
        TBResetButtonEnable.Text = "collapsed";
        TBSaveButtonEnable.Text = "collapsed";
        TBTimeErrorVisible.Text = "collapsed";

        inpEntryDate.IsEnabled = true;
        cboxProject.IsEnabled = true;
        Time_DataGrid.DataContext = null;
        InitializeComponent();
    }
    #endregion Toolbar Reset button pressed
}
