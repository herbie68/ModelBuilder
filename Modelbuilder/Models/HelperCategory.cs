using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder
{
    /// <summary>
    /// The helper category.
    /// </summary>
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
        /// <summary>
        /// Gets or Sets the connection str.
        /// </summary>
        public string ConnectionStr { get; set; }

        public string DbCategoryTable = "category";
        public CultureInfo Culture = new("nl-NL");
        #endregion public Variables

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
    }
}
