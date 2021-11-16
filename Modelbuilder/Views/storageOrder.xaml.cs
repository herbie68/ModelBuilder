using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Modelbuilder.Views
{
    /// <summary>
    /// Interaction logic for storageOrder.xaml
    /// </summary>
    public partial class storageOrder : Page
    {
        private HelperOrder _helper;
        private DataTable _dt, _dtPS;
        private int _dbRowCount;
        private int _currentDataGridIndex, _currentDataGridPSIndex;
        static string DatabaseSupplierTable = "supplier", DatabaseProductTable = "product", DatabaseProjectTable = "project";

        public storageOrder()
        {
            var SupplierList = new List<HelperOrder.Supplier>();
            var ProjectList = new List<HelperOrder.Project>();
            var ProductList = new List<HelperOrder.Product>();

            InitializeComponent();
            InitializeHelper();

            cboxSupplier.ItemsSource = _helper.GetSupplierList(SupplierList);
            cboxProject.ItemsSource = _helper.GetProjectList(ProjectList);
            cboxProduct.ItemsSource = _helper.GetProductList(ProductList);

        }

        #region InitializeHelper (connect to database)
        private void InitializeHelper()
        {
            if (_helper == null)
            {
                _helper = new HelperOrder("localhost", 3306, "modelbuilder", "root", "admin");
            }
        }
        #endregion InitializeHelper (connect to database)

        #region Datagridselectionchanged
        private void OrderDetail_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Toolbar for Orders
        #region Click New button (on Order toolbar)
        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Click Save Data button (on Order toolbar)
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Click Delete button (on Order toolbar)
        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #endregion

        private void OrderCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
