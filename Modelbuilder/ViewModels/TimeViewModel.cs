using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Modelbuilder;

public class TimeViewModel
{
    public ICollectionView TimeView { get; set; }
    public TimeViewModel()
    {
        //https://www.youtube.com/watch?v=S_0ZHvXCtYY&t=60s
        IList<TimeEntry> entries = new TimeEntry();
        TimeView = CollectionViewSource.GetDefaultView(entries);
    }

}
