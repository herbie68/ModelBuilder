namespace ConnectionNamespace;

public class Connection_Query
{
    public MySqlConnection connection;

    // Local environment
    public static readonly string server = "localhost";
    //public static readonly string server = "192.168.1.100";
    public static readonly string database = "modelbuilder";
    public static readonly string port = "3306";
    public static readonly string profiler = "4040";
    public static readonly string uid = "root";
    public static readonly string password = "admin";
    //public static readonly string password = "OefenenKHMK24!";
    //SERVER=localhost;PORT=3306;DATABASE=modelbuilder;UID=root;PASSWORD=admin;

    // db4free environment
    // public static readonly string server = "db4free.net";
    // public static readonly string database = "modelbuilder";
    // public static readonly string port = "3306";
    // public static readonly string profiler = "4040";
    // public static readonly string uid = "herbie68";
    // public static readonly string password = "9b9749c1";

    public static string connectionString = "SERVER=" + server + ";PORT=" + port + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
}
