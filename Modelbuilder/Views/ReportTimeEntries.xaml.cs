using System.ComponentModel;
using System.Windows.Data;

namespace Modelbuilder;
/// <summary>
/// Interaction logic for ReportTimeEntries.xaml
/// </summary>
public partial class ReportTimeEntries : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt;
    public string projectName = "";
    public string _PreviousContent;
    public string[] GroupBy = { "", "", "", "" };

    public ReportTimeEntries()
    {
        var ProjectList = new List<HelperGeneral.Project>();

        InitializeComponent();
        InitializeHelper();
        GetData();

        cboxProject.ItemsSource = _helperGeneral.GetProjectList(ProjectList);
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

    #region Get the data
    private void GetData()
    {
        InitializeHelper();
        // Get data from database
        _dt = _helperGeneral.GetData(HelperGeneral.DbTimeReportView, new string[1, 3]
        {
            {HelperGeneral.DbTimeViewFieldNameProjectName, HelperGeneral.DbTimeViewFieldTypeProjectName, projectName }
        });

        TimeReportList.ItemsSource = _dt.DefaultView;
    }
    #endregion

    #region Selection Changed: Project combobox
    private void cboxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (HelperGeneral.Project project in e.AddedItems)
        {
            cboxProject.SelectedItem = project;
            projectName = project.ProjectName.ToString();
            if(projectName != "" && projectName !="Geen") { stackPanelGrouping.Visibility = Visibility.Visible; } else { stackPanelGrouping.Visibility = Visibility.Hidden; }
        }

        GetData();
    }
    #endregion Selection Changed: Project combobox

    private void grpButton_Click(object sender, RoutedEventArgs e)
    {
        //Get the name and content of the pressed button
        var ButtonName = (sender as Button).Name.ToString();
        var ButtonContent = (sender as Button).Content.ToString();

        (sender as Button).Visibility = Visibility.Collapsed;

        btnNumber.Text = (int.Parse(btnNumber.Text) + 1).ToString();
        GroupBy[int.Parse(btnNumber.Text)] = ButtonName.Replace("grp", "");
        switch (btnNumber.Text)
        {
            case "0":
                btnName0.Text = ButtonName;
                C0.Content= ButtonContent;
                C0.Visibility = Visibility.Visible;
                break;
            case "1":
                btnName1.Text = ButtonName;
                C1.Content = ButtonContent;
                C1.Visibility = Visibility.Visible;
                break;
            case "2":
                btnName2.Text = ButtonName;
                C2.Content = ButtonContent;
                C2.Visibility = Visibility.Visible;
                break;
            case "3":
                btnName3.Text = ButtonName;
                C3.Content = ButtonContent;
                C3.Visibility = Visibility.Visible;
                break;
        }
        UpdateGrouping();
    }
    private void ungrpButton_Click(object sender, RoutedEventArgs e)
    {
        //Get the name and content of the pressed button
        var ButtonName = (sender as Button).Name.ToString();
        var ButtonContent = (sender as Button).Content.ToString();
        var _restoreButtonName = "";
        switch (ButtonName)
        {
            case "C0":
                _restoreButtonName = btnName0.Text;
                // Move following Columns 1 position to the left
                btnName0.Text = btnName1.Text;
                btnName1.Text = btnName2.Text;
                btnName2.Text = btnName3.Text;
                btnName3.Text = "";
                C0.Content = C1.Content.ToString(); 
                C1.Content = C2.Content.ToString();
                C2.Content = C3.Content.ToString();
                C3.Content = "";
                if (C0.Content.ToString() == "") { C0.Visibility = Visibility.Collapsed; }
                if (C1.Content.ToString() == "") { C1.Visibility = Visibility.Collapsed; }
                if (C2.Content.ToString() == "") { C2.Visibility = Visibility.Collapsed; }
                C3.Visibility = Visibility.Collapsed;
                break;
            case "C1":
                _restoreButtonName = btnName1.Text;
                // Move following Columns 1 position to the left
                btnName1.Text = btnName2.Text;
                btnName2.Text = btnName3.Text;
                btnName3.Text = "";
                C1.Content = C2.Content.ToString();
                C2.Content = C3.Content.ToString();
                C3.Content = "";
                if (C1.Content.ToString() == "") { C1.Visibility = Visibility.Collapsed; }
                if (C2.Content.ToString() == "") { C2.Visibility = Visibility.Collapsed; }
                C3.Visibility = Visibility.Collapsed;
                break;
            case "C2":
                _restoreButtonName = btnName2.Text;
                // Move Last Column to this possision
                btnName2.Text = btnName3.Text;
                btnName3.Text = "";
                C2.Content = C3.Content.ToString();
                C3.Content = "";
                C3.Visibility = Visibility.Collapsed;
                break;
            case "C3":
                _restoreButtonName = btnName3.Text;
                // Last Column no need to roll back
                btnName3.Text = "";
                C3.Content = "";
                C3.Visibility= Visibility.Collapsed;
                break;
        }

        switch (_restoreButtonName)
        {
            case "grpWorktypeName":
                grpWorktypeName.Visibility= Visibility.Visible;
                break;
            case "grpYear":
                grpYear.Visibility = Visibility.Visible;
                break;
            case "grpMonth":
                grpMonth.Visibility = Visibility.Visible;
                break;
            case "grpDay":
                grpDay.Visibility = Visibility.Visible;
                break;
        }
        GroupBy[0] = btnName0.Text.Replace("grp", "");
        GroupBy[1] = btnName1.Text.Replace("grp", "");
        GroupBy[2] = btnName2.Text.Replace("grp", "");
        GroupBy[3] = btnName3.Text.Replace("grp", "");

        btnNumber.Text = (int.Parse(btnNumber.Text) - 1).ToString();
        UpdateGrouping();
    }

    private void UpdateGrouping()
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(TimeReportList.ItemsSource);
        view.GroupDescriptions.Clear();
        for (int i = 0; i < 4; i++)
        {
            if (GroupBy[i] != "") 
            {
                PropertyGroupDescription groupDescription = new PropertyGroupDescription(GroupBy[i]);
                view.GroupDescriptions.Add(groupDescription);
            }
        }
        //PropertyGroupDescription groupDescription = new PropertyGroupDescription(Selection);
        //view.GroupDescriptions.Add(groupDescription);

    }
}
