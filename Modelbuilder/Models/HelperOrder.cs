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
using static Modelbuilder.HelperMySQL;
using Org.BouncyCastle.Math;
using static Modelbuilder.HelperOrder;

namespace Modelbuilder;

internal class HelperOrder
{
    #region Available databasefields
    /// ***************************************************************************************
    ///   Available fields for Order table
    /// ***************************************************************************************
    ///   Table Fieldname           	Variable            Parameter      		Type
    ///   -----------------------------------------------------------------------------------
    ///   order_Id						Id					@Id					int
    ///   order_SupplierId				SupplierId			@SupplierId			int
    ///   order_Date 					OrderDate			@OrderDate			date
    ///   order_CurrencySymbol 			CurrencySymbol		@CurrencySymbol		string(2)
    ///   order_CurrencyConversionRate 	ConversionRate		@ConversionRate		float
    ///   order_ShippingCosts 			ShippingCosts		@ShippingCosts		double
    ///   order_OrderCosts 				OrderCosts			@OrderCosts			double
    ///   order_Memo 					Memo				@Memo				string
    ///   order_Closed 					OrderClosed			@OrderClosed		bool (0 or 1)
    ///   order_ClosedDate 				OrderClosedDate		@OrderClosedDate	date
    /// ***************************************************************************************
    /// order_Id, order_SupplierId, order_Date, order_CurrencySymbol, order_CurrencyConversionRate, order_ShippingCosts, order_OrderCosts, order_Memo, order_Closed, order_ClosedDate
    /// Id, SupplierId, OrderDate, CurrencySymbol, ConversionRate, ShippingCosts, OrderCosts, Memo, OrderClosed, OrderClosedDate
    /// @Id, @SupplierId, @OrderDate, @CurrencySymbol, @ConversionRate, @ShippingCosts, @OrderCosts, @Memo, @OrderClosed, @OrderClosedDate

    /// ***************************************************************************************
    ///   Available fields for Orderline table
    /// ***************************************************************************************
    ///   Table Fieldname               Variable            Parameter           Type
    ///   ----------------------------------------------------------------------------------
    ///   orderline_Id                  Id                  @Id                 int
    ///   orderline_OrderId             OrderId             @OrderId            int
    ///   orderline_ProductId           ProductId           @ProductId          int
    ///   orderline_ProjectId           ProjectId           @ProjectId          int
    ///   orderline_CategoryId          CategoryId          @CategoryId         int
    ///   orderline_Number              Number              @Number             double
    ///   orderline_Price               Price               @Price              double
    ///   orderline_RealRowTotal        RealRowTotal        @RealRowTot         double
    ///   orderline_Closed              RowClosed           @RowClosed          bool (0 or 1)
    ///   orderline_ClosedDate          RowClosedDate       @RowClosedDate      date
    /// ***************************************************************************************
    // orderline_Id, orderline_OrderId, orderline_ProductId, orderline_ProjectId, orderline_CategoryId, orderline_Number, orderline_Price, orderline_RealRowTotal, orderline_Closed, orderline_ClosedDate 
    // Id, OrderId, ProductId, ProjectId, CategoryId, Number, Price, RealRowTotal, RowClosed, RowClosedDate 
    // @Id, @OrderId, @ProductId, @ProjectId, @CategoryId, @Number, @Price, @RealRowTotal, @RowClosed, @RowClosedDate 
    #endregion Available databasefields

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
        float resultFloat = 0;

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

        resultFloat = (float)cmd.ExecuteScalar();

        resultString=resultFloat.ToString();   

        return resultString;
    }
    #endregion Get Data from Table: Orderline

    #region Get Single Data from provided Table
    public string GetSingleData(int Id = 0, string Table = "", string Field = "", string Type = "")
    {
        string sqlText = string.Empty;
        string resultString = String.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT " + Field + " from " + Table + " where " + Table.ToLower() + "_Id = @Id";
        }
        else
        {
            sqlText = "SELECT * from " + Table;
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);
        cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;

        switch (Type.ToLower())
        {
            case "double":
                resultString = ((double)cmd.ExecuteScalar()).ToString();
                break;
            case "float":
                resultString = ((float)cmd.ExecuteScalar()).ToString();
                break;
            case "int":
                resultString = ((int)cmd.ExecuteScalar()).ToString();
                break;
            case "string":
                resultString= (string)cmd.ExecuteScalar();
                break;
            default:
                resultString = (string)cmd.ExecuteScalar();
                break;
        }

        return resultString;
    }
    #endregion Get Single Data from provided Table

    #region Get Single with multiple criteria Data from provided Table
    public string GetSingleDataMultiSelect(string Table = "", string RetrieveField = "", string Type = "", string ConditionField1 = "", int Id1 = 0, string Condition1 = "", string ConditionField2 = "", int Id2 = 0)
    {
        string sqlText = string.Empty;
        string resultString = String.Empty;
        // SELECT productSupplier_ProductPrice FROM productSupplier WHERE productSupplier_ProductId = 1 AND productSupplier_SupplierId = 7 
        if (Id1 > 0)
        {
            sqlText = "SELECT " + RetrieveField + " FROM " + Table + " WHERE " + ConditionField1 + " = @Id1 " + Condition1.ToUpper() + " " + ConditionField2 + " = @Id2 ";
        }
        else
        {
            sqlText = "SELECT * from " + Table;
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);
        cmd.Parameters.Add("@Id1", MySqlDbType.Int32).Value = Id1;
        cmd.Parameters.Add("@Id2", MySqlDbType.Int32).Value = Id2;

        switch (Type.ToLower())
        {
            case "double":
                try
                {
                    resultString = ((double)cmd.ExecuteScalar()).ToString();
                }
                catch (NullReferenceException)
                {
                    resultString = "-1";
                }
                break;

            case "float":
                resultString = ((float)cmd.ExecuteScalar()).ToString();
                break;
            case "int":
                resultString = ((int)cmd.ExecuteScalar()).ToString();
                break;
            case "string":
                resultString = (string)cmd.ExecuteScalar();
                break;
            default:
                resultString = (string)cmd.ExecuteScalar();
                break;
        }
        return resultString;
    }
    #endregion Get Single Data from provided Table

    #region Insert in Table: Order
    // TODO Is now for Orderline replace fields for order
    public string InsertTblOrder(int OrderId, int ProductId, int ProjectId, int CategoryId, double Number, double Price, double RealRowTotal)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO Order (orderline_OrderId, orderline_ProductId, orderline_ProjectId, orderline_CategoryId, orderline_Number, orderline_Price, orderline_RealRowTotal) VALUES (@OrderId, @ProductId, @ProjectId, @CategoryId, @Number, @Price, @RealRowTotal);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrderline(sqlText, OrderId, ProductId, ProjectId, CategoryId, Number, Price, RealRowTotal);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij toegevoegd.");
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Insert in Table: Order

    #region Insert in Table: Orderline
    public string InsertTblOrderline(int OrderId, int ProductId, int ProjectId, int CategoryId, double Number, double Price, double RealRowTotal)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO Orderline (orderline_OrderId, orderline_ProductId, orderline_ProjectId, orderline_CategoryId, orderline_Number, orderline_Price, orderline_RealRowTotal) VALUES (@OrderId, @ProductId, @ProjectId, @CategoryId, @Number, @Price, @RealRowTotal);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrderline(sqlText, OrderId, ProductId, ProjectId, CategoryId, Number, Price, RealRowTotal);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij toegevoegd.");
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Insert in Table: Orderline

    #region Execute Non Query Table: Orderline
    public int ExecuteNonQueryTblOrderline(string sqlText, int OrderId, int ProductId, int ProjectId, int CategoryId, double Number, double Price, double RealRowTotal, int OrderlineId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {
                // add parameters setting string values to DBNull.Value
                // int parameters
                cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;
                cmd.Parameters.Add("@ProjectId", MySqlDbType.Int32).Value = ProjectId;
                cmd.Parameters.Add("@CategoryId", MySqlDbType.Int32).Value = CategoryId;

                // double parameters
                cmd.Parameters.Add("@Number", MySqlDbType.Double).Value = Number;
                cmd.Parameters.Add("@Price", MySqlDbType.Double).Value = Price;
                cmd.Parameters.Add("@RealRowTotal", MySqlDbType.Double).Value = RealRowTotal;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table: Orderline


    #region Create lists to populate dropdowns for order Page
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
    #endregion Fill Supplier dropdown

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

    #region Fill Category dropdown
    public List<Category> GetCategoryList(List<Category> categoryList)
    {

        Database dbCategoryConnection = new()
        {
            TableName = DbCategoryTable
        };

        dbCategoryConnection.SqlSelectionString = "category_Name, category_Id";
        dbCategoryConnection.SqlOrderByString = "category_Id";
        dbCategoryConnection.TableName = DbCategoryTable;

        DataTable dtCategorySelection = dbCategoryConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtCategorySelection.Rows.Count; i++)
        {
            categoryList.Add(new Category
            {
                CategoryName = dtCategorySelection.Rows[i][0].ToString(),
                CategoryId = int.Parse(dtCategorySelection.Rows[i][1].ToString(), Culture)
            });

        };
        return categoryList;
    }
    #endregion
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

    #region Create object for all categories in table for dropdown
    public class Category
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
    #endregion

    #region Create object for all products in table for dropdown
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
    #endregion Create object for all products in table for dropdown    
    #endregion Helper classes to for creating objects to populate dropdowns

    #endregion Create lists to populate dropdowns for order Page
}
