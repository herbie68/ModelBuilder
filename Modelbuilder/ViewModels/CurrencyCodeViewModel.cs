namespace Modelbuilder
{
    internal class CurrencyCodeViewModel
    {
        /// <summary>
        /// Gets or Sets the currency collection.
        /// </summary>
        public List<string> CurrencyCollection { get; set; }
        private readonly string DatabaseCurrencyTable = "currency";
        public string TableId = "Id";

        public CurrencyCodeViewModel()
        {
            CurrencyCollection = new List<string> { };

            Database dbCurrencyConnection = new()
            {
                TableName = DatabaseCurrencyTable
            };

            //DataTable dataTable = new DataTable();
            dbCurrencyConnection.SqlSelectionString = "Symbol";
            dbCurrencyConnection.SqlOrderByString = "Id";
            dbCurrencyConnection.TableName = DatabaseCurrencyTable;

            DataTable dtCurrencySelection = dbCurrencyConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtCurrencySelection.Rows.Count; i++)
            {
                CurrencyCollection.Add(dtCurrencySelection.Rows[i][0].ToString());
            };
        }
    }
}