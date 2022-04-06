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

namespace Modelbuilder;

/// <summary>
/// Interaction logic for ReportingTime.xaml
/// </summary>
public partial class ReportingTime : Page
{
    private HelperGeneral _helperGeneral;
    public ReportingTime ()
    {
        var ProjectList = new List<HelperGeneral.Project> ();
        String ProjectName = ""; 
        InitializeComponent ();
        InitializeHelper ();
        DataContext = new TimeViewModel(ProjectName);

        cboxProject.ItemsSource = _helperGeneral.GetProjectList ( ProjectList );

    }

    #region InitializeHelper (connect to database)
    private void InitializeHelper ()
    {
        if (_helperGeneral == null)
        {
            _helperGeneral = new HelperGeneral ( Connection_Query.server, int.Parse ( Connection_Query.port ), Connection_Query.database, Connection_Query.uid, Connection_Query.password );
        }
    }
    #endregion

    #region Selection Changed: Project combobox
    private void cboxProject_SelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        var projectName="";
        foreach (HelperGeneral.Project project in e.AddedItems)
        {
            cboxProject.SelectedItem = project;
            projectName = project.ProjectName.ToString ();
            btnGroupProject.IsEnabled = false;
        }
        DataContext = new TimeViewModel(projectName);
        if(projectName=="" || projectName == "Geen") 
        {
            btnGroupProject.IsEnabled = true;
        }
    }
    #endregion Selection Changed: Project combobox
}
