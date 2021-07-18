using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Modelbuilder
{
    public class MainStorageLocation
    {
        public int storage_Id { get; set; }
        public int storage_ParentId { get; set; }
        public string storage_FullPath { get; set; }
        public string storage_Code { get; set; }
        public string storage_Name { get; set; }
        public int storage_Level { get; set; }
        public List<SubStorageLocation> SubStorageLocations { get; set; }
    }
    public class SubStorageLocation
    {
        public int storage_Id { get; set; }
        public int storage_ParentId { get; set; }
        public string storage_FullPath { get; set; }
        public string storage_Code { get; set; }
        public string storage_Name { get; set; }
        public int storage_Level { get; set; }
    }

    public class StorageLocation
    {
        public int storage_Id { get; set; }
        public int storage_ParentId { get; set; }
        public string storage_FullPath { get; set; }
        public string storage_Code { get; set; }
        public string storage_Name { get; set; }
        public int storage_Level { get; set; }
        public List<SubStorageLocation> SubStorageLocations { get; set; }
    }
}
