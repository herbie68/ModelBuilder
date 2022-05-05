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

    #region Units class
    public class Units
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
#endregion

    #region Add zeros to a timestring
    public string AddZeros(string TempString, int TotalLength)
    {
        var _temp = new string('0', TotalLength);
        var NewString = (_temp + TempString.Trim()).Substring((_temp + TempString.Trim()).Length - TotalLength, TotalLength);

        return NewString;
    }
#endregion

    #region Return given minutes in hh:mm format 
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
#endregion

    #region Calculate time duration in years, mont, weeks, days, hours, minutes on given time in minutes
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
#endregion
    
    #region Convert date to long date format
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
#endregion

    #region Export data to CSV file
    public void ExportToCsv(DataTable _dt, string FileName, string[] Columns, string Header)
    {
        int _column = 0;
        StreamWriter sw = new(FileName, false);

        // Check if a Header is wanted in the CSV File
        if (Header.ToLower() != "header")
        {
            for (int i = 0; i < _dt.Columns.Count; i++)
            {
                // Check if the Column name is Selected for export
                if (Columns.Contains(_dt.Columns[i].ToString()))
                {
                    _column++;
                    sw.Write(_dt.Columns[i]);
                    // Check if the Column is the last column that has to be written, if not we need a ;
                    if (_column < Columns.Length)
                    {
                        if (i < _dt.Columns.Count - 1)
                        {
                            sw.Write(";");
                        }
                    }
                }
            }
            sw.Write(sw.NewLine);
        }

        _column = 0;
        int _rowCount = 0;
        foreach (DataRow dr in _dt.Rows)
        {
            _rowCount++;
            for (int i = 0; i < _dt.Columns.Count; i++)
            {
                _column++;
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    // Check if the vaule belongs to a selected Column
                    if (Columns.Contains(_dt.Columns[i].ToString()))
                    {
                        // Check if the string contains a ; what is also the value separator
                        if (value.Contains(';'))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                }
                // Check if the Column is the last column that has to be written, if not we need a ;
                if (_column < Columns.Length)
                {
                    if (i < _dt.Columns.Count - 1)
                    {
                        sw.Write(";");
                    }
                }
            }
            _column = 0;
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }
    #endregion

    #region Get Timestamp as Filename prefix
    public string GetFilePrefix()
    {
        var _tempMonth = "0" + DateTime.Now.Month.ToString();
        var _tempDay = "0" + DateTime.Now.Day.ToString();
        var _tempHour = "0" + DateTime.Now.Hour.ToString();
        var _tempMinute = "0" + DateTime.Now.Minute.ToString();
        var _tempSecond = "0" + DateTime.Now.Second.ToString();

        var Prefix = DateTime.Now.Year.ToString() +
            _tempMonth.Substring(_tempMonth.Length - 2, 2) +
            _tempDay.Substring(_tempDay.Length - 2, 2) +
            _tempHour.Substring(_tempHour.Length - 2, 2) +
            _tempMinute.Substring(_tempMinute.Length - 2, 2) +
            _tempSecond.Substring(_tempSecond.Length - 2, 2) +
            " - ";

        return Prefix;
    }
    #endregion

    #region Write header to empty CSV file
    public void PrepareCsv(string FileName, string[] Columns)
    {
        int _column = 0;
        StreamWriter sw = new(FileName, false);

        for (int i = 0; i < Columns.Length; i++)
        {
            _column++;
            sw.Write(Columns[i]);
            // Check if the Column is the last column that has to be written, if not we need a ;
            if (_column < Columns.Length)
            {
                if (i < Columns.Length - 1)
                {
                    sw.Write(";");
                }
            }
        }
        sw.Write(sw.NewLine);
        sw.Close();
    }
    #endregion

    #region Headers for Import/Export
    #region Brand Headers
    public string[] GetBrandHeaders()
    {
        string[] Header = new string[] { HelperGeneral.DbBrandTableFieldNameName };
        return Header;
    }
    #endregion

    #region Category Headers
    public string[] GetCategoryHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbCategoryTableFieldNameId, 
            HelperGeneral.DbCategoryTableFieldNameParentId, 
            HelperGeneral.DbCategoryTableFieldNameName, 
            HelperGeneral.DbCategoryTableFieldNameFullpath
        };
        return Header;
    }
    #endregion

    #region Contacttype Headers
    public string[] GetContactTypeHeaders()
    {
        string[] Header = new string[] { HelperGeneral.DbContactTypeTableFieldNameName };
        return Header;
    }
    #endregion

    #region Country Headers
    public string[] GetCountryHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbCountryTableFieldNameCode, 
            HelperGeneral.DbCountryTableFieldNameName, 
            HelperGeneral.DbCountryTableFieldNameDefCurrencySymbol, 
            HelperGeneral.DbCountryTableFieldNameDefCurrencyId
        };
        return Header;
    }
    #endregion

    #region Currency Headers
    public string[] GetCurrencyHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbCurrencyTableFieldNameCode, 
            HelperGeneral.DbCurrencyTableFieldNameName, 
            HelperGeneral.DbCurrencyTableFieldNameSymbol, 
            HelperGeneral.DbCurrencyTableFieldNameRate
        };
        return Header;
    }
    #endregion

    #region Product Headers
    public string[] GetProductHeaders()
    {
        string[] Header = new string[] 
        { 
            HelperGeneral.DbProductTableFieldNameCode,
            HelperGeneral.DbProductTableFieldNameName,
            HelperGeneral.DbProductTableFieldNameDimensions,
            HelperGeneral.DbProductTableFieldNamePrice,
            HelperGeneral.DbProductTableFieldNameMinimalStock,
            HelperGeneral.DbProductTableFieldNameStandardOrderQuantity,
            HelperGeneral.DbProductTableFieldNameProjectCosts,
            HelperGeneral.DbProductTableFieldNameUnitId,
            HelperGeneral.DbProductTableFieldNameBrandId,
            HelperGeneral.DbProductTableFieldNameCategoryId,
            HelperGeneral.DbProductTableFieldNameStorageId 
        };
        return Header;
    }
    #endregion

    #region ProductSupplier Headers
    public string[] GetProductSupplierHeaders()
    {
        string[] Header = new string[] 
        {
            HelperGeneral.DbProductSupplierTableFieldNameProductCode,
            HelperGeneral.DbProductSupplierTableFieldNameSupplierId,
            HelperGeneral.DbProductSupplierTableFieldNameCurrencyId,
            HelperGeneral.DbProductSupplierTableFieldNameProductNumber,
            HelperGeneral.DbProductSupplierTableFieldNameProductName,
            HelperGeneral.DbProductSupplierTableFieldNamePrice,
            HelperGeneral.DbProductSupplierTableFieldNameProductUrl
        };
        return Header;
    }
    #endregion

    #region Project Headers
    public string[] GetProjectHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbProjectTableFieldNameCode, 
            HelperGeneral.DbProjectTableFieldNameName, 
            HelperGeneral.DbProjectTableFieldNameStartDate, 
            HelperGeneral.DbProjectTableFieldNameEndDate, 
            HelperGeneral.DbProjectTableFieldNameExpectedTime, 
            HelperGeneral.DbProjectTableFieldNameClosed
        };
        return Header;
    }
    #endregion

    #region Storage Headers
    public string[] GetStorageHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbStorageTableFieldNameId, 
            HelperGeneral.DbStorageTableFieldNameParentId, 
            HelperGeneral.DbStorageTableFieldNameName, 
            HelperGeneral.DbStorageTableFieldNameFullpath
        };
        return Header;
    }
    #endregion

    #region Supplier Headers
    public string[] GetSupplierHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbSupplierFieldNameCode,
            HelperGeneral.DbSupplierFieldNameName,
            HelperGeneral.DbSupplierFieldNameAddress1,
            HelperGeneral.DbSupplierFieldNameAddress2,
            HelperGeneral.DbSupplierFieldNameZip,
            HelperGeneral.DbSupplierFieldNameCity,
            HelperGeneral.DbSupplierFieldNameUrl,
            HelperGeneral.DbSupplierFieldNameShippingCosts,
            HelperGeneral.DbSupplierFieldNameMinShippingCosts,
            HelperGeneral.DbSupplierFieldNameOrderCosts,
            HelperGeneral.DbSupplierFieldNameCurrencyId,
            HelperGeneral.DbSupplierFieldNameCountryId
        };
        return Header;
    }
    #endregion

    #region SupplierContact Headers
    public string[] GetSupplierContactHeaders()
    {
        string[] Header = new string[]
        {
            HelperGeneral.DbSupplierContactFieldNameSupplierId, 
            HelperGeneral.DbSupplierContactFieldNameTypeId, 
            HelperGeneral.DbSupplierContactFieldNameName, 
            HelperGeneral.DbSupplierContactFieldNameMail, 
            HelperGeneral.DbSupplierContactFieldNamePhone
        };
        return Header;
    }
    #endregion

    #region TimeEntry Headers
    public string[] GetTimeEntryHeaders()
    {
        string[] Header = new string[] 
        { 
            HelperGeneral.DbTimeTableFieldNameProjectId, 
            HelperGeneral.DbTimeTableFieldNameWorktypeId, 
            HelperGeneral.DbTimeTableFieldNameWorkDate, 
            HelperGeneral.DbTimeTableFieldNameStartTime, 
            HelperGeneral.DbTimeTableFieldNameEndTime, 
            HelperGeneral.DbTimeTableFieldNameComment 
        };
        return Header;
    }
    #endregion

    #region Unit Headers
    public string[] GetUnitHeaders()
    {
        string[] Header = new string[] { HelperGeneral.DbUnitTableFieldNameUnitName };
        return Header;
    }
    #endregion

    #region Worktype Headers
    public string[] GetWorktypeHeaders()
    {
        string[] Header = new string[] 
        { 
            HelperGeneral.DbWorktypeTableFieldNameId, 
            HelperGeneral.DbWorktypeTableFieldNameParentId, 
            HelperGeneral.DbWorktypeTableFieldNameName, 
            HelperGeneral.DbWorktypeTableFieldNameFullpath 
        };
        return Header;
    }
    #endregion
    #endregion
}
