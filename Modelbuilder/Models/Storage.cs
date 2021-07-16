using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder.Models
{
    public class Storage : INotifyPropertyChanged
    {
        public Storage(int storageId, int storageParentId, string storageFullPath, string storageCode, string storageName, int storageLevel)
        {
            Id = storageId;
            ParentId = storageParentId;
            FullPath = storageFullPath;
            Code = storageCode;
            Name = storageName;
            Level = storageLevel;
        }

        private int _Id;
        private int _ParentId;
        private string _FullPath;
        private string _Code;
        private string _Name;
        private int _Level;      

        public int Id { get { return _Id; } set { _Id = value; OnPropertyChanged("Id");}}
        public int ParentId { get { return _ParentId; } set { _ParentId = value; OnPropertyChanged("ParentId"); } }
        public string FullPath { get { return _FullPath; } set { _FullPath = value; OnPropertyChanged("FullPath"); } }
        public string Code { get { return _Code; } set { _Code = value; OnPropertyChanged("Code"); } }
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged("Name"); } }
        public int Level { get { return _Level; } set { _Level = value; OnPropertyChanged("Level"); } }

        #region INotyfyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
