namespace Modelbuilder
{
    internal class CountryListViewModel
    {
        /// <summary>
        /// Gets or Sets the supplier list.
        /// </summary>
        public List<string> SupplierList { get; set; }
        private readonly string DatabaseTable = "country";
        public CountryListViewModel()
        {
            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            dbConnection.SqlSelectionString = "Name, Id";
            dbConnection.SqlOrderByString = "Id";
            dbConnection.TableName = DatabaseTable;

            DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

            List<Country> CountryList = new();

            for (int i = 0; i < dtSelection.Rows.Count; i++)
            {
                CountryList.Add(new Country(dtSelection.Rows[i][0].ToString(),
                    int.Parse(dtSelection.Rows[i][1].ToString())));
            };
        }

        private class Country
        {
            public Country(string Name, int Id)
            {
                countryName = Name;
                countryId = Id;
            }

            /// <summary>
            /// Gets or Sets the country name.
            /// </summary>
            public string countryName { get; set; }
            /// <summary>
            /// Gets or Sets the country id.
            /// </summary>
            public int countryId { get; set; }
        }

    }
}
