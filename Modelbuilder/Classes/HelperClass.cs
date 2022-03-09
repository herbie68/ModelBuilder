using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;

internal class HelperClass
{
    public HelperClass()
    {
        // No action needed here (yet)
    }

    public string AddZeros(string TempString, int TotalLength)
    {
        var _temp = new string('0', TotalLength);
        var NewString = (_temp + TempString.Trim()).Substring((_temp + TempString.Trim()).Length - TotalLength, TotalLength);

        return NewString;
    }

}
