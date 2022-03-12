using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Modelbuilder;

public class TimeViewModel
{
    public ICollectionView TimeView { get; set; }
    public TimeViewModel()
    {
        IList<TimeEntry> entries = new TimeEntries();
        TimeView = CollectionViewSource.GetDefaultView(entries);
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));

        groupByProjectCommand = new GroupByProjectCommand(this);
        groupByWorktypeCommand = new GroupByWorktypeCommand(this);
        groupByYearCommand = new GroupByYearCommand(this);
        groupByMonthCommand = new GroupByMonthCommand(this);
        groupByYearMonthCommand = new GroupByYearMonthCommand(this);
        groupByDayCommand = new GroupByDayCommand(this);
        groupByYearDayCommand = new GroupByYearDayCommand(this);
        removeGroupCommand = new RemoveGroupCommand(this);
    }

    #region Remove Group
    public void RemoveGroup()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));
    }
    #endregion Remove Group

    #region Group by Project
    public void GroupByProject()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("ProjectName"));
    }
    #endregion Group by Project

    #region Group by Worktype
    public void GroupByWorktype()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("WorktypeName"));
    }
    #endregion Group by Worktype

    #region Group by Year
    public void GroupByYear()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("Year"));
    }
    #endregion Group by Year

    #region Group by Month
    public void GroupByMonth()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("Month"));
    }
    #endregion Group by Month

    #region Group by YearMonth
    public void GroupByYearMonth()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("YearMonth"));
    }
    #endregion Group by YearMonth

    #region Group by Day
    public void GroupByDay()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("Day"));
    }
    #endregion Group by Day

    #region Group by YearDay
    public void GroupByYearDay()
    {
        TimeView.GroupDescriptions.Clear();
        TimeView.GroupDescriptions.Add(new PropertyGroupDescription("YearDay"));
    }
    #endregion Group by YearDay

    #region ICommand Group by Project Command
    public ICommand groupByProjectCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by Project Command

    #region ICommand Group by Worktype Command
    public ICommand groupByWorktypeCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by Worktype Command

    #region ICommand Group by Year Command
    public ICommand groupByYearCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by Year Command

    #region ICommand Group by Month Command
    public ICommand groupByMonthCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by Month Command

    #region ICommand Group by YearMonth Command
    public ICommand groupByYearMonthCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by YearMonth Command

    #region ICommand Group by Day Command
    public ICommand groupByDayCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by Day Command

    #region ICommand Group by YearDay Command
    public ICommand groupByYearDayCommand
    {
        get;
        private set;
    }
    #endregion ICommand Group by YearDay Command

    #region ICommand Remove Group Command
    public ICommand removeGroupCommand
    {
        get;
        private set;
    }
    #endregion ICommand Remove Group Command
}

#region Get totals by group
public class GroupsToTotalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is ReadOnlyObservableCollection<object>)
        {
            var items = (ReadOnlyObservableCollection<object>)value;
            Decimal total = 0;
            foreach (TimeEntry element in items)
            {
                total += element.WorkedMinutes;
            }
            return total.ToString();
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value;
    }
}
#endregion Get totals by group