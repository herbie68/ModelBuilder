using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for storageStockChanges.xaml
    /// </summary>
    public partial class storageStockChanges : Page
    {
        private HelperGeneral _helperGeneral;
        private DataTable _dt;
        private int _dbRowCount;
        private int _currentDataGridIndex;

        public storageStockChanges()
        {
            InitializeComponent();
            InitializeHelper();
            GetData();

        }

        #region InitializeHelper (connect to database)
        private void InitializeHelper()
        {
            if (_helperGeneral == null)
            {
                _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
            }
        }
        #endregion InitializeHelper (connect to database)

        #region Get the Stock data
        private void GetData()
        {
            InitializeHelper();

            // Get data from database
            _dt = _helperGeneral.GetData(HelperGeneral.DbStockView);

            // Populate data in datagrid from datatable
            StockStorage_DataGrid.DataContext = _dt;

            // Set value
            _dbRowCount = _dt.Rows.Count;
            RecordCountStockRows.Text = _dbRowCount.ToString();

            string tmpStr = "";
            //update status
            if (_dt.Rows.Count != 1) { tmpStr = "en"; };
            string msg = "Status: " + _dt.Rows.Count + " product" + tmpStr + " ingelezen.";

            UpdateStatus(msg);
        }
        #endregion Get the Stock data

        private void StockStorage_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridIndex = dg.SelectedIndex;
            valueId.Text = Row_Selected["Id"].ToString();
            valueAmount.Text = Row_Selected["Amount"].ToString();
            valueRow.Text = dg.SelectedIndex.ToString();
        }

        #region Update status
        private void UpdateStatus(string msg)
        {
            if (!String.IsNullOrEmpty(msg))
            {
                if (!msg.StartsWith("Error") && !msg.StartsWith("Status"))
                {
                    textBoxStatusStockRows.Text = String.Format("Status: {0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                }
                else
                {
                    textBoxStatusStockRows.Text = String.Format("{0} ({1})", msg, DateTime.Now.ToString("HH:mm:ss"));
                }
            }
        }
        #endregion Update status

    }
}
