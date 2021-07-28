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
        #region public Variables
        public string ConnectionStr { get; set; } = string.Empty;
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
        public int ExecuteNonQueryTblSupplier(string sqlText, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, string supplierCountryCode, string supplierCountryName, int supplierCurrencyId, string supplierCurrencyCode, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, int supplierId = 0)
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
                    cmd.Parameters.Add("@supplierCountryCode", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierCountryName", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierCurrencyId", MySqlDbType.Int32).Value = supplierCurrencyId;
                    cmd.Parameters.Add("@supplierCurrencyCode", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierCurrencySymbol", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierPhoneGeneral", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierPhoneSales", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierPhoneSupport", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierMailGeneral", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierMailSales", MySqlDbType.VarChar).Value = DBNull.Value;
                    cmd.Parameters.Add("@supplierMailSupport", MySqlDbType.VarChar).Value = DBNull.Value;

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

                    if (!String.IsNullOrEmpty(supplierCountryCode))
                        cmd.Parameters["@supplierCountryCode"].Value = supplierCountryCode;

                    if (!String.IsNullOrEmpty(supplierCountryName))
                        cmd.Parameters["@supplierCountryName"].Value = supplierCountryName;

                    if (!String.IsNullOrEmpty(supplierCurrencyCode))
                        cmd.Parameters["@supplierCurrencyCode"].Value = supplierCurrencyCode;

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
                sqlText = "SELECT currency_Id, currencyy_Code from Currency WHERE currency_Symbol = @supplierCurrencySymbol";
            }
            else
            {
                sqlText = "SELECT currency_Id, currencyy_Code, currency_Symbol from Currency";
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
        public string InsertTblSupplier(string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, string supplierMemo, int supplierCountryId, string supplierCountryCode, string supplierCountryName, int supplierCurrencyId, string supplierCurrencyCode, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport)
        {
            string result = string.Empty;
            string sqlText = "INSERT INTO Supplier  (supplier_Code, supplier_Name, supplier_Address1, supplier_Address2, supplier_Zip, supplier_City, supplier_Url, supplier_Memo, supplier_CountryId, supplier_CountryCode, supplier_CountryName, supplier_CurrencyId, supplier_CurrencyCode, supplier_CurrencySymbol, supplier_PhoneGeneral, supplier_PhoneSales, supplier_PhoneSupport, supplier_MailGeneral, supplier_MailSales, supplier_MailSupport) VALUES (@supplierCode, @supplierName, @supplierAddress1, @supplierAddress2, @supplierZip, @supplierCity, @supplierUrl, @supplierMemo, @supplierCountryId, @supplierCountryCode, @supplierCountryName, @supplierCurrencyId, @supplierCurrencyCode, @supplierCurrencySymbol, @supplierPhoneGeneral, @supplierPhoneSales, @supplierPhoneSupport, @supplierMailGeneral, @supplierMailSales, @supplierMailSupport);";

            try
            {
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCountryCode, supplierCountryName, supplierCurrencyId, supplierCurrencyCode, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport);

                if (rowsAffected > 0)
                {

                    result = String.Format("Row added.");
                }
                else
                {
                    result = "Row not added.";
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

        #region Update Table: Supplier
        public string UpdateTblSupplier( int supplierId, string supplierCode, string supplierName, string supplierAddress1, string supplierAddress2, string supplierZip, string supplierCity, string supplierUrl, int supplierCountryId, string supplierCountryCode, string supplierCountryName, int supplierCurrencyId, string supplierCurrencyCode, string supplierCurrencySymbol, string supplierPhoneGeneral, string supplierPhoneSales, string supplierPhoneSupport, string supplierMailGeneral, string supplierMailSales, string supplierMailSupport, string supplierMemo)
        {
            string result = string.Empty;
            string sqlText = "UPDATE Supplier SET supplier_Code = @supplierCode, supplier_name = @supplierName, supplier_Address1 = @supplierAddress1, supplier_Address2 = @supplierAddress2, supplier_Zip = @supplierZip, supplier_City = @supplierCity, supplier_Url = @supplierUrl, supplier_CountryId = @supplierCountryId, supplier_CountryCode = @supplierCountryCode, supplier_CountryName = @supplierCountryName, supplier_CurrencyId = @supplierCurrencyId, supplier_CurrencyCode = @supplierCurrencyCode, supplier_CurrencyCode = @supplierCurrencySymbol, supplier_PhoneGeneral = @supplierPhoneGeneral, supplier_PhoneSales = @supplierPhoneSales, supplier_PhoneSupport = @supplierPhoneSupport, supplier_MailGeneral = @supplierMailGeneral, supplier_MailSales = @supplierMailSales, supplier_MailSupport = @supplierMailSupport,supplier_Memo = @supplierMemo WHERE supplier_Id = @supplierId;";

            try
            {
                // Do we need to add supplierId here?
                int rowsAffected = ExecuteNonQueryTblSupplier(sqlText, supplierCode, supplierName, supplierAddress1, supplierAddress2, supplierZip, supplierCity, supplierUrl, supplierMemo, supplierCountryId, supplierCountryCode, supplierCountryName, supplierCurrencyId, supplierCurrencyCode, supplierCurrencySymbol, supplierPhoneGeneral, supplierPhoneSales, supplierPhoneSupport, supplierMailGeneral, supplierMailSales, supplierMailSupport, supplierId);

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
    }
}
