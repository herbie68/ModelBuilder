namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for dialogCategory.xaml
    /// </summary>
    public partial class dialogCategory : Window
    {
        public static string diaLogCategoryValue = String.Empty;

        public dialogCategory()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            diaLogCategoryValue = NewCategoryName.Text;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}