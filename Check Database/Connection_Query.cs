using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check_Database
{
    /// <summary>
    /// The connection_ query.
    /// </summary>
    public class Connection_Query
    {
        public MySqlConnection? connection;
        // Local environment
        public static readonly string server = "localhost";
        public static readonly string database = "modelbuilder";
        public static readonly string port = "3306";
        public static readonly string profiler = "4040";
        public static readonly string uid = "root";
        public static readonly string password = "admin";
        //SERVER=localhost;PORT=3306;DATABASE=modelbuilder;UID=root;PASSWORD=admin;

        public static string connectionString = "SERVER=" + server + ";PORT=" + port + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";

    }
}
