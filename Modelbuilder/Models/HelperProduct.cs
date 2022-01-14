namespace Modelbuilder;
internal class HelperProduct
{
    #region Available databasefields
    /// ***************************************************************************************
    ///   Available fields for Supplier table
    /// ***************************************************************************************
    ///   Table Fieldname           Variable                Parameter      		    Type
    ///   --------------------------------------------------------------------------------------
    ///   Id						Id					    @Id					    int
    ///   Code				        Code			        @Code   			    varchar
    ///   Name                      Name                    @Name                   varchar
    ///   Dimensions				Dimensions			    @Dimensions			    varchar
    ///   Price 			        Price		            @Price      		    double (6,2)
    ///   MinimalStock 	            MinimalStock		    @MinimalStock		    double (6,4)
    ///   StandardOrderQuantity     StandardOrderQuantity   @StandardOrderQuantity  double (6,4)
    ///   ProjectCosts   			ProjectCosts		    @ProjectCosts		    double (6,2)
    ///   Id 				    UnitId			        @UnitId			        int
    ///   ImageRotationAngle 		ImageRotationAngle		@ImageRotationAngle		varchar
    ///   Image 					Image			        @Image		            longblob
    ///   Id 				    BrandId		            @BrandId	            int
    ///   Id               CategoryId              @CategoryId             int
    ///   Id                StorageId               @StorageId              int
    ///   Memo                      Memo                    @Memo                   longtext
    ///   Created                                                                   datetime
    ///   Modified                                                                  datetime
    /// ***************************************************************************************

    /// ***************************************************************************************
    ///   Available fields for ProductSupplier table
    /// ***************************************************************************************
    ///   Table Fieldname               Variable            Parameter           Type
    ///   ----------------------------------------------------------------------------------
    ///   Id                            Id                  @Id                 int
    ///   Id                   SupplierId          @SupplierId         int
    ///   Id                    ProductId           @ProductId          int
    ///   Id                   CurrencyId          @CurrencyId         int
    ///   ProductNumber                 ProductNumber       @ProductNumber      varchar
    ///   Name                          Name                @Name               varchar
    ///   Price                         Price               @Price              double (6,2)
    ///   Default                       Default             @Default            varchar
    ///   Created                                                                   datetime
    ///   Modified                                                                  datetime
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
    /// <summary>
    /// Gets or Sets the connection str.
    /// </summary>
    /// <summary>
    /// Gets or Sets the connection str.
    /// </summary>
    public string ConnectionStr { get; set; }

    public string DbProductTable = "product";
    public string DbProductView = "view_product";
    public string DbProductSupplierTable = "productsupplier";
    public string DbProductSupplierView = "view_productsupplier";

    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables

    #region Connector to database
    public HelperProduct(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperProduct(string serverName, int portNumber, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
    }
    #endregion Connector to database

    #region Delete row in Table: Product
    public string DeleteTblProduct(int productId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM " + DbProductTable + " WHERE Id=@productId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductId(sqlText, productId);

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
            Debug.WriteLine("Error (DeleteTblProduct - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: Product

    #region Delete row in Table: ProductSupplier
    public string DeleteTblProductSupplier(int productSupplierId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM " + DbProductSupplierTable + " WHERE Id=@productSupplierId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplierId(sqlText, productSupplierId);

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
            Debug.WriteLine("Error (DeleteTblProduct - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: Product

    #region Execute Non Query Table: Product
    /// <summary>
    /// Parameters necesary to execute non query
    /// </summary>
    /// <param name="sqlText"></param>
    /// <param name="productCode"></param>
    /// <param name="productName"></param>
    /// <param name="productMinimalStock"></param>
    /// <param name="productStandardOrderQuantity"></param>
    /// <param name="productPrice"></param>
    /// <param name="ProductNumber"></param>
    /// <param name="productProjectCosts"></param>
    /// <param name="productCategoryId"></param>
    /// <param name="productCategoryName"></param>
    /// <param name="productStorageId"></param>
    /// <param name="productStorageName"></param>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="productBrandId"></param>
    /// <param name="productBrandName"></param>
    /// <param name="productUnitId"></param>
    /// <param name="productUnitName"></param>
    /// <param name="productMemo"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public int ExecuteNonQueryTblProduct(string sqlText, string Code, string Name, double MinimalStock, double StandardOrderQuantity, double Price, int ProjectCosts, int CategoryId, int StorageId, int BrandId, int UnitId, string Memo, string ImageRotationAngle, byte[] Image, string Dimensions, int Id = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@BrandId", MySqlDbType.Int32).Value = BrandId;
            cmd.Parameters.Add("@CategoryId", MySqlDbType.Int32).Value = CategoryId;
            cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;
            cmd.Parameters.Add("@ProjectCosts", MySqlDbType.Int32).Value = ProjectCosts;
            cmd.Parameters.Add("@StorageId", MySqlDbType.Int32).Value = StorageId;
            cmd.Parameters.Add("@UnitId", MySqlDbType.Int32).Value = UnitId;

            // Add Double values
            cmd.Parameters.Add("@MinimalStock", MySqlDbType.Double).Value = MinimalStock;
            cmd.Parameters.Add("@Price", MySqlDbType.Double).Value = Price;
            cmd.Parameters.Add("@StandardOrderQuantity", MySqlDbType.Double).Value = StandardOrderQuantity;

            // Add VarChar values
            cmd.Parameters.Add("@Code", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@Dimensions", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@ImageRotationAngle", MySqlDbType.VarChar).Value = DBNull.Value;

            // Add LongText values
            cmd.Parameters.Add("@Memo", MySqlDbType.LongText).Value = DBNull.Value;

            // Add Images
            cmd.Parameters.Add("@Image", MySqlDbType.Blob).Value = Image;


            //set values
             if (!String.IsNullOrEmpty(Code))
            {
                cmd.Parameters["@Code"].Value = Code;
            }

            if (!String.IsNullOrEmpty(Name))
            {
                cmd.Parameters["@Name"].Value = Name;
            }

            if (!String.IsNullOrEmpty(Dimensions))
            {
                cmd.Parameters["@Dimensions"].Value = Dimensions;
            }

            if (!String.IsNullOrEmpty(Memo))
            {
                cmd.Parameters["@Memo"].Value = Memo;
            }

            if (!String.IsNullOrEmpty(ImageRotationAngle))
            {
                cmd.Parameters["@ImageRotationAngle"].Value = ImageRotationAngle;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table: ProductSupplier
    public int ExecuteNonQueryTblProductSupplier(string sqlText, int ProductId, int SupplierId, int CurrencyId, string ProductNumber, string ProductName, double ProductPrice, string Default, int Id = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;
                cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;
                cmd.Parameters.Add("@CurrencyId", MySqlDbType.Int32).Value = CurrencyId;
                cmd.Parameters.Add("@ProductNumber", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@ProductPrice", MySqlDbType.Double).Value = ProductPrice;
                cmd.Parameters.Add("@Default", MySqlDbType.VarChar).Value = DBNull.Value;

                //set values
                if (!String.IsNullOrEmpty(ProductNumber))
                {
                    cmd.Parameters["@ProductNumber"].Value = ProductNumber;
                }

                if (!String.IsNullOrEmpty(ProductName))
                {
                    cmd.Parameters["@ProductName"].Value = ProductName;
                }

                if (!String.IsNullOrEmpty(Default))
                {
                    cmd.Parameters["@Default"].Value = Default;
                }

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table: ProductSupplier

    #region Execute Non Query Table Id: Product
    public int ExecuteNonQueryTblProductId(string sqlText, int productId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@productId", MySqlDbType.Int32).Value = productId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table Id: Product

    #region Execute Non Query Table ProductSupplierId: ProductSupplier
    public int ExecuteNonQueryTblProductSupplierId(string sqlText, int productSupplierId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@productSupplierId", MySqlDbType.Int32).Value = productSupplierId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table Id: ProductSupplier

    #region Get Data from Table: Product
    public DataTable GetDataTblProduct(int productId = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (productId > 0)
        {
            sqlText = "SELECT * from " + DbProductView + " where Id = @productId";
        }
        else
        {
            sqlText = "SELECT * from " + DbProductView;
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@productId", MySqlDbType.Int32).Value = productId;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }

        return dt;
    }
    #endregion Get Data from Table: Product

    #region Get Data from Table: ProductSupplier
    public DataTable GetDataTblProductSupplier(int ProductId = 0)
    {
        DataTable dtPS = new DataTable();
        string sqlText = string.Empty;

        if (ProductId > 0)
        {
            sqlText = "SELECT * FROM " + DbProductSupplierView + " WHERE Product_Id = @ProductId";
        }
        else
        {
            sqlText = "SELECT * FROM " + DbProductSupplierView;
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;

            using MySqlDataAdapter daPS = new(cmd);
            //use DataAdapter to fill DataTable
            daPS.Fill(dtPS);
        }

        return dtPS;
    }
    #endregion Get Data from Table: Product

    #region Insert in Table: Product
    public string InsertTblProduct(string Code, string Name, double MinimalStock, double StandardOrderQuantity, double Price, int ProjectCosts, int CategoryId, int StorageId, int BrandId, int UnitId, string Memo, string ImageRotationAngle, byte[] Image, string Dimensions)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO " + DbProductTable + " (Code, Name, MinimalStock, StandardOrderQuantity, Price, ProjectCosts, Category_Id, Storage_Id, Brand_Id, Unit_Id, Memo, ImageRotationAngle, Image, Dimensions) VALUES (@Code, @Name, @MinimalStock, @StandardOrderQuantity, @Price, @ProjectCosts, @CategoryId, @StorageId, @BrandId, @UnitId, @Memo, @ImageRotationAngle, @Image, @Dimensions);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProduct(sqlText, Code, Name, MinimalStock, StandardOrderQuantity, Price, ProjectCosts, CategoryId, StorageId, BrandId, UnitId, Memo, ImageRotationAngle, Image, Dimensions);

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
            Debug.WriteLine("Error (UpdateTblProduct - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblProduct): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Insert in Table: Product

    #region Insert in Table: ProductSupplier
    public string InsertTblProductSupplier(int ProductId, int SupplierId, int CurrencyId, string ProductNumber, string ProductName, double ProductPrice, string Default)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO " + DbProductSupplierTable + " (Product_Id, Supplier_Id, Currency_Id, ProductNumber,  ProductName, Price, DefaultSupplier) VALUES (@ProductId, @SupplierId, @CurrencyId, @ProductNumber, @ProductName, @ProductPrice, @Default);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, ProductId, SupplierId, CurrencyId, ProductNumber, ProductName, ProductPrice, Default);

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
            Debug.WriteLine("Error (UpdateTblProduct - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblProduct): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion

    #region Update Table: Product
    public string UpdateTblProduct(int productId, string productCode, string productName, double productMinimalStock, double productStandardOrderQuantity, double productPrice, int productProjectCosts, int productCategoryId, int productStorageId, int productBrandId, int productUnitId, string productMemo, string productImageRotationAngle, byte[] productImage, string productDimensions)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + DbProductTable + " SET Code = @Code, name = @Name, MinimalStock = @MinimalStock,StandardOrderQuantity = @StandardOrderQuantity, Price = @Price, ProjectCosts = @ProjectCosts, Category_Id = @CategoryId, Storage_Id = @StorageId, Brand_Id = @BrandId, Unit_Id = @UnitId, Memo = @Memo, ImageRotationAngle = @ImageRotationAngle, Image = @Image, Dimensions = @Dimensions WHERE Id = @Id;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProduct(sqlText, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productStorageId, productBrandId, productUnitId, productMemo, productImageRotationAngle, productImage, productDimensions, productId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? productName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblProduct - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblProduct): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update Table: Product

    #region Update Table: ProductSupplier
    public string UpdateTblProductSupplier(int Id, int ProductId, int SupplierId, int CurrencyId, string ProductNumber, string ProductName, double ProductPrice, string Default)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + DbProductSupplierTable + " SET Product_Id = @ProductId, Supplier_Id = @SupplierId, Currency_Id = @CurrencyId, ProductNumber = @ProductNumber,  ProductName = @ProductName, Price = @ProductPrice, DefaultSupplier = @Default WHERE Id = @Id;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, ProductId, SupplierId, CurrencyId, ProductNumber, ProductName, ProductPrice, Default, Id);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? ProductName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblProductSupplier - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblProductSupplier): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update Table: Product

    #region Uncheck the DefaultSupplier: ProductSupplier
    public int UncheckDefaultSupplierTblProductSupplier(string sqlText, int Id, int ProductId)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion

    #region Uncheck other default suppliers for product
    public string UncheckDefaultSupplierTblProductSupplier(int Id, int ProductId)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + DbProductSupplierTable + " SET DefaultSupplier = '' WHERE Id != @Id AND Product_Id = @ProductId;";

        try
        {
            int rowsAffected = UncheckDefaultSupplierTblProductSupplier(sqlText, Id, ProductId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? Id + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblProductSupplier - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblProductSupplier): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Uncheck other default suppliers for product
}
