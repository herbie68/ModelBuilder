namespace Modelbuilder
{
    public partial class metadataWorktype : Page
    {
        private readonly string DatabaseTable = "Worktype";

        public metadataWorktype()
        {
            InitializeComponent();
            BuildTree();
        }

        #region Command Binding CanExecute region
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion Command Binding CanExecute region

        #region Buildtree
        public void BuildTree()
        {
            Database dbConnection = new Database
            {
                TableName = DatabaseTable
            };

            _ = new DataTable();

            dbConnection.SqlSelectionString = "*";
            dbConnection.SqlOrderByString = "Fullpath";
            dbConnection.TableName = DatabaseTable;

            DataTable dtWorktypeCodes = dbConnection.LoadSpecificMySqlData();
            //Use a DataSet to manage the data
            DataSet dsWorktypeCodes = new DataSet();
            dsWorktypeCodes.Tables.Add(dtWorktypeCodes);

            // add a relationship
            dsWorktypeCodes.Relations.Add("rsParentChild", dsWorktypeCodes.Tables[DatabaseTable].Columns["Id"], dsWorktypeCodes.Tables[DatabaseTable].Columns["ParentId"]);

            foreach (DataRow row in dsWorktypeCodes.Tables[DatabaseTable].Rows)
            {
                if (row["ParentId"] == DBNull.Value)
                {
                    TreeViewItem root = new TreeViewItem();
                    root.Header = row["Name"].ToString();
                    root.Name = "P" + row["Id"].ToString();
                    root.Tag = row["Fullpath"].ToString();
                    treeViewWorktype.Items.Add(root);
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
                cChild.Header = row["Name"].ToString();
                cChild.Name = "P" + row["ParentId"].ToString() + "C" + row["Id"].ToString(); // Store ID and Parent_Id in the tag
                cChild.Tag = row["Fullpath"].ToString();
                pNode.Items.Add(cChild);
                //Recursively build the tree
                PopulateTree(row, cChild);
            }
        }
        #endregion Populate tree during build
        #endregion Buildtree

        #region SelectedItemChanged
        private void treeViewSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            // Changed RoutedPropertyChangedEventArgs<object> e into RoutedEventArgs e
            TreeViewItem SelectedItem = treeViewWorktype.SelectedItem as TreeViewItem;

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
            valueSelectedItem.Text = SelectedItem.Header.ToString();
            valueFullpath.Text = SelectedItem.Tag.ToString();
            valueParentFullpath.Text = ParentPathValue;
            valueParentId.Text = ParentValue;
            valueId.Text = ChildValue;
            valueOriginalName.Text = SelectedItem.Header.ToString();
            inpWorktypeName.Text = SelectedItem.Header.ToString();


            // Perhaps the switch can be used for a different menu on root item and sub item, but not sure if necesarry
            switch (SelectedItem.Tag.ToString())
            {
                case "Solution":
                    treeViewWorktype.ContextMenu = treeViewWorktype.Resources["SolutionContext"] as System.Windows.Controls.ContextMenu;
                    break;

                case "Folder":
                    treeViewWorktype.ContextMenu = treeViewWorktype.Resources["FolderContext"] as System.Windows.Controls.ContextMenu;
                    break;
            }
            treeViewWorktype.ContextMenu = treeViewWorktype.Resources["FolderContext"] as System.Windows.Controls.ContextMenu;
        }
        #endregion SelectedItemChanged

        #region Add a Sub Worktype

        private void ButtonAddSubWorktype(object sender, RoutedEventArgs e)
        {
            dialogWorktype dialogWorktype = new();
            dialogWorktype.LabelDialogWorktype.Text = "Sub Werksoort toevoegen";
            dialogWorktype.ShowDialog();

            Database dbConnection = new();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(`Name`, `FullPath`, `ParentId`) " + "VALUES('" +
                dialogWorktype.diaLogWorktypeValue + "', '" +
                valueFullpath.Text.Replace("\\", "\\\\") + "\\\\" + dialogWorktype.diaLogWorktypeValue + "', '" +
                valueId.Text + "');";
            Console.WriteLine(valueFullpath.Text + "\\" + dialogWorktype.diaLogWorktypeValue);
            dbConnection.TableName = DatabaseTable;

            int ID = dbConnection.UpdateMySqlDataRecord();
            DataTable dtWorktypeCodes = dbConnection.LoadMySqlData();

            // Insert new value to the teeview so refresh of treeview not needed
            TreeViewItem cChild = new TreeViewItem();
            cChild.Header = dialogWorktype.diaLogWorktypeValue;
            cChild.Name = "P" + valueId.Text + "C" + ID.ToString(); // Store ID and Parent_Id in the tag
            cChild.Tag = valueFullpath.Text + "\\" + dialogWorktype.diaLogWorktypeValue;
            TreeViewItem ParentItem = treeViewWorktype.SelectedItem as TreeViewItem;
            ParentItem.Items.Add(cChild);
        }

        #endregion Add a Sub Worktype

        #region Add a Main (root) Worktype

        private void ButtonAddMainWorktype(object sender, RoutedEventArgs e)
        {
            dialogWorktype dialogWorktype = new dialogWorktype();
            dialogWorktype.LabelDialogWorktype.Text = "Hoofd Werksoort toevoegen";
            dialogWorktype.ShowDialog();

            Database dbConnection = new();
            dbConnection.Connect();

            dbConnection.SqlCommand = "INSERT INTO ";
            dbConnection.SqlCommandString = "(`Name`, `FullPath`) " + "VALUES('" +
                dialogWorktype.diaLogWorktypeValue + "', '" +
                dialogWorktype.diaLogWorktypeValue + "');";
            dbConnection.TableName = DatabaseTable;
            int ID = dbConnection.UpdateMySqlDataRecord();
            DataTable dtWorktypeCodes = dbConnection.LoadMySqlData();

            // Insert new value to the teeview so refresh of treeview not needed
            TreeViewItem root = new();
            root.Header = dialogWorktype.diaLogWorktypeValue;
            root.Name = "P" + ID.ToString(); // Store ID in the tag
            root.Tag = dialogWorktype.diaLogWorktypeValue;
            treeViewWorktype.Items.Add(root);
        }

        #endregion Add a Main (root) Worktype

        #region Delete a Worktype, including all subs under the selected worktype
        private void ButtonDeleteWorktype(object sender, RoutedEventArgs e)
        {
            Database dbConnection = new Database();
            dbConnection.Connect();

            int ID = int.Parse(valueId.Text);
            dbConnection.SqlCommand = "DELETE FROM ";
            dbConnection.SqlCommandString = " WHERE Fullpath LIKE '" + valueFullpath.Text.Replace("\\", "\\\\\\\\") + "%';";
            dbConnection.TableName = DatabaseTable;
            dbConnection.UpdateMySqlDataRecord();
            _ = dbConnection.LoadMySqlData();

            TreeViewItem item = treeViewWorktype.SelectedItem as TreeViewItem;
            TreeViewItem parent = item.Parent as TreeViewItem;
            if (parent != null)
            {
                parent.Items.Remove(item);
            }
            treeViewWorktype.Items.Refresh();
        }
        #endregion Delete a Worktype, including all subs under the selected worktype

        #region Rename Worktype save changes to tree and database
        private void ToolbarButtonSave(object sender, RoutedEventArgs e)
        {
            if (inpWorktypeName.Text != "")
            {
                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                dbConnection.Connect();

                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "Name = '" + inpWorktypeName.Text + "' WHERE " +
                    "Id = " + valueId.Text + ";";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();

                string OriginalPath = valueParentFullpath.Text + "\\" + valueOriginalName.Text;
                string NewPath = valueParentFullpath.Text + "\\" + inpWorktypeName.Text;
                dbConnection.SqlCommand = "UPDATE ";
                dbConnection.SqlCommandString = " SET " +
                    "Fullpath = REPLACE(Fullpath, '" + OriginalPath.Replace("\\", "\\\\") + "', '" +
                    NewPath.Replace("\\", "\\\\") + "') WHERE " +
                    "FullPath LIKE ('" + OriginalPath.Replace("\\", "\\\\\\\\") + "%');";

                dbConnection.TableName = DatabaseTable;

                _ = dbConnection.UpdateMySqlDataRecord();

                TreeViewItem SelectedItem = treeViewWorktype.SelectedItem as TreeViewItem;

                // Change the header of the selected treview to update the view
                SelectedItem.Header = inpWorktypeName.Text;
            }
        }
        #endregion

        #region Get Treeview Item
        private TreeViewItem GetTreeViewItem(ItemsControl container, object item)
        {
            if (container != null)
            {
                if (container.DataContext == item)
                {
                    return container as TreeViewItem;
                }

                // Expand the current container
                if (container is TreeViewItem && !((TreeViewItem)container).IsExpanded)
                {
                    container.SetValue(TreeViewItem.IsExpandedProperty, true);
                }

                // Try to generate the ItemsPresenter and the ItemsPanel.
                // by calling ApplyTemplate.  Note that in the
                // virtualizing case even if the item is marked
                // expanded we still need to do this step in order to
                // regenerate the visuals because they may have been virtualized away.

                container.ApplyTemplate();
                ItemsPresenter itemsPresenter =
                    (ItemsPresenter)container.Template.FindName("ItemsHost", container);
                if (itemsPresenter != null)
                {
                    itemsPresenter.ApplyTemplate();
                }
                else
                {
                    // The Tree template has not named the ItemsPresenter,
                    // so walk the descendents and find the child.
                    itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                    if (itemsPresenter == null)
                    {
                        container.UpdateLayout();

                        itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                    }
                }

                Panel itemsHostPanel = (Panel)VisualTreeHelper.GetChild(itemsPresenter, 0);

                // Ensure that the generator for this panel has been created.
                UIElementCollection children = itemsHostPanel.Children;

                for (int i = 0, count = container.Items.Count; i < count; i++)
                {
                    TreeViewItem subContainer;
                    if (itemsHostPanel is MyVirtualizingStackPanel virtualizingPanel)
                    {
                        // Bring the item into view so
                        // that the container will be generated.
                        virtualizingPanel.BringIntoView(i);

                        subContainer =
                            (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);
                    }
                    else
                    {
                        subContainer =
                            (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);

                        // Bring the item into view to maintain the
                        // same behavior as with a virtualizing panel.
                        subContainer.BringIntoView();
                    }

                    if (subContainer != null)
                    {
                        // Search the next level for the object.
                        TreeViewItem resultContainer = GetTreeViewItem(subContainer, item);
                        if (resultContainer != null)
                        {
                            return resultContainer;
                        }
                        else
                        {
                            // The object is not under this TreeViewItem
                            // so collapse it.
                            subContainer.IsExpanded = false;
                        }
                    }
                }
            }

            return null;
        }
        #endregion

        #region Find Visual Child
        /// <summary>
        /// Search for an element of a certain type in the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of element to find.</typeparam>
        /// <param name="visual">The parent element.</param>
        /// <returns></returns>
        private T FindVisualChild<T>(Visual visual) where T : Visual
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual child = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (child != null)
                {
                    T correctlyTyped = child as T;
                    if (correctlyTyped != null)
                    {
                        return correctlyTyped;
                    }

                    T descendent = FindVisualChild<T>(child);
                    if (descendent != null)
                    {
                        return descendent;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Virtualizing Stackpanel
        public class MyVirtualizingStackPanel : VirtualizingStackPanel
        {

            public void BringIntoView(int index)
            {
                BringIndexIntoView(index);
            }
        }
        #endregion

    }
}