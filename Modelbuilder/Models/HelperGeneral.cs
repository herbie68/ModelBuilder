using ConnectionNamespace;

using MySql.Data.MySqlClient;

namespace Modelbuilder;
/// <summary>
/// Initializes a new instance of the <see cref="HelperGeneral"/> class.
/// </summary>
internal class HelperGeneral
{
    #region public Variables
    /// <summary>
    /// Gets or Sets the connection str.
    /// </summary>
    public string ConnectionStr { get; set; }

    public string DbBrandTable = "brand";
    public string DbCategoryTable = "category";
    public string DbContactTypeTable = "contacttype";
    public string DbCountryTable = "country";
    public string DbCurrencyTable = "currency";
    public string DbOrderTable = "supplyorder";
    public string DbOrderLineTable = "supplyorderline";
    public string DbProductTable = "product";
    public string DbProductSupplierTable = "productsupplier";
    public string DbProjectTable = "project";
    public string DbStorageTable = "storage";
    public string DbSupplierTable = "supplier";
    public string DbUnitTable = "unit";
    public string DbWorktypeTable = "worktype";

    public CultureInfo Culture = new("nl-NL");

    /// <summary>
    /// Gets or Sets the sql order by string.
    /// </summary>
    public string SqlOrderByString { get; set; }
    /// <summary>
    /// Gets or Sets the sql selection string.
    /// </summary>
    public string SqlSelectionString { get; set; }
    /// <summary>
    /// Gets or Sets the sql where string.
    /// </summary>
    public string SqlWhereString { get; set; }
    /// <summary>
    /// Gets or Sets the table name.
    /// </summary>
    public string TableName { get; set; }
    #endregion public Variables

    #region Connector to database
    /// <summary>
    /// Initializes a new instance of the <see cref="HelperGeneral"/> class.
    /// </summary>
    /// <param name="serverName">The server name.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="username">The username.</param>
    /// <param name="userPwd">The user pwd.</param>
    public HelperGeneral(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HelperGeneral"/> class.
    /// </summary>
    /// <param name="serverName">The server name.</param>
    /// <param name="portNumber">The port number.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="username">The username.</param>
    /// <param name="userPwd">The user pwd.</param>
    public HelperGeneral(string serverName, int portNumber, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
    }
    #endregion Connector to database

    #region Execute Non Query
    /// <summary>
    /// Executes the non query.
    /// </summary>
    /// <param name="sqlText">The sql text.</param>
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

    #region Create lists to populate dropdowns for order Page
    #region Fill the dropdownlists
    #region Fill Brand dropdown
    /// <summary>
    /// Gets the brand list.
    /// </summary>
    /// <param name="brandList">The brand list.</param>
    /// <returns><![CDATA[List<Brand>]]></returns>
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
        }
        return brandList;
    }
    #endregion Brand dropdown

    #region Fill Category dropdown
    /// <summary>
    /// Gets the category list.
    /// </summary>
    /// <param name="categoryList">The category list.</param>
    /// <returns><![CDATA[List<Category>]]></returns>
    public List<Category> GetCategoryList(List<Category> categoryList)
    {

        string DatabaseTable = DbCategoryTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DbCategoryTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            categoryList.Add(new Category
            {
                CategoryName = dtSelection.Rows[i][0].ToString(),
                CategoryId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });

        }
        return categoryList;
    }
    #endregion

    #region Fill ContactType dropdown
    /// <summary>
    /// Gets the contact type list.
    /// </summary>
    /// <param name="contactTypeList">The contact type list.</param>
    /// <returns><![CDATA[List<ContactType>]]></returns>
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

    #region Fill Country dropdown
    /// <summary>
    /// Gets the country list.
    /// </summary>
    /// <param name="countryList">The country list.</param>
    /// <returns><![CDATA[List<Country>]]></returns>
    public List<Country> GetCountryList(List<Country> countryList)
    {

        string DatabaseTable = DbCountryTable;
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
            countryList.Add(new Country
            {
                CountryName = dtSelection.Rows[i][0].ToString(),
                CountryId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return countryList;
    }
    #endregion

    #region Fill Currency dropdown
    /// <summary>
    /// Gets the currency list.
    /// </summary>
    /// <param name="currencyList">The currency list.</param>
    /// <returns><![CDATA[List<Currency>]]></returns>
    public List<Currency> GetCurrencyList(List<Currency> currencyList)
    {
        string DatabaseTable = DbCurrencyTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Symbol, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            currencyList.Add(new Currency
            {
                CurrencySymbol = dtSelection.Rows[i][0].ToString(),
                CurrencyId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return currencyList;
    }
    # endregion Currency dropdown

    #region Fill Project dropdown
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
    #endregion Fill Project dropdown

    #region Fill Product dropdown
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
    #endregion Fill Project dropdown

    #region Fill Storage dropdown
    public List<Storage> GetStorageList(List<Storage> storageList)
    {

        string DatabaseTable = DbStorageTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DbStorageTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            storageList.Add(new Storage
            {
                StorageName = dtSelection.Rows[i][0].ToString(),
                StorageId = int.Parse(dtSelection.Rows[i][1].ToString())
                });
        }
        return storageList;
    }
    #endregion Fill Storage dropdown

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

    #region Fill Unit dropdown
    public List<Unit> GetUnitList(List<Unit> unitList)
    {
        string DatabaseTable = DbUnitTable;
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
            unitList.Add(new Unit
            {
                UnitName = dtSelection.Rows[i][0].ToString(),
                UnitId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return unitList;
    }
    #endregion
    #endregion Fill the dropdownlists

    #region Helper classes to for creating objects to populate dropdowns
    #region Create object for all brands in table for dropdown
    public class Brand
    {
        /// <summary>
        /// Gets or Sets the brand name.
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// Gets or Sets the brand id.
        /// </summary>
        public int BrandId { get; set; }
    }
    #endregion Create object for all brands in table for dropdown

    #region Create object for all categories in table for dropdown
    public class Category
    {
        /// <summary>
        /// Gets or Sets the category name.
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Gets or Sets the category id.
        /// </summary>
        public int CategoryId { get; set; }
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

    #region Create object for all countries in table for dropdown
    public class Country
    {
        /// <summary>
        /// Gets or Sets the country name.
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// Gets or Sets the country id.
        /// </summary>
        public int CountryId { get; set; }
    }
    #endregion

    #region Create object for all currencies in table for dropdown
    public class Currency
    {
        /// <summary>
        /// Gets or Sets the currency symbol.
        /// </summary>
        public string CurrencySymbol { get; set; }
        /// <summary>
        /// Gets or Sets the currency id.
        /// </summary>
        public int CurrencyId { get; set; }
    }
    #endregion Create object for all currencies in table for dropdown

    #region Create object for all projects in table for dropdown
    public class Project
    {
        /// <summary>
        /// Gets or Sets the project name.
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Gets or Sets the project id.
        /// </summary>
        public int ProjectId { get; set; }
    }
    #endregion Create object for all projects in table for dropdown

    #region Create object for all products in table for dropdown
    public class Product
    {
        /// <summary>
        /// Gets or Sets the product name.
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Gets or Sets the product id.
        /// </summary>
        public int ProductId { get; set; }
    }
    #endregion Create object for all products in table for dropdown    

    #region Create object for all storage locations in table for dropdown
    public class Storage
    {
        /// <summary>
        /// Gets or Sets the storage name.
        /// </summary>
        public string StorageName { get; set; }
        /// <summary>
        /// Gets or Sets the storage id.
        /// </summary>
        public int StorageId { get; set; }
    }
    #endregion Create object for all storage locations in table for dropdown

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
    #endregion Create object for all suppliers in table for dropdown

    #region Create object for all units in table for dropdown
    public class Unit
    {
        /// <summary>
        /// Gets or Sets the unit name.
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// Gets or Sets the unit id.
        /// </summary>
        public int UnitId { get; set; }
    }
    #endregion
    #endregion Helper classes to for creating objects to populate dropdowns
    #endregion Create lists to populate dropdowns for order Page
}

