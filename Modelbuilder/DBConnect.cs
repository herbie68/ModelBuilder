using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//Add MySql Library
using MySql.Data.MySqlClient;

namespace ModelbouwBeheer
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string port;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            port = "3306"; // Direct connection to MySql Server
            // port = "4040"; // Use Neor profiler
            database = "modelbuilder";
            uid = "root";
            password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";PORT=" + port + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                //connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Kan geen verbinding maken met de server.");
                        break;

                    case 1045:
                        MessageBox.Show("Ongeldige gebruikersnaam of wachtwoord om de database te openen");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                //connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

/*        //Insert statement
        public void Insert()
        {
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public List<string>[] Select()
        {
        }

        //Count statement
        public int Count()
        {
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
*/    }
}
