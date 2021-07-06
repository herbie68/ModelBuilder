using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataCountry.xaml
    /// </summary>
    public partial class metadataCategory : Page
    {
        private readonly string DatabaseTable = "category";
        public metadataCategory()
        {
            InitializeComponent();
            Database dbConnection = new Database();
            dbConnection.Connect();

            DataTable dtCategoryCodes = dbConnection.LoadMySqlData();

            // Load the data from the database into the datagrid
            //CountryCode_DataGrid.DataContext = dtCountryCodes;

            // Make sure the first row in the datagrid is selected
            //CountryCode_DataGrid.SelectedIndex = 0;
            //CountryCode_DataGrid.Focus();
        }
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
