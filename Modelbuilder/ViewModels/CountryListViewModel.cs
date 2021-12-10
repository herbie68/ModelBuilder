namespace Modelbuilder
{
    internal class CountryListViewModel
    {
        public List<string> SupplierList { get; set; }
        private readonly string DatabaseTable = "country";
        public CountryListViewModel()
        {
            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            dbConnection.SqlSelectionString = "country_Name, country_Id";
            dbConnection.SqlOrderByString = "country_Id";
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

            public string countryName { get; set; }
            public int countryId { get; set; }
        }

    }
}
