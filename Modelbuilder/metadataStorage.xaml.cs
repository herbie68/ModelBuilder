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
        private readonly string DatabaseTable = "storage";
        public metadataStorage()
        {
            InitializeComponent();
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
            inpStorageLocationName.Text = SelectedItem.Header.ToString();


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
            dbConnection.SqlOrderByString = "storage_Fullpath";
            dbConnection.TableName = DatabaseTable;

            DataTable dtStorageCodes = dbConnection.LoadSpecificMySqlData();
            DataSet dsStorageCodes = new();
            dsStorageCodes.Tables.Add(dtStorageCodes);
            
            // add a relationship
            dsStorageCodes.Relations.Add("rsStorageParentChild", dsStorageCodes.Tables[DatabaseTable].Columns["storage_Id"], dsStorageCodes.Tables[DatabaseTable].Columns["storage_ParentId"]);

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
        }

        public void PopulateTree(DataRow dr, TreeViewItem pNode)
        {
            foreach (DataRow row in dr.GetChildRows("rsStorageParentChild"))
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

        #region Add Sublocation to the tree
        private void ButtonAddSubLocation(object sender, RoutedEventArgs e)
        {
            dialogStorageLocation dialogStorageLocation = new dialogStorageLocation();
            dialogStorageLocation.LabelDialogStorageLocation.Text = "Sublocatie toevoegen";
            dialogStorageLocation.ShowDialog();

            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(`Storage_Name`, `Storage_FullPath`, `Storage_ParentId`) " + "VALUES('" +
                dialogStorageLocation.diaLogStorageLocationValue + "', '" +
                valueFullpath.Text.Replace("\\", "\\\\") + "\\\\" + dialogStorageLocation.diaLogStorageLocationValue + "', '" +
                valueId.Text + "');";
            dbConnection.TableName = DatabaseTable;

            int ID = dbConnection.UpdateMySqlDataRecord();
            DataTable dtStorageCodes = dbConnection.LoadMySqlData();

            // Insert new value to the teeview so refresh of treeview not needed
            TreeViewItem cChild = new TreeViewItem();
            cChild.Header = dialogStorageLocation.diaLogStorageLocationValue;
            cChild.Name = "P" + valueId.Text + "C" + ID.ToString(); // Store ID and Parent_Id in the tag
            cChild.Tag = valueFullpath.Text + "\\" + dialogStorageLocation.diaLogStorageLocationValue;
            TreeViewItem ParentItem = treeViewStorage.SelectedItem as TreeViewItem;
            ParentItem.Items.Add(cChild);
        }
        #endregion

        #region Add Mainlocation to the tree
        private void ButtonAddMainLocation(object sender, RoutedEventArgs e)
        {
            dialogStorageLocation dialogStorageLocation = new dialogStorageLocation();
            dialogStorageLocation.LabelDialogStorageLocation.Text = "Hoofdlocatie toevoegen";
            dialogStorageLocation.ShowDialog();

            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(`Storage_Name`, `Storage_FullPath`) " + "VALUES('" +
                dialogStorageLocation.diaLogStorageLocationValue + "', '" +
                dialogStorageLocation.diaLogStorageLocationValue + "');";
            dbConnection.TableName = DatabaseTable;
            int ID = dbConnection.UpdateMySqlDataRecord();
            DataTable dtStorageCodes = dbConnection.LoadMySqlData();

            // Insert new value to the teeview so refresh of treeview not needed
            TreeViewItem root = new TreeViewItem();
            root.Header = dialogStorageLocation.diaLogStorageLocationValue;
            root.Name = "P" + ID.ToString(); // Store ID in the tag
            root.Tag = dialogStorageLocation.diaLogStorageLocationValue;
            treeViewStorage.Items.Add(root);
        }
        #endregion

        #region Delete Storage location and all sublocations if available.
        private void ButtonDeleteLocation(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            int ID = int.Parse(valueId.Text);
            dbConnection.SqlCommand = "DELETE FROM ";
            dbConnection.SqlCommandString = " WHERE Storage_Fullpath LIKE '" + valueFullpath.Text.Replace("\\", "\\\\\\\\") + "%';";
            dbConnection.TableName = DatabaseTable;
            dbConnection.UpdateMySqlDataRecord();
            _ = dbConnection.LoadMySqlData();

            TreeViewItem item = treeViewStorage.SelectedItem as TreeViewItem;
            if (item.Parent is TreeViewItem parent)
            {
                parent.Items.Remove(item);
            }

            treeViewStorage.Items.Refresh();
        }
        #endregion

        #region Rename Storage Loocation
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            if (inpStorageLocationName.Text != "")
            {
                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                dbConnection.Connect();

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "storage_Id = '" + valueId.Text + "', " +
                    "storage_Name = '" + inpStorageLocationName.Text + "', " +
                    "storage_FullPath = '" + valueParentFullpath.Text + "', " +
                    "storage_ParentId = '" + valueParentId.Text + "' WHERE " +
                    "storage_FullPath = " + valueParentFullpath.Text + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();
                DataTable dtCategoryCodes = dbConnection.LoadMySqlData();
            }
        }
        #endregion
    }
}
