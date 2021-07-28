using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Modelbuilder
{
    internal class SupplierCodeViewModel
    {
        public List<string> CurrencyCollection { get; set; }
        public List<string> CurrencyCollectionId { get; set; }
        public List<string> CurrencyCollectionCode { get; set; }
        public List<string> CountryCollection { get; set; }
        private readonly string DatabaseCurrencyTable = "currency";
        private readonly string DatabaseCountryTable = "country";
        public string TableId = "supplier_Id";

        public SupplierCodeViewModel()
        {
            CurrencyCollection = new List<string> { };
            CurrencyCollectionId = new List<string> { };
            CurrencyCollectionCode = new List<string> { };


            Database dbCurrencyConnection = new()
            {
                TableName = DatabaseCurrencyTable
            };

            //DataTable dataTable = new DataTable();
            dbCurrencyConnection.SqlSelectionString = "currency_Symbol, currency_Id, currency_Code";
            dbCurrencyConnection.SqlOrderByString = "currency_Id";
            dbCurrencyConnection.TableName = DatabaseCurrencyTable;

            DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
            {
                CurrencyCollection.Add(dtCurrencySelection.Rows[i][0].ToString());
                CurrencyCollectionId.Add(dtCurrencySelection.Rows[i][1].ToString());
                CurrencyCollectionCode.Add(dtCurrencySelection.Rows[i][2].ToString());
            };

            CountryCollection = new List<string> { };

            Database dbCountryConnection = new()
            {
                TableName = DatabaseCountryTable
            };

            //DataTable dataTable = new DataTable();
            dbCountryConnection.SqlSelectionString = "country_Name, country_Id, country_Code";
            dbCountryConnection.SqlOrderByString = "country_Id";
            dbCountryConnection.TableName = DatabaseCountryTable;

            DataTable dtCountrySelection = dbCountryConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtCountrySelection.Rows.Count; i++)
            {
                CountryCollection.Add(dtCountrySelection.Rows[i][0].ToString());
            };
        }

        private class Currency
        {
            public Currency(string Symbol, int Id, string Code)
            {
                currencySymbol = Symbol;
                currencyId = Id;
                currencyCode = Code;
            }

            public string currencySymbol { get; set; }

            public int currencyId { get; set; }

            public string currencyCode { get; set; }

        }
    }
}