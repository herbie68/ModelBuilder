using System.Globalization;
using System.Windows;

namespace Modelbuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl-NL");
        }
    }
}