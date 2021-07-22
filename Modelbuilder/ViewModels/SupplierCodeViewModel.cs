using System.Collections.Generic;
using System.Data;

namespace Modelbuilder
{
    internal class SupplierCodeViewModel
    {
        public List<string> CurrencyCollection { get; set; }
        public List<string> CountryCollection { get; set; }
        private readonly string DatabaseCurrencyTable = "currency";
        private readonly string DatabaseCountryTable = "country";
        public string TableId = "supplier_Id";

        public SupplierCodeViewModel()
        {
            CurrencyCollection = new List<string> { };

            Database dbCurrencyConnection = new()
            {
                TableName = DatabaseCurrencyTable
            };

            //DataTable dataTable = new DataTable();
            dbCurrencyConnection.SqlSelectionString = "currency_Symbol";
            dbCurrencyConnection.SqlOrderByString = "currency_Id";
            dbCurrencyConnection.TableName = DatabaseCurrencyTable;

            DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
            {
                CurrencyCollection.Add(dtCurrencySelection.Rows[i][0].ToString());
            };

            CountryCollection = new List<string> { };

            Database dbCountryConnection = new()
            {
                TableName = DatabaseCountryTable
            };

            //DataTable dataTable = new DataTable();
            dbCountryConnection.SqlSelectionString = "country_Name";
            dbCountryConnection.SqlOrderByString = "country_Id";
            dbCountryConnection.TableName = DatabaseCountryTable;

            DataTable dtCountrySelection = dbCountryConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtCountrySelection.Rows.Count; i++)
            {
                CountryCollection.Add(dtCountrySelection.Rows[i][0].ToString());
            };

        }
    }
}