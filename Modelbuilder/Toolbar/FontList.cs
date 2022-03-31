using System.Collections.ObjectModel;

namespace Modelbuilder
{
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
