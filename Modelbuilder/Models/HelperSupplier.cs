namespace Modelbuilder
{
    internal class HelperSupplier
    {
        #region public Variables
        /// <summary>
        /// Gets or Sets the connection str.
        /// </summary>
        public string ConnectionStr { get; set; }

        public string DbBrandTable = "brand";
        public string DbCategoryTable = "category";
        public string DbCountryTable = "country";
        public string DbCurrencyTable = "currency";
        public string DbProductTable = "product";
        public string DbProductSupplierTable = "productsupplier";
        public string DbProjectTable = "project";
        public string DbStorageTable = "storage";
        public string DbSupplierTable = "supplier";
        public string DbSupplierContactTable = "suppliercontact";
        public string DbUnitTable = "unit";
        public string DbWorktypeTable = "worktype";
        public string DbContactTypeTable = "contacttype";

        public CultureInfo Culture = new("nl-NL");

        #endregion public Variables

        #region Connector to database
        public HelperSupplier(string serverName, string databaseName, string username, string userPwd)
        {
            ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
        }

        public HelperSupplier(string serverName, int portNumber, string databaseName, string username, string userPwd)
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
        public int ExecuteNonQueryTblSupplier(string sqlText, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, int supplierCurrencyId, double supplierShippingCosts, double supplierMinShippingCosts, double supplierOrderCosts, int supplierId = 0)
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
                    cmd.Parameters.Add("@supplierCurrencyId", MySqlDbType.Int32).Value = supplierCurrencyId;
                    cmd.Parameters.Add("@supplierShippingCosts", MySqlDbType.Float).Value = supplierShippingCosts;
                    cmd.Parameters.Add("@supplierMinShippingCosts", MySqlDbType.Float).Value = supplierMinShippingCosts;
                    cmd.Parameters.Add("@supplierOrderCosts", MySqlDbType.Float).Value = supplierOrderCosts;

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

                    //execute; returns the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        #endregion Execute Non Query Table: Supplier

        #region Execute Non Query Table Id: Supplier
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
        #endregion Execute Non Query Table Id: Supplier

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
        #endregion Execute Non Query Table Id: ProductSupplier

        #region Get Data from Table: Supplier
        public DataTable GetDataTblSupplier(int supplierId = 0)
        {
            DataTable dt = new DataTable();
            string sqlText = string.Empty;

            if (supplierId > 0)
            {
                sqlText = "SELECT * from " + DbSupplierTable + " where Id = @supplierId";
            }
            else
            {
                sqlText = "SELECT * from " + DbSupplierTable;
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

        #region Get Data from Table: SupplierContact
        public DataTable GetDataTblSupplierContact(int SupplierId = 0)
        {
            DataTable dtSC = new DataTable();
            string sqlText = string.Empty;

            if (SupplierId > 0)
            {
                sqlText = "SELECT * from " + DbSupplierContactTable + " where Id = @SupplierId";
            }
            else
            {
                sqlText = "SELECT * from " + DbSupplierContactTable;
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

        #region Insert in Table: Supplier
        public string InsertTblSupplier(string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, int supplierCurrencyId, double supplierShippingCosts, double supplierMinShippingCosts, double supplierOrderCosts)
        {
            string result = string.Empty;
            string sqlText = "INSERT INTO " + DbSupplierTable + " (Code, Name, Address1, Address2, Zip, City, Url, Memo, CountryId, CurrencyId, ShippingCosts, MinShippingCosts, OrderCosts) VALUES (@supplierCode, @supplierName, @supplierAddress1, @supplierAddress2, @supplierZip, @supplierCity, @supplierUrl, @supplierMemo, @supplierCountryId, @supplierCurrencyId, @supplierShippingCosts, @supplierMinShippingCosts, @supplierOrderCosts);";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCurrencyId, supplierShippingCosts, supplierMinShippingCosts, supplierOrderCosts);

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

        #region Insert in Table: SupplierContact
        public string InsertTblSupplierContact(int supplierId, string supplierContactContactName, int supplierContactContactTypeId, string supplierContactContactTypeName, string supplierContactContactPhone, string supplierContactContactMail)
        {
            string result = string.Empty;
            string sqlText = "INSERT INTO " + DbSupplierContactTable + " (SupplierId, Name, TypeId, TypeName, Phone, Mail) VALUES (@supplierId, @supplierContactContactName, @supplierContactContactTypeId, @supplierContactContactTypeName, @supplierContactContactPhone, @supplierContactContactMail);";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplierContact(sqlText, supplierId, supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail);

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
        #endregion

        #region Delete row in Table: Supplier
        public string DeleteTblSupplier(int supplierId)
        {
            string result = string.Empty;
            string sqlText = "DELETE FROM " + DbSupplierTable + " WHERE Id=@supplierId";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplierId(sqlText, supplierId);

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
                Debug.WriteLine("Error (DeleteTblSupplier - MySqlException): " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error (DeleteTblSupplier): " + ex.Message);
                throw;
            }

            return result;
        }
        #endregion Delete row in Table: Supplier

        #region Delete row in Table: SupplierContact
        public string DeleteTblSupplierContact(int supplierContactId)
        {
            string result = string.Empty;
            string sqlText = "DELETE FROM " + DbSupplierContactTable + " WHERE Id=@supplierContactId";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplierContactId(sqlText, supplierContactId);

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

        #region Update Table: Supplier
        public string UpdateTblSupplier(int supplierId, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, int supplierCountryId, int supplierCurrencyId, string supplierMemo, double supplierShippingCosts, double supplierMinShippingCosts, double supplierOrderCosts)
        {
            string result = string.Empty;
            string sqlText = "UPDATE " + DbSupplierTable + " SET Code = @supplierCode, name = @supplierName, Address1 = @supplierAddress1, Address2 = @supplierAddress2, Zip = @supplierZip, City = @supplierCity, Url = @supplierUrl, CountryId = @supplierCountryId, CurrencyId = @supplierCurrencyId, Memo = @supplierMemo, ShippingCosts = @supplierShippingCosts, MinShippingCosts = @supplierMinShippingCosts, OrderCosts = @supplierOrderCosts WHERE Id = @supplierId;";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCurrencyId, supplierShippingCosts, supplierMinShippingCosts, supplierOrderCosts, supplierId);

                //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
                result = rowsAffected > 0 ? supplierName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Fout (UpdateTblSupplier - MySqlException): " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fout (UpdateTblSupplier): " + ex.Message);
                throw;
            }

            return result;
        }
        #endregion Update Table: Supplier

        #region Update Table: SupplierContact
        public string UpdateTblSupplierContact(int supplierContactContactId, int supplierId, string supplierContactContactName, int supplierContactContactTypeId, string supplierContactContactTypeName, string supplierContactContactPhone, string supplierContactContactMail)
        {
            string result = string.Empty;
            string sqlText = "UPDATE " + DbSupplierContactTable + " SET SupplierId=@supplierId, Name=@supplierContactContactName, TypeId=@supplierContactContactTypeId, TypeName=@supplierContactContactTypeName, Phone=@supplierContactContactPhone, Mail=@supplierContactContactMail WHERE Id = @supplierContactContactId;";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplierContact(sqlText, supplierId, supplierContactContactName, supplierContactContactTypeId, supplierContactContactTypeName, supplierContactContactPhone, supplierContactContactMail, supplierContactContactId);

                //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
                result = rowsAffected > 0 ? supplierContactContactName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
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
            string sqlText = string.Empty;

            switch (Base)
            {
                case "Brand":
                    sqlText = "SELECT Name, Id FROM brand ORDER by Id";
                    break;
                default:
                    sqlText = "UPDATE Product SET Code = @productCode, name = @productName, MinimalStock = @productMinimalStock,StandardOrderQuantity = @productStandardOrderQuantity, Price = @productPrice, SupplierProductNumber = @productSupplierProductNumber, ProjectCosts = @productProjectCosts, CategoryId = @productCategoryId, CategoryName = @productCategoryName, StorageId = @productStorageId, StorageName = @productStorageName, SupplierId = @productSupplierId, SupplierName = @productSupplierName, BrandId = @productBrandId, BrandName = @productBrandName, UnitId = @productUnitId, UnitName = @productUnitName, Memo = @productMemo WHERE Id = @productId;";
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
        #region Fill Country dropdown
        public List<Country> CountryList()
        {

            Database dbCountryConnection = new()
            {
                TableName = DbCountryTable
            };

            dbCountryConnection.SqlSelectionString = "Name, Id";
            dbCountryConnection.SqlOrderByString = "Id";
            dbCountryConnection.TableName = DbCountryTable;

            DataTable dtCountrySelection = dbCountryConnection.LoadSpecificMySqlData();

            List<Country> CountryList = new();

            for (int i = 0; i < dtCountrySelection.Rows.Count; i++)
            {
                CountryList.Add(new Country(dtCountrySelection.Rows[i][0].ToString(),
                    int.Parse(dtCountrySelection.Rows[i][1].ToString())));
            }
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

            dbCurrencyConnection.SqlSelectionString = "Symbol, Id";
            dbCurrencyConnection.SqlOrderByString = "Id";
            dbCurrencyConnection.TableName = DbCurrencyTable;

            DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

            List<Currency> CurrencyList = new();

            for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
            {
                CurrencyList.Add(new Currency(dtCurrencySelection.Rows[i][0].ToString(),
                    dtCurrencySelection.Rows[i][1].ToString()));
            }
            return CurrencyList;
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
        #endregion

        #region Fill ContactType dropdown
        public List<ContactType> GetContactTypeList(List<ContactType> contactTypeList)
        {
            string DatabaseTable = DbContactTypeTable;
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
                contactTypeList.Add(new ContactType
                {
                    ContactTypeName = dtSelection.Rows[i][0].ToString(),
                    ContactTypeId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture),
                });
            }
            return contactTypeList;
        }
        #endregion
        #endregion Fill the dropdownlists

        #region Helper classes to for creating objects to populate dropdowns
        #region Create object for all countries in table for dropdown
        public class Country
        {
            public Country(string Name, int Id)
            {
                countryName = Name;
                countryId = Id;
            }

            /// <summary>
            /// Gets or Sets the country name.
            /// </summary>
            public string countryName { get; set; }
            /// <summary>
            /// Gets or Sets the country id.
            /// </summary>
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

            /// <summary>
            /// Gets or Sets the currency symbol.
            /// </summary>
            public string currencySymbol { get; set; }
            /// <summary>
            /// Gets or Sets the currency id.
            /// </summary>
            public string currencyId { get; set; }
        }
        #endregion

        #region Create object for all suppliers in table for dropdown
        public class Supplier
        {
            /// <summary>
            /// Gets or Sets the supplier name.
            /// </summary>
            public string SupplierName { get; set; }
            /// <summary>
            /// Gets or Sets the supplier id.
            /// </summary>
            public int SupplierId { get; set; }
            /// <summary>
            /// Gets or Sets the supplier currency symbol.
            /// </summary>
            public string SupplierCurrencySymbol { get; set; }
            /// <summary>
            /// Gets or Sets the supplier currency id.
            /// </summary>
            public int SupplierCurrencyId { get; set; }
        }
        #endregion

        #region Create object for all contacttypes in table for dropdown
        public class ContactType
        {
            /// <summary>
            /// Gets or Sets the contact type name.
            /// </summary>
            public string ContactTypeName { get; set; }
            /// <summary>
            /// Gets or Sets the contact type id.
            /// </summary>
            public int ContactTypeId { get; set; }
        }
        #endregion
        #endregion Helper classes to for creating objects to populate dropdowns
        #endregion Create lists to populate dropdowns for metadata pages
    }
}
