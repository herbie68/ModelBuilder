using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Modelbuilder
{
    public partial class metadataCategory : Page
    {
        private readonly string DatabaseTable = "category";
        public metadataCategory()
        {
            InitializeComponent();
            BuildTree();
        }

        #region Command Binding CanExecute region
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region Buildtree
        public void BuildTree()
        {
            Database dbConnection = new Database
            {
                TableName = DatabaseTable
            };

            _ = new DataTable();

            dbConnection.SqlSelectionString = "*";
            dbConnection.SqlOrderByString = "category_Fullpath";
            dbConnection.TableName = DatabaseTable;

            DataTable dtCategoryCodes = dbConnection.LoadSpecificMySqlData();
            //Use a DataSet to manage the data
            DataSet dsCategoryCodes = new DataSet();
            dsCategoryCodes.Tables.Add(dtCategoryCodes);

            // add a relationship
            dsCategoryCodes.Relations.Add("rsParentChild", dsCategoryCodes.Tables[DatabaseTable].Columns["category_Id"], dsCategoryCodes.Tables[DatabaseTable].Columns["category_ParentId"]);

            foreach (DataRow row in dsCategoryCodes.Tables[DatabaseTable].Rows)
            {
                if (row["category_ParentId"] == DBNull.Value)
                {
                    TreeViewItem root = new TreeViewItem();
                    root.Header = row["category_Name"].ToString();
                    root.Name = "P" + row["category_Id"].ToString();
                    root.Tag = row["category_Fullpath"].ToString();
                    treeViewCategory.Items.Add(root);
                    PopulateTree(row, root);
                }
            }
        }

        #region Populate tree during build
        public void PopulateTree(DataRow dr, TreeViewItem pNode)
        {
            foreach (DataRow row in dr.GetChildRows("rsParentChild"))
            {
                TreeViewItem cChild = new TreeViewItem();
                cChild.Header = row["category_Name"].ToString();
                cChild.Name = "P" + row["category_ParentId"].ToString() + "C" + row["category_Id"].ToString(); // Store ID and Parent_Id in the tag
                cChild.Tag = row["category_Fullpath"].ToString();
                pNode.Items.Add(cChild);
                //Recursively build the tree
                PopulateTree(row, cChild);
            }
        }
        #endregion
        #endregion

        #region SelectedItemChanged
        private void treeViewCategory_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            // Changed RoutedPropertyChangedEventArgs<object> e into RoutedEventArgs e
            TreeViewItem SelectedItem = treeViewCategory.SelectedItem as TreeViewItem;

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
            inpCategoryName.Text = SelectedItem.Header.ToString();


            // Perhaps the switch can be used for a different menu on root item and sub item, but not sure if necesarry
            switch (SelectedItem.Tag.ToString())
            {
                case "Solution":
                    treeViewCategory.ContextMenu = treeViewCategory.Resources["SolutionContext"] as System.Windows.Controls.ContextMenu;
                    break;
                case "Folder":
                    treeViewCategory.ContextMenu = treeViewCategory.Resources["FolderContext"] as System.Windows.Controls.ContextMenu;
                    break;
            }
            treeViewCategory.ContextMenu = treeViewCategory.Resources["FolderContext"] as System.Windows.Controls.ContextMenu;
        }
        #endregion

        #region Add a Sub category
        private void ButtonAddSubCategory(object sender, RoutedEventArgs e)
        {
            dialogCategory dialogCategory = new dialogCategory();
            dialogCategory.LabelDialogCategory.Text = "Subcategory toevoegen";
            dialogCategory.ShowDialog();

            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(`category_Name`, `category_FullPath`, `category_ParentId`) " + "VALUES('" +
                dialogCategory.diaLogCategoryValue + "', '" +
                valueFullpath.Text.Replace("\\", "\\\\") + "\\\\" + dialogCategory.diaLogCategoryValue + "', '" +
                valueId.Text + "');";
            Console.WriteLine(valueFullpath.Text + "\\" + dialogCategory.diaLogCategoryValue);
            dbConnection.TableName = DatabaseTable;

            int ID = dbConnection.UpdateMySqlDataRecord();
            DataTable dtCategoryCodes = dbConnection.LoadMySqlData();

            // Insert new value to the teeview so refresh of treeview not needed
            TreeViewItem cChild = new TreeViewItem();
            cChild.Header = dialogCategory.diaLogCategoryValue;
            cChild.Name = "P" + valueId.Text + "C" + ID.ToString(); // Store ID and Parent_Id in the tag
            cChild.Tag = valueFullpath.Text + "\\" + dialogCategory.diaLogCategoryValue;
            TreeViewItem ParentItem = treeViewCategory.SelectedItem as TreeViewItem;
            ParentItem.Items.Add(cChild);
        }
        #endregion

        #region Add a Main (root) category
        private void ButtonAddMainCategory(object sender, RoutedEventArgs e)
        {
            dialogCategory dialogCategory = new dialogCategory();
            dialogCategory.LabelDialogCategory.Text = "Hoofdcategory toevoegen";
            dialogCategory.ShowDialog();

            Database dbConnection = new Database();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(`category_Name`, `category_FullPath`) " + "VALUES('" +
                dialogCategory.diaLogCategoryValue + "', '" +
                dialogCategory.diaLogCategoryValue + "');";
            dbConnection.TableName = DatabaseTable;
            int ID = dbConnection.UpdateMySqlDataRecord();
            DataTable dtCategoryCodes = dbConnection.LoadMySqlData();

            // Insert new value to the teeview so refresh of treeview not needed
            TreeViewItem root = new TreeViewItem();
            root.Header = dialogCategory.diaLogCategoryValue;
            root.Name = "P" + ID.ToString(); // Store ID in the tag
            root.Tag = dialogCategory.diaLogCategoryValue;
            treeViewCategory.Items.Add(root);
        }
        #endregion

        #region Delete a category, including all subs under the selected catogory
        private void ButtonDeleteCategory(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            int ID = int.Parse(valueId.Text);
            dbConnection.SqlCommand = "DELETE FROM ";
            dbConnection.SqlCommandString = " WHERE category_Fullpath LIKE '" + valueFullpath.Text.Replace("\\", "\\\\\\\\") + "%';";
            dbConnection.TableName = DatabaseTable;
            dbConnection.UpdateMySqlDataRecord();
            _ = dbConnection.LoadMySqlData();

            var item = treeViewCategory.SelectedItem as TreeViewItem;
            var parent = item.Parent as TreeViewItem;
            if (parent != null)
            {
                parent.Items.Remove(item);
            }
          
            treeViewCategory.Items.Refresh();
        }
        #endregion

        #region Rename category save changes to tree and database
        private void ToolbarButtonSave(object sender, RoutedEventArgs e) 
        {
            if (inpCategoryName.Text != "")
            {
                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                dbConnection.Connect();

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "category_Id = '" + valueId.Text + "', " +
                    "category_Name = '" + inpCategoryName.Text + "', " +
                    "category_FullPath = '" + valueParentFullpath.Text + "', " +
                    "category_ParentId = '" + valueParentId.Text + "' WHERE " +
                    "category_FullPath = " + valueParentFullpath.Text + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();
                DataTable dtCategoryCodes = dbConnection.LoadMySqlData();
            }
        }
        #endregion

    }
}
