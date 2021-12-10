namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for dialogWorktype.xaml
    /// </summary>
    public partial class dialogWorktype : Window
    {
        public static string diaLogWorktypeValue = String.Empty;
        public dialogWorktype()
        {
            InitializeComponent();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            diaLogWorktypeValue = NewWorktypeName.Text;
            this.Close();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
