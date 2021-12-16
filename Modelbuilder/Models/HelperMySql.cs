namespace Modelbuilder;

public class HelperMySql
{
    #region public Variables
    public string ConnectionStr { get; set; }

    private const string DbBrandTable = "brand";
    private const string DbCategoryTable = "category";
    private const string DbCountryTable = "country";
    private const string DbCurrencyTable = "currency";
    private const string DbStorageTable = "storage";
    private const string DbSupplierTable = "supplier";
    private const string DbContactTypeTable = "contacttype";

    private readonly CultureInfo Culture = new("nl-NL");

    #endregion public Variables

    #region Connector to database
    public HelperMySql(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperMySql(string serverName, int portNumber, string databaseName, string username, string userPwd)
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

    #region Execute Non Query Table: Brand
    /// <summary>
    /// Parameters necesary to execute non query
    /// </summary>
    /// <param name="sqlText"></param>
    /// <param name="brandName"></param>
    /// <param name="brandId"></param>
    /// <returns></returns>
    public int ExecuteNonQueryTblBrand(string sqlText, string brandName, int brandId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@brandId", MySqlDbType.Int32).Value = brandId;

            // Add VarChar values
            cmd.Parameters.Add("@brandName", MySqlDbType.VarChar).Value = DBNull.Value;

            //set values
            if (!String.IsNullOrEmpty(brandName))
            {
                cmd.Parameters["@brandName"].Value = brandName;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table: Category
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlText"></param>
    /// <param name="categoryName"></param>
    /// <param name="categoryParentId"></param>
    /// <param name="categoryFullPath"></param>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public int ExecuteNonQueryTblCategory(string sqlText, string categoryName, int categoryParentId, string categoryFullPath, int categoryId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@categoryId", MySqlDbType.Int32).Value = categoryId;
            cmd.Parameters.Add("@categoryParentId", MySqlDbType.Int32).Value = categoryParentId;

            // Add VarChar values
            cmd.Parameters.Add("@categoryFullPath", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = DBNull.Value;

            //set values
            if (!String.IsNullOrEmpty(categoryFullPath))
            {
                cmd.Parameters["@categoryFullPath"].Value = categoryFullPath;
            }

            if (!String.IsNullOrEmpty(categoryName))
            {
                cmd.Parameters["@categoryName"].Value = categoryName;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table: Country
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlText"></param>
    /// <param name="countryCode"></param>
    /// <param name="countryDefaultcurrencySymbol"></param>
    /// <param name="countryName"></param>
    /// <param name="countryDefaultcurrencyId"></param>
    /// <param name="countryId"></param>
    /// <returns></returns>
    public int ExecuteNonQueryTblCountry(string sqlText, string countryCode, string countryDefaultcurrencySymbol, string countryName, int countryDefaultcurrencyId, int countryId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@countryId", MySqlDbType.Int32).Value = countryId;
            cmd.Parameters.Add("@countryDefaultcurrencyId", MySqlDbType.Int32).Value = countryDefaultcurrencyId;

            // Add VarChar values
            cmd.Parameters.Add("@countryCode", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@countryDefaultcurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@countryName", MySqlDbType.VarChar).Value = DBNull.Value;

            //set values
            if (!String.IsNullOrEmpty(countryCode))
            {
                cmd.Parameters["@countryCode"].Value = countryCode;
            }

            if (!String.IsNullOrEmpty(countryCode))
            {
                cmd.Parameters["@countryDefaultcurrencySymbol"].Value = countryDefaultcurrencySymbol;
            }

            if (!String.IsNullOrEmpty(countryName))
            {
                cmd.Parameters["@countryName"].Value = countryName;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table: Currency
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlText"></param>
    /// <param name="currencyCode"></param>
    /// <param name="currencySymbol"></param>
    /// <param name="currencyName"></param>
    /// <param name="currencyConversionRate"></param>
    /// <param name="currencyId"></param>
    /// <returns></returns>
    public int ExecuteNonQueryTblCurrency(string sqlText, string currencyCode, string currencySymbol, string currencyName, double currencyConversionRate, int currencyId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@currencyId", MySqlDbType.Int32).Value = currencyId;

            // Add Double values
            cmd.Parameters.Add("@currencyConversionRate", MySqlDbType.Double).Value = currencyConversionRate;

            // Add VarChar values
            cmd.Parameters.Add("@currencyCode", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@currencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@countryName", MySqlDbType.VarChar).Value = DBNull.Value;

            //set values
            if (!String.IsNullOrEmpty(currencyCode))
            {
                cmd.Parameters["@currencyCode"].Value = currencyCode;
            }

            if (!String.IsNullOrEmpty(currencySymbol))
            {
                cmd.Parameters["@currencySymbol"].Value = currencySymbol;
            }

            if (!String.IsNullOrEmpty(currencyName))
            {
                cmd.Parameters["@currencyName"].Value = currencyName;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

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
    /// <param name="productSupplierProductNumber"></param>
    /// <param name="productProjectCosts"></param>
    /// <param name="productCategoryId"></param>
    /// <param name="productCategoryName"></param>
    /// <param name="productStorageId"></param>
    /// <param name="productStorageName"></param>
    /// <param name="productSupplierId"></param>
    /// <param name="productSupplierName"></param>
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
    public int ExecuteNonQueryTblProductSupplier(string sqlText, int productSupplierProductId, int productSupplierSupplierId, string productSupplierSupplierName, int productSupplierCurrencyId, string productSupplierCurrencySymbol, string productSupplierProductNumber, string productSupplierProductName, float productSupplierProductPrice, string productSupplierDefault, int productSupplierId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@productSupplierId", MySqlDbType.Int32).Value = productSupplierId;
                cmd.Parameters.Add("@productSupplierProductId", MySqlDbType.Int32).Value = productSupplierProductId;
                cmd.Parameters.Add("@productSupplierSupplierId", MySqlDbType.Int32).Value = productSupplierSupplierId;
                cmd.Parameters.Add("@productSupplierSupplierName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@productSupplierCurrencyId", MySqlDbType.Int32).Value = productSupplierCurrencyId;
                cmd.Parameters.Add("@productSupplierCurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@productSupplierProductNumber", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@productSupplierProductName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@productSupplierProductPrice", MySqlDbType.Float).Value = productSupplierProductPrice;
                cmd.Parameters.Add("@productSupplierDefault", MySqlDbType.VarChar).Value = DBNull.Value;

                //set values
                if (!String.IsNullOrEmpty(productSupplierSupplierName))
                {
                    cmd.Parameters["@productSupplierSupplierName"].Value = productSupplierSupplierName;
                }

                if (!String.IsNullOrEmpty(productSupplierCurrencySymbol))
                {
                    cmd.Parameters["@productSupplierCurrencySymbol"].Value = productSupplierCurrencySymbol;
                }

                if (!String.IsNullOrEmpty(productSupplierProductNumber))
                {
                    cmd.Parameters["@productSupplierProductNumber"].Value = productSupplierProductNumber;
                }

                if (!String.IsNullOrEmpty(productSupplierProductName))
                {
                    cmd.Parameters["@productSupplierProductName"].Value = productSupplierProductName;
                }

                if (!String.IsNullOrEmpty(productSupplierDefault))
                {
                    cmd.Parameters["@productSupplierDefault"].Value = productSupplierDefault;
                }

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table: ProductSupplier

    #region Execute Non Query Table: SupplierContact
    public int ExecuteNonQueryTblSupplierContact(string sqlText, int supplierId, string supplierContactContactName, int supplierContactContactTypeId, string supplierContactContactTypeName, string supplierContactContactPhone, string supplierContactContactMail, int supplierContactContactId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@supplierContactContactId", MySqlDbType.Int32).Value = supplierContactContactId;
                cmd.Parameters.Add("@supplierContactContactTypeId", MySqlDbType.Int32).Value = supplierContactContactTypeId;
                cmd.Parameters.Add("@supplierId", MySqlDbType.Int32).Value = supplierId;
                cmd.Parameters.Add("@supplierContactContactName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierContactContactTypeName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierContactContactPhone", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierContactContactMail", MySqlDbType.VarChar).Value = DBNull.Value;

                //set values
                if (!String.IsNullOrEmpty(supplierContactContactName))
                {
                    cmd.Parameters["@supplierContactContactName"].Value = supplierContactContactName;
                }

                if (!String.IsNullOrEmpty(supplierContactContactTypeName))
                {
                    cmd.Parameters["@supplierContactContactTypeName"].Value = supplierContactContactTypeName;
                }

                if (!String.IsNullOrEmpty(supplierContactContactPhone))
                {
                    cmd.Parameters["@supplierContactContactPhone"].Value = supplierContactContactPhone;
                }

                if (!String.IsNullOrEmpty(supplierContactContactMail))
                {
                    cmd.Parameters["@supplierContactContactMail"].Value = supplierContactContactMail;
                }

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table: Supplier
    public int ExecuteNonQueryTblSupplier(string sqlText, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, string supplierCountryName, int supplierCurrencyId, string supplierCurrencySymbol, double supplierOrderCosts, double supplierMinOrderCosts, int supplierId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //set values - if they exist
                //if a value is null, one must use DBNull.Value
                //if the value is DBNull.Value, and the table column doesn't allow nulls, this will cause an error

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@supplierId", MySqlDbType.Int32).Value = supplierId;
                cmd.Parameters.Add("@supplierCode", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierAddress1", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierAddress2", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierZip", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierCity", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierUrl", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierMemo", MySqlDbType.LongText).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierCountryId", MySqlDbType.Int32).Value = supplierCountryId;
                cmd.Parameters.Add("@supplierCountryName", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierCurrencyId", MySqlDbType.Int32).Value = supplierCurrencyId;
                cmd.Parameters.Add("@supplierCurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@supplierOrderCosts", MySqlDbType.Float).Value = supplierOrderCosts;
                cmd.Parameters.Add("@supplierMinOrderCosts", MySqlDbType.Float).Value = supplierMinOrderCosts;

                //set values
                if (!String.IsNullOrEmpty(supplierCode))
                {
                    cmd.Parameters["@supplierCode"].Value = supplierCode;
                }

                if (!String.IsNullOrEmpty(supplierName))
                {
                    cmd.Parameters["@supplierName"].Value = supplierName;
                }

                if (!String.IsNullOrEmpty(supplierAddress1))
                {
                    cmd.Parameters["@supplierAddress1"].Value = supplierAddress1;
                }

                if (!String.IsNullOrEmpty(supplierAddress2))
                {
                    cmd.Parameters["@supplierAddress2"].Value = supplierAddress2;
                }

                if (!String.IsNullOrEmpty(supplierZip))
                {
                    cmd.Parameters["@supplierZip"].Value = supplierZip;
                }

                if (!String.IsNullOrEmpty(supplierCity))
                {
                    cmd.Parameters["@supplierCity"].Value = supplierCity;
                }

                if (!String.IsNullOrEmpty(supplierUrl))
                {
                    cmd.Parameters["@supplierUrl"].Value = supplierUrl;
                }

                if (supplierMemo != null && supplierMemo.Length > 0)
                {
                    cmd.Parameters["@supplierMemo"].Value = supplierMemo;
                }

                if (!String.IsNullOrEmpty(supplierCountryName))
                {
                    cmd.Parameters["@supplierCountryName"].Value = supplierCountryName;
                }

                if (!String.IsNullOrEmpty(supplierCurrencySymbol))
                {
                    cmd.Parameters["@supplierCurrencySymbol"].Value = supplierCurrencySymbol;
                }

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table: Supplier

    #region Execute Non Query Table Supplier_Id: Supplier
    public int ExecuteNonQueryTblSupplierId(string sqlText, int supplierId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@supplierId", MySqlDbType.Int32).Value = supplierId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table Supplier_Id: Supplier

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

    #region Execute Non Query Table ProductSupplierId: SupplierContact
    public int ExecuteNonQueryTblSupplierContactId(string sqlText, int supplierContactId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@supplierContactId", MySqlDbType.Int32).Value = supplierContactId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table ProductSupplier_Id: ProductSupplier

    #region Uncheck the DefaultSupplier: ProductSupplier
    public int UncheckDefaultSupplierTblProductSupplier(string sqlText, int productSupplierId, int productSupplierProductId)
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
                cmd.Parameters.Add("@productSupplierProductId", MySqlDbType.Int32).Value = productSupplierProductId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion

    #region Get Data from Table: Supplier
    public DataTable GetDataTblSupplier(int supplierId = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (supplierId > 0)
        {
            sqlText = "SELECT * from Supplier where supplier_Id = @supplierId";
        }
        else
        {
            sqlText = "SELECT * from Supplier";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@supplierId", MySqlDbType.Int32).Value = supplierId;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }

        return dt;
    }
    #endregion Get Data from Table: Supplier

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
            sqlText = "SELECT * from ProductSupplier where productSupplier_ProductId = @ProductId";
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

    #region Get Data from Table: SupplierContact
    public DataTable GetDataTblSupplierContact(int SupplierId = 0)
    {
        DataTable dtSC = new DataTable();
        string sqlText = string.Empty;

        if (SupplierId > 0)
        {
            sqlText = "SELECT * from SupplierContact where suppliercontact_SupplierId = @SupplierId";
        }
        else
        {
            sqlText = "SELECT * from SupplierContact";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@SupplierId", MySqlDbType.Int32).Value = SupplierId;

            using MySqlDataAdapter daPS = new(cmd);
            //use DataAdapter to fill DataTable
            daPS.Fill(dtSC);
        }

        return dtSC;
    }
    #endregion

    #region Get Data from Table: Country
    public DataTable GetDataTblCountry(string supplierCountryName = "")
    {
        DataTable dtCountry = new DataTable();
        string sqlText = string.Empty;

        if (supplierCountryName != "")
        {
            sqlText = "SELECT country_Id, country_Code FROM country WHERE country_Name = @supplierCountryName";
        }
        else
        {
            sqlText = "SELECT country_Id, country_Code, country_Name FROM Country";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@supplierCountryName", MySqlDbType.String).Value = supplierCountryName;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dtCountry);
        }
        Console.WriteLine(dtCountry.Rows[0][0].ToString());

        return dtCountry;
    }
    #endregion Get Data from Table: Country

    #region Get Data from Table: Currency
    public DataTable GetDataTblCurrency(string supplierCurrencySymbol = "")
    {
        DataTable dtCurrency = new DataTable();
        string sqlText = string.Empty;

        if (supplierCurrencySymbol != "")
        {
            sqlText = "SELECT currency_Id, from Currency WHERE currency_Symbol = @supplierCurrencySymbol";
        }
        else
        {
            sqlText = "SELECT currency_Id, currency_Symbol from Currency";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@supplierCurrencySymbol", MySqlDbType.String).Value = supplierCurrencySymbol;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dtCurrency);
        }

        return dtCurrency;
    }
    #endregion Get Data from Table: Currency

    #region Insert in Table: Supplier
    public string InsertTblSupplier(string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, string supplierCountryName, int supplierCurrencyId, string supplierCurrencySymbol, double supplierOrderCosts, double supplierMinOrderCosts)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO Supplier (supplier_Code, supplier_Name, supplier_Address1, supplier_Address2, supplier_Zip, supplier_City, supplier_Url, supplier_Memo, supplier_CountryId, supplier_CountryName, supplier_CurrencyId, supplier_CurrencySymbol, supplier_ShippingCosts, supplier_MinShippingCosts) VALUES (@supplierCode, @supplierName, @supplierAddress1, @supplierAddress2, @supplierZip, @supplierCity, @supplierUrl, @supplierMemo, @supplierCountryId, @supplierCountryName, @supplierCurrencyId, @supplierCurrencySymbol, @supplierOrderCosts, @supplierMinOrderCosts);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCountryName, supplierCurrencyId, supplierCurrencySymbol, supplierOrderCosts, supplierMinOrderCosts);

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
            Debug.WriteLine("Error (UpdateTblSupplier - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblSupplier): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Insert in Table: Supplier

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
            throw ex;
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
    public string InsertTblProductSupplier(int productSupplierProductId, int productSupplierSupplierId, string productSupplierSupplierName, int productSupplierCurrencyId, string productSupplierCurrencySymbol, string productSupplierProductNumber, string productSupplierProductName, float productSupplierProductPrice, string productSupplierDefault)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO ProductSupplier (productSupplier_ProductId, productSupplier_SupplierId, productSupplier_SupplierName, productSupplier_CurrencyId, productSupplier_CurrencySymbol, productSupplier_ProductNumber,  productSupplier_ProductName, productSupplier_ProductPrice, productSupplier_Default) VALUES (@productSupplierProductId, @productSupplierSupplierId, @productSupplierSupplierName, @productSupplierCurrencyId, @productSupplierCurrencySymbol, @productSupplierProductNumber, @productSupplierProductName, @productSupplierProductPrice, @productSupplierDefault);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, productSupplierProductId, productSupplierSupplierId, productSupplierSupplierName, productSupplierCurrencyId, productSupplierCurrencySymbol, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice, productSupplierDefault);

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

    #region Insert in Table: SupplierContact
    public string InsertTblSupplierContact(int supplierId, string supplierContactContactName, int supplierContactContactTypeId, string supplierContactContactTypeName, string supplierContactContactPhone, string supplierContactContactMail)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO SupplierContact (suppliercontact_SupplierId, suppliercontact_Name, suppliercontact_TypeId, suppliercontact_TypeName, suppliercontact_Phone, suppliercontact_Mail) VALUES (@supplierId, @supplierContactContactName, @supplierContactContactTypeId, @supplierContactContactTypeName, @supplierContactContactPhone, @supplierContactContactMail);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplierContact(sqlText, supplierId, supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail);

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
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblProduct): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion

    #region Delete row in Table: Supplier
    public string DeleteTblSupplier(int supplierId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM Supplier WHERE supplier_Id=@supplierId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplierId(sqlText, supplierId);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij verwijderd.");
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblSupplier - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblSupplier): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: Supplier

    #region Delete row in Table: Product
    public string DeleteTblProduct(int productId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM Product WHERE product_Id=@productId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductId(sqlText, productId);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij verwijderd.");
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct - MySqlException): " + ex.Message);
            throw ex;
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
        string sqlText = "DELETE FROM ProductSupplier WHERE productSupplier_Id=@productSupplierId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplierId(sqlText, productSupplierId);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij verwijderd.");
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: Product

    #region Delete row in Table: SupplierContact
    public string DeleteTblSupplierContact(int supplierContactId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM SupplierContact WHERE supplierContact_Id=@supplierContactId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplierContactId(sqlText, supplierContactId);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij verwijderd.");
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblProduct): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: Product

    #region Update Table: Supplier
    public string UpdateTblSupplier(int supplierId, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, int supplierCountryId, string supplierCountryName, int supplierCurrencyId, string supplierCurrencySymbol, string supplierMemo, double supplierOrderCosts, double supplierMinOrderCosts)
    {
        string result = string.Empty;
        string sqlText = "UPDATE Supplier SET supplier_Code = @supplierCode, supplier_name = @supplierName, supplier_Address1 = @supplierAddress1, supplier_Address2 = @supplierAddress2, supplier_Zip = @supplierZip, supplier_City = @supplierCity, supplier_Url = @supplierUrl, supplier_CountryId = @supplierCountryId, supplier_CountryName = @supplierCountryName, supplier_CurrencyId = @supplierCurrencyId, supplier_CurrencySymbol = @supplierCurrencySymbol, supplier_Memo = @supplierMemo, supplier_ShippingCosts = @supplierShippingCosts, supplier_MinShippingCosts = @supplierMinShippingCosts WHERE supplier_Id = @supplierId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCountryName, supplierCurrencyId, supplierCurrencySymbol, supplierOrderCosts, supplierMinOrderCosts, supplierId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? supplierName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblSupplier - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblSupplier): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update Table: Supplier

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
    public string UpdateTblProductSupplier(int productSupplierId, int productSupplierProductId, int productSupplierSupplierId, string productSupplierSupplierName, int productSupplierCurrencyId, string productSupplierCurrencySymbol, string productSupplierProductNumber, string productSupplierProductName, float productSupplierProductPrice, string productSupplierDefault)
    {
        string result = string.Empty;
        string sqlText = "UPDATE ProductSupplier SET productSupplier_ProductId = @productSupplierProductId, productSupplier_SupplierId = @productSupplierSupplierId, productSupplier_SupplierName = @productSupplierSupplierName, productSupplier_CurrencyId = @productSupplierCurrencyId, productSupplier_CurrencySymbol = @productSupplierCurrencySymbol, productSupplier_ProductNumber = @productSupplierProductNumber,  productSupplier_ProductName = @productSupplierProductName, productSupplier_ProductPrice = @productSupplierProductPrice, productSupplier_Default = @productSupplierDefault WHERE productSupplier_Id = @productSupplierId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, productSupplierProductId, productSupplierSupplierId, productSupplierSupplierName, productSupplierCurrencyId, productSupplierCurrencySymbol, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice, productSupplierDefault, productSupplierId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? productSupplierProductName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
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

    #region Update Table: SupplierContact
    public string UpdateTblSupplierContact(int supplierContactContactId, int supplierId, string supplierContactContactName, int supplierContactContactTypeId, string supplierContactContactTypeName, string supplierContactContactPhone, string supplierContactContactMail)
    {
        string result = string.Empty;
        string sqlText = "UPDATE SupplierContact SET suppliercontact_SupplierId=@supplierId, suppliercontact_Name=@supplierContactContactName, suppliercontact_TypeId=@supplierContactContactTypeId, suppliercontact_TypeName=@supplierContactContactTypeName, suppliercontact_Phone=@supplierContactContactPhone, suppliercontact_Mail=@supplierContactContactMail WHERE suppliercontact_Id = @supplierContactContactId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblSupplierContact(sqlText, supplierId, supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail, supplierContactContactId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? supplierContactContactName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
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

    #region Unselect other default suppliers for product
    public string UncheckDefaultSupplierTblProductSupplier(int productSupplierId, int productSupplierProductId)
    {
        string result = string.Empty;
        string sqlText = "UPDATE ProductSupplier SET productSupplier_Default = '' WHERE productSupplier_Id != @productSupplierId AND productSupplier_ProductId = @productSupplierProductId;";

        try
        {
            int rowsAffected = UncheckDefaultSupplierTblProductSupplier(sqlText, productSupplierId, productSupplierProductId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? productSupplierId + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblProductSupplier - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblProductSupplier): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update Table: Product

    #region Execute Query Table
    public DataTable ExecuteQuery(string Base)
    {
        string result = string.Empty, sqlText = string.Empty;

        switch (Base)
        {
            case "Brand":
                sqlText = "SELECT Name, Id FROM brand ORDER by Id";
                break;
            default:
                sqlText = "UPDATE Product SET product_Code = @productCode, product_name = @productName, product_MinimalStock = @productMinimalStock,product_StandardOrderQuantity = @productStandardOrderQuantity, product_Price = @productPrice, product_SupplierProductNumber = @productSupplierProductNumber, product_ProjectCosts = @productProjectCosts, product_CategoryId = @productCategoryId, product_CategoryName = @productCategoryName, product_StorageId = @productStorageId, product_StorageName = @productStorageName, product_SupplierId = @productSupplierId, product_SupplierName = @productSupplierName, product_BrandId = @productBrandId, product_BrandName = @productBrandName, product_UnitId = @productUnitId, product_UnitName = @productUnitName, product_Memo = @productMemo WHERE product_Id = @productId;";
                break;
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);

        DataTable tempDataTable = new DataTable();
        tempDataTable.Load(cmd.ExecuteReader());

        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        return dt;
    }
    #endregion

    #region Create lists to populate dropdowns for metadata pages
    #region Fill the dropdownlists
    #region Fill Category dropdown
    public List<Category> CategoryList()
    {

        Database dbCategoryConnection = new()
        {
            TableName = DbCategoryTable
        };

        dbCategoryConnection.SqlSelectionString = "category_Name, category_Id";
        dbCategoryConnection.SqlOrderByString = "category_Id";
        dbCategoryConnection.TableName = DbCategoryTable;

        DataTable dtCategorySelection = dbCategoryConnection.LoadSpecificMySqlData();

        List<Category> CategoryList = new();

        for (int i = 0; i < dtCategorySelection.Rows.Count; i++)
        {
            CategoryList.Add(new Category(dtCategorySelection.Rows[i][0].ToString(),
                int.Parse(dtCategorySelection.Rows[i][1].ToString())));
        };
        return CategoryList;
    }
    #endregion

    #region Fill Country dropdown
    public List<Country> CountryList()
    {

        Database dbCountryConnection = new()
        {
            TableName = DbCountryTable
        };

        dbCountryConnection.SqlSelectionString = "country_Name, country_Id";
        dbCountryConnection.SqlOrderByString = "country_Id";
        dbCountryConnection.TableName = DbCountryTable;

        DataTable dtCountrySelection = dbCountryConnection.LoadSpecificMySqlData();

        List<Country> CountryList = new();

        for (int i = 0; i < dtCountrySelection.Rows.Count; i++)
        {
            CountryList.Add(new Country(dtCountrySelection.Rows[i][0].ToString(),
                int.Parse(dtCountrySelection.Rows[i][1].ToString())));
        };
        return CountryList;
    }
    #endregion

    #region Fill Currency dropdown
    public List<Currency> CurrencyList()
    {

        Database dbCurrencyConnection = new()
        {
            TableName = DbCurrencyTable
        };

        dbCurrencyConnection.SqlSelectionString = "currency_Symbol, currency_Id";
        dbCurrencyConnection.SqlOrderByString = "currency_Id";
        dbCurrencyConnection.TableName = DbCurrencyTable;

        DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

        List<Currency> CurrencyList = new();

        for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
        {
            CurrencyList.Add(new Currency(dtCurrencySelection.Rows[i][0].ToString(),
                dtCurrencySelection.Rows[i][1].ToString()));
        };
        return CurrencyList;
    }
    #endregion

    #region Fill Storage dropdown
    public List<Storage> StorageList()
    {

        Database dbStorageConnection = new()
        {
            TableName = DbStorageTable
        };

        dbStorageConnection.SqlSelectionString = "storage_Name, storage_Id";
        dbStorageConnection.SqlOrderByString = "storage_Id";
        dbStorageConnection.TableName = DbStorageTable;

        DataTable dtStorageSelection = dbStorageConnection.LoadSpecificMySqlData();

        List<Storage> StorageList = new();

        for (int i = 0; i < dtStorageSelection.Rows.Count; i++)
        {
            StorageList.Add(new Storage(dtStorageSelection.Rows[i][0].ToString(),
                int.Parse(dtStorageSelection.Rows[i][1].ToString())));
        };
        return StorageList;
    }
    #endregion

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

    #region Fill ContactType dropdown
    public List<ContactType> GetContactTypeList(List<ContactType> contactTypeList)
    {
        string DatabaseTable = DbContactTypeTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "contacttype_Name, contacttype_Id";
        dbConnection.SqlOrderByString = "contacttype_Name";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            contactTypeList.Add(new ContactType
            {
                ContactTypeName = dtSelection.Rows[i][0].ToString(),
                ContactTypeId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture),
            });
        };
        return contactTypeList;
    }
    #endregion

    #region Fill the Brand dropdown
    public List<Brand> GetBrandList(List<Brand> brandList)
    {
        string DatabaseTable = DbBrandTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Name";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            brandList.Add(new Brand
            {
                BrandName = dtSelection.Rows[i][0].ToString(),
                BrandId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        };
        return brandList;
    }
    #endregion
    #endregion Fill the dropdownlists

    #region Helper classes to for creating objects to populate dropdowns
    #region Create object for all categories in table for dropdown
    public class Category
    {
        public Category(string Name, int Id)
        {
            categoryName = Name;
            categoryId = Id;
        }

        public string categoryName { get; set; }
        public int categoryId { get; set; }
    }
    #endregion

    #region Create object for all countries in table for dropdown
    public class Country
    {
        public Country(string Name, int Id)
        {
            countryName = Name;
            countryId = Id;
        }

        public string countryName { get; set; }
        public int countryId { get; set; }
    }
    #endregion

    #region Create object for all currencies in table for dropdown
    public class Currency
    {
        public Currency(string Symbol, string Id)
        {
            currencySymbol = Symbol;
            currencyId = Id;
        }

        public string currencySymbol { get; set; }
        public string currencyId { get; set; }
    }
    #endregion

    #region Create object for all storage locations in table for dropdown
    public class Storage
    {
        public Storage(string Name, int Id)
        {
            storageName = Name;
            storageId = Id;
        }
        public string storageName { get; set; }
        public int storageId { get; set; }
    }
    #endregion

    #region Create object for all suppliers in table for dropdown
    public class Supplier
    {
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCurrencySymbol { get; set; }
        public int SupplierCurrencyId { get; set; }
    }
    #endregion

    #region Create object for all contacttypes in table for dropdown
    public class ContactType
    {
        public string ContactTypeName { get; set; }
        public int ContactTypeId { get; set; }
    }
    #endregion

    #region Create object for all brands in table for dropdown
    public class Brand
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
    }
    #endregion
    #endregion
    #endregion
}
