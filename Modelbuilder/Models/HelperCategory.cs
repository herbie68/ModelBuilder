using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder
{
    internal class HelperCategory
    {
        #region Available databasefields
        /// ***************************************************************************************
        ///   Available fields for Category table
        /// ***************************************************************************************
        ///   Table Fieldname           	Variable            Parameter      		Type
        ///   -----------------------------------------------------------------------------------
        ///   Id						Id					@Id					int
        ///   ParentId				    ParentId			@ParentId			int
        ///   Name                      Name                @Name               varchar
        ///   FullPath 					FullPath			@FullPath			varchar
        ///   Created 			                                                datetime
        ///   Modified 			                                                datetime
        /// ***************************************************************************************
        #endregion Available databasefields

        #region public Variables
        public string ConnectionStr { get; set; }
        public string DbCategoryTable = "category";
        public CultureInfo Culture = new("nl-NL");
        #endregion public Variables

        #region Connector to database
        public HelperCategory(string serverName, string databaseName, string username, string userPwd)
        {
            ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
        }

        public HelperCategory(string serverName, int portNumber, string databaseName, string username, string userPwd)
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
        
        #region Execute Non Query Table: Category
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="Name"></param>
        /// <param name="ParentId"></param>
        /// <param name="FullPath"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int ExecuteNonQueryTblCategory(string sqlText, string Name, int ParentId, string FullPath, int Id = 0)
        {
            int rowsAffected = 0;

            using (MySqlConnection con = new(ConnectionStr))
            {
                con.Open();

                using MySqlCommand cmd = new(sqlText, con);
                // Add Int values
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;
                cmd.Parameters.Add("@ParentId", MySqlDbType.Int32).Value = ParentId;

                // Add VarChar values
                cmd.Parameters.Add("@FullPath", MySqlDbType.VarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = DBNull.Value;

                //set values
                if (!String.IsNullOrEmpty(FullPath))
                {
                    cmd.Parameters["@FullPath"].Value = FullPath;
                }

                if (!String.IsNullOrEmpty(Name))
                {
                    cmd.Parameters["@Name"].Value = Name;
                }

                rowsAffected = cmd.ExecuteNonQuery();
            }
            return rowsAffected;
        }
        #endregion

        #region Fill Category dropdown
        public List<Category> CategoryList()
        {

            Database dbCategoryConnection = new()
            {
                TableName = DbCategoryTable
            };

            dbCategoryConnection.SqlSelectionString = "Name, Id";
            dbCategoryConnection.SqlOrderByString = "Id";
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
        #endregion Fill Category dropdown

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
        #endregion Create object for all categories in table for dropdown
    }
}
