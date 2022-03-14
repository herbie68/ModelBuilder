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

        InitializeComponent ();
        InitializeHelper ();
        DataContext = new TimeViewModel();

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

    #region CommonCommandBinding_CanExecute
    private void CommonCommandBinding_CanExecute ( object sender, CanExecuteRoutedEventArgs e )
    {
        e.CanExecute = true;
    }
    #endregion CommonCommandBinding_CanExecute


    #region Selection Changed: Project combobox
    private void cboxProject_SelectionChanged ( object sender, SelectionChangedEventArgs e )
    {
        foreach (HelperGeneral.Project project in e.AddedItems)
        {
            cboxProject.SelectedItem = project;
            //valueProjectId.Text = project.ProjectId.ToString ();
            //dispSelectedProject.Text = project.ProjectName.ToString ();
        }
    }
    #endregion Selection Changed: Project combobox
}
