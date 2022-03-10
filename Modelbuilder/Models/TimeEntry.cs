using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;

public class TimeEntry
{
    public int ProjectId            { get; set; }
    public string ProjectName       { get; set; }
    public int WorktypeId           { get; set; }
    public string WorktypeName      { get; set; }
    public string EntryDate         { get; set; }
    public string Year                 { get; set; }
    public string YearMonth            { get; set; }
    public string Month                { get; set; }
    public int DayNo                { get; set; }
    public string Day               { get; set; }
    public string YearDay              { get; set; }
    public string Worktime          { get; set; }
    
    public string NoGroup
    {
        get { return "Total"; }
    }

    public class TimeEntries : ObservableCollection<TimeEntry>
    {
        public TimeEntries()
        {
            this.Add(new TimeEntry
            {
                ProjectId = 1,
                ProjectName = "SILHOUET",
                WorktypeId = 1,
                WorktypeName = "Afwerking",
                EntryDate = "01-01-2021",
                Year = "2021",
                YearMonth = "202101",
                Month = "01",
                DayNo = 0,
                Day = "Zondag",
                YearDay = "20210",
                Worktime = "04:30"
            });
            this.Add(new TimeEntry
            {
                ProjectId = 1,
                ProjectName = "SILHOUET",
                WorktypeId = 1,
                WorktypeName = "Afwerking",
                EntryDate = "02-01-2021",
                Year = "2021",
                YearMonth = "202101",
                Month = "01",
                DayNo = 1,
                Day = "Maandag",
                YearDay = "20211",
                Worktime = "04:00"
            });
            this.Add(new TimeEntry
            {
                ProjectId = 1,
                ProjectName = "SILHOUET",
                WorktypeId = 2,
                WorktypeName = "Dek",
                EntryDate = "03-01-2021",
                Year = "2021",
                YearMonth = "202101",
                Month = "01",
                DayNo = 2,
                Day = "Dinsdag",
                YearDay = "20212",
                Worktime = "03:00"
            });
            this.Add(new TimeEntry
            {
                ProjectId = 2,
                ProjectName = "ZUIDERZEEBOTTER",
                WorktypeId = 1,
                WorktypeName = "Afwerking",
                EntryDate = "01-01-2022",
                Year = "2022",
                YearMonth = "202201",
                Month = "01",
                DayNo = 1,
                Day = "Maandag",
                YearDay = "20221",
                Worktime = "04:30"
            });
            this.Add(new TimeEntry
            {
                ProjectId = 1,
                ProjectName = "ZUIDERZEEBOTTER",
                WorktypeId = 1,
                WorktypeName = "Afwerking",
                EntryDate = "02-01-2022",
                Year = "2022",
                YearMonth = "202201",
                Month = "01",
                DayNo = 2,
                Day = "Dinsdag",
                YearDay = "20222",
                Worktime = "04:00"
            });
            this.Add(new TimeEntry
            {
                ProjectId = 1,
                ProjectName = "ZUIDERZEEBOTTER",
                WorktypeId = 2,
                WorktypeName = "Dek",
                EntryDate = "03-01-2022",
                Year = "2022",
                YearMonth = "202201",
                Month = "01",
                DayNo = 3,
                Day = "Woennsdag",
                YearDay = "20223",
                Worktime = "03:00"
            });
        }
    }
}
