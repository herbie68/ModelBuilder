using ConnectionNamespace;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder
{
    public class Database
    {
        public string TableName { get; set; }
        public string DataTable { get; set; }
        public string SqlCommand { get; set; }
        public string SqlCommandString { get; set; }
        public string SqlSelectionString { get; set; }
        public string SqlOrderByString { get; set; }
        public string SqlWhereString { get; set; }

        public static MySqlConnection myConnection = null;

        public void Connect()
        {
            myConnection = new MySqlConnection(Connection_Query.connectionString);
            MySqlConnection connection = myConnection;

            connection.Open();
        }

        public DataTable LoadMySqlData()
        {
            string mySelectQuery = "SELECT * FROM " + Connection_Query.database + "." + TableName;
            MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

            myConnection.Open();

            var myCommand = new MySqlCommand(mySelectQuery, myConnection);

            DataTable tempDatatable = new DataTable();
            tempDatatable.Load(myCommand.ExecuteReader());
            myConnection.Close();

            return tempDatatable;
        }

        public DataTable LoadSpecificMySqlData()
        {
            string OrderString= "", WhereString = "";

            if (SqlOrderByString != "")
            {
                if (SqlOrderByString != null) OrderString = " ORDER BY " + SqlOrderByString;
            }
            else
            {
                OrderString = "";
            }

            if (SqlWhereString != "")
            {
                if (SqlWhereString != null) WhereString = " WHERE " + SqlWhereString;
            }
            else
            {
                WhereString = "";
            }

            string mySelectQuery = "SELECT " + SqlSelectionString + " FROM " + Connection_Query.database + "." + TableName + WhereString + OrderString;
            
            MySqlConnection myConnection = new MySqlConnection(Connection_Query.connectionString);

            myConnection.Open();

            var myCommand = new MySqlCommand(mySelectQuery, myConnection);

            DataTable tempDataTable = new DataTable();
            tempDataTable.Load(myCommand.ExecuteReader());

            return tempDataTable;
        }
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

            var myCommand = new MySqlCommand(myRetrieveQuery, myConnection);

            MySqlDataReader retrieveData = myCommand.ExecuteReader();

            retrieveData.Read();
            var reader = retrieveData.GetOrdinal("currency_Id");
            
            int ID = retrieveData.GetInt32(reader);
            
            myConnection.Close();

            return ID;
        }
    }
}
