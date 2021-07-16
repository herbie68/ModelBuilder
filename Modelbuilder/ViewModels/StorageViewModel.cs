using Modelbuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder.ViewModels
{
    internal class StorageViewModel
    {
        public StorageViewModel()
        {
            // TODO: Add the connection to the database and getting the data from the data right here

            // TODO: The StorageParent can be null, but an int cannot be converted to null, so need a fix, when value = null then????

            //TODO: https://www.youtube.com/watch?v=EpGvqVtSYjs continue from minute 12
            _Storage = new Storage(0, 0, "Test", "Test", "Eerste locatie", 0);
        }

        private Storage _Storage;

        public Storage Storage { get { return _Storage; } }

        public void SaveChanges()
        {
            // TODO: Implement the save data functionality here
        }
    }
}
