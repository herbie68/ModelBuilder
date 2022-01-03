namespace Modelbuilder;
internal class HelperOrder
{
    #region Available databasefields
    /// ***************************************************************************************
    ///   Available fields for SupplyOrder table
    /// ***************************************************************************************
    ///   Table Fieldname           	Variable            Parameter      		Type
    ///   -----------------------------------------------------------------------------------
    ///   Id						Id					@Id					int
    ///   SupplierId				SupplierId			@SupplierId			int
    ///   SupplierName              SupplierName        @SupplierName       varchar
    ///   Date 					    OrderDate			@OrderDate			date
    ///   CurrencySymbol 			CurrencySymbol		@CurrencySymbol		string(2)
    ///   CurrencyConversionRate 	ConversionRate		@ConversionRate		double (6.4)
    ///   ShippingCosts 			ShippingCosts		@ShippingCosts		double (6,2)
    ///   OrderCosts 				OrderCosts			@OrderCosts			double (6,2)
    ///   Memo 					    OrderMemo			@OrderMemo			string
    ///   Closed 					OrderClosed			@OrderClosed		bool (0 or 1)
    ///   ClosedDate 				OrderClosedDate		@OrderClosedDate	date
    /// ***************************************************************************************

    /// ***************************************************************************************
    ///   Available fields for SupplyOrderline table
    /// ***************************************************************************************
    ///   Table Fieldname               Variable            Parameter           Type
    ///   ----------------------------------------------------------------------------------
    ///   Id                  Id                  @Id                 int
    ///   OrderId             OrderId             @OrderId            int
    ///   ProductId           ProductId           @ProductId          int
    ///   ProductName         ProductName         @ProductName        varchar
    ///   ProjectId           ProjectId           @ProjectId          int
    ///   ProjectName         ProjectName         @ProjectName        varchar
    ///   CategoryId          CategoryId          @CategoryId         int
    ///   CategoryName        CategoryName        @CategoryName       varchar
    ///   Number              Number              @Number             double
    ///   Price               Price               @Price              double
    ///   RealRowTotal        RealRowTotal        @RealRowTot         double
    ///   Closed              RowClosed           @RowClosed          bool (0 or 1)
    ///   ClosedDate          RowClosedDate       @RowClosedDate      date
    /// ***************************************************************************************
    /// 
    /// Examples how to use joins to get values from different tables
    /// Give me a list of all products where the supplier has a price for a product
    /// SELECT Name, SupplierName, Price, ProductPrice FROM product INNER JOIN productsupplier ON Id = ProductId
    ///
    /// Give me a list with brandnames for all products
    /// SELECT Name, brand1_Name FROM product INNER JOIN brand ON BrandId = brand1_Id

    #endregion Available databasefields

    #region public Variables
    public string ConnectionStr { get; set; }

    public string DbCategoryTable = "category";
    public string DbOrderTable = "supplyorder";
    public string DbOrderView = "view_supplyorder";
    public string DbOrderLineTable = "supplyorderline";
    public string DbOrderLineView = "view_supplyorderline";
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
        DataTable dt = new();
        string sqlText = string.Empty;

        if (OrderId > 0)
        {
            sqlText = "SELECT * from " + DbOrderView + " where Id = @IdOrder";
        }
        else
        {
            sqlText = "SELECT * from " + DbOrderView;
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
            sqlText = "SELECT * from " + DbOrderLineView + " where Supplyorder_Id = @Id";
        }
        else
        {
            sqlText = "SELECT * from " + DbOrderLineView;
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
        string sqlText = string.Empty;
        string resultString = String.Empty;
        float resultFloat = 0;

        if (Id > 0)
        {
            sqlText = "SELECT ConversionRate from Currency where Id = @Id";
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

        resultString = resultFloat.ToString();

        return resultString;
    }
    #endregion Get Data from Table: SupplyOrderline

    #region Get Single Data from provided Table
    public string GetSingleData(int Id = 0, string Table = "", string Field = "", string Type = "")
    {
        string sqlText;
        if (Id > 0)
        {
            sqlText = "SELECT " + Field + " from " + Table.ToLower() + " where Id = @Id";
        }
        else
        {
            sqlText = "SELECT * from " + Table.ToLower();
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new(sqlText, con);
        cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;

        string resultString;
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

    #region Get Single with multiple criteria Data from provided Table
    public string GetSingleDataMultiSelect(string Table = "", string RetrieveField = "", string Type = "", string ConditionField1 = "", int Id1 = 0, string Condition1 = "", string ConditionField2 = "", int Id2 = 0)
    {
        string sqlText = string.Empty;
        string resultString = String.Empty;
        // SELECT ProductPrice FROM productSupplier WHERE ProductId = 1 AND SupplierId = 7 
        if (Id1 > 0)
        {
            sqlText = "SELECT " + RetrieveField + " FROM " + Table.ToLower() + " WHERE " + ConditionField1 + " = @Id1 " + Condition1.ToUpper() + " " + ConditionField2 + " = @Id2 ";
        }
        else
        {
            sqlText = "SELECT * from " + Table.ToLower();
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

    #region Get Orderrowtotals for OrderId
    public string GetOrderTotal(int OrderId)
    {
        string sqlText = string.Empty;
        string resultString = String.Empty;
        sqlText = "SELECT SUM(Number * Price) RowTotal FROM " + DbOrderLineTable + " WHERE OrderId = @OrderId ";

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);
        cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId;

        try
        {
            resultString = ((double)cmd.ExecuteScalar()).ToString();
        }
        catch
        {
            resultString = "0";
        }
        con.Close();
        return resultString;
    }
    #endregion Get Orderrowtotals for OrderId

    #region Insert in Table: SupplyOrder
    public string InsertTblOrder(int SupplierId, int CurrencyId, string OrderNumber, string OrderDate, string CurrencySymbol, double ConversionRate, double ShippingCosts, double OrderCosts, string OrderMemo)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO " + DbOrderTable + " (Supplier_Id, Currency_Id, Ordernumber, OrderDate, CurrencySymbol, CurrencyConversionRate, ShippingCosts, OrderCosts, Memo) VALUES (@SupplierId, @CurrencyId, @OrderNumber, @OrderDate, @CurrencySymbol, @ConversionRate, @ShippingCosts, @OrderCosts, @OrderMemo);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrder(sqlText, SupplierId, CurrencyId, OrderNumber, OrderDate, CurrencySymbol, ConversionRate, ShippingCosts, OrderCosts, OrderMemo);

            if (rowsAffected > 0)
            {

                result = "Rij toegevoegd.";
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (UpdateTblOrder - MySqlException): " + ex.Message);
            throw;
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
    public string InsertTblOrderline(int OrderId, int SupplierId, int ProductId, int ProjectId, int CategoryId, double Number, double Price)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO " + DbOrderLineTable + " (SupplyOrder_Id, Supplier_Id, Product_Id, Project_Id, Category_Id, Amount, Price) VALUES (@OrderId, @SupplierId, @ProductId, @ProjectId, @CategoryId, @Number, @Price);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrderline(sqlText, OrderId, SupplierId, ProductId, ProjectId, CategoryId, Number, Price);

            if (rowsAffected > 0)
            {

                result = "Rij toegevoegd.";
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline - MySqlException): " + ex.Message);
            throw;
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
    public string UpdateTblOrder(int OrderId, int SupplierId, int CurrencyId, string OrderNumber, string OrderDate, string CurrencySymbol, double ConversionRate, double ShippingCosts, double OrderCosts, string OrderMemo)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + DbOrderTable + " SET Supplier_Id = @SupplierId, Currency_Id = @CurrencyId, OrderNumber = @OrderNumber, OrderDate = @OrderDate, CurrencySymbol = @CurrencySymbol, CurrencyConversionRate = @ConversionRate, ShippingCosts = @ShippingCosts, OrderCosts = @OrderCosts, Memo = @OrderMemo WHERE Id = @OrderId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrder(sqlText, SupplierId, CurrencyId, OrderNumber, OrderDate, CurrencySymbol, ConversionRate, ShippingCosts, OrderCosts, OrderMemo, OrderId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? OrderNumber + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblOrder - MySqlException): " + ex.Message);
            throw;
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
    public string UpdateTblOrderline(int OrderId, int SupplierId, int ProductId, int ProjectId, int CategoryId, double Number, double Price, int OrderLineId)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + DbOrderLineTable + " SET Supplyorder_Id = @OrderId, Supplier_Id = @SupplierId, Product_Id = @ProductId, Project_Id = @ProjectId, Category_Id = @CategoryId, Amount = @Number, Price = @Price WHERE Id = @OrderlineId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblOrderline(sqlText, OrderId, SupplierId, ProductId, ProjectId, CategoryId, Number, Price, OrderLineId);

            if (rowsAffected > 0)
            {

                result = "Rij toegevoegd.";
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblOrderline): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Update Table: SupplyOrderline

    #region Delete row in Table: SupplyOrder
    public string DeleteTblSupplyOrder(int orderId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM " + DbOrderTable + " WHERE order_Id=@orderId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplyOrderId(sqlText, orderId);

            if (rowsAffected > 0)
            {

                result = "Rij verwijderd.";
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblSupplyOrder - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblSupplyOrder): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: SupplyOrder

    #region Delete row in Table: SupplyOrderline
    public string DeleteTblSupplyOrderline(int Id, string Type)
    {
        string result = string.Empty;
        string sqlText = "";

        if (Type == "order")
        {
            sqlText = "DELETE FROM " + DbOrderLineTable + " WHERE OrderId=@OrderId";
        }
        else
        {
            sqlText = "DELETE FROM " + DbOrderLineTable + " WHERE Id=@OrderId";
        }

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplyOrderId(sqlText, Id);

            if (rowsAffected > 0)
            {

                result = "Rij verwijderd.";
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblSupplyOrderline - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblSupplyOrderline): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: SupplyOrderline

    #region Update SupplierId in Table: Supplyorderline
    public string UpdateSupplierTblOrderline(int OrderId, int SupplierId)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + DbOrderLineTable + " SET SupplierId = @SupplierId WHERE OrderId = @OrderId;";

        try
        {
            int rowsAffected = ExecuteNonQuerySupplierTblOrderline(sqlText, SupplierId, OrderId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? "Orderregels bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateSupplierTblOrderline - MySqlException): " + ex.Message);
            throw;
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
    public int ExecuteNonQueryTblOrder(string sqlText, int SupplierId, int CurrencyId, string OrderNumber, string OrderDate, string CurrencySymbol, double ConversionRate, double ShippingCosts, double OrderCosts, string OrderMemo, int OrderId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // add parameters setting string values to DBNull.Value
            // int parameters
            cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId;
            cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;
            cmd.Parameters.Add("@CurrencyId", MySqlDbType.Int32).Value = CurrencyId;

            // double parameters
            cmd.Parameters.Add("@ShippingCosts", MySqlDbType.Double).Value = ShippingCosts;
            cmd.Parameters.Add("@OrderCosts", MySqlDbType.Double).Value = OrderCosts;
            cmd.Parameters.Add("@ConversionRate", MySqlDbType.Double).Value = ConversionRate;

            // Date parameters
            cmd.Parameters.Add("@OrderDate", MySqlDbType.Date).Value = DBNull.Value;


            // Add VarChar values
            cmd.Parameters.Add("@OrderNumber", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@CurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;

            // Add LongText values
            cmd.Parameters.Add("@OrderMemo", MySqlDbType.LongText).Value = DBNull.Value;

            //set values
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

    #region Execute Non Query Table Id: SupplyOrder
    public int ExecuteNonQueryTblSupplyOrderId(string sqlText, int orderId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@orderId", MySqlDbType.Int32).Value = orderId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table Id: SupplyOrder

    #region Execute Non Query Table: SupplyOrderline
    public int ExecuteNonQueryTblOrderline(string sqlText, int OrderId, int SupplierId, int ProductId, int ProjectId, int CategoryId, double Number, double Price, int OrderlineId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new (ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText, con))
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

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table: SupplyOrderline

    #region Check if an item exists in table: productsupplier
    public int CheckProductForSupplier(int SupplierId = 0, int ProductId = 0)
    {
        int result = 0;
        string sqlText = "SELECT COUNT(*) FROM " + DbProductSupplierTable + " WHERE Supplier_Id = @SupplierId AND Product_Id = @ProductId";

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {
                cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;

                result = cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        return result;
    }
    #endregion Check if an item exists in table: productsupplier

    #region Check if there are orderrows for givven project
    public int CheckOrderRowsForOrder(int OrderId = 0)
    {
        int result = 0;
        string sqlText = "SELECT COUNT(*) FROM " + DbOrderLineTable + " WHERE OrderId = @OrderId";

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText, con))
            {
                cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId;
                result = cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        return result;
    }
    #endregion Check if there are orderrows for given project

    #region Execute Non Query Supplier in Table: SupplyOrderline
    public int ExecuteNonQuerySupplierTblOrderline(string sqlText, int SupplierId, int OrderId)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId;
            cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;

            //execute; returns the number of rows affected
            rowsAffected = cmd.ExecuteNonQuery();
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

        dbConnection.SqlSelectionString = "Name, Id, CurrencySymbol, CurrencyId";
        dbConnection.SqlOrderByString = "Id";
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
        }
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

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            projectList.Add(new Project
            {
                ProjectName = dtSelection.Rows[i][0].ToString(),
                ProjectId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
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

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        // To add the default line "Select a product"to the dropdownlist at the first position, there has to be an extra row added, so Rows.count + 1,
        // but for the real products this +1 has to be substraced again otherwise it points to the wrong row
        for (int i = 0; i < dtSelection.Rows.Count + 1; i++)
        {
            if (i == 0)
            {
                productList.Add(new Product { ProductName = "Selecteer een product", ProductId = 0 });
            }
            else
            {
                productList.Add(new Product
                {
                    ProductName = dtSelection.Rows[i - 1][0].ToString(),
                    ProductId = int.Parse(dtSelection.Rows[i - 1][1].ToString(), Culture)
                });
            }
        }
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

        dbCategoryConnection.SqlSelectionString = "Name, Id";
        dbCategoryConnection.SqlOrderByString = "Id";
        dbCategoryConnection.TableName = DbCategoryTable;

        DataTable dtCategorySelection = dbCategoryConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtCategorySelection.Rows.Count; i++)
        {
            categoryList.Add(new Category
            {
                CategoryName = dtCategorySelection.Rows[i][0].ToString(),
                CategoryId = int.Parse(dtCategorySelection.Rows[i][1].ToString(), Culture)
            });

        }
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
