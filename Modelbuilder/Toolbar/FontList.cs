﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Media;


namespace Modelbuilder
{
    /// <summary>
    /// Get the list of all installed Fonts from the syetem
    /// </summary>
    internal class FontList : ObservableCollection<string>
    {
        public FontList() 
        {
            foreach (FontFamily f in Fonts.SystemFontFamilies)
            {
                Add(f.ToString());
            }
        }
    }
}