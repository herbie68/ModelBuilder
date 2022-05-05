namespace Modelbuilder;
public partial class ExportProductsSupplier : Page
{
	private HelperGeneral _helperGeneral;
	private DataTable _dt;
	private HelperClass _helper;

	public ExportProductsSupplier()
	{
		InitializeComponent();
		InitializeHelper();
		GetData();
	}

	#region Get the Data
	private void GetData()
	{
		InitializeHelper();

		// Get data from database
		_dt = _helperGeneral.GetData(HelperGeneral.DbProductSupplierTable);
		_dt.Columns.Remove("Id");
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

	#region CommonCommandBinding_CanExecute
	private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = true;
	}
	#endregion CommonCommandBinding_CanExecute

	#region Perform Export after selecting folder
	private void ClickedSelectFolderButton(object sender, RoutedEventArgs e)
	{
		var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
		System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();

		_helper = new HelperClass();

		var FileName = _helper.GetFilePrefix() + Languages.Cultures.ExportProductsSupplier_FileName + ".csv";
		string[] Columns = _helper.GetProductSupplierHeaders();
	 
		dispFolderName.Text = folderDialog.SelectedPath + @"\" + FileName;
		_helper.ExportToCsv(_dt, folderDialog.SelectedPath + @"\" + FileName, Columns, "Header");
		btnBrowseFolder.IsEnabled = false;

		dispStatusLine.Text = _dt.Rows.Count + " " + Languages.Cultures.Export_Statusline_Status_Completed;
	}
	#endregion
}
