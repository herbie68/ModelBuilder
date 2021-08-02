using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder
{
    internal class SupplierListViewModel
    {
        public List<string> SupplierList { get; set; }
        private readonly string DatabaseTable = "supplier";
        public SupplierListViewModel()
        {
            Database dbConnection = new()
            {
                TableName = DatabaseTable
            };

            dbConnection.SqlSelectionString = "supplier_Name, supplier_Id";
            dbConnection.SqlOrderByString = "supplier_Id";
            dbConnection.TableName = DatabaseTable;

            DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

            List<Supplier> SupplierList = new();

            for (int i = 0; i < dtSelection.Rows.Count; i++)
            {
                SupplierList.Add(new Supplier(dtSelection.Rows[i][0].ToString(),
                    int.Parse(dtSelection.Rows[i][1].ToString())));
            };
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
