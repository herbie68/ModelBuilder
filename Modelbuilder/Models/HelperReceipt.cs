using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;
internal class HelperReceipt
{
    #region public Variables
    /// <summary>
    /// Gets or Sets the connection str.
    /// </summary>
    public string ConnectionStr { get; set; }

    public static string DbCategoryTable = "category";
    public static string DbOrderTable = "supplyorder";
    public static string DbOrderView = "view_supplyopenorder";
    public static string DbOrderLineTable = "supplyorderline";
    public static string DbOrderLineView = "view_supplyopenorderline";
    public static string DbCurrencyTable = "currency";
    public static string DbProductTable = "product";
    public static string DbProductSupplierTable = "productsupplier";
    public static string DbProjectTable = "project";
    public static string DbSupplierTable = "supplier";
    public static string DbStockTable = "stock";
    public static string DbStockView = "view_stock";
    public static string DbStocklogTable = "stocklog";

    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables

    #region Connector to database
    public HelperReceipt(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperReceipt(string serverName, int portNumber, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
    }
    #endregion Connector to database

    #region Execute Non Query
    public void ExecuteNonQuery(string sqlText)
    {
        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    #endregion Execute NonQuery

    #region Get Data from TableView or Table
    public DataTable GetData(string Table, string WhereString = "", int Id = 0)
    {
        DataTable dt = new();
        string sqlText = string.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT * FROM " + Table + " WHERE " + WhereString + " = @Id";
        }
        else
        {
            sqlText = "SELECT * FROM " + Table;
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            if (Id > 0) { cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id; }

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }
        return dt;
    }
    #endregion Get Data from TableView: SupplyOpenOrder

    #region Check if there is a record in the table based on Ids
    public int CheckForRecords(string Table, string WhereString1, int Id1, string WhereString2="", int Id2=0, string WhereString3 = "", int Id3 = 0)
    {
        int result = 0;
        string sqlText = "SELECT COUNT(*) FROM " + Table + " WHERE " + WhereString1 + " = @Id1";

        if (WhereString2!= "") { string.Concat(sqlText, " AND ", WhereString2, " = @Id2"); }
        if (WhereString3 != "") { string.Concat(sqlText, " AND ", WhereString3, " = @Id3"); }
        string.Concat(sqlText,";");

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText, con))
            {
                cmd.Parameters.Add("@Id1", MySqlDbType.Int32).Value = Id1;
                if (WhereString2 != "") { cmd.Parameters.Add("@Id2", MySqlDbType.Int32).Value = Id2; }
                if (WhereString3 != "") { cmd.Parameters.Add("@Id3", MySqlDbType.Int32).Value = Id3; }
                result = cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        return result;
    }
    #endregion Check if there is a record in the table

    #region Get Single field with multiple criteria Data from provided Table
    public string GetValueFromTable(string Table, string RetrieveField = "", string Type = "", string ConditionField1 = "", int Id1 = 0, string ConditionString1 = "", string ConditionField2 = "", int Id2 = 0, string ConditionString2 = "", string ConditionField3 = "", int Id3 = 0, string ConditionString3 = "")
    {
        // There is an Id or String available for each condition, so one of them has a value the other one is 0 or ""
        string sqlText = "SELECT * from " + Table.ToLower() + " ";
        string resultString = String.Empty;

        if (Id1 > 0 || ConditionString1 != "")
        {
            if (Id1 > 0) { sqlText = string.Concat(sqlText, " WHERE ", ConditionField1, " = @Id1"); } else { sqlText = string.Concat(sqlText, " WHERE ", ConditionField1, " = @ConditionString1"); }
        }

        if (Id2 > 0 || ConditionString2 != "")
        {
            if (Id2 > 0) { sqlText = string.Concat(sqlText, " AND ", ConditionField2, " = @Id2"); } else { sqlText = string.Concat(sqlText, " AND ", ConditionField2, " = @ConditionString2"); }
        }


        if (Id3 > 0 || ConditionString3 != "")
        {
            if (Id3 > 0) { sqlText = string.Concat(sqlText, " AND ", ConditionField3, " = @Id3"); } else { sqlText = string.Concat(sqlText, " AND ", ConditionField3, " = @ConditionString3"); }
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);
        if (Id1 > 0) { cmd.Parameters.Add("@Id1", MySqlDbType.Int32).Value = Id1; }
        if (ConditionString1 != "") { cmd.Parameters.Add("@ConditionString1", MySqlDbType.String).Value = ConditionString1; }
        if (Id2 > 0) { cmd.Parameters.Add("@Id2", MySqlDbType.Int32).Value = Id2; }
        if (ConditionString2 != "") { cmd.Parameters.Add("@ConditionString2", MySqlDbType.String).Value = ConditionString2; }
        if (Id3 > 0) { cmd.Parameters.Add("@Id3", MySqlDbType.Int32).Value = Id3; }
        if (ConditionString3 != "") { cmd.Parameters.Add("@ConditionString3", MySqlDbType.String).Value = ConditionString3; }

        switch (Type.ToLower())
        {
            case "double":
                try
                {
                    resultString = ((double)cmd.ExecuteScalar()).ToString();
                }
                catch (NullReferenceException)
                {
                    resultString = "-1";
                }
                break;

            case "float":
                resultString = ((float)cmd.ExecuteScalar()).ToString();
                break;
            case "int":
                resultString = ((int)cmd.ExecuteScalar()).ToString();
                break;
            case "string":
                resultString = (string)cmd.ExecuteScalar();
                break;
            default:
                resultString = (string)cmd.ExecuteScalar();
                break;
        }
        return resultString;
    }
    #endregion Get Single Data from provided Table

    #region Insert in Table: SupplyOrderline
    public string InsertInTable(string Table,
        string Field1 = "", string Type1 = "", string Value1 = "", 
        string Field2 = "", string Type2 = "", string Value2 = "", 
        string Field3 = "", string Type3 = "", string Value3 = "", 
        string Field4 = "", string Type4 = "", string Value4 = "", 
        string Field5 = "", string Type5 = "", string Value5 = "", 
        string Field6 = "", string Type6 = "", string Value6 = "", 
        string Field7 = "", string Type7 = "", string Value7 = "", 
        string Field8 = "", string Type8 = "", string Value8 = "", 
        string Field9 = "", string Type9 = "", string Value9 = "", 
        string Field10 = "", string Type10 = "", string Value10 = "", 
        string Field11 = "", string Type11 = "", string Value11 = "", 
        string Field12 = "", string Type12 = "", string Value12 = "", 
        string Field13 = "", string Type13 = "", string Value13 = "", 
        string Field14 = "", string Type14 = "", string Value14 = "", 
        string Field15 = "", string Type15 = "", string Value15 = "")
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO " + Table + " ";

        string sqlFields = "(" + Field1;
        string sqlValues = "(@" + Field1;

        if (Field2 != "") { sqlFields = string.Concat(sqlFields, ", ", Field2); sqlValues = string.Concat(sqlValues, ", @", Field2); }
        if (Field3 != "") { sqlFields = string.Concat(sqlFields, ", ", Field3); sqlValues = string.Concat(sqlValues, ", @", Field3); }
        if (Field4 != "") { sqlFields = string.Concat(sqlFields, ", ", Field4); sqlValues = string.Concat(sqlValues, ", @", Field4); }
        if (Field5 != "") { sqlFields = string.Concat(sqlFields, ", ", Field5); sqlValues = string.Concat(sqlValues, ", @", Field5); }
        if (Field6 != "") { sqlFields = string.Concat(sqlFields, ", ", Field6); sqlValues = string.Concat(sqlValues, ", @", Field6); }
        if (Field7 != "") { sqlFields = string.Concat(sqlFields, ", ", Field7); sqlValues = string.Concat(sqlValues, ", @", Field7); }
        if (Field8 != "") { sqlFields = string.Concat(sqlFields, ", ", Field8); sqlValues = string.Concat(sqlValues, ", @", Field8); }
        if (Field9 != "") { sqlFields = string.Concat(sqlFields, ", ", Field9); sqlValues = string.Concat(sqlValues, ", @", Field9); }
        if (Field10 != "") { sqlFields = string.Concat(sqlFields, ", ", Field10); sqlValues = string.Concat(sqlValues, ", @", Field10); }
        if (Field11 != "") { sqlFields = string.Concat(sqlFields, ", ", Field11); sqlValues = string.Concat(sqlValues, ", @", Field11); }
        if (Field12 != "") { sqlFields = string.Concat(sqlFields, ", ", Field12); sqlValues = string.Concat(sqlValues, ", @", Field12); }
        if (Field13 != "") { sqlFields = string.Concat(sqlFields, ", ", Field13); sqlValues = string.Concat(sqlValues, ", @", Field13); }
        if (Field14 != "") { sqlFields = string.Concat(sqlFields, ", ", Field14); sqlValues = string.Concat(sqlValues, ", @", Field14); }
        if (Field15 != "") { sqlFields = string.Concat(sqlFields, ", ", Field15); sqlValues = string.Concat(sqlValues, ", @", Field15); }
        
        sqlFields = sqlFields + ")";
        sqlValues = sqlValues + ")";

        sqlText = string.Concat(sqlText, sqlFields, " VALUES ", sqlValues, ";");

        try
        {
            int rowsAffected = ExecuteNonQueryTableNoCondition(sqlText, Field1, Type1, Value1, Field2, Type2, Value2, Field3, Type3, Value3, Field4, Type4, Value4, Field5, Type5, Value5, Field6, Type6, Value6, Field7, Type7, Value7, Field8, Type8, Value8, Field9, Type9, Value9, Field10, Type10, Value10, Field11, Type11, Value11, Field12, Type12, Value12, Field13, Type13, Value13, Field14, Type14, Value14, Field15, Type15, Value15);

            if (rowsAffected > 0)
            {

                result = "Rij toegevoegd.";
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (Insert in Table - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (Insert in Table): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Insert in Table

    #region Update Single Field in Table
    public string UpdateFieldInTable(string Table, string WhereField, string WhereValue, string WhereType, string Field1 = "", string Value1 = "", string Type1 = "", string Field2 = "", string Value2 = "", string Type2 = "", string Field3 = "", string Value3 = "", string Type3="")
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + Table.ToLower();

        if (Field1 != "")
        {
            sqlText = string.Concat(sqlText, " SET ", Field1, " = ", "@", Field1);
        }

        if (Field2 != "")
        {
            sqlText = string.Concat(sqlText, ", ", Field2, " = ", "@", Field2);
        }

        if (Field3 != "")
        {
            sqlText = string.Concat(sqlText, ", ", Field3, " = ", "@", Field3);
        }

        sqlText = string.Concat(sqlText, " WHERE ", WhereField, " = ", "@", WhereField, ";");

        try
        {
            int rowsAffected = ExecuteNonQueryTable(sqlText, WhereField, WhereValue, WhereType, Field1, Value1, Type1, Field2, Value2, Type2, Field3, Value3, Type3);

            if (rowsAffected > 0)
            {

                result = "Rij toegevoegd.";
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (Update Table - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (Update Table): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Update Table: SupplyOrderline

    #region Execute Non Query Table conditional
    public int ExecuteNonQueryTable(string sqlText, string WhereField, string WhereValue, string WhereType, string Field1 = "", string Value1 = "", string Type1 = "", string Field2 = "", string Value2 = "", string Type2 = "", string Field3 = "", string Value3 = "", string Type3 = "")
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText, con))
            {
                // add parameters setting string values to DBNull.Value
                // int parameters

                switch (WhereType.ToLower())
                {
                    case "string":
                        cmd.Parameters.Add("@" + WhereField, MySqlDbType.String).Value = WhereValue;
                        break;
                    case "int":
                        cmd.Parameters.Add("@" + WhereField, MySqlDbType.Int32).Value = int.Parse(WhereValue);
                        break;
                    case "double":
                        cmd.Parameters.Add("@" + WhereField, MySqlDbType.Double).Value = double.Parse(WhereValue);
                        break;
                    case "float":
                        cmd.Parameters.Add("@" + WhereField, MySqlDbType.Float).Value = float.Parse(WhereValue);
                        break;
                    case "date":
                        String[] _tempDates = WhereValue.Split("-");
                        var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                        cmd.Parameters.Add("@" + WhereField, MySqlDbType.String).Value = _tempDate;
                        break;
                }

                if (Field1 != "")
                {
                    switch (Type1.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.String).Value = Value1;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.Int32).Value = int.Parse(Value1);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.Double).Value = double.Parse(Value1);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.Float).Value = float.Parse(Value1);
                            break;
                        case "date":
                            String[] _tempDates = Value1.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field2 != "")
                {
                    switch (Type2.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.String).Value = Value2;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.Int32).Value = int.Parse(Value2);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.Double).Value = double.Parse(Value2);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.Float).Value = float.Parse(Value2);
                            break;
                        case "date":
                            String[] _tempDates = Value2.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field3 != "")
                {
                    switch (Type3.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.String).Value = Value3;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.Int32).Value = int.Parse(Value3);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.Double).Value = double.Parse(Value3);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.Float).Value = float.Parse(Value3);
                            break;
                        case "date":
                            String[] _tempDates = Value3.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table conditional

    #region Execute Non Query Table, uncunditional
    public int ExecuteNonQueryTableNoCondition(string sqlText, 
        string Field1 = "", string Type1 = "", string Value1 = "",
        string Field2 = "", string Type2 = "", string Value2 = "",
        string Field3 = "", string Type3 = "", string Value3 = "",
        string Field4 = "", string Type4 = "", string Value4 = "",
        string Field5 = "", string Type5 = "", string Value5 = "",
        string Field6 = "", string Type6 = "", string Value6 = "",
        string Field7 = "", string Type7 = "", string Value7 = "",
        string Field8 = "", string Type8 = "", string Value8 = "",
        string Field9 = "", string Type9 = "", string Value9 = "",
        string Field10 = "", string Type10 = "", string Value10 = "",
        string Field11 = "", string Type11 = "", string Value11 = "",
        string Field12 = "", string Type12 = "", string Value12 = "",
        string Field13 = "", string Type13 = "", string Value13 = "",
        string Field14 = "", string Type14 = "", string Value14 = "",
        string Field15 = "", string Type15 = "", string Value15 = "")
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText, con))
            {
                // add parameters setting string values to DBNull.Value
                // int parameters

                if (Field1 != "")
                {
                    switch (Type1.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.String).Value = Value1;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.Int32).Value = int.Parse(Value1);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.Double).Value = double.Parse(Value1);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.Float).Value = float.Parse(Value1);
                            break;
                        case "date":
                            String[] _tempDates = Value1.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field1, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field2 != "")
                {
                    switch (Type2.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.String).Value = Value2;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.Int32).Value = int.Parse(Value2);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.Double).Value = double.Parse(Value2);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.Float).Value = float.Parse(Value2);
                            break;
                        case "date":
                            String[] _tempDates = Value2.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field2, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field3 != "")
                {
                    switch (Type3.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.String).Value = Value3;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.Int32).Value = int.Parse(Value3);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.Double).Value = double.Parse(Value3);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.Float).Value = float.Parse(Value3);
                            break;
                        case "date":
                            String[] _tempDates = Value3.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field3, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field4 != "")
                {
                    switch (Type4.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field4, MySqlDbType.String).Value = Value4;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field4, MySqlDbType.Int32).Value = int.Parse(Value4);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field4, MySqlDbType.Double).Value = double.Parse(Value4);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field4, MySqlDbType.Float).Value = float.Parse(Value4);
                            break;
                        case "date":
                            String[] _tempDates = Value4.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field4, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field5 != "")
                {
                    switch (Type5.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field5, MySqlDbType.String).Value = Value5;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field5, MySqlDbType.Int32).Value = int.Parse(Value5);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field5, MySqlDbType.Double).Value = double.Parse(Value5);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field5, MySqlDbType.Float).Value = float.Parse(Value5);
                            break;
                        case "date":
                            String[] _tempDates = Value5.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field5, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field6 != "")
                {
                    switch (Type6.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field6, MySqlDbType.String).Value = Value6;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field6, MySqlDbType.Int32).Value = int.Parse(Value6);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field6, MySqlDbType.Double).Value = double.Parse(Value6);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field6, MySqlDbType.Float).Value = float.Parse(Value6);
                            break;
                        case "date":
                            String[] _tempDates = Value6.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field6, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field7 != "")
                {
                    switch (Type7.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field7, MySqlDbType.String).Value = Value7;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field7, MySqlDbType.Int32).Value = int.Parse(Value7);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field7, MySqlDbType.Double).Value = double.Parse(Value7);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field7, MySqlDbType.Float).Value = float.Parse(Value7);
                            break;
                        case "date":
                            String[] _tempDates = Value7.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field7, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field8 != "")
                {
                    switch (Type8.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field8, MySqlDbType.String).Value = Value8;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field8, MySqlDbType.Int32).Value = int.Parse(Value8);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field8, MySqlDbType.Double).Value = double.Parse(Value8);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field8, MySqlDbType.Float).Value = float.Parse(Value8);
                            break;
                        case "date":
                            String[] _tempDates = Value8.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field8, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field9 != "")
                {
                    switch (Type9.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field9, MySqlDbType.String).Value = Value9;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field9, MySqlDbType.Int32).Value = int.Parse(Value9);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field9, MySqlDbType.Double).Value = double.Parse(Value9);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field9, MySqlDbType.Float).Value = float.Parse(Value9);
                            break;
                        case "date":
                            String[] _tempDates = Value9.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field9, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field10 != "")
                {
                    switch (Type10.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field10, MySqlDbType.String).Value = Value10;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field10, MySqlDbType.Int32).Value = int.Parse(Value10);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field10, MySqlDbType.Double).Value = double.Parse(Value10);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field10, MySqlDbType.Float).Value = float.Parse(Value10);
                            break;
                        case "date":
                            String[] _tempDates = Value10.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field10, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field11 != "")
                {
                    switch (Type11.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field11, MySqlDbType.String).Value = Value11;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field11, MySqlDbType.Int32).Value = int.Parse(Value11);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field11, MySqlDbType.Double).Value = double.Parse(Value11);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field11, MySqlDbType.Float).Value = float.Parse(Value11);
                            break;
                        case "date":
                            String[] _tempDates = Value11.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field11, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field12 != "")
                {
                    switch (Type12.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field12, MySqlDbType.String).Value = Value12;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field12, MySqlDbType.Int32).Value = int.Parse(Value12);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field12, MySqlDbType.Double).Value = double.Parse(Value12);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field12, MySqlDbType.Float).Value = float.Parse(Value12);
                            break;
                        case "date":
                            String[] _tempDates = Value12.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field12, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field13 != "")
                {
                    switch (Type13.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field13, MySqlDbType.String).Value = Value13;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field13, MySqlDbType.Int32).Value = int.Parse(Value13);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field13, MySqlDbType.Double).Value = double.Parse(Value13);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field13, MySqlDbType.Float).Value = float.Parse(Value13);
                            break;
                        case "date":
                            String[] _tempDates = Value13.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field13, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field14 != "")
                {
                    switch (Type14.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field14, MySqlDbType.String).Value = Value14;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field14, MySqlDbType.Int32).Value = int.Parse(Value14);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field14, MySqlDbType.Double).Value = double.Parse(Value14);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field14, MySqlDbType.Float).Value = float.Parse(Value14);
                            break;
                        case "date":
                            String[] _tempDates = Value14.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field14, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                if (Field15 != "")
                {
                    switch (Type15.ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + Field15, MySqlDbType.String).Value = Value15;
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + Field15, MySqlDbType.Int32).Value = int.Parse(Value15);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + Field15, MySqlDbType.Double).Value = double.Parse(Value15);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + Field15, MySqlDbType.Float).Value = float.Parse(Value15);
                            break;
                        case "date":
                            String[] _tempDates = Value15.Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + Field15, MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }
        return rowsAffected;
    }
    #endregion Execute Non Query Table unconditional

}
