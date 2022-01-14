using ConnectionNamespace;

namespace Modelbuilder
{
    /// <summary>
    /// The database.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Gets or Sets the table name.
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Gets or Sets the data table.
        /// </summary>
        public string DataTable { get; set; }
        /// <summary>
        /// Gets or Sets the sql command.
        /// </summary>
        public string SqlCommand { get; set; }
        /// <summary>
        /// Gets or Sets the table id.
        /// </summary>
        public string TableId { get; set; }
        /// <summary>
        /// Gets or Sets the sql command string.
        /// </summary>
        public string SqlCommandString { get; set; }
        /// <summary>
        /// Gets or Sets the sql selection string.
        /// </summary>
        public string SqlSelectionString { get; set; }
        /// <summary>
        /// Gets or Sets the sql order by string.
        /// </summary>
        public string SqlOrderByString { get; set; }
        /// <summary>
        /// Gets or Sets the sql where string.
        /// </summary>
        public string SqlWhereString { get; set; }

        public static MySqlConnection myConnection = null;

        /// <summary>
        /// 
        /// </summary>
        public void Connect()
        {
            myConnection = new MySqlConnection(Connection_Query.connectionString);
            MySqlConnection connection = myConnection;

            connection.Open();
        }

        /// <summary>
        /// Loads the my sql data.
        /// </summary>
        /// <returns>A DataTable.</returns>
        public DataTable LoadMySqlData()
        {
            string mySelectQuery = "SELECT * FROM " + Connection_Query.database + "." + TableName;
            MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);

            DataTable tempDatatable = new DataTable();
            tempDatatable.Load(myCommand.ExecuteReader());
            myConnection.Close();

            return tempDatatable;
        }

        /// <summary>
        /// Loads the specific my sql data.
        /// </summary>
        /// <returns>A DataTable.</returns>
        public DataTable LoadSpecificMySqlData()
        {
            string OrderString = "", WhereString = "";

            if (SqlOrderByString != "")
            {
                if (SqlOrderByString != null)
                {
                    OrderString = " ORDER BY " + SqlOrderByString;
                }
            }
            else
            {
                OrderString = "";
            }

            if (SqlWhereString != "")
            {
                if (SqlWhereString != null)
                {
                    WhereString = " WHERE " + SqlWhereString;
                }
            }
            else
            {
                WhereString = "";
            }

            string mySelectQuery = "SELECT " + SqlSelectionString + " FROM " + Connection_Query.database + "." + TableName + WhereString + OrderString;

            MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);

            DataTable tempDataTable = new DataTable();
            tempDataTable.Load(myCommand.ExecuteReader());

            return tempDataTable;
        }

        /// <summary>
        /// Updates the my sql data record.
        /// </summary>
        /// <returns>An int.</returns>
        public int UpdateMySqlDataRecord()
        {
            MySqlCommand SqlCmdUpdate = new MySqlCommand
            {
                Connection = Database.myConnection,
                CommandText = SqlCommand + Connection_Query.database + "." + TableName + SqlCommandString
            };

            MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

            myConnection.Open();

            SqlCmdUpdate.ExecuteNonQuery();
            long ID = SqlCmdUpdate.LastInsertedId;

            myConnection.Close();

            return (int)ID;
        }

        /// <summary>
        /// Retrieves the specific id from my sql data.
        /// </summary>
        /// <returns>An int.</returns>
        public int RetrieveSpecificIdFromMySqlData()
        {
            string WhereString;

            if (SqlOrderByString != "")
            {
                WhereString = " WHERE " + SqlWhereString;
            }
            else
            {
                WhereString = "";
            }

            string myRetrieveQuery = "SELECT " + SqlCommandString + " FROM " + Connection_Query.database + "." + TableName + WhereString;
            MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

            myConnection.Open();

            MySqlCommand myCommand = new MySqlCommand(myRetrieveQuery, myConnection);

            MySqlDataReader retrieveData = myCommand.ExecuteReader();

            retrieveData.Read();

            // These two rows are cemmented out because its causing an error when storing a Supplier
            // Unsure why in this general read function specificly a currency Id is read.
            // var reader = retrieveData.GetOrdinal("Id");
            // int ID = retrieveData.GetInt32(reader);

            int reader = retrieveData.GetOrdinal(TableId);
            int ID = retrieveData.GetInt32(reader);

            myConnection.Close();

            return ID;
        }
    }
}