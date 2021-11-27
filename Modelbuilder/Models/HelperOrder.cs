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
using Org.BouncyCastle.Math;
using static Modelbuilder.HelperOrder;

namespace Modelbuilder;

internal class HelperOrder
{
    #region Available databasefields
    /// ***************************************************************************************
    ///   Available fields for SupplyOrder table
    /// ***************************************************************************************
    ///   Table Fieldname           	Variable            Parameter      		Type
    ///   -----------------------------------------------------------------------------------
    ///   order_Id						Id					@Id					int
    ///   order_SupplierId				SupplierId			@SupplierId			int
    ///   order_SupplierName            SupplierName        @SupplierName       varchar
    ///   order_Date 					OrderDate			@OrderDate			date
    ///   order_CurrencySymbol 			CurrencySymbol		@CurrencySymbol		string(2)
    ///   order_CurrencyConversionRate 	ConversionRate		@ConversionRate		double (6.4)
    ///   order_ShippingCosts 			ShippingCosts		@ShippingCosts		double (6,2)
    ///   order_OrderCosts 				OrderCosts			@OrderCosts			double (6,2)
    ///   order_Memo 					OrderMemo				@OrderMemo				string
    ///   order_Closed 					OrderClosed			@OrderClosed		bool (0 or 1)
    ///   order_ClosedDate 				OrderClosedDate		@OrderClosedDate	date
    /// ***************************************************************************************

    /// ***************************************************************************************
    ///   Available fields for SupplyOrderline table
    /// ***************************************************************************************
    ///   Table Fieldname               Variable            Parameter           Type
    ///   ----------------------------------------------------------------------------------
    ///   orderline_Id                  Id                  @Id                 int
    ///   orderline_OrderId             OrderId             @OrderId            int
    ///   orderline_ProductId           ProductId           @ProductId          int
    ///   orderline_ProductName         ProductName         @ProductName        varchar
    ///   orderline_ProjectId           ProjectId           @ProjectId          int
    ///   orderline_ProjectName         ProjectName         @ProjectName        varchar
    ///   orderline_CategoryId          CategoryId          @CategoryId         int
    ///   orderline_CategoryName        CategoryName        @CategoryName       varchar
    ///   orderline_Number              Number              @Number             double
    ///   orderline_Price               Price               @Price              double
    ///   orderline_RealRowTotal        RealRowTotal        @RealRowTot         double
    ///   orderline_Closed              RowClosed           @RowClosed          bool (0 or 1)
    ///   orderline_ClosedDate          RowClosedDate       @RowClosedDate      date
    /// ***************************************************************************************
    #endregion Available databasefields

    #region public Variables
    public string ConnectionStr { get; set; } = string.Empty;

    public string DbCategoryTable = "category";
    public string DbOrderTable = "supplyorder";
    public string DbOrderLineTable = "supplyorderline";
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

    #region Get Data from Table: SupplyOrder
    public DataTable GetDataTblOrder(int OrderId = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (OrderId > 0)
        {
            sqlText = "SELECT * from supplyorder where order_Id = @IdOrder";
        }
        else
        {
            sqlText = "SELECT * from supplyorder";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            if (OrderId > 0) { cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId; }

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }

        return dt;
    }
    #endregion Get Data from Table: SupplyOrder

    #region Get Data from Table: SupplyOrderline
    public DataTable GetDataTblOrderline(int Id = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT * from SupplyOrderline where orderline_OrderId = @Id";
        }
        else
        {
            sqlText = "SELECT * from SupplyOrderline";
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
    #endregion Get Data from Table: SupplyOrderline

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
    #endregion Get Data from Table: SupplyOrderline

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

    #region Insert in Table: SupplyOrder
    public string InsertTblOrder(int SupplierId, string SupplierName, string OrderNumber, string OrderDate, string CurrencySymbol, double ConversionRate, double ShippingCosts, double OrderCosts, string OrderMemo)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO SupplyOrder (order_SupplierId, order_SupplierName, order_Ordernumber, order_Date, order_CurrencySymbol, order_CurrencyConversionRate, order_ShippingCosts, order_OrderCosts, order_Memo) VALUES (@SupplierId, @SupplierName, @OrderNumber, @OrderDate, @CurrencySymbol, @ConversionRate, @ShippingCosts, @OrderCosts, @OrderMemo);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrder(sqlText, SupplierId, SupplierName, OrderNumber, OrderDate, CurrencySymbol, ConversionRate, ShippingCosts, OrderCosts, OrderMemo);

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
            Debug.WriteLine("Error (UpdateTblOrder - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblOrder): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Insert in Table: SupplyOrder

    #region Insert in Table: SupplyOrderline
    public string InsertTblOrderline(int OrderId, int SupplierId, int ProductId, string ProductName, int ProjectId, string ProjectName, int CategoryId, string CategoryName, double Number, double Price)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO SupplyOrderline (orderline_OrderId, orderline_SupplierId, orderline_ProductId, orderline_ProductName, orderline_ProjectId, orderline_ProjectName, orderline_CategoryId, orderline_CategoryName, orderline_Number, orderline_Price) VALUES (@OrderId, @SupplierId, @ProductId, @ProductName, @ProjectId, @ProjectName, @CategoryId, @CategoryName, @Number, @Price);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrderline(sqlText, OrderId, SupplierId, ProductId, ProductName, ProjectId, ProjectName, CategoryId, CategoryName, Number, Price);

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
    #endregion Insert in Table: SupplyOrderline

    #region Update Table: Supplyorder
    public string UpdateTblOrder(int OrderId, int SupplierId, string SupplierName, string OrderNumber, string OrderDate, string CurrencySymbol, double ConversionRate, double ShippingCosts, double OrderCosts, string OrderMemo)
    {
        string result = string.Empty;
        string sqlText = "UPDATE Supplyorder SET order_SupplierId = @SupplierId, order_SupplierName = @SupplierName, order_OrderNumber = @OrderNumber, order_Date = @OrderDate, order_CurrencySymbol = @CurrencySymbol, order_CurrencyConversionRate = @ConversionRate, order_ShippingCosts = @ShippingCosts, order_OrderCosts = @OrderCosts, order_Memo = @OrderMemo WHERE order_Id = @OrderId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrder(sqlText, SupplierId, SupplierName, OrderNumber, OrderDate, CurrencySymbol, ConversionRate, ShippingCosts, OrderCosts, OrderMemo, OrderId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? OrderNumber + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblOrder - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblOrder): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update Table: Supplyorder
    
    #region Update Table: SupplyOrderline
    public string UpdateTblOrderline(int OrderId, int SupplierId, int ProductId, string ProductName, int ProjectId, string ProjectName, int CategoryId, string CategoryName, double Number, double Price, int OrderLineId)
    {
        string result = string.Empty;
        string sqlText = "UPDATE supplyorderline SET orderline_OrderId = @OrderId, orderline_SupplierId = @SupplierId, orderline_ProductId = @ProductId, orderline_ProductName = @ProductName, orderline_ProjectId = @ProjectId, orderline_ProjectName = @ProjectName, orderline_CategoryId = @CategoryId, orderline_CategoryName = @CategoryName, orderline_Number = @Number, orderline_Price = @Price WHERE orderline_Id = @OrderlineId;";
        
        try
        {
            int rowsAffected = ExecuteNonQueryTblOrderline(sqlText, OrderId, SupplierId, ProductId, ProductName, ProjectId, ProjectName, CategoryId, CategoryName, Number, Price, OrderLineId);

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
    #endregion Update Table: SupplyOrderline

    #region Update SupplierId in Table: Supplyorderline
    public string UpdateSupplierTblOrderline(int OrderId, int SupplierId)
    {
        string result = string.Empty;
        //UPDATE supplyorderline SET orderline_SupplierId = 6 WHERE orderline_OrderId = 1;
        string sqlText = "UPDATE supplyorderline SET orderline_SupplierId = @SupplierId WHERE orderline_OrderId = @OrderId;";

        try
        {
            int rowsAffected = ExecuteNonQuerySupplierTblOrderline(sqlText, SupplierId, OrderId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? "Orderregels bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateSupplierTblOrderline - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateSupplierTblOrderline): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update SupplierId in Table: Supplyorderline

    #region Execute Non Query Table: SupplyOrder
    public int ExecuteNonQueryTblOrder(string sqlText, int SupplierId, string SupplierName, string OrderNumber, string OrderDate, string CurrencySymbol, double ConversionRate, double ShippingCosts, double OrderCosts, string OrderMemo, int OrderId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // add parameters setting string values to DBNull.Value
            // int parameters
            cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId;
            cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;

            // double parameters
            cmd.Parameters.Add("@ShippingCosts", MySqlDbType.Double).Value = ShippingCosts;
            cmd.Parameters.Add("@OrderCosts", MySqlDbType.Double).Value = OrderCosts;
            cmd.Parameters.Add("@ConversionRate", MySqlDbType.Double).Value = ConversionRate;
                
            // Date parameters
            cmd.Parameters.Add("@OrderDate", MySqlDbType.Date).Value = DBNull.Value;


            // Add VarChar values
            cmd.Parameters.Add("@SupplierName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@OrderNumber", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@CurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;

            // Add LongText values
            cmd.Parameters.Add("@OrderMemo", MySqlDbType.LongText).Value = DBNull.Value;

            //set values
            if (!String.IsNullOrEmpty(SupplierName))
            {
                cmd.Parameters["@SupplierName"].Value = SupplierName;
            }

            if (!String.IsNullOrEmpty(OrderNumber))
            {
                cmd.Parameters["@OrderNumber"].Value = OrderNumber;
            }

            if (!String.IsNullOrEmpty(CurrencySymbol))
            {
                cmd.Parameters["@CurrencySymbol"].Value = CurrencySymbol;
            }

            if (!String.IsNullOrEmpty(OrderDate))
            {
                cmd.Parameters["@OrderDate"].Value = DateTime.Parse(OrderDate, Culture);
            }

            if (!String.IsNullOrEmpty(OrderMemo))
            {
                cmd.Parameters["@OrderMemo"].Value = OrderMemo;
            }

            //execute; returns the number of rows affected
            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table: SupplyOrder

    #region Execute Non Query Table: SupplyOrderline
    public int ExecuteNonQueryTblOrderline(string sqlText, int OrderId, int SupplierId, int ProductId, string ProductName, int ProjectId, string ProjectName, int CategoryId, string CategoryName, double Number, double Price, int OrderlineId = 0)
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
                if (OrderlineId != 0) { cmd.Parameters.Add("@OrderlineId", MySqlDbType.Int32).Value = OrderlineId; }
                cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;
                cmd.Parameters.Add("@ProjectId", MySqlDbType.Int32).Value = ProjectId;
                cmd.Parameters.Add("@CategoryId", MySqlDbType.Int32).Value = CategoryId;

                // double parameters
                cmd.Parameters.Add("@Number", MySqlDbType.Double).Value = Number;
                cmd.Parameters.Add("@Price", MySqlDbType.Double).Value = Price;
    
                // Add VarChar values
                cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = ProductName;
                cmd.Parameters.Add("@ProjectName", MySqlDbType.VarChar).Value = ProjectName;
                cmd.Parameters.Add("@CategoryName", MySqlDbType.VarChar).Value = CategoryName;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table: SupplyOrderline

    #region Execute Non Query Supplier in Table: SupplyOrderline
    public int ExecuteNonQuerySupplierTblOrderline(string sqlText, int SupplierId, int OrderId)
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
                cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table: SupplyOrderline

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
