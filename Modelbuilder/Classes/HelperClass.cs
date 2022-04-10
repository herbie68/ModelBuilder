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

    public string HourMinute(int CalcMinutes)
    {
        StringBuilder DurationString = new("");
        var HourMinutes = 60;

        int Hours = CalcMinutes / HourMinutes; CalcMinutes -= Hours * HourMinutes;
        int Minutes = CalcMinutes;

        string Mins = ("00" + Minutes);
        //somestring.Substring(somestring.Length-nCount,nCount)

        DurationString.Append(Hours + ":" + Mins.Substring(Mins.Length - 2, 2));
        return DurationString.ToString().Trim();

    }

        public string TimeDuration(int CalcMinutes)
    {
        StringBuilder DurationString = new("");
        var YearMinutes = 525600;   var MonthMinutes = 43800;   var WeekMinutes = 10080;    var DayMinutes = 1440;  var HourMinutes = 60;

        int Years = CalcMinutes / YearMinutes;      CalcMinutes -= Years * YearMinutes;
        int Months = CalcMinutes / MonthMinutes;    CalcMinutes -= Months * MonthMinutes; 
        int Weeks = CalcMinutes / WeekMinutes;      CalcMinutes -= Weeks * WeekMinutes;
        int Days = CalcMinutes / DayMinutes;        CalcMinutes -= Days * DayMinutes;
        int Hours = CalcMinutes / HourMinutes;      CalcMinutes -= Hours * HourMinutes;
        int Minutes = CalcMinutes;

        if(Years > 0)
        {
            DurationString.Append(Years.ToString(CultureInfo.InvariantCulture) + " ");
            if (Years == 1)
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Year + " ");
            }
            else
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Years + " ");
            }
        }

        if (Months > 0)
        {
            DurationString.Append(Months.ToString(CultureInfo.InvariantCulture) + " ");
            if (Months == 1)
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Month + " ");
            }
            else
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Months + " ");
            }
        }

        if (Weeks > 0)
        {
            DurationString.Append(Weeks.ToString(CultureInfo.InvariantCulture) + " ");
            if (Weeks == 1)
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Week + " ");
            }
            else
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Weeks + " ");
            }
        }

        if (Days > 0)
        {
            DurationString.Append(Days.ToString(CultureInfo.InvariantCulture) + " ");
            if (Months == 1)
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Day + " ");
            }
            else
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Days + " ");
            }
        }

        if (Hours > 0)
        {
            DurationString.Append(Hours.ToString(CultureInfo.InvariantCulture) + " ");
            if (Months == 1)
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Hour + " ");
            }
            else
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Hours + " ");
            }
        }

        if (Minutes > 0)
        {
            DurationString.Append(Minutes.ToString(CultureInfo.InvariantCulture) + " ");
            if (Minutes == 1)
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Minute);
            }
            else
            {
                DurationString.Append(Modelbuilder.Languages.Cultures.general_datetime_Minutes);
            }
        }

        return DurationString.ToString().Trim();
    }

}
