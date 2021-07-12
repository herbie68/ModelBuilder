﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for metadataCountry.xaml
    /// </summary>
    public partial class metadataCategory : Page
    {
        private readonly string DatabaseTable = "category";
        public metadataCategory()
        {
            InitializeComponent();
            BuildTree();
        }
        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
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

        private void ButtonDeleteCategorie(object sender, RoutedEventArgs e)
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

            //treeViewCategory.Items.Remove(treeViewCategory.SelectedItem);

            
            treeViewCategory.Items.Refresh();

            //treeViewCategory.Items.Clear();
            //BuildTree();
        }

        private void ToolbarButtonSave(object sender, RoutedEventArgs e) 
        {
            if (inpCategoryName.Text != "")
            {
                Database dbConnection = new Database
                {
                    TableName = DatabaseTable
                };

                //Database dbConnection = new Database();
                dbConnection.Connect();

                /*
                 valueFullpath.Text = SelectedItem.Tag.ToString();
                valueParentFullpath.Text = ParentPathValue;
                valueParentId.Text = ParentValue;
                valueId.Text = ChildValue;
                inpCategoryName.Text = SelectedItem.Header.ToString();
                 */

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
                /*
                // Load the data from the database into the datagrid
                CategoryCode_DataGrid.DataContext = dtCountryCodes;

                // Make sure the eddited row in the datagrid is selected
                CountryCode_DataGrid.SelectedIndex = int.Parse(inpCountryId.Text) - 1;
                CountryCode_DataGrid.Focus();
                */
            }
        }

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

                MyVirtualizingStackPanel virtualizingPanel =
                    itemsHostPanel as MyVirtualizingStackPanel;

                for (int i = 0, count = container.Items.Count; i < count; i++)
                {
                    TreeViewItem subContainer;
                    if (virtualizingPanel != null)
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

        public class MyVirtualizingStackPanel : VirtualizingStackPanel
        {
            /// <summary>
            /// Publically expose BringIndexIntoView.
            /// </summary>
            public void BringIntoView(int index)
            {

                this.BringIndexIntoView(index);
            }
        }


    }
}
