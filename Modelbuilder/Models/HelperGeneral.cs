using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Modelbuilder.HelperMySql;

namespace Modelbuilder
{
    internal class HelperGeneral
    {
        #region public Variables
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
        #endregion public Variables

        #region Connector to database
        public HelperGeneral(string serverName, string databaseName, string username, string userPwd)
        {
            ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
        }

        public HelperGeneral(string serverName, int portNumber, string databaseName, string username, string userPwd)
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

        #region Create lists to populate dropdowns for order Page
        #region Fill the dropdownlists
        #region Fill Brand dropdown
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
        # endregion Brand dropdown

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

        #region Fill Project dropdown
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

            dbConnection.SqlSelectionString = "product_Name, product_Id";
            dbConnection.SqlOrderByString = "product_Id";
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
        #endregion Fill Storage dropdown

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
            }
            return supplierList;
        }
        #endregion Fill Supplier dropdown
        #endregion Fill the dropdownlists

        #region Helper classes to for creating objects to populate dropdowns
        #region Create object for all brands in table for dropdown
        public class Brand
        {
            public string BrandName { get; set; }
            public int BrandId { get; set; }
        }
        #endregion Create object for all brands in table for dropdown

        #region Create object for all categories in table for dropdown
        public class Category
        {
            public string CategoryName { get; set; }
            public int CategoryId { get; set; }
        }
        #endregion

        #region Create object for all contacttypes in table for dropdown
        public class ContactType
        {
            public string ContactTypeName { get; set; }
            public int ContactTypeId { get; set; }
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
        #endregion Create object for all products in table for dropdown    

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
        #endregion Create object for all storage locations in table for dropdown

        #region Create object for all suppliers in table for dropdown
        public class Supplier
        {
            public string SupplierName { get; set; }
            public int SupplierId { get; set; }
            public string SupplierCurrencySymbol { get; set; }
            public int SupplierCurrencyId { get; set; }
        }
        #endregion Create object for all suppliers in table for dropdown
        #endregion Helper classes to for creating objects to populate dropdowns
        #endregion Create lists to populate dropdowns for order Page
    }
}
