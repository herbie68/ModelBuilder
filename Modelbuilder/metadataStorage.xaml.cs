using Modelbuilder.ViewModels;
using MySqlX.XDevAPI.Relational;
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

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataStorage.xaml
    /// </summary>
    public partial class metadataStorage : Page
    {
        //public List<storageLocation> StorageList = new();
        private readonly string DatabaseTable = "storage";
        public metadataStorage()
        {
            InitializeComponent();
            DataContext = new StorageViewModel();
            BuildTree();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #region treeviewStorage_SelectedItemChanged
        private void treeViewStorage_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            // Changed RoutedPropertyChangedEventArgs<object> e into RoutedEventArgs e
            TreeViewItem SelectedItem = treeViewStorage.SelectedItem as TreeViewItem;

            string ParentValue = "<root>", ParentPathValue = "<root>", ChildValue = "";
            int seperatorValue = SelectedItem.Name.ToString().IndexOf("C", 0);
            if (seperatorValue <= 0)
            {
                ChildValue = SelectedItem.Name.ToString().Replace("P", "");
            }
            else
            {
                ParentValue = SelectedItem.Name.ToString().Substring(1, seperatorValue - 1);
                ChildValue = SelectedItem.Name.ToString().Substring(seperatorValue + 1);
                ParentPathValue = SelectedItem.Tag.ToString().Replace("\\" + SelectedItem.Header.ToString(), "");
            }
            valueFullpath.Text = SelectedItem.Tag.ToString();
            valueParentFullpath.Text = ParentPathValue;
            valueParentId.Text = ParentValue;
            valueId.Text = ChildValue;
            //inpStorageName.Text = SelectedItem.Header.ToString();


            // Perhaps the switch can be used for a different menu on root item and sub item, but not sure if necesarry
            switch (SelectedItem.Tag.ToString())
            {
                case "Solution":
                    treeViewStorage.ContextMenu = treeViewStorage.Resources["SolutionContext"] as System.Windows.Controls.ContextMenu;
                    break;
                case "Folder":
                    treeViewStorage.ContextMenu = treeViewStorage.Resources["FolderContext"] as System.Windows.Controls.ContextMenu;
                    break;
            }
            treeViewStorage.ContextMenu = treeViewStorage.Resources["FolderContext"] as System.Windows.Controls.ContextMenu;
        }
        #endregion

        #region Build Storage tree
        public void BuildTree()
        {
            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            _ = new DataTable();

            // https://stackoverflow.com/questions/24569156/display-treeviewitem-as-grid-rows-in-wpf

            dbConnection.SqlSelectionString = "*";
            dbConnection.SqlOrderByString = "Storage_Fullpath";
            dbConnection.TableName = DatabaseTable;

            DataTable dtStorageCodes = dbConnection.LoadSpecificMySqlData();
            DataSet dsStorageCodes = new();
            dsStorageCodes.Tables.Add(dtStorageCodes);

            List<MainStorageLocation> StorageLocations = new();

            // Sub storage locations are not in the tree as sublocation. Perhaps I should filter only categories that use the ParentId of the mainlocation
            
            List<StorageLocation> storageList = new List<StorageLocation>();
            foreach (DataRow datarow in dsStorageCodes.Tables[DatabaseTable].Rows)
            {
                //var result = storageList.OfType<StorageLocation>().Where(s => s.storage_ParentId == (int)datarow["storage_Id"]).ToList();

                if (datarow["Storage_ParentId"] == DBNull.Value)
                {
                    storageList.Add(new StorageLocation()
                    {
                        storage_Id = (int)datarow["storage_Id"],
                        storage_FullPath = datarow["storage_FullPath"].ToString(),
                        storage_Code = datarow["storage_Code"].ToString(),
                        storage_Name = datarow["storage_Name"].ToString(),
                        storage_Level = (int)datarow["storage_Level"],
                        SubStorageLocations = new List<SubStorageLocation>()
                        {new SubStorageLocation(){storage_Code = datarow["storage_Code"].ToString(), storage_Name =datarow["storage_Name"].ToString() }, }
                    }
                    );
                }
                else
                {
                    DataTable tblFiltered = dtStorageCodes.AsEnumerable().Where(r => r.Field<int>("storage_Id") == (int)datarow["storage_ParentId"]).CopyToDataTable();
                    List<SubStorageLocation> subStorageList = new List<SubStorageLocation>();
                    subStorageList = (from DataRow dr in tblFiltered.Rows select new SubStorageLocation()
                    {
                        storage_Id = (int)dr["storage_Id"],
                        storage_ParentId = (int)dr["storage_ParentId"],
                        storage_Code = dr["storage_Code"].ToString(),
                        storage_Name = dr["storage_Name"].ToString()
                    }).ToList();
                    storageList.Add(new StorageLocation()
                    {
                        storage_Id = (int)datarow["storage_Id"],
                        storage_ParentId = (int)datarow["storage_ParentId"],
                        storage_FullPath = datarow["storage_FullPath"].ToString(),
                        storage_Code = datarow["storage_Code"].ToString(),
                        storage_Name = datarow["storage_Name"].ToString(),
                        storage_Level = (int)datarow["storage_Level"],
                        SubStorageLocations = subStorageList
                        // SubStorageLocations = new List<SubStorageLocation>()
                        // {new SubStorageLocation(){storage_Code = datarow["storage_Code"].ToString(), storage_Name =datarow["storage_Name"].ToString() }, }
                    }
                    );
                }
            }
            treeViewStorage.ItemsSource = storageList;

            //var result = storageList.OfType<StorageLocation>().Where(s => s.storage_ParentId > 1);
            //Console.WriteLine(result);

            /*
            List<StorageLocation> storageList = new List<StorageLocation>();
            storageList = (from DataRow datarow in dtStorageCodes.Rows
                           select new StorageLocation()
                           {
                               storage_Id = (int)datarow["storage_Id"],
                               storage_ParentId = (int)datarow["storage_ParentId"],
                               storage_FullPath = datarow["storage_FullPath"].ToString(),
                               storage_Code = datarow["storage_Code"].ToString(),
                               storage_Name = datarow["storage_Name"].ToString(),
                               storage_Level = (int)datarow["storage_Level"],
                           }).ToList();

            foreach (DataRow row in dsStorageCodes.Tables[DatabaseTable].Rows)
{
                DataTable tblFiltered = dtStorageCodes.AsEnumerable().Where(r => r.Field<int>("storage_ParentId") == (int)row["storage_Id"])
                             .CopyToDataTable();
                if (row["Storage_ParentId"] == DBNull.Value)
                {
                    StorageLocations.Add(new MainStorageLocation()
                    {
                        storage_Id = (int)row["storage_Id"],
                        storage_FullPath = row["storage_FullPath"].ToString(),
                        storage_Code = row["storage_Code"].ToString(),
                        storage_Name = row["storage_Name"].ToString(),
                        storage_Level = (int)row["storage_Level"],
                        SubStorageLocations = new List<SubStorageLocation>()
                        {new SubStorageLocation(){storage_Code = row["storage_Code"].ToString(), storage_Name =row["storage_Name"].ToString() }, }
                    }
                    );
                }
                else
                {
                    StorageLocations.Add(new MainStorageLocation()
                        {
                            storage_Id = (int)row["storage_Id"],
                            storage_ParentId = (int)row["storage_ParentId"],
                            storage_FullPath = row["storage_FullPath"].ToString(),
                            storage_Code = row["storage_Code"].ToString(),
                            storage_Name = row["storage_Name"].ToString(),
                            storage_Level = (int)row["storage_Level"],
                        SubStorageLocations = new List<SubStorageLocation>()
                        { new SubStorageLocation() { storage_Code = row["storage_Code"].ToString(), storage_Name = row["storage_Name"].ToString() }, }
                    }
                        );
                }
            }
            */
            //treeViewStorage.ItemsSource = StorageLocations;
        }
        #endregion
    }
}
