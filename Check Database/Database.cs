using MySql.Data.MySqlClient;

using System.Data;

namespace Check_Database;

/// <summary>
/// The Database.
/// </summary>
public class Database
{
    public static MySqlConnection? myConnection = null;
    /// <summary>
    /// Gets or Sets the connection str.
    /// </summary>
    public string ConnectionStr { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public void Connect()
    {
        myConnection = new MySqlConnection(Connection_Query.connectionString);
        MySqlConnection connection = myConnection;
    }

    #region Connector to database
    /// <summary>
    /// Initializes a new instance of the <see cref="Database"/> class.
    /// </summary>
    /// <param name="serverName">The server name.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="username">The username.</param>
    /// <param name="userPwd">The user pwd.</param>
    public Database(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Databse"/> class.
    /// </summary>
    /// <param name="serverName">The server name.</param>
    /// <param name="portNumber">The port number.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="username">The username.</param>
    /// <param name="userPwd">The user pwd.</param>
    public Database(string serverName, int portNumber, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
    }
    #endregion Connector to database

    /// <summary>
    /// Loads the my sql data.
    /// </summary>
    /// <returns>A DataTable.</returns>
    public DataTable LoadMySqlSchema()
    {
        //string mySelectQuery = "SELECT * FROM " + Connection_Query.database + "." + TableName;
        string mySelectQuery = "SELECT * FROM " + Connection_Query.database;
        MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

        myConnection.Open();

        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);

        DataTable tempDatatable = new DataTable();
        tempDatatable.Load(myCommand.ExecuteReader());
        myConnection.Close();

        return tempDatatable;
    }

    /// <summary>
    /// Gets all tables from the datbase
    /// </summary>
    /// <returns></returns>
    public DataTable GetDatabaseTable()
    {
        string myQuery = "SHOW TABLES;";

        MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

        myConnection.Open();

        MySqlCommand myCommand = new MySqlCommand(myQuery, myConnection);

        DataTable tempDatatable = new DataTable();
        tempDatatable.Load(myCommand.ExecuteReader());
        myConnection.Close();

        return tempDatatable;
    }
}
