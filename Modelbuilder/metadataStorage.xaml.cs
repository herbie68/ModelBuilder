using Modelbuilder.ViewModels;
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
        public List<storageLocation> StorageList = new();
        private readonly string DatabaseTable = "storage";
        public metadataStorage()
        {
            InitializeComponent();
            DataContext = new StorageViewModel();
            //BuildTree();
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

            /// Available Column names
            /// storage_Id          -- int          -- AUTO_INCREMENT
            /// storage_ParentId    -- int
            /// storage_Parent      -- varchar(50)                              [storage_Code0]/[storage_Code1]/[storage_Code2] etc.
            /// storage_Code        -- varchar(20)  -- NOT NULL -- unique
            /// storage_Name        -- varchar(150)
            /// storage_Level       -- int          -- NOT NULL -- Default 0

            dbConnection.SqlSelectionString = "*";
            dbConnection.SqlOrderByString = "Storage_Fullpath";
            dbConnection.TableName = DatabaseTable;

            DataTable dtStorageCodes = dbConnection.LoadSpecificMySqlData();
            //Use a DataSet to manage the data
            DataSet dsStorageCodes = new();
            dsStorageCodes.Tables.Add(dtStorageCodes);

            foreach (DataRow row in dsStorageCodes.Tables[DatabaseTable].Rows)
            {
                /// storage_Id = int.Parse(row["storage_Id"].ToString()),
                /// storage_ParentId = int.Parse(row["storage_ParentId"].ToString()),
                /// storage_FullPath = row["storage_FullPath"].ToString(),
                /// storage_Code = row["storage_Code"].ToString(),
                /// storage_Name = row["storage_Name"].ToString(),
                /// storage_Level = int.Parse(row["storage_Level"].ToString()),

                if (row["Storage_ParentId"] == DBNull.Value)
                {
                    StorageList.Add(
                    new storageLocation()
                    {
                        storage_Id = (int)row["storage_Id"],
                        storage_FullPath = row["storage_FullPath"].ToString(),
                        storage_Code = row["storage_Code"].ToString(),
                        storage_Name = row["storage_Name"].ToString(),
                        storage_Level = (int)row["storage_Level"],
                    }
                    );
                }
                else
                {
                    StorageList.Add(
                        new storageLocation()
                        {
                            storage_Id = (int)row["storage_Id"],
                            storage_ParentId = (int)row["storage_ParentId"],
                            storage_FullPath = row["storage_FullPath"].ToString(),
                            storage_Code = row["storage_Code"].ToString(),
                            storage_Name = row["storage_Name"].ToString(),
                            storage_Level = (int)row["storage_Level"],
                        }
                        );
                }
            }
            Console.WriteLine(StorageList);

            // add a relationship
            //dsStorageCodes.Relations.Add("rsParentChild", dsStorageCodes.Tables[DatabaseTable].Columns["Storage_Id"], dsStorageCodes.Tables[DatabaseTable].Columns["Storage_ParentId"]);

            /*
            foreach (DataRow row in dsStorageCodes.Tables[DatabaseTable].Rows)
            {
                if (row["Storage_ParentId"] == DBNull.Value)
                {
                    TreeViewItem root = new TreeViewItem();
                    root.Header = row["Storage_Name"].ToString();
                    root.Name = "P" + row["Storage_Id"].ToString();
                    root.Tag = row["Storage_Fullpath"].ToString();
                    treeViewStorage.Items.Add(root);
                    PopulateTree(row, root);
                }
            }
            */
        }
        #endregion

        #region Populate the tree
        public void PopulateTree(DataRow dr, TreeViewItem pNode)
        {
            foreach (DataRow row in dr.GetChildRows("rsParentChild"))
            {
                TreeViewItem cChild = new TreeViewItem();
                cChild.Header = row["Storage_Name"].ToString();
                cChild.Name = "P" + row["Storage_ParentId"].ToString() + "C" + row["Storage_Id"].ToString(); // Store ID and Parent_Id in the tag
                cChild.Tag = row["Storage_Fullpath"].ToString();
                pNode.Items.Add(cChild);
                //Recursively build the tree
                PopulateTree(row, cChild);
            }
        }

        #endregion
    }
}
