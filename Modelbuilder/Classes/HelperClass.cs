using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

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

        if (Hours > 0)
        {
            DurationString.Append(Hours.ToString(CultureInfo.InvariantCulture) + " ");
            if (Hours == 1)
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

    public string ConvertToLongDate(int Day, int Month, int Year, string DisplayWeekday = "long", string DisplayMonth = "long")
    {
        var _result = "";

        // Determin the day of the week and get the correct weekdayname in the _result string
        DateTime _dateValue = new DateTime(Year, Month, Day);
        var _weekday = (int)_dateValue.DayOfWeek;
        if (DisplayWeekday == "long")
        {
            switch (_weekday)
            {
                case 0:
                    _result = Languages.Cultures.general_datetime_Day0;
                    break;
                case 1:
                    _result = Languages.Cultures.general_datetime_Day1;
                    break;
                case 2:
                    _result = Languages.Cultures.general_datetime_Day2;
                    break;
                case 3:
                    _result = Languages.Cultures.general_datetime_Day3;
                    break;
                case 4:
                    _result = Languages.Cultures.general_datetime_Day4;
                    break;
                case 5:
                    _result = Languages.Cultures.general_datetime_Day5;
                    break;
                case 6:
                    _result = Languages.Cultures.general_datetime_Day6;
                    break;
            }
        }
        else
        {
            switch (_weekday)
            {
                case 0:
                    _result = Languages.Cultures.general_datetime_Day0_Short;
                    break;
                case 1:
                    _result = Languages.Cultures.general_datetime_Day1_Short;
                    break;
                case 2:
                    _result = Languages.Cultures.general_datetime_Day2_Short;
                    break;
                case 3:
                    _result = Languages.Cultures.general_datetime_Day3_Short;
                    break;
                case 4:
                    _result = Languages.Cultures.general_datetime_Day4_Short;
                    break;
                case 5:
                    _result = Languages.Cultures.general_datetime_Day5_Short;
                    break;
                case 6:
                    _result = Languages.Cultures.general_datetime_Day6_Short;
                    break;
            }
        }

        // Add the calender day to the _result string
        _result += " " + Day.ToString() + " ";

        // Get the name of the month and put it in the _result string
        if (DisplayMonth == "long")
        {
            switch (Month)
            {
                case 1:
                    _result += Languages.Cultures.general_DateTime_Month1;
                    break;
                case 2:
                    _result += Languages.Cultures.general_DateTime_Month2;
                    break;
                case 3:
                    _result += Languages.Cultures.general_DateTime_Month3;
                    break;
                case 4:
                    _result += Languages.Cultures.general_DateTime_Month4;
                    break;
                case 5:
                    _result += Languages.Cultures.general_DateTime_Month5;
                    break;
                case 6:
                    _result += Languages.Cultures.general_DateTime_Month6;
                    break;
                case 7:
                    _result += Languages.Cultures.general_DateTime_Month7;
                    break;
                case 8:
                    _result += Languages.Cultures.general_DateTime_Month8;
                    break;
                case 9:
                    _result += Languages.Cultures.general_DateTime_Month9;
                    break;
                case 10:
                    _result += Languages.Cultures.general_DateTime_Month10;
                    break;
                case 11:
                    _result += Languages.Cultures.general_DateTime_Month11;
                    break;
                case 12:
                    _result += Languages.Cultures.general_DateTime_Month12;
                    break;
            }
        }
        else
        {
            switch (Month)
            {
                case 1:
                    _result += Languages.Cultures.general_DateTime_Month1_Short;
                    break;
                case 2:
                    _result += Languages.Cultures.general_DateTime_Month2_Short;
                    break;
                case 3:
                    _result += Languages.Cultures.general_DateTime_Month3_Short;
                    break;
                case 4:
                    _result += Languages.Cultures.general_DateTime_Month4_Short;
                    break;
                case 5:
                    _result += Languages.Cultures.general_DateTime_Month5_Short;
                    break;
                case 6:
                    _result += Languages.Cultures.general_DateTime_Month6_Short;
                    break;
                case 7:
                    _result += Languages.Cultures.general_DateTime_Month7_Short;
                    break;
                case 8:
                    _result += Languages.Cultures.general_DateTime_Month8_Short;
                    break;
                case 9:
                    _result += Languages.Cultures.general_DateTime_Month9_Short;
                    break;
                case 10:
                    _result += Languages.Cultures.general_DateTime_Month10_Short;
                    break;
                case 11:
                    _result += Languages.Cultures.general_DateTime_Month11_Short;
                    break;
                case 12:
                    _result += Languages.Cultures.general_DateTime_Month12_Short;
                    break;
            }
        }

        // Add the year to the _result string
        _result += " " + Year.ToString();

        return _result;
    }

}
