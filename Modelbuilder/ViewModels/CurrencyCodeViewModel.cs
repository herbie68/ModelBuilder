using System.Collections.Generic;
using System.Data;

namespace Modelbuilder
{
    internal class CurrencyCodeViewModel
    {
        public List<string> CurrencyCollection { get; set; }
        private readonly string DatabaseCurrencyTable = "currency";
        public string TableId = "currency_Id";

        public CurrencyCodeViewModel()
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
        }
    }
}