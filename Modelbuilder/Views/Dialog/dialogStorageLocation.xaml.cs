namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for dialogCategory.xaml
    /// </summary>
    public partial class dialogStorageLocation : Window
    {
        public static string diaLogStorageLocationValue = String.Empty;

        public dialogStorageLocation()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            diaLogStorageLocationValue = NewStorageLocationName.Text;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}