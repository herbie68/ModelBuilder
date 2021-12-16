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
    ///   Unit_Id 				    UnitId			        @UnitId			        int
    ///   ImageRotationAngle 		ImageRotationAngle		@ImageRotationAngle		varchar
    ///   Image 					Image			        @Image		            longblob
    ///   Brand_Id 				    BrandId		            @BrandId	            int
    ///   Category_Id               CategoryId              @CategoryId             int
    ///   Storage_Id                StorageId               @StorageId              int
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
    ///   Supplier_Id                   SupplierId          @SupplierId         int
    ///   Product_Id                    ProductId           @ProductId          int
    ///   Currency_Id                   CurrencyId          @CurrencyId         int
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
    /// SELECT product_Name, SupplierName, product_Price, ProductPrice FROM product INNER JOIN productsupplier ON product_Id = ProductId
    ///
    /// Give me a list with brandnames for all products
    /// SELECT product_Name, brand1_Name FROM product INNER JOIN brand ON product_BrandId = brand1_Id
    #endregion Available databasefields

    #region public Variables
    public string ConnectionStr { get; set; }

    public string DbProductTable = "product";
    public string DbProductSupplierTable = "productsupplier";

    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables

    #region Delete row in Table: Product
    public string DeleteTblProduct(int productId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM Product WHERE Id=@productId";

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
        string sqlText = "DELETE FROM ProductSupplier WHERE Id=@productSupplierId";

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
    public int ExecuteNonQueryTblProduct(string sqlText, string productCode, string productName, double productMinimalStock, double productStandardOrderQuantity, double productPrice, int productProjectCosts, int productCategoryId, string productCategoryName, int productStorageId, string productStorageName, int productBrandId, string productBrandName, int productUnitId, string productUnitName, string productMemo, string productImageRotationAngle, byte[] productImage, int productId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@productBrandId", MySqlDbType.Int32).Value = productBrandId;
            cmd.Parameters.Add("@productCategoryId", MySqlDbType.Int32).Value = productCategoryId;
            cmd.Parameters.Add("@productId", MySqlDbType.Int32).Value = productId;
            cmd.Parameters.Add("@productProjectCosts", MySqlDbType.Int32).Value = productProjectCosts;
            cmd.Parameters.Add("@productStorageId", MySqlDbType.Int32).Value = productStorageId;
            cmd.Parameters.Add("@productUnitId", MySqlDbType.Int32).Value = productUnitId;

            // Add Double values
            cmd.Parameters.Add("@productMinimalStock", MySqlDbType.Double).Value = productMinimalStock;
            cmd.Parameters.Add("@productPrice", MySqlDbType.Double).Value = productPrice;
            cmd.Parameters.Add("@productStandardOrderQuantity", MySqlDbType.Double).Value = productStandardOrderQuantity;

            // Add VarChar values
            cmd.Parameters.Add("@productBrandName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@productCategoryName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@productCode", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@productName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@productStorageName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@productUnitName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@productImageRotationAngle", MySqlDbType.VarChar).Value = DBNull.Value;

            // Add LongText values
            cmd.Parameters.Add("@productMemo", MySqlDbType.LongText).Value = DBNull.Value;

            // Add Images
            cmd.Parameters.Add("@productImage", MySqlDbType.Blob).Value = productImage;


            //set values
            if (!String.IsNullOrEmpty(productBrandName))
            {
                cmd.Parameters["@productBrandName"].Value = productBrandName;
            }

            if (!String.IsNullOrEmpty(productCategoryName))
            {
                cmd.Parameters["@productCategoryName"].Value = productCategoryName;
            }

            if (!String.IsNullOrEmpty(productCode))
            {
                cmd.Parameters["@productCode"].Value = productCode;
            }

            if (!String.IsNullOrEmpty(productName))
            {
                cmd.Parameters["@productName"].Value = productName;
            }

            if (!String.IsNullOrEmpty(productStorageName))
            {
                cmd.Parameters["@productStorageName"].Value = productStorageName;
            }

            if (!String.IsNullOrEmpty(productUnitName))
            {
                cmd.Parameters["@productUnitName"].Value = productUnitName;
            }

            if (!String.IsNullOrEmpty(productMemo))
            {
                cmd.Parameters["@productMemo"].Value = productMemo;
            }

            if (!String.IsNullOrEmpty(productImageRotationAngle))
            {
                cmd.Parameters["@productImageRotationAngle"].Value = productImageRotationAngle;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table: ProductSupplier
    public int ExecuteNonQueryTblProductSupplier(string sqlText, int ProductId, int SupplierId, string SupplierName, int CurrencyId, string CurrencySymbol, string ProductNumber, string ProductName, float ProductPrice, string Default, int Id = 0)
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
                cmd.Parameters.Add("@SupplierName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@CurrencyId", MySqlDbType.Int32).Value = CurrencyId;
                cmd.Parameters.Add("@CurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@ProductNumber", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@ProductPrice", MySqlDbType.Float).Value = ProductPrice;
                cmd.Parameters.Add("@Default", MySqlDbType.VarChar).Value = DBNull.Value;

                //set values
                if (!String.IsNullOrEmpty(SupplierName))
                {
                    cmd.Parameters["@SupplierName"].Value = SupplierName;
                }

                if (!String.IsNullOrEmpty(CurrencySymbol))
                {
                    cmd.Parameters["@CurrencySymbol"].Value = CurrencySymbol;
                }

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

    #region Execute Non Query Table Product_Id: Product
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
    #endregion Execute Non Query Table Supplier_Id: Product

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
    #endregion Execute Non Query Table ProductSupplier_Id: ProductSupplier

    #region Get Data from Table: Product
    public DataTable GetDataTblProduct(int productId = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (productId > 0)
        {
            sqlText = "SELECT * from Product where product_Id = @productId";
        }
        else
        {
            sqlText = "SELECT * from Product";
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
            sqlText = "SELECT * from ProductSupplier where ProductId = @ProductId";
        }
        else
        {
            sqlText = "SELECT * from ProductSupplier";
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
    public string InsertTblProduct(string productCode, string productName, double productMinimalStock, double productStandardOrderQuantity, double productPrice, int productProjectCosts, int productCategoryId, string productCategoryName, int productStorageId, string productStorageName, int productBrandId, string productBrandName, int productUnitId, string productUnitName, string productMemo, string productImageRotationAngle, byte[] productImage)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO Product (product_Code, product_Name, product_MinimalStock, product_StandardOrderQuantity, product_Price, product_ProjectCosts, product_CategoryId, product_CategoryName, product_StorageId, product_StorageName, product_BrandId, product_BrandName, product_UnitId, product_UnitName, product_Memo, product_ImageRotationAngle, product_Image) VALUES (@productCode, @productName, @productMinimalStock, @productStandardOrderQuantity, @productPrice, @productProjectCosts, @productCategoryId, @productCategoryName, @productStorageId, @productStorageName, @productBrandId, @productBrandName, @productUnitId, @productUnitName, @productMemo, @productImageRotationAngle, @productImage);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProduct(sqlText, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productBrandId, productBrandName, productUnitId, productUnitName, productMemo, productImageRotationAngle, productImage);

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
    public string InsertTblProductSupplier(int ProductId, int SupplierId, string SupplierName, int CurrencyId, string CurrencySymbol, string ProductNumber, string ProductName, float ProductPrice, string Default)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO ProductSupplier (ProductId, SupplierId, SupplierName, CurrencyId, CurrencySymbol, ProductNumber,  ProductName, ProductPrice, Default) VALUES (@ProductId, @SupplierId, @SupplierName, @CurrencyId, @CurrencySymbol, @ProductNumber, @ProductName, @ProductPrice, @Default);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, ProductId, SupplierId, SupplierName, CurrencyId, CurrencySymbol, ProductNumber, ProductName, ProductPrice, Default);

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
    public string UpdateTblProduct(int productId, string productCode, string productName, double productMinimalStock, double productStandardOrderQuantity, double productPrice, int productProjectCosts, int productCategoryId, string productCategoryName, int productStorageId, string productStorageName, int productBrandId, string productBrandName, int productUnitId, string productUnitName, string productMemo, string productImageRotationAngle, byte[] productImage)
    {
        string result = string.Empty;
        string sqlText = "UPDATE Product SET product_Code = @productCode, product_name = @productName, product_MinimalStock = @productMinimalStock,product_StandardOrderQuantity = @productStandardOrderQuantity, product_Price = @productPrice, product_ProjectCosts = @productProjectCosts, product_CategoryId = @productCategoryId, product_CategoryName = @productCategoryName, product_StorageId = @productStorageId, product_StorageName = @productStorageName, product_BrandId = @productBrandId, product_BrandName = @productBrandName, product_UnitId = @productUnitId, product_UnitName = @productUnitName, product_Memo = @productMemo, product_ImageRotationAngle = @productImageRotationAngle, product_Image = @productImage WHERE product_Id = @productId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProduct(sqlText, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productBrandId, productBrandName, productUnitId, productUnitName, productMemo, productImageRotationAngle, productImage, productId);

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
    public string UpdateTblProductSupplier(int Id, int ProductId, int SupplierId, string SupplierName, int CurrencyId, string CurrencySymbol, string ProductNumber, string ProductName, float ProductPrice, string Default)
    {
        string result = string.Empty;
        string sqlText = "UPDATE ProductSupplier SET ProductId = @ProductId, SupplierId = @SupplierId, SupplierName = @SupplierName, CurrencyId = @CurrencyId, CurrencySymbol = @CurrencySymbol, ProductNumber = @ProductNumber,  ProductName = @ProductName, ProductPrice = @ProductPrice, Default = @Default WHERE Id = @Id;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, ProductId, SupplierId, SupplierName, CurrencyId, CurrencySymbol, ProductNumber, ProductName, ProductPrice, Default, Id);

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
        string sqlText = "UPDATE ProductSupplier SET Default = '' WHERE Id != @Id AND ProductId = @ProductId;";

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
