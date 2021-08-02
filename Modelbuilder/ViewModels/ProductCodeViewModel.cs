﻿using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace Modelbuilder
{
    internal class ProductCodeViewModel
    {
        public List<string> CategoryCollection { get; set; }
        public List<string> StorageCollection { get; set; }
        public List<string> SupplierCollection { get; set; }
        public List<string> SupplierList { get; set; }
        private readonly string DatabaseCategoryTable = "category";
        private readonly string DatabaseSupplierTable = "supplier";
        private readonly string DatabaseStorageTable = "storage";
		public string TableId = "product_Id";

        public ProductCodeViewModel()
        {
            CategoryCollection = new List<string> { };
            StorageCollection = new List<string> { };
			SupplierCollection = new List<string> { };

            Database dbCategoryConnection = new()
            {
                TableName = DatabaseCategoryTable
            };

            //DataTable dataTable = new DataTable();
            dbCategoryConnection.SqlSelectionString = "category_Name, category_Id";
            dbCategoryConnection.SqlOrderByString = "category_Id";
            dbCategoryConnection.TableName = DatabaseCategoryTable;

            DataTable dtCategorySelection = dbCategoryConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtCategorySelection.Rows.Count; i++)
            {
                CategoryCollection.Add(dtCategorySelection.Rows[i][0].ToString());
                CategoryCollection.Add(dtCategorySelection.Rows[i][1].ToString());
            };

            SupplierCollection = new List<string> { };

            Database dbStorageConnection = new()
            {
                TableName = DatabaseSupplierTable
            };

            //DataTable dataTable = new DataTable();
            dbStorageConnection.SqlSelectionString = "storage_Name, storage_Id";
            dbStorageConnection.SqlOrderByString = "storage_Id";
            dbStorageConnection.TableName = DatabaseStorageTable;

            DataTable dtStorageSelection = dbStorageConnection.LoadSpecificMySqlData();

            for (int i = 0; i < dtStorageSelection.Rows.Count; i++)
            {
                StorageCollection.Add(dtStorageSelection.Rows[i][0].ToString());
            };
            StorageCollection = new List<string> { };

            #region Fill Supplier dropdown
            Database dbSupplierConnection = new()
            {
                TableName = DatabaseSupplierTable
            };

            dbSupplierConnection.SqlSelectionString = "supplier_Name, supplier_Id";
            dbSupplierConnection.SqlOrderByString = "supplier_Id";
            dbSupplierConnection.TableName = DatabaseSupplierTable;

            DataTable dtSupplierSelection = dbSupplierConnection.LoadSpecificMySqlData();

            List<Supplier> SupplierList = new();

            for (int i = 0; i < dtSupplierSelection.Rows.Count; i++)
            {
                SupplierList.Add(new Supplier(dtSupplierSelection.Rows[i][0].ToString(),
                    int.Parse(dtSupplierSelection.Rows[i][1].ToString())));
            };

            //cboxProductSupplierName.ItemsSource = SupplierList;
            #endregion


        }

        private class Category
        {
            public Category(string Name, int Id)
            {
                categoryName = Name;
                categoryId = Id;
            }

            public string categoryName { get; set; }
            public int categoryId { get; set; }
        }

        private class Storage
        {
            public Storage(string Name, int Id)
            {
                storageName = Name;
                storageId = Id;
            }

            public string storageName { get; set; }
            public int storageId { get; set; }
        }

        private class Supplier
        {
            public Supplier(string Name, int Id)
            {
                supplierName = Name;
                supplierId = Id;
            }

            public string supplierName { get; set; }
            public int supplierId { get; set; }
        }
    }
}