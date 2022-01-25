using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MySql;

namespace Check_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database _helper;
        private DataTable _dt;
        public string TableName="";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow() => InitializeComponent();


        #region InitializeHelper (connect to database)
        private void InitializeHelper()
        {
            if (_helper == null)
            {
                _helper = new Database(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
            }
        }
        #endregion InitializeHelper (connect to database)


        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void GetSchema(object sender, RoutedEventArgs e)
        {
            var Tables = new List<tblList>();
            var Views = new List<viewList>();

            GetTableList(Tables);
            GetViewList(Views);


            
        }

        public List<tblList> GetTableList(List<tblList> tableList) 
        {
            InitializeHelper();
            _dt = _helper.GetDatabaseTable();

            if (_dt.Rows.Count != 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i][0].ToString().ToLower().Length <= 4)
                    {
                        //The tablename is to short to contain view_ therefore is always a table
                        tableList.Add(new tblList
                        {
                            TableName = _dt.Rows[i][0].ToString().ToLower()
                        });
                    }
                    else
                    {
                        if (_dt.Rows[i][0].ToString().ToLower().Substring(0, 5) != "view_")
                        {
                            tableList.Add(new tblList
                            {
                                TableName = _dt.Rows[i][0].ToString().ToLower()
                            });
                        }
                    }
                };
            }
            return tableList;
        }

        public List<viewList> GetViewList(List<viewList> viewsList)
        {
            InitializeHelper();
            _dt = _helper.GetDatabaseTable();

            if (_dt.Rows.Count != 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    if (_dt.Rows[i][0].ToString().ToLower().Length >= 5)
                    {
                        //Names with less then 5 charachters is always a table because name cannot contain the prefix view_
                        if (_dt.Rows[i][0].ToString().ToLower().Substring(0, 5) == "view_")
                        {
                            viewsList.Add(new viewList
                            {
                                ViewName = _dt.Rows[i][0].ToString().ToLower()
                            });
                        }
                    }
                };
            }
            return viewsList;
        }


        #region Create object for all tables in database
        public class tblList
        {
            public string TableName { get; set; }
        }
        #endregion

        #region Create object for all tableviews in database
        public class viewList
        {
            public string ViewName { get; set; }
        }
        #endregion
    }
}
