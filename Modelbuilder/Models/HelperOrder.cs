using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using ConnectionNamespace;

namespace Modelbuilder;

internal class HelperOrder
{
    #region public Variables
    public string ConnectionStr { get; set; } = string.Empty;

    public string DbCategoryTable = "category";
    public string DbCurrencyTable = "currency";
    public string DbProductTable = "product";
    public string DbProductSupplierTable = "productsupplier";
    public string DbProjectTable = "project";
    public string DbSupplierTable = "supplier";

    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables

    #region Connector to database
    public HelperOrder(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperOrder(string serverName, int portNumber, string databaseName, string username, string userPwd)
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

    #region Get Data from Table: Order
    public DataTable GetDataTblOrder(int Id = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT * from Order where order_Id = @Id";
        }
        else
        {
            sqlText = "SELECT * from Order";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }

        return dt;
    }
    #endregion Get Data from Table: Order

    #region Get Data from Table: Orderline
    public DataTable GetDataTblOrderline(int Id = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT * from Orderline where orderline_OrderId = @Id";
        }
        else
        {
            sqlText = "SELECT * from Orderline";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }

        return dt;
    }
    #endregion Get Data from Table: Orderline

    #region Get Data from Table: Currency (GetConversionrate)
    public string GetConversionRate(int Id = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;
        string resultString = String.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT currency_ConversionRate from Currency where currency_Id = @Id";
        }
        else
        {
            sqlText = "SELECT * from Currency";
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);
        cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;

        MySqlDataReader retrieveData = cmd.ExecuteReader();

        retrieveData.Read();

        // These two rows are cemmented out because its causing an error when storing a Supplier
        // Unsure why in this general read function specificly a currency Id is read.
        // var reader = retrieveData.GetOrdinal("currency_Id");
        // int ID = retrieveData.GetInt32(reader);

        //string reader = retrieveData.GetOrdinal("currency_ConversionRate");
        //string Rate = retrieveData.GetInt32(reader);
        resultString = retrieveData.GetString("0");
        con.Close();

        return resultString;
    }
    #endregion Get Data from Table: Orderline


    #region Create lists to populate dropdowns for metadata pages
    #region Fill the dropdownlists
    #region Fill Supplier dropdown
    public List<Supplier> GetSupplierList(List<Supplier> supplierList)
    {
        string DatabaseTable = DbSupplierTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "supplier_Name, supplier_Id, supplier_CurrencySymbol, supplier_CurrencyId";
        dbConnection.SqlOrderByString = "supplier_Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            supplierList.Add(new Supplier
            {
                SupplierName = dtSelection.Rows[i][0].ToString(),
                SupplierId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture),
                SupplierCurrencySymbol = dtSelection.Rows[i][2].ToString(),
                SupplierCurrencyId = int.Parse(dtSelection.Rows[i][3].ToString(), Culture)
            });
        };
        return supplierList;
    }
    #endregion

    #region Fill the Project dropdown
    public List<Project> GetProjectList(List<Project> projectList)
    {
        string DatabaseTable = DbProjectTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "project_Name, project_Id";
        dbConnection.SqlOrderByString = "project_Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            projectList.Add(new Project
            {
                ProjectName = dtSelection.Rows[i][0].ToString(),
                ProjectId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        };
        return projectList;
    }
    #endregion Fill the Project dropdown

    #region Fill the Product dropdown
    public List<Product> GetProductList(List<Product> productList)
    {
        string DatabaseTable = DbProductTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "product_Name, product_Id";
        dbConnection.SqlOrderByString = "product_Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            productList.Add(new Product
            {
                ProductName = dtSelection.Rows[i][0].ToString(),
                ProductId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        };
        return productList;
    }
    #endregion Fill the Project dropdown
    #endregion Fill the dropdownlists

    #region Helper classes to for creating objects to populate dropdowns
    #region Create object for all suppliers in table for dropdown
    public class Supplier
    {
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCurrencySymbol { get; set; }
        public int SupplierCurrencyId { get; set; }
    }
    #endregion Create object for all suppliers in table for dropdown

    #region Create object for all projects in table for dropdown
    public class Project
    {
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
    }
    #endregion Create object for all projects in table for dropdown

    #region Create object for all products in table for dropdown
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
    #endregion Create object for all products in table for dropdown    #endregion Helper classes to for creating objects to populate dropdowns
    #endregion Create lists to populate dropdowns for metadata pages
    #endregion Create lists to populate dropdowns for metadata pages
}
