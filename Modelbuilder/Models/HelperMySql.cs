using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder
{
    public class HelperMySQL
    {
        public string ConnectionStr { get; set; } = string.Empty;

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

        public void ExecuteNonQuery(string sqlText)
        {
            using MySqlConnection con = new(ConnectionStr);

            con.Open();

            using MySqlCommand cmd = new(sqlText, con);

            _ = cmd.ExecuteNonQuery();
        }

        public int ExecuteNonQueryTblSupplier(string sqlext, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, int supplierCountryId, string supplierCountryCode, string supplierCountryName, int supplierCurrencyId, string supplierCurrencyCode, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, string supplierMemo, int supplierId = 0)
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

                    //execute; returns the number of rows affected
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

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

        public string UpdateTblSupplier(int supplierId, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, int supplierCountryId, string supplierCountryCode, string supplierCountryName, int supplierCurrencyId, string supplierCurrencyCode, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, string supplierMemo)
        {
            string result = string.Empty;
            string sqlText = "UPDATE Supplier SET supplier_Code = @supplierCode, supplier_name = @supplierName, supplier_Address1 = @supplierAddress1, supplier_Address2 = @supplierAddress2, supplier_Zip = @supplierZip, supplier_City = @supplierCity, supplier_Url = @supplierUrl, supplier_CountryId = @supplierCountryId, supplier_CountryCode = @supplierCountryCode, supplier_CountryName = @supplierCountryName, supplier_CurrencyId = @supplierCurrencyId, supplier_CurrencyCode = @supplierCurrencyCode, supplier_CurrencyCode = @supplierCurrencySymbol, supplier_PhoneGeneral = @supplierPhoneGeneral, supplier_PhoneSales = @supplierPhoneSales, supplier_PhoneSupport = @supplierPhoneSupport, supplier_MailGeneral = @supplierMailGeneral, supplier_MailSales = @supplierMailSales, supplier_MailSupport = @supplierMailSupport,supplier_Memo = @supplierMemo WHERE supplier_Id = @supplierId;";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierCountryId, supplierCountryCode, supplierCountryName, supplierCurrencyId, supplierCurrencyCode, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport, supplierMemo, supplierId);

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
    }
}
