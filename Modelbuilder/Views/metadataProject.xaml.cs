using Microsoft.Win32;

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

using static Modelbuilder.HelperMySQL;

namespace Modelbuilder.Views
{
    /// <summary>
    /// Interaction logic for metadataProject.xaml
    /// </summary>
    public partial class metadataProject : Page
    {
        private HelperMySQL _helper;
        private DataTable _dt, _dtPS;
        private int _dbRowCount;
        private int _currentDataGridIndex, _currentDataGridPSIndex;
        static string DatabaseProjectTable = "project";

        public metadataProject()
        {
            InitializeComponent();
        }

        #region CommonCommandBinding_CanExecute
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region Get the data
        private void GetData()
        {
            InitializeHelper();

            // Get data from database
            _dt = _helper.GetDataTblProduct();

            // Populate data in datagrid from datatable
            ProjectCode_DataGrid.DataContext = _dt;

            // Set value
            _dbRowCount = _dt.Rows.Count;
            RecordsCount.Text = _dbRowCount.ToString();

            // Clear existing memo data
            inpProjectMemo.Document.Blocks.Clear();

            string tmpStr = "";
            //update status
            if (_dt.Rows.Count != 1) { tmpStr = "s"; };
            string msg = "Status: " + _dt.Rows.Count + " projecten" + tmpStr + " ingelezen.";
            UpdateStatus(msg);
        }
        #endregion

        #region Get content of Memofield
        private void GetMemo(int index)
        {
            string ContentProjectMemo = string.Empty;

            if (_dt != null && index >= 0 && index < _dt.Rows.Count)
            {
                //set value
                DataRow row = _dt.Rows[index];


                if (row["project_Memo"] != null && row["project_Memo"] != DBNull.Value)
                {
                    //get value from DataTable
                    ContentProjectMemo = row["project_Memo"].ToString();
                }

                if (!String.IsNullOrEmpty(ContentProjectMemo))
                {
                    //clear existing data
                    inpProjectMemo.Document.Blocks.Clear();

                    //convert to byte[]
                    byte[] dataArr = Encoding.UTF8.GetBytes(ContentProjectMemo);

                    using (MemoryStream ms = new(dataArr))
                    {
                        //load data
                        TextRange flowDocRange = new TextRange(inpProjectMemo.Document.ContentStart, inpProjectMemo.Document.ContentEnd);
                        flowDocRange.Load(ms, DataFormats.Rtf);
                    }
                }
            }
        }
        #endregion

        #region InitializeHelper (connect to database)
        private void InitializeHelper()
        {
            if (_helper == null)
            {
                _helper = new HelperMySQL("localhost", 3306, "modelbuilder", "root", "admin");
            }
        }
        #endregion

        #region Selection changed ProjectCode
        private void ProjectCode_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;

            if (dg.SelectedItem is not DataRowView Row_Selected) { return; }

            //set value
            _currentDataGridIndex = dg.SelectedIndex;

            GetMemo(dg.SelectedIndex);

            valueProjectId.Text = Row_Selected["project_Id"].ToString();
            valueImageRotationAngle.Text = Row_Selected["project_ImageRotationAngle"].ToString();
            inpProjectCode.Text = Row_Selected["project_Code"].ToString();
            inpProjectName.Text = Row_Selected["project_Name"].ToString();
            inpProjectStartdate.Text = Row_Selected["project_Startdate"].ToString();
            inpProjectExpTime.Text = Row_Selected["project_ExpectedTime"].ToString();
            
            if (Row_Selected["project_Closed"].ToString() == "1")
            { 
                inpProjectClosed.IsChecked = true;
                dispProjectExpEnddate.Visibility = Visibility.Hidden;
                lblExpEndDate.Visibility = Visibility.Hidden;
                inpProjectEnddate.Visibility = Visibility.Visible;
                lblProjectEndDate.Visibility = Visibility.Visible;
            }
            else 
            {
                inpProjectClosed.IsChecked = false;
                inpProjectEnddate.Visibility = Visibility.Hidden;
                lblProjectEndDate.Visibility = Visibility.Hidden;
                dispProjectExpEnddate.Visibility = Visibility.Visible;
                lblExpEndDate.Visibility = Visibility.Visible;
            }

            // Retrieve Product Image            
            if (Row_Selected["project_Image"].ToString() != "")
            {
                byte[] projectImageByte = (byte[])Row_Selected["project_Image"];
                var stream = new MemoryStream(projectImageByte);
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                BitmapSource source = decoder.Frames[0];

                imgProjectImage.Source = source;
                imgProjectImage.LayoutTransform = new RotateTransform(int.Parse(valueImageRotationAngle.Text));
            }
            else
            {
                imgProjectImage.Source = new BitmapImage(new Uri("..\\Resources\\noImage.png", UriKind.Relative));
            }

        }
        #endregion


        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {

        }

        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {

        }

        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {

        }

        #region Add a product Image
        private void ImageAdd(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ImageDialog = new OpenFileDialog();
            ImageDialog.Title = "Selecteer een afbeelding voor dit project";
            ImageDialog.Filter = "Afbeeldingen (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (ImageDialog.ShowDialog() == true)
            {
                imgProjectImage.Source = new BitmapImage(new Uri(ImageDialog.FileName));
            }
        }
        #endregion

        #region Delete a product Image
        private void ImageDelete(object sender, RoutedEventArgs e)
        {
            imgProjectImage.Source = null;
            valueImageRotationAngle.Text = "0";
        }
        #endregion

        #region Rotate a product Image
        private void ImageRotate(object sender, RoutedEventArgs e)
        {
            var _tempValue = (int.Parse(valueImageRotationAngle.Text) + 90);
            if (_tempValue == 360)
            {
                _tempValue = 0;
            }
            valueImageRotationAngle.Text = _tempValue.ToString();
            imgProjectImage.LayoutTransform = new RotateTransform(int.Parse(valueImageRotationAngle.Text));

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
    }
}
