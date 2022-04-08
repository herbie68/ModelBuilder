using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;

public class CostEntry
{
    public string ProjectName { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public double AmountUsed    {get; set;}
    public double Price         {get; set;}
    public double Total         {get; set;}
    public static string NoGroup
    {
        get { return "Totaal"; }
    }
}

public class CostEntries : ObservableCollection<CostEntry>
{
    public CostEntries(string ProjectName)
    {
        var sqlText = "SELECT * FROM " + HelperGeneral.DbProjectCostsView;

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
            double _total = (double)_records["Total"];
            var Total = _total;
            var AmountUsed = (double)_records["AmountUsed"];
            var CategoryName = (string)_records["Category"];
            this.Add(new CostEntry
            {
                ProjectName = (string)_records["ProjectName"],
                CategoryName = (string)_records["Category"],
                ProductName = (string)_records["ProductName"],
                AmountUsed = (double)_records["AmountUsed"],
                Price = (double)_records["Price"],
                Total = (double)_records["Total"]
            });
        }
    }
}