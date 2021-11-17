using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private DataTable _dt, _dtSC;
        private int _dbRowCount, _dbRowCountSC, _currentDataGridIndex, _currentDataGridSCIndex;
        // static string DatabaseSupplierTable = "supplier", DatabaseProductTable = "product", DatabaseProjectTable = "project";

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

            //GetData();
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

        #region Get the Order data
        private void GetData()
        {
            InitializeHelper();

            // Get data from database
            _dt = _helper.GetDataTblOrder();
            _dtSC = _helper.GetDataTblOrderline();

            // Populate data in datagrid from datatable
            OrderCode_DataGrid.DataContext = _dt;
            OrderDetail_DataGrid.DataContext = _dtSC;

            // Set value
            _dbRowCount = _dt.Rows.Count;
            _dbRowCountSC = _dtSC.Rows.Count;
            RecordsCount.Text = _dbRowCount.ToString();
            RecordsCountSC.Text = _dbRowCountSC.ToString();

            // Clear existing memo data
            inpOrderMemo.Document.Blocks.Clear();

            string tmpStr = "";
            //update status
            if (_dt.Rows.Count != 1) { tmpStr = "s"; };
            string msg = "Status: " + _dt.Rows.Count + " Bestelling" + tmpStr + " ingelezen.";
            if (_dtSC.Rows.Count != 1) { tmpStr = "s"; };
            string msgSC = "Status: " + _dtSC.Rows.Count + " Bestelregel" + tmpStr + " ingelezen.";

            UpdateStatus(msg);
            UpdateStatus(msgSC);
        }
        #endregion

        #region Datagridselectionchanged
        private void OrderDetail_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Selection changed: Combobox Supplier
        private void cboxSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (HelperOrder.Supplier item in e.AddedItems)
            {
                valueSupplierId.Text = item.SupplierId.ToString();
                valueCurrencyId.Text = item.SupplierCurrencyId.ToString();
                inpCurrencySymbol.Text = item.SupplierCurrencySymbol.ToString();
            }
            inpCurrencyRate.Text = _helper.GetConversionRate(int.Parse(valueCurrencyId.Text));
        }
        #endregion


        #region Update status
        private void UpdateStatus(string msg)
        {
            if (!String.IsNullOrEmpty(msg))
            {
                if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
                {
                    textBoxStatus.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                    Debug.WriteLine(String.Format("{0} - Status: {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
                else
                {
                    textBoxStatus.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                    Debug.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), msg));
                }
            }
        }
        #endregion

        #region rtfToolbar actions
        // private bool dataChanged = false; // Unsaved textchanges

        private string privateText = null; // Content of RTFBox in txt-Format
        public string text
        {
            get
            {
                TextRange range = new TextRange(inpOrderMemo.Document.ContentStart, inpOrderMemo.Document.ContentEnd);
                return range.Text;
            }
            set
            {
                privateText = value;
            }
        }

        private string ShowRow; // aktuelle Zeile der Cursorposition
        private int _CurrentRow = 1;
        public int CurrentRow
        {
            get { return _CurrentRow; }
            set
            {
                _CurrentRow = value;
                ShowRow = "Rij: " + value;
                //Uncomment when statusbar is in place
                //LabelRowNr.Content = ShowRow;
            }
        }

        private string ShowColumn; // aktuelle Spalte der Cursorposition
        private int _CurrentColumn = 1;

        public int CurrentColumn
        {
            get { return _CurrentColumn; }
            set
            {
                _CurrentColumn = value;
                ShowColumn = "Kol: " + value;
                // Uncomment when statusbar is in place
                //LabelColumnNr.Content = ShowColumn;
            }
        }
        #endregion rtfToolbar actions

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
