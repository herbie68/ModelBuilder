using K4os.Compression.LZ4.Internal;

namespace Modelbuilder.Views
{
    /// <summary>
    /// Interaction logic for metadataProject.xaml
    /// </summary>
    public partial class metadataProject : Page
    {
        private HelperProject _helper;
        private HelperGeneral _helperGeneral;
        private DataTable _dt;
        private int _dbRowCount;
        private int _currentDataGridIndex;

        public metadataProject()
        {
            InitializeComponent();
            GetData();
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
            _dt = _helperGeneral.GetData(HelperGeneral.DbProjectTable);

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


                if (row["Memo"] != null && row["Memo"] != DBNull.Value)
                {
                    //get value from DataTable
                    ContentProjectMemo = row["Memo"].ToString();
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
            if (_helperGeneral == null)
            {
                _helperGeneral = new HelperGeneral(Connection_Query.server, int.Parse(Connection_Query.port), Connection_Query.database, Connection_Query.uid, Connection_Query.password);
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

            valueProjectId.Text = Row_Selected["Id"].ToString();
            valueImageRotationAngle.Text = Row_Selected["ImageRotationAngle"].ToString();
            inpProjectCode.Text = Row_Selected["Code"].ToString();
            inpProjectName.Text = Row_Selected["Name"].ToString();
            inpProjectStartdate.Text = Row_Selected["Startdate"].ToString();
            inpProjectEnddate.Text = Row_Selected["Enddate"].ToString();

            if (Row_Selected["Closed"].ToString() == "1")
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
            if (Row_Selected["Image"].ToString() != "")
            {
                byte[] projectImageByte = (byte[])Row_Selected["Image"];
                var stream = new MemoryStream(projectImageByte);
                PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                BitmapSource source = decoder.Frames[0];
                if (valueImageRotationAngle.Text == "") { valueImageRotationAngle.Text = "0"; }
                imgProjectImage.Source = source;
                imgProjectImage.LayoutTransform = new RotateTransform(int.Parse(valueImageRotationAngle.Text));
            }
            else
            {
                imgProjectImage.Source = new BitmapImage(new Uri("..\\Resources\\noImage.png", UriKind.Relative));
            }

            MemoTab.IsEnabled = true;
            TimeTab.IsEnabled = true;
            CostsTab.IsEnabled = true;

        }
        #endregion

        #region Get rich text from flow document
        private string GetRichTextFromFlowDocument(FlowDocument fDoc)
        {
            string result = string.Empty;

            //convert to string
            if (fDoc != null)
            {
                TextRange tr = new TextRange(fDoc.ContentStart, fDoc.ContentEnd);

                using (MemoryStream ms = new MemoryStream())
                {
                    tr.Save(ms, DataFormats.Rtf);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return result;
        }
        #endregion

        #region Click New button (on toolbar)
        private void ToolbarButtonNew(object sender, RoutedEventArgs e)
        {
            // When the ProductId is not empty, a excisting row is selected when the add new row button is hit. 
            // In this case a new row with blank values should be added to the dtable and selected.
            // Otherwise it can be that fields are already filled with data befor the add new row button was hit.
            // In this case the existing value should be used instead of emptying all the data from the form.

            var projectCode = "";
            var projectName = "";
            var projectStartDate = "";
            var projectEndDate = "";
            var projectClosed = 0;
            var projectImageRotationAngle = "0";
            byte[] projectImage = null;

            if (valueProjectId.Text == "")
            {
                // No existing product selected, use formdata if entered
                // check on entered data on formated field because they throw an error on adding a new row
                projectCode = inpProjectCode.Text;
                projectName = inpProjectName.Text;

                projectImageRotationAngle = valueImageRotationAngle.Text;

                var bitmap = imgProjectImage.Source as BitmapSource;
                var encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    projectImage = stream.ToArray();
                }
            }

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpProjectMemo.Document);

            if (_dt.Rows.Count != 0)
            { DataRow row = _dt.Rows[_dt.Rows.Count - 1]; }

            InitializeHelper();

            var result = _helperGeneral.InsertInTable(HelperGeneral.DbProjectTable, new string[7, 3]
            {   {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeName, projectName},
                {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeCode, projectCode},
                {HelperGeneral.DbProjectTableFieldNameStartDate, HelperGeneral.DbProjectTableFieldTypeStartDate, projectStartDate},
                {HelperGeneral.DbProjectTableFieldNameEndDate, HelperGeneral.DbProjectTableFieldTypeEndDate, projectEndDate},
                {HelperGeneral.DbProjectTableFieldNameClosed, HelperGeneral.DbProjectTableFieldTypeClosed, projectClosed.ToString()},
                {HelperGeneral.DbProjectTableFieldNameMemo, HelperGeneral.DbProjectTableFieldTypeMemo, memo},
                {HelperGeneral.DbProjectTableFieldNameImageRotationAngle, HelperGeneral.DbProjectTableFieldTypeImageRotationAngle, projectImageRotationAngle} }, projectImage, HelperGeneral.DbProjectTableFieldNameImage);            
            
            UpdateStatus(result);

            // Get data from database
            _dt = _helperGeneral.GetData(HelperGeneral.DbProjectTable);

            // Populate data in datagrid from datatable
            ProjectCode_DataGrid.DataContext = _dt;
            //dataGridView1.Rows[e.RowIndex].Selected = true;
            if (ProjectCode_DataGrid.SelectedItem is not DataRowView Row_Selected)
            {
                return;
            }
        }
        #endregion

        #region Click Save Data button (on toolbar)
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            // When there is an existing Project selected the other tabpages can be activated
            MemoTab.IsEnabled = inpProjectCode.Text != "";
            TimeTab.IsEnabled = inpProjectCode.Text != "";
            CostsTab.IsEnabled = inpProjectCode.Text != "";

            if (valueProjectId.Text != "")
            {
                UpdateRowProject(ProjectCode_DataGrid.SelectedIndex);
            }

            GetData();

            // Make sure the eddited row in the datagrid is selected
            ProjectCode_DataGrid.SelectedIndex = rowIndex;
            ProjectCode_DataGrid.Focus();
        }
        #endregion

        #region Click Delete button (on toolbar)
        private void ToolbarButtonDelete(object sender, RoutedEventArgs e)
        {
            int rowIndex = _currentDataGridIndex;

            DeleteRowProject(ProjectCode_DataGrid.SelectedIndex);

            GetData();

            if (rowIndex == 0)
            {
                ProjectCode_DataGrid.SelectedIndex = 0;
            }
            else
            {
                ProjectCode_DataGrid.SelectedIndex = rowIndex - 1;
            }

            ProjectCode_DataGrid.Focus();

        }
        #endregion

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

        #region Update row Project Table
        private void UpdateRowProject(int dgIndex)
        {
            //when DataGrid SelectionChanged occurs, the value of '_currentDataGridIndex' is set
            //to DataGrid SelectedIndex
            //get data from DataTable
            DataRow row = _dt.Rows[_currentDataGridIndex];

            var projectId = int.Parse(valueProjectId.Text);
            var projectClosed = 0;
            string projectCode = inpProjectCode.Text;
            string projectName = inpProjectName.Text;
            var projectStartDate = inpProjectStartdate.Text;
            string projectEndDate = inpProjectEnddate.Text;
            var projectImageRotationAngle = valueImageRotationAngle.Text;
            byte[] projectImage;
            var bitmap = imgProjectImage.Source as BitmapSource;

            if ((bool)inpProjectClosed.IsChecked)
            {
                projectClosed = 1;
            }
            else
            {
                projectClosed = 0;
            }

            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                projectImage = stream.ToArray();
            }

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpProjectMemo.Document);

            InitializeHelper();

            var result = _helperGeneral.UpdateFieldInTable(HelperGeneral.DbProjectTable, new string[1, 3] 
            {   {HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId, projectId.ToString()} },new string[7, 3]
            {   {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeName, projectName},
                {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeCode, projectCode},
                {HelperGeneral.DbProjectTableFieldNameStartDate, HelperGeneral.DbProjectTableFieldTypeStartDate, projectStartDate},
                {HelperGeneral.DbProjectTableFieldNameEndDate, HelperGeneral.DbProjectTableFieldTypeEndDate, projectEndDate},
                {HelperGeneral.DbProjectTableFieldNameClosed, HelperGeneral.DbProjectTableFieldTypeClosed, projectClosed.ToString()},
                {HelperGeneral.DbProjectTableFieldNameMemo, HelperGeneral.DbProjectTableFieldTypeMemo, memo},
                {HelperGeneral.DbProjectTableFieldNameImageRotationAngle, HelperGeneral.DbProjectTableFieldTypeImageRotationAngle, projectImageRotationAngle} });
            
            _ = _helperGeneral.UpdateImageFieldInTable(HelperGeneral.DbProjectTable, new string[1, 3]
            {   {HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId, projectId.ToString()} }, projectImage, HelperGeneral.DbProjectTableFieldNameImage);

            UpdateStatus(result);
        }
        #endregion

        #region Insert new row in Product Table
        private void InsertRowProduct(int dgIndex)
        {
            //since the DataGrid DataContext is set to the DataTable, 
            //the DataTable is updated when data is modified in the DataGrid
            //get last row
            DataRow row = _dt.Rows[_dt.Rows.Count - 1];
            var projectId = int.Parse(valueProjectId.Text);
            var projectClosed = 0;
            string projectCode = inpProjectCode.Text;
            string projectName = inpProjectName.Text;
            string projectStartDate = inpProjectStartdate.Text;
            string projectEndDate = inpProjectEnddate.Text;
            var projectImageRotationAngle = valueImageRotationAngle.Text;
            byte[] projectImage;
            var bitmap = imgProjectImage.Source as BitmapSource;

            if ((bool)inpProjectClosed.IsChecked) { projectClosed = 1; }
            { projectClosed = 0; }

            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                projectImage = stream.ToArray();
            }

            //convert RTF to string
            string memo = GetRichTextFromFlowDocument(inpProjectMemo.Document);

            InitializeHelper();

            var result = _helperGeneral.InsertInTable(HelperGeneral.DbProjectTable, new string[7, 3]
            {   {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeName, projectName},
                {HelperGeneral.DbProjectTableFieldNameName, HelperGeneral.DbProjectTableFieldTypeCode, projectCode},
                {HelperGeneral.DbProjectTableFieldNameStartDate, HelperGeneral.DbProjectTableFieldTypeStartDate, projectStartDate},
                {HelperGeneral.DbProjectTableFieldNameEndDate, HelperGeneral.DbProjectTableFieldTypeEndDate, projectEndDate},
                {HelperGeneral.DbProjectTableFieldNameClosed, HelperGeneral.DbProjectTableFieldTypeClosed, projectClosed.ToString()},
                {HelperGeneral.DbProjectTableFieldNameMemo, HelperGeneral.DbProjectTableFieldTypeMemo, memo},
                {HelperGeneral.DbProjectTableFieldNameImageRotationAngle, HelperGeneral.DbProjectTableFieldTypeImageRotationAngle, projectImageRotationAngle} }, projectImage, HelperGeneral.DbProjectTableFieldNameImage);

            UpdateStatus(result);
        }
        #endregion

        #region Delete row from Project table
        private void DeleteRowProject(int dgIndex)
        {
            if (valueProjectId.Text != "")
            {
                var projectId = int.Parse(valueProjectId.Text);

                InitializeHelper();

                var result = _helperGeneral.DeleteRecordFromTable(HelperGeneral.DbProjectTable, new string[1, 3]
                {   {HelperGeneral.DbProjectTableFieldNameId, HelperGeneral.DbProjectTableFieldTypeId, projectId.ToString() } });
                UpdateStatus(result);
            }
        }
        #endregion

        #region Checkbox for project completion is checked/unchecked
        private void ProjectIsCompleted(object sender, RoutedEventArgs e)
        {
            if (inpProjectClosed.IsChecked == true)
            {
                dispProjectExpEnddate.Visibility = Visibility.Hidden;
                lblExpEndDate.Visibility = Visibility.Hidden;
                inpProjectEnddate.Visibility = Visibility.Visible;
                lblProjectEndDate.Visibility = Visibility.Visible;
            }
            else
            {
                inpProjectEnddate.Visibility = Visibility.Hidden;
                lblProjectEndDate.Visibility = Visibility.Hidden;
                dispProjectExpEnddate.Visibility = Visibility.Visible;
                lblExpEndDate.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
