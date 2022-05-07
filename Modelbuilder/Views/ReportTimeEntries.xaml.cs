namespace Modelbuilder;
/// <summary>
/// Interaction logic for ReportTimeEntries.xaml
/// </summary>
public partial class ReportTimeEntries : Page
{
    private HelperGeneral _helperGeneral;
    private DataTable _dt;
    public string projectName = "geen";
    public string _PreviousContent;

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
        }

        GetData();
    }
    #endregion Selection Changed: Project combobox

   private void Canvas_Drop(object sender, DragEventArgs e)
    {
        Canvas canvas = sender as Canvas;
        Button button = e.Data.GetData("myFormat") as Button;

        GroupFields.Children.Remove(btn_Button); //remove button from first canvas
        //canvas.Children.Add(button); //add button to Canvas
        GroupBy.Children.Add(btn_Button);

        //reposition the button inside the Canvas by setting its Canvas.Left and Canvas.Top properties:
        //Canvas.SetTop(btn_Button, canvas.ActualHeight / 2 - btn_Button.Height / 2);
        //Canvas.SetLeft(btn_Button, canvas.ActualWidth / 2 - btn_Button.Width / 2);

    }

    private void StackPanel_MouseMove(object sender, MouseEventArgs e)
    {
        DependencyObject element = Mouse.DirectlyOver as DependencyObject;
        if (element != null && (element == btn_Button || FindVisualParent<Button>(element) == btn_Button))
        {
            DataObject dragData = new DataObject("myFormat", btn_Button);
            DragDrop.DoDragDrop(GroupBy, dragData, DragDropEffects.Move);
        }
    }

    public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject == null)
            return null;

        T parent = parentObject as T;
        if (parent != null)
            return parent;
        else
            return FindVisualParent<T>(parentObject);
    }
}
