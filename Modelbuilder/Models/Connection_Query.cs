using MySql.Data.MySqlClient;

//namespace ModelbouwBeheer
namespace ConnectionNamespace
{
    public class Connection_Query
    {
        public MySqlConnection connection;
        public static readonly string server = "localhost";
        public static readonly string database = "modelbuilder";
        public static readonly string port = "3306";
        public static readonly string profiler = "4040";
        public static readonly string uid = "root";
        public static readonly string password = "admin";

        public static string connectionString = "SERVER=" + server + ";PORT=" + port + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
    }
}