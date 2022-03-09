namespace Modelbuilder;
internal class HelperTimeManagement
{
    #region public Variables
    public string ConnectionStr { get; set; }
	
    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables
	
    #region Connector to database
    public HelperTimeManagement(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperTimeManagement(string serverName, int portNumber, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
    }
    #endregion Connector to database

    #region Execute Non Query
    public void ExecuteNonQuery(string sqlText)
    {
        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    #endregion Execute NonQuery

    #region Get Data from TableView or Table
    //public DataTable GetData ( string Table, string SelectString, string[,] WhereFields, string OrderBy ="")
    public List<WorkingHours> GetWorkingHoursList(List<WorkingHours> workinghoursList, string Table, string SelectString, string[,] WhereFields, string OrderBy = "")
    {
        DataTable dt = new ();

        StringBuilder sqlText = new StringBuilder ( "SELECT " );
        sqlText.Append ( SelectString );
        sqlText.Append(" FROM ");
        sqlText.Append ( Table.ToLower () );
        sqlText.Append ( " WHERE " );

        string prefix = "";

        if (WhereFields.GetLength ( 0 ) > 0)
        {
            for (int i = 0; i < WhereFields.GetLength ( 0 ); i++)
            {
                if (i != 0) { prefix = " AND "; }
                sqlText.Append ( prefix );
                sqlText.Append ( WhereFields[i, 0] );
                sqlText.Append ("=@");
                sqlText.Append ( WhereFields[i, 0] );
            }
        }

        if (OrderBy != string.Empty) { sqlText.Append ( " ORDER BY " ); sqlText.Append(OrderBy); }
        
        MySqlConnection con = new MySqlConnection ( ConnectionStr );

        con.Open ();

        MySqlCommand cmd = new MySqlCommand ( sqlText.ToString(), con );

        for (int i = 0; i < WhereFields.GetLength ( 0 ); i++)
        {
            switch (WhereFields[i, 1].ToLower ())
            {
                case "string":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.String ).Value = WhereFields[i, 2];
                    break;
                case "int":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.Int32 ).Value = int.Parse ( WhereFields[i, 2] );
                    break;
                case "double":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.Double ).Value = double.Parse ( WhereFields[i, 2] );
                    break;
                case "float":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.Float ).Value = float.Parse ( WhereFields[i, 2] );
                    break;
                case "time":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.String ).Value = WhereFields[i, 2];
                    break;
                case "date":
                    String[] _tempDates = WhereFields[i, 2].Split ( "-" );
                    var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.String ).Value = _tempDate;
                    break;
            }
        }

        using MySqlDataAdapter da = new ( cmd );
        da.Fill ( dt );

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            workinghoursList.Add(new WorkingHours
            {
                StartTime = int.Parse(dt.Rows[i][0].ToString(), Culture),
                EndTime = int.Parse(dt.Rows[i][1].ToString(), Culture)
            });
        }
        return workinghoursList;
    }
    #endregion Get Data from TableView or Table

    #region Create object for all workinghours in table for specific date
    public class WorkingHours
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
    #endregion Create object for all workinghours in table for specific date

}

