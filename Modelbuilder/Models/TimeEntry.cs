using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;

public class TimeEntry
{
    public string ProjectName       { get; set; }
    public string WorktypeName      { get; set; }
    public string EntryDate         { get; set; }
    public string Year              { get; set; }
    public string YearMonth         { get; set; }
    public string Month             { get; set; }
    public int DayNo                { get; set; }
    public string Day               { get; set; }
    public string YearDay           { get; set; }
    public string Worktime          { get; set; }
    public int WorkedMinutes        { get; set; }
    
    public static string NoGroup
    {
        get { return "Totaal"; }
    }
}

public class TimeEntries : ObservableCollection<TimeEntry>
{
    public TimeEntries(string ProjectName)
    {
        var sqlText = "SELECT * FROM " + HelperGeneral.DbTimeReportView;
        
        if (ProjectName != "" && ProjectName != "Geen")
        {
            sqlText += " WHERE ProjectName='" + ProjectName + "'"; 
        }
        MySqlConnection con = new MySqlConnection(ConnectionNamespace.Connection_Query.connectionString);
        con.Open();
        MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), con);
        MySqlDataReader _records = cmd.ExecuteReader();

        while (_records.Read())
        {
            double _minutes = Convert.ToInt32((double)_records["WorkedMinutes"]);
            int Minutes = Convert.ToInt32(_minutes);
            this.Add(new TimeEntry
            {
                ProjectName = (string)_records["ProjectName"],
                WorktypeName = (string)_records["WorktypeName"],
                EntryDate = ((string)_records["WorkDate"]).Substring(8 ,2) + "-" + ((string)_records["WorkDate"]).Substring(5, 2) + "-" + ((string)_records["WorkDate"]).Substring(0 , 4),
                Year = (string)_records["Year"],
                YearMonth = (string)_records["YearMonth"],
                Month = (string)_records["Month"],
                Day = (string)_records["Day"],
                YearDay = (string)_records["YearDay"],
                Worktime = (string)_records["WorkTime"],
                WorkedMinutes = Convert.ToInt32((double)_records["WorkedMinutes"])
            });
        }
    }
}