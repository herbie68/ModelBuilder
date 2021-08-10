using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using K4os.Compression.LZ4.Internal;

namespace Modelbuilder
{
    public class HelperMySQL
    {
        #region public Variables
        public string ConnectionStr { get; set; } = string.Empty;
        
        public string DatabaseCategoryTable = "category";
        public string DatabaseCountryTable = "country";
        public string DatabaseCurrencyTable = "currency";
        public string DatabaseStorageTable = "storage";
        public string DatabaseSupplierTable = "supplier";

        #endregion public Variables

        #region Connector to database
        public HelperMySQL(string serverName, string databaseName, string username, string userPwd)
        {
            ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
        }

        public HelperMySQL(string serverName, int portNumber, string databaseName, string username, string userPwd)
        {
            ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
        }
        #endregion Connector to database

        #region Execute Non Query
        public void ExecuteNonQuery(string sqlText)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionStr))
            {
                //open
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
                {
                    //execute
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion Execute NonQuery

        #region Execute Non Query Table: Supplier
        public int ExecuteNonQueryTblSupplier(string sqlText, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, string supplierCountryName, int supplierCurrencyId, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, float supplierOrderCosts, float supplierMinOrderCosts, int supplierId = 0)
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
                    cmd.Parameters.Add("@supplierPhoneGeneral", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierPhoneSales", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierPhoneSupport", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierMailGeneral", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierMailSales", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierMailSupport", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierOrderCosts", MySqlDbType.Float).Value = supplierOrderCosts;
                    cmd.Parameters.Add("@supplierMinOrderCosts", MySqlDbType.Float).Value = supplierMinOrderCosts;

                    //set values
                    if (!String.IsNullOrEmpty(supplierCode))
                        cmd.Parameters["@supplierCode"].Value = supplierCode;

                    if (!String.IsNullOrEmpty(supplierName))
                        cmd.Parameters["@supplierName"].Value = supplierName;

                    if (!String.IsNullOrEmpty(supplierAddress1))
                        cmd.Parameters["@supplierAddress1"].Value = supplierAddress1;

                    if (!String.IsNullOrEmpty(supplierAddress2))
                        cmd.Parameters["@supplierAddress2"].Value = supplierAddress2;

                    if (!String.IsNullOrEmpty(supplierZip))
                        cmd.Parameters["@supplierZip"].Value = supplierZip;

                    if (!String.IsNullOrEmpty(supplierCity))
                        cmd.Parameters["@supplierCity"].Value = supplierCity;

                    if (!String.IsNullOrEmpty(supplierUrl))
                        cmd.Parameters["@supplierUrl"].Value = supplierUrl;

                    if (supplierMemo != null && supplierMemo.Length > 0)
                        cmd.Parameters["@supplierMemo"].Value = supplierMemo;

                    if (!String.IsNullOrEmpty(supplierCountryName))
                        cmd.Parameters["@supplierCountryName"].Value = supplierCountryName;

                    if (!String.IsNullOrEmpty(supplierCurrencySymbol))
                        cmd.Parameters["@supplierCurrencySymbol"].Value = supplierCurrencySymbol;

                    if (!String.IsNullOrEmpty(supplierPhoneGeneral))
                        cmd.Parameters["@supplierPhoneGeneral"].Value = supplierPhoneGeneral;

                    if (!String.IsNullOrEmpty(supplierPhoneSales))
                        cmd.Parameters["@supplierPhoneSales"].Value = supplierPhoneSales;

                    if (!String.IsNullOrEmpty(supplierPhoneSupport))
                        cmd.Parameters["@supplierPhoneSupport"].Value = supplierPhoneSupport;

                    if (!String.IsNullOrEmpty(supplierMailGeneral))
                        cmd.Parameters["@supplierMailGeneral"].Value = supplierMailGeneral;

                    if (!String.IsNullOrEmpty(supplierMailSales))
                        cmd.Parameters["@supplierMailSales"].Value = supplierMailSales;

                    if (!String.IsNullOrEmpty(supplierMailSupport))
                        cmd.Parameters["@supplierMailSupport"].Value = supplierMailSupport;

                    //execute; returns the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        #endregion Execute Non Query Table: Supplier

        #region Execute Non Query Table: Product
        public int ExecuteNonQueryTblProduct(string sqlText, string productCode, string productName, float productMinimalStock, float productStandardOrderQuantity, float productPrice, string productSupplierProductNumber, int productProjectCosts, int productCategoryId, string productCategoryName, int productStorageId, string productStorageName, int productSupplierId, string productSupplierName, int productBrandId, string productBrandName, string productDimensions, int productUnitId, string productUnitName, string productMemo, int productId = 0)
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
                    // floating cells will be stored as INT and therefore will be multiplied by 100 before saving

                    //add parameters setting string values to DBNull.Value
                    cmd.Parameters.Add("@productId", MySqlDbType.Int32).Value = productId;
                    cmd.Parameters.Add("@productCode", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productMinimalStock", MySqlDbType.Float).Value = productMinimalStock;
                    cmd.Parameters.Add("@productStandardOrderQuantity", MySqlDbType.Float).Value = productStandardOrderQuantity;
                    cmd.Parameters.Add("@productPrice", MySqlDbType.Float).Value = productPrice;
                    cmd.Parameters.Add("@productSupplierProductNumber", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productProjectCosts", MySqlDbType.Int32).Value = productProjectCosts;
                    cmd.Parameters.Add("@productCategoryId", MySqlDbType.Int32).Value = productCategoryId;
                    cmd.Parameters.Add("@productCategoryName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productStorageId", MySqlDbType.Int32).Value = productStorageId;
                    cmd.Parameters.Add("@productStorageName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productSupplierId", MySqlDbType.Int32).Value = productSupplierId;
                    cmd.Parameters.Add("@productSupplierName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productBrandId", MySqlDbType.Int32).Value = productBrandId;
                    cmd.Parameters.Add("@productBrandName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productDimensions", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productUnitId", MySqlDbType.Int32).Value = productUnitId;
                    cmd.Parameters.Add("@productUnitName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productMemo", MySqlDbType.LongText).Value = DBNull.Value;

                    //set values
                    if (!String.IsNullOrEmpty(productCode))
                        cmd.Parameters["@productCode"].Value = productCode;

                    if (!String.IsNullOrEmpty(productName))
                        cmd.Parameters["@productName"].Value = productName;

                    if (!String.IsNullOrEmpty(productSupplierProductNumber))
                        cmd.Parameters["@productSupplierProductNumber"].Value = productSupplierProductNumber;

                    if (!String.IsNullOrEmpty(productCategoryName))
                        cmd.Parameters["@productCategoryName"].Value = productCategoryName;

                    if (!String.IsNullOrEmpty(productStorageName))
                        cmd.Parameters["@productStorageName"].Value = productStorageName;

                    if (!String.IsNullOrEmpty(productSupplierName))
                        cmd.Parameters["@productSupplierName"].Value = productSupplierName;

                    if (!String.IsNullOrEmpty(productBrandName))
                        cmd.Parameters["@productBrandName"].Value = productBrandName;

                    if (!String.IsNullOrEmpty(productUnitName))
                        cmd.Parameters["@productUnitName"].Value = productUnitName;

                    if (!String.IsNullOrEmpty(productDimensions))
                        cmd.Parameters["@productDimensions"].Value = productDimensions;

                    if (!String.IsNullOrEmpty(productMemo))
                        cmd.Parameters["@productMemo"].Value = productMemo;

                    //execute; returns the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        #endregion Execute Non Query Table: Product

        #region Execute Non Query Table: ProductSupplier
        public int ExecuteNonQueryTblProductSupplier(string sqlText, int productSupplierProductId, int productSupplierSupplierId, string productSupplierSupplierName, int productSupplierCurrencyId, string productSupplierCurrencySymbol, string productSupplierProductNumber, string productSupplierProductName, float productSupplierProductPrice, int productSupplierId = 0)
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
                    cmd.Parameters.Add("@productSupplierId", MySqlDbType.Int32).Value = productSupplierId;
                    cmd.Parameters.Add("@productSupplierProductId", MySqlDbType.Int32).Value = productSupplierProductId;
                    cmd.Parameters.Add("@productSupplierSupplierId", MySqlDbType.Int32).Value = productSupplierSupplierId;
                    cmd.Parameters.Add("@productSupplierSupplierName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productSupplierCurrencyId", MySqlDbType.Int32).Value = productSupplierCurrencyId;
                    cmd.Parameters.Add("@productSupplierCurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productSupplierProductNumber", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productSupplierProductName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@productSupplierProductPrice", MySqlDbType.Float).Value = productSupplierProductPrice;

                    //set values
                    if (!String.IsNullOrEmpty(productSupplierSupplierName))
                        cmd.Parameters["@productSupplierSupplierName"].Value = productSupplierSupplierName;

                    if (!String.IsNullOrEmpty(productSupplierCurrencySymbol))
                        cmd.Parameters["@productSupplierCurrencySymbol"].Value = productSupplierCurrencySymbol;

                    if (!String.IsNullOrEmpty(productSupplierProductNumber))
                        cmd.Parameters["@productSupplierProductNumber"].Value = productSupplierProductNumber;

                    if (!String.IsNullOrEmpty(productSupplierProductName))
                        cmd.Parameters["@productSupplierProductName"].Value = productSupplierProductName;

                    //execute; returns the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        #endregion Execute Non Query Table: ProductSupplier

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

        #region Execute Non Query Table Supplier_Id: Product
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
        public int ExecuteNonQueryTblProductSupplierId(string sqlText, int productId = 0)
        {
            int rowsAffected = 0;

            using (MySqlConnection con = new MySqlConnection(ConnectionStr))
            {
                //open
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
                {

                    //add parameters setting string values to DBNull.Value
                    cmd.Parameters.Add("@productSupplierId", MySqlDbType.Int32).Value = productId;

                    //execute; returns the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        #endregion Execute Non Query Table ProductSupplier_Id: ProductSupplier

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

        # region Get Data from Table: Country
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
        public string InsertTblSupplier(string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, string supplierCountryName, int supplierCurrencyId, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, float supplierOrderCosts, float supplierMinOrderCosts)
        {
            string result = string.Empty;
            string sqlText = "INSERT INTO Supplier (supplier_Code, supplier_Name, supplier_Address1, supplier_Address2, supplier_Zip, supplier_City, supplier_Url, supplier_Memo, supplier_CountryId, supplier_CountryName, supplier_CurrencyId, supplier_CurrencySymbol, supplier_PhoneGeneral, supplier_PhoneSales, supplier_PhoneSupport, supplier_MailGeneral, supplier_MailSales, supplier_MailSupport, supplier_OrderCosts, supplier_MinOrderCosts) VALUES (@supplierCode, @supplierName, @supplierAddress1, @supplierAddress2, @supplierZip, @supplierCity, @supplierUrl, @supplierMemo, @supplierCountryId, @supplierCountryName, @supplierCurrencyId, @supplierCurrencySymbol, @supplierPhoneGeneral, @supplierPhoneSales, @supplierPhoneSupport, @supplierMailGeneral, @supplierMailSales, @supplierMailSupport, @supplierOrderCosts, @supplierMinOrderCosts);";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCountryName, supplierCurrencyId, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport, supplierOrderCosts, supplierMinOrderCosts);

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
                Debug.WriteLine("Error (UpdateTblSupplier - MySqlException): " + ex.Message);
                throw ex;
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
        public string InsertTblProduct(string productCode, string productName, float productMinimalStock, float productStandardOrderQuantity, float productPrice, string productSupplierProductNumber, int productProjectCosts, int productCategoryId, string productCategoryName, int productStorageId, string productStorageName, int productSupplierId, string productSupplierName, int productBrandId, string productBrandName, string productDimensions, int productUnitId, string productUnitName, string productMemo)
        {
            string result = string.Empty;
            string sqlText = "INSERT INTO Product (product_Code, product_Name, product_MinimalStock, product_StandardOrderQuantity, product_Price, product_SupplierProductNumber, product_ProjectCosts, product_CategoryId, product_CategoryName, product_StorageId, product_StorageName, product_SupplierId, product_SupplierName, , product_BrandId, product_BrandName, product_Dimensions, product_UnitId, product_UnitName, product_Memo);";
            
            try
            {
                int rowsAffected = ExecuteNonQueryTblProduct(sqlText, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productSupplierProductNumber, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productSupplierId, productSupplierName, productBrandId, productBrandName, productDimensions, productUnitId, productUnitName, productMemo);

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
        #endregion Insert in Table: Product

        #region Insert in Table: ProductSupplier
        public string InsertTblProductSupplier(int productSupplierProductId, int productSupplierSupplierId, string productSupplierSupplierName, int productSupplierCurrencyId, string productSupplierCurrencySymbol, string productSupplierProductNumber, string productSupplierProductName, float productSupplierProductPrice)
        {
            string result = string.Empty;
            string sqlText = "INSERT INTO ProductSupplier (productSupplier_ProductId, productSupplier_SupplierId, productSupplier_SupplierName, productSupplier_CurrencyId, productSupplier_CurrencySymbol, productSupplier_ProductNumber,  productSupplier_ProductName, productSupplier_ProductPrice);";

            try
            {
                int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, productSupplierProductId, productSupplierSupplierId, productSupplierSupplierName, productSupplierCurrencyId, productSupplierCurrencySymbol, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice);

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
        #endregion Insert in Table: Product

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
                int rowsAffected = ExecuteNonQueryTblSupplierId(sqlText, productId);

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
        public string UpdateTblSupplier( int supplierId, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, int supplierCountryId, string supplierCountryName, int supplierCurrencyId, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, string supplierMemo, float supplierOrderCosts, float supplierMinOrderCosts)
        {
            string result = string.Empty;
            string sqlText = "UPDATE Supplier SET supplier_Code = @supplierCode, supplier_name = @supplierName, supplier_Address1 = @supplierAddress1, supplier_Address2 = @supplierAddress2, supplier_Zip = @supplierZip, supplier_City = @supplierCity, supplier_Url = @supplierUrl, supplier_CountryId = @supplierCountryId, supplier_CountryName = @supplierCountryName, supplier_CurrencyId = @supplierCurrencyId, supplier_CurrencySymbol = @supplierCurrencySymbol, supplier_PhoneGeneral = @supplierPhoneGeneral, supplier_PhoneSales = @supplierPhoneSales, supplier_PhoneSupport = @supplierPhoneSupport, supplier_MailGeneral = @supplierMailGeneral, supplier_MailSales = @supplierMailSales, supplier_MailSupport = @supplierMailSupport,supplier_Memo = @supplierMemo, supplier_OrderCosts = @supplierOrderCosts, supplier_MinOrderCosts = @supplierMinOrderCosts WHERE supplier_Id = @supplierId;";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCountryName, supplierCurrencyId, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport, supplierOrderCosts, supplierMinOrderCosts, supplierId);

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
        public string UpdateTblProduct(int productId, string productCode, string productName, float productMinimalStock, float productStandardOrderQuantity, float productPrice, string productSupplierProductNumber, int productProjectCosts, int productCategoryId, string productCategoryName, int productStorageId, string productStorageName, int productSupplierId, string productSupplierName, int productBrandId, string productBrandName, string productDimensions, int productUnitId, string productUnitName, string productMemo)
        {
            string result = string.Empty;
            string sqlText = "UPDATE Product SET product_Code = @productCode, product_name = @productName, product_MinimalStock = @productMinimalStock,product_StandardOrderQuantity = @productStandardOrderQuantity, product_Price = @productPrice, product_SupplierProductNumber = @productSupplierProductNumber, product_ProjectCosts = @productProjectCosts, product_CategoryId = @productCategoryId, product_CategoryName = @productCategoryName, product_StorageId = @productStorageId, product_StorageName = @productStorageName, product_SupplierId = @productSupplierId, product_SupplierName = @productSupplierName, product_BrandId = @productBrandId, product_BrandName = @productBrandName, product_Dimensions = @productDimensions, product_UnitId = @productUnitId, product_UnitName = @productUnitName, product_Memo = @productMemo WHERE product_Id = @productId;";

            try
            {
                int rowsAffected = ExecuteNonQueryTblProduct(sqlText, productCode, productName, productMinimalStock, productStandardOrderQuantity, productPrice, productSupplierProductNumber, productProjectCosts, productCategoryId, productCategoryName, productStorageId, productStorageName, productSupplierId, productSupplierName, productBrandId, productBrandName, productDimensions, productUnitId, productUnitName, productMemo, productId);

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
        public string UpdateTblProductSupplier(int productSupplierId, int productSupplierProductId, int productSupplierSupplierId, string productSupplierSupplierName, int productSupplierCurrencyId, string productSupplierCurrencySymbol, string productSupplierProductNumber, string productSupplierProductName, float productSupplierProductPrice)
        {
            string result = string.Empty;
            string sqlText = "UPDATE Product SET productSupplier_ProductId = @productSupplierProductId, productSupplier_SupplierId = @productSupplierSupplierId, productSupplier_SupplierName = @productSupplierSupplierName, productSupplier_CurrencyId = @productSupplierCurrencyId, productSupplier_CurrencySymbol = @productSupplierCurrencySymbol, productSupplier_ProductNumber = @productSupplierProductNumber,  productSupplier_ProductName = @productSupplierProductName, productSupplier_ProductPrice = @productSupplierProductPrice WHERE productSupplier_Id = @productSupplierId;";

            try
            {
                int rowsAffected = ExecuteNonQueryTblProductSupplier(sqlText, productSupplierProductId, productSupplierSupplierId, productSupplierSupplierName, productSupplierCurrencyId, productSupplierCurrencySymbol, productSupplierProductNumber, productSupplierProductName, productSupplierProductPrice, productSupplierId);

                //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
                result = rowsAffected > 0 ? productSupplierProductName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
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

        #region Create lists to populate dropdowns for metadata pages
        #region Fill the dropdownlists
        #region Fill Category dropdown
        public List<Category> CategoryList()
        {

            Database dbCategoryConnection = new()
            {
                TableName = DatabaseCategoryTable
            };

            dbCategoryConnection.SqlSelectionString = "category_Name, category_Id";
            dbCategoryConnection.SqlOrderByString = "category_Id";
            dbCategoryConnection.TableName = DatabaseCategoryTable;

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
                TableName = DatabaseCountryTable
            };

            dbCountryConnection.SqlSelectionString = "country_Name, country_Id";
            dbCountryConnection.SqlOrderByString = "country_Id";
            dbCountryConnection.TableName = DatabaseCountryTable;

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
                TableName = DatabaseCurrencyTable
            };

            dbCurrencyConnection.SqlSelectionString = "currency_Symbol, currency_Id";
            dbCurrencyConnection.SqlOrderByString = "currency_Id";
            dbCurrencyConnection.TableName = DatabaseCurrencyTable;

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
                TableName = DatabaseStorageTable
            };

            dbStorageConnection.SqlSelectionString = "storage_Name, storage_Id";
            dbStorageConnection.SqlOrderByString = "storage_Id";
            dbStorageConnection.TableName = DatabaseStorageTable;

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
        public List<Supplier> SupplierList()
        {

            Database dbSupplierConnection = new()
            {
                TableName = DatabaseSupplierTable
            };

            dbSupplierConnection.SqlSelectionString = "supplier_Name, supplier_Id";
            dbSupplierConnection.SqlOrderByString = "supplier_Id";
            dbSupplierConnection.TableName = DatabaseSupplierTable;

            DataTable dtSupplierSelection = dbSupplierConnection.LoadSpecificMySqlData();

            List<Supplier> SupplierList = new();

            for (int i = 0; i < dtSupplierSelection.Rows.Count; i++)
            {
                SupplierList.Add(new Supplier(dtSupplierSelection.Rows[i][0].ToString(),
                    int.Parse(dtSupplierSelection.Rows[i][1].ToString())));
            };
            return SupplierList;
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
            public Supplier(string Name, int Id)
            {
                supplierName = Name;
                supplierId = Id;
            }

            public string supplierName { get; set; }
            public int supplierId { get; set; }
        }
        #endregion
        #endregion Helper classes to for creating objects to populate dropdowns
        #endregion Create lists to populate dropdowns for metadata pages
    }
}
