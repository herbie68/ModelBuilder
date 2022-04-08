using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Modelbuilder;

public class ProjectCostsViewModel
{
    public ICollectionView ProjectCostsView { get; set; }
    public string ProjectName { get; set; }
    public ProjectCostsViewModel(string ProjectName)
    {
        IList<CostEntry> ProjectCostsViewentries = new CostEntries(ProjectName);
        ProjectCostsView = CollectionViewSource.GetDefaultView(ProjectCostsViewentries);
        ProjectCostsView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));

        groupByProjectCostsCategoryCommand = new GroupByProjectCostsCategoryCommand(this);
        RemoveProjectCostsGroupCommand = new RemoveProjectCostsGroupCommand(this);
    }

    #region Remove Group
    public void RemoveProjectCostsGroup()
{
        ProjectCostsView.GroupDescriptions.Clear();
        ProjectCostsView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));
    }
    #endregion Remove Group

    #region Group by Category
    public void GroupByProjectCostsCategory()
{
        ProjectCostsView.GroupDescriptions.Clear();
        ProjectCostsView.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
    }
    #endregion Group by Category

    #region ICommand Remove Group Command
    public ICommand RemoveProjectCostsGroupCommand
    {
        get;
        private set;
    }
    #endregion ICommand Remove Group Command

    #region ICommand Group by Category Command
    public ICommand groupByProjectCostsCategoryCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by Category Command
}

#region Get totals by group
public class ProjectGroupsToTotalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is ReadOnlyObservableCollection<object>)
        {
            var items = (ReadOnlyObservableCollection<object>)value;
            var total = 0.00;
            foreach (CostEntry element in items)
            {
                total += element.Total;
            }

            return total;
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value;
    }
}
#endregion Get totals by group

