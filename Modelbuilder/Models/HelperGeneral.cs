
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Modelbuilder;
internal class HelperGeneral
{
    #region public Variables
    public string ConnectionStr { get; set; }

    public static string DbBrandTable = "brand";

    public static string DbCategoryTable = "category";

    public static string DbContactTypeTable = "contacttype";

    public static string DbCurrencyTable = "currency";

    public static string DbCountryTable = "country";

    public static string DbProductTable = "product";
    public static string DbProductView = "view_product";
    public static string DbProductTableFieldNameId = "Id";
    public static string DbProductTableFieldTypeId = "int";


    public static string DbProductSupplierTable = "productsupplier";
    public static string DbProductSupplierView = "view_productsupplier";
    public static string DbProductSupplierTableFieldNameId = "Id";
    public static string DbProductSupplierTableFieldTypeId = "int";
    public static string DbProductSupplierTableFieldNameProductId = "Product_Id";
    public static string DbProductSupplierTableFieldTypeProductId = "int";
    public static string DbProjectTable = "project";

    public static string DbStockTable = "stock";
    public static string DbStockView = "view_stock";
    public static string DbStockTableFieldNameId = "Id";
    public static string DbStockTableFieldTypeId = "int";
    public static string DbStockTableFieldNameProductId = "product_Id";
    public static string DbStockTableFieldTypeProductId = "int";
    public static string DbStockTableFieldNameStorageId = "storage_Id";
    public static string DbStockTableFieldTypeStorageId = "int";
    public static string DbStockTableFieldNameAmount = "Amount";
    public static string DbStockTableFieldTypeAmount = "double";
    public static string DbStockViewFieldNameId = "Id";
    public static string DbStockViewFieldTypeId = "int";
    public static string DbStockViewFieldNameProductId = "product_Id";
    public static string DbStockViewFieldTypeProductId = "int";
    public static string DbStockViewFieldNameStorageId = "storage_Id";
    public static string DbStockViewFieldTypeStorageId = "Int";

    public static string DbStocklogTable = "stocklog";
    public static string DbStocklogTableFieldNameProductId = "product_Id";
    public static string DbStocklogTableFieldTypeProductId = "int";
    public static string DbStocklogTableFieldNameStorageId = "storage_Id";
    public static string DbStocklogTableFieldTypeStorageId = "int";
    public static string DbStocklogTableFieldNameSupplyOrderId = "supplyorder_Id";
    public static string DbStocklogTableFieldTypeSupplyOrderId = "int";
    public static string DbStocklogTableFieldNameSupplyOrderlineId = "supplyorderline_Id";
    public static string DbStocklogTableFieldTypeSupplyOrderlineId = "int";
    public static string DbStocklogTableFieldNameAmountReceived = "AmountReceived";
    public static string DbStocklogTableFieldTypeAmountReceived = "double";
    public static string DbStocklogTableFieldNameDate = "Date";
    public static string DbStocklogTableFieldTypeDate = "date";

    public static string DbStorageTable = "storage";

    public static string DbSupplierTable = "supplier";
    public static string DbSupplierView = "view_supplier";

    public static string DbSupplierContactTable = "suppliercontact";
    public static string DbSupplierContactView = "view_suppliercontact";

    #region Time
    public static string DbTimeTable = "time";
    public static string DbTimeTableFieldNameId = "Id";
    public static string DbTimeTableFieldTypeId = "int";
    public static string DbTimeTableFieldNameProjectId = "project_Id";
    public static string DbTimeTableFieldTypeProjectId = "int";
    public static string DbTimeTableFieldNameWorktypeId = "worktype_Id";
    public static string DbTimeTableFieldTypeWorktypeId = "int";
    public static string DbTimeTableFieldNameDate = "Date";
    public static string DbTimeTableFieldTypeDate = "date";
    public static string DbTimeTableFieldNameStartTime = "StartTime";
    public static string DbTimeTableFieldTypeStartTime = "time";
    public static string DbTimeTableFieldNameEndTime = "EndTime";
    public static string DbTimeTableFieldTypeEndTime = "time";
    public static string DbTimeTableFieldNameComment = "Comment";
    public static string DbTimeTableFieldTypeComment = "string";

    public static string DbTimeView = "view_time";
    public static string DbTimeViewFieldNameId = "Id";
    public static string DbTimeViewFieldTypeId = "int";
    public static string DbTimeViewFieldNameProjectId = "ProjectId";
    public static string DbTimeViewFieldTypeProjectId = "int";
    public static string DbTimeViewFieldNameWorktypeId = "WorktypeId";
    public static string DbTimeViewFieldTypeWorktypeId = "int";
    public static string DbTimeViewFieldNameProjectName = "ProjectName";
    public static string DbTimeViewFieldTypeProjectName = "string";
    public static string DbTimeViewFieldNameWorktypeName = "WorktypeName";
    public static string DbTimeViewFieldTypeWorktypeName = "string";
    public static string DbTimeViewFieldNameDate = "Date";
    public static string DbTimeViewFieldTypeDate = "date";
    public static string DbTimeViewFieldNameStartTime = "StartTime";
    public static string DbTimeViewFieldTypeStartTime = "time";
    public static string DbTimeViewFieldNameEndTime = "EndTime";
    public static string DbTimeViewFieldTypeEndTime = "time";
    #endregion Time

    #region ProductUsage
    public static string DbProductUsageTable = "productusage";
    public static string DbProductUsageTableFieldNameId = "Id";
    public static string DbProductUsageTableFieldTypeId = "int";
    public static string DbProductUsageTableFieldNameProjectId = "project_Id";
    public static string DbProductUsageTableFieldTypeProjectId = "int";
    public static string DbProductUsageTableFieldNameProductId = "product_Id";
    public static string DbProductUsageTableFieldTypeProductId = "int";
    public static string DbProductUsageTableFieldNameAmountReceived = "AmountUsed";
    public static string DbProductUsageTableFieldTypeAmountReceived = "double";
    public static string DbProductUsageTableFieldNameDate = "Date";
    public static string DbProductUsageTableFieldTypeDate = "date";

    public static string DbProductUsageView = "viewproductusage";
    public static string DbProductUsageViewFieldNameId = "Id";
    public static string DbProductUsageViewFieldTypeId = "int";
    public static string DbProductUsageViewFieldNameProjectId = "ProjectId";
    public static string DbProductUsageViewFieldTypeProjectId = "int";
    public static string DbProductUsageViewFieldNameProductId = "ProductId";
    public static string DbProductUsageViewFieldTypeProductId = "int";
    public static string DbProductUsageViewFieldNameStorageId = "StorageId";
    public static string DbProductUsageViewFieldTypeStorageId = "int";
    public static string DbProductUsageViewFieldNameCategoryId = "CategoryId";
    public static string DbProductUsageViewFieldTypeCategoryId = "int";
    public static string DbProductUsageViewFieldNameProjectName = "ProjectName";
    public static string DbProductUsageViewFieldTypeProjectName = "string";
    public static string DbProductUsageViewFieldNameProductName = "ProductName";
    public static string DbProductUsageViewFieldTypeProductName = "string";
    public static string DbProductUsageViewFieldNameStorageName = "StorageName";
    public static string DbProductUsageViewFieldTypeStorageName = "string";
    public static string DbProductUsageViewFieldNameCategoryName = "CategoryName";
    public static string DbProductUsageViewFieldTypeCategoryName = "string";
    public static string DbProductUsageViewFieldNameAmountReceived = "AmountUsed";
    public static string DbProductUsageViewFieldTypeAmountReceived = "double";
    public static string DbProductUsageViewFieldNameDate = "Date";
    public static string DbProductUsageViewFieldTypeDate = "date";
    #endregion ProductUsage

    public static string DbOrderTable = "supplyorder";
    public static string DbOrderView = "view_supplyorder";
    public static string DbOpenOrderView = "view_supplyopenorder";
    public static string DbOrderTableFieldNameId = "Id";
    public static string DbOrderTableFieldTypeId = "int";
    public static string DbOrderTableFieldNameClosed = "Closed";
    public static string DbOrderTableFieldTypeClosed = "int";
    public static string DbOrderTableFieldNameClosedDate = "ClosedDate";
    public static string DbOrderTableFieldTypeClosedDate = "date";

    public static string DbOrderLineTable = "supplyorderline";
    public static string DbOrderLineView = "view_supplyorderline";
    public static string DbOpenOrderLineView = "view_supplyopenorderline";
    public static string DbOrderLineFieldNameId = "Id";
    public static string DbOrderLineFieldTypeId = "int";
    public static string DbOrderLineFieldNameOrderId = "supplyorder_Id";
    public static string DbOrderLineFieldTypeOrderId = "int";
    public static string DbOrderLineFieldNameAmount = "Amount";
    public static string DbOrderLineFieldTypeAmount = "double";
    public static string DbOrderLineFieldNameOpenAmount = "OpenAmount";
    public static string DbOrderLineFieldTypeOpenAmount = "double";
    public static string DbOrderLineFieldNameClosed = "Closed";
    public static string DbOrderLineFieldTypeClosed = "int";
    public static string DbOrderLineFieldNameClosedDate = "ClosedDate";
    public static string DbOrderLineFieldTypeClosedDate = "date";


    public static string DbOpenOrderLineFieldNameSupplyOrderId = "Supplyorder_Id";
    public static string DbOpenOrderLineFieldTypeSupplyOrderId = "int";

    public static string DbUnitTable = "unit";

    public static string DbWorktypeTable = "worktype";


    public CultureInfo Culture = new("nl-NL");

    public string SqlOrderByString { get; set; }
    public string SqlSelectionString { get; set; }
    public string SqlWhereString { get; set; }
    public string TableName { get; set; }
    #endregion public Variables

    #region Connector to database
    public HelperGeneral(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperGeneral(string serverName, int portNumber, string databaseName, string username, string userPwd)
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
            
            if (Id > 0) { cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id; }

            using MySqlDataAdapter da = new(cmd);
            
            da.Fill(dt);
        }
        return dt;
    }
    public DataTable GetData ( string Table, string[,] WhereFields )
    {
        DataTable dt = new ();

        string sqlText = "SELECT * FROM " + Table.ToLower() + " WHERE ";
        string prefix = "";

        if (WhereFields.GetLength ( 0 ) > 0)
        {
            for (int i = 0; i < WhereFields.GetLength ( 0 ); i++)
            {
                if (i != 0) { prefix = " AND "; }
                sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
            }
        }

        MySqlConnection con = new MySqlConnection ( ConnectionStr );

        con.Open ();

        MySqlCommand cmd = new MySqlCommand ( sqlText, con );

        for (int i = 0; i < WhereFields.GetLength ( 0 ); i++)
        {
            switch (WhereFields[i, 1].ToLower ())
            {
                case "string":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.String ).Value = WhereFields[i, 2];
                    break;
                case "int":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.Int32 ).Value = int.Parse ( WhereFields[i, 2] );
                    break;
                case "double":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.Double ).Value = double.Parse ( WhereFields[i, 2] );
                    break;
                case "float":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.Float ).Value = float.Parse ( WhereFields[i, 2] );
                    break;
                case "time":
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.String ).Value = WhereFields[i, 2];
                    break;
                case "date":
                    String[] _tempDates = WhereFields[i, 2].Split ( "-" );
                    var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                    cmd.Parameters.Add ( "@" + WhereFields[i, 0], MySqlDbType.String ).Value = _tempDate;
                    break;
            }
        }

        using MySqlDataAdapter da = new ( cmd );
        da.Fill ( dt );

        return dt;
    }
    #endregion Get Data from TableView or Table

    #region Get Field(s) from table
    public string GetValueFromTable(string Table, string[,] WhereFields, string[,] Fields)
    {
        // There is an Id or String available for each condition, so one of them has a value the other one is 0 or ""
        string sqlText = "SELECT ";
        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText = sqlText + prefix + Fields[i, 0];
        }

        sqlText = sqlText + " FROM " + Table.ToLower() + " ";
        prefix = "";

        if(WhereFields.GetLength(0) > 0)
        {
            sqlText += " WHERE ";

            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                if (i != 0) { prefix = " AND "; }
                sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
            }
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);

        for (int i = 0; i < WhereFields.GetLength(0); i++)
        {
            switch (WhereFields[i, 1].ToLower())
            {
                case "string":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = WhereFields[i, 2];
                    break;
                case "int":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Int32).Value = int.Parse(WhereFields[i, 2]);
                    break;
                case "double":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Double).Value = double.Parse(WhereFields[i, 2]);
                    break;
                case "float":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Float).Value = float.Parse(WhereFields[i, 2]);
                    break;
                case "date":
                    String[] _tempDates = WhereFields[i, 2].Split("-");
                    var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                    cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = _tempDate;
                    break;
            }
        }
        string resultString="";
        int resultInt;
        double resultDouble;
        float resultFloat;

        if (Fields[0, 1].ToLower() == "string" || Fields[0, 1].ToLower() == "date" || Fields[0, 1].ToLower() == "time") { resultString = (string)cmd.ExecuteScalar(); };
        if (Fields[0, 1].ToLower() == "int") { resultInt = (int)cmd.ExecuteScalar(); resultString = resultInt.ToString(); };
        if (Fields[0, 1].ToLower() == "double") { resultDouble = (double)cmd.ExecuteScalar(); resultString = resultDouble.ToString(); };
        if (Fields[0, 1].ToLower() == "float") { resultFloat = (float)cmd.ExecuteScalar(); resultString = resultFloat.ToString(); };
        //string resultString = (string)cmd.ExecuteScalar();

        return resultString;
    }
    #endregion Get Field(s) from table

    #region Get Max value for Field from table
    public string GetMaxValueFromTable(string Table, string[,] WhereFields, string[,] Fields)
    {
        // There is an Id or String available for each condition, so one of them has a value the other one is 0 or ""
        string sqlText = "SELECT ";
        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText = sqlText + prefix + "MAX(" + Fields[i, 0] + ")";
        }

        sqlText = sqlText + " FROM " + Table.ToLower() + " ";
        prefix = "";

        if (WhereFields.GetLength(0) > 0)
        {
            sqlText += " WHERE ";

            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                if (i != 0) { prefix = " AND "; }
                sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
            }
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);

        for (int i = 0; i < WhereFields.GetLength(0); i++)
        {
            switch (WhereFields[i, 1].ToLower())
            {
                case "string":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = WhereFields[i, 2];
                    break;
                case "int":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Int32).Value = int.Parse(WhereFields[i, 2]);
                    break;
                case "double":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Double).Value = double.Parse(WhereFields[i, 2]);
                    break;
                case "float":
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Float).Value = float.Parse(WhereFields[i, 2]);
                    break;
                case "date":
                    String[] _tempDates = WhereFields[i, 2].Split("-");
                    var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                    cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = _tempDate;
                    break;
            }
        }
        string resultString = "";
        int resultInt;
        double resultDouble;
        float resultFloat;
        DateTime resultDate;

        if (Fields[0, 1].ToLower() == "string") { resultString = (string)cmd.ExecuteScalar(); };
        if (Fields[0, 1].ToLower() == "int") { resultInt = (int)cmd.ExecuteScalar(); resultString = resultInt.ToString(); };
        if (Fields[0, 1].ToLower() == "double") { resultDouble = (double)cmd.ExecuteScalar(); resultString = resultDouble.ToString(); };
        if (Fields[0, 1].ToLower() == "float") { resultFloat = (float)cmd.ExecuteScalar(); resultString = resultFloat.ToString(); };
        if (Fields[0, 1].ToLower() == "date") { resultDate = (DateTime)cmd.ExecuteScalar(); resultString = resultDate.ToShortDateString(); };

        return resultString;
    }
    #endregion Get Max value for Field from table

    #region Get Latest added Id from table
    public string GetLatestIdFromTable(string Table)
    {
        // There is an Id or String available for each condition, so one of them has a value the other one is 0 or ""
        string sqlText = "SELECT MAX(Id) FROM " + Table.ToLower();

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText, con);

        string resultString = ((int)cmd.ExecuteScalar()).ToString();

        //resultString = ((int)cmd.ExecuteScalar()).ToString();

        return resultString;
    }
    #endregion Get Latest added Id from table

    #region Check if there is a record in the table based (returns no of records)
    public int CheckForRecords(string Table, string[,] WhereFields)
    {
        int result = 0;
        string sqlText = "SELECT COUNT(*) FROM " + Table + " WHERE ";
        string prefix = "";

        for (int i = 0; i < WhereFields.GetLength(0); i++)
        {
            if (i != 0) { prefix = " AND "; }
            sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
        }

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText, con))
            {

                for (int i = 0; i < WhereFields.GetLength(0); i++)
                {
                    if (i != 0) { prefix = " AND "; }
                    sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
                }


                for (int i = 0; i < WhereFields.GetLength(0); i++)
                {
                    switch (WhereFields[i, 1].ToLower())
                    {
                        case "string":
                            cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = WhereFields[i, 2];
                            break;
                        case "int":
                            cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Int32).Value = int.Parse(WhereFields[i, 2]);
                            break;
                        case "double":
                            cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Double).Value = double.Parse(WhereFields[i, 2]);
                            break;
                        case "float":
                            cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Float).Value = float.Parse(WhereFields[i, 2]);
                            break;
                        case "date":
                            String[] _tempDates = WhereFields[i, 2].Split("-");
                            var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                            cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = _tempDate;
                            break;
                    }
                }
                result = (int)(long)cmd.ExecuteScalar();
            }
            con.Close();
        }
        return result;
    }
    #endregion Check if there is a record in the table based (returns no of records)

    #region Insert new record in Table
    public string InsertInTable(string Table, string[,] Fields)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO " + Table + " ";

        string sqlFields = "(";
        string sqlValues = "(";
        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlFields = string.Concat(sqlFields, prefix, Fields[i, 0]);
            sqlValues = string.Concat(sqlValues, prefix, "@", Fields[i, 0]);
        }

        sqlFields += ")";
        sqlValues += ")";

        sqlText = string.Concat(sqlText, sqlFields, " VALUES ", sqlValues, ";");

        try
        {
            int rowsAffected = ExecuteNonQueryTable(sqlText, Fields);

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
    #endregion Insert new record in Table

    #region Update Field(s) in Table
    public string UpdateFieldInTable(string Table, string[,] WhereFields, string[,] Fields)
    {
        string result = string.Empty;
        string sqlText = "UPDATE " + Table.ToLower() + " SET ";
        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText = sqlText + prefix + Fields[i, 0] + " = @" + Fields[i, 0] ;
        }

        sqlText = string.Concat(sqlText, " WHERE ");
        prefix = "";

        for (int i = 0; i < WhereFields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
        }

        try
        {
            int rowsAffected = ExecuteNonQueryTable(sqlText, WhereFields, Fields);

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

    #region Delete record from Table
    public string DeleteRecordFromTable(string Table, string[,] WhereFields)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM " + Table.ToLower() + " WHERE ";

        string prefix = "";

        if (WhereFields.GetLength(0) > 0)
        {
            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                if (i != 0) { prefix = " AND "; }
                sqlText = sqlText + prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0];
            }
        }

        try
        {
            int rowsAffected = ExecuteNonQueryTable(sqlText, WhereFields);

            if (rowsAffected > 0)
            {

                result = "Rij verwijderd.";
            }
            else
            {
                result = "Rij niet verwiijdersd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (Delete from Table - MySqlException): " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (Delete from Table): " + ex.Message);
            throw;
        }
        return result;
    }
    #endregion Delete record from Table

    #region Execute Non Query Table
    public int ExecuteNonQueryTable(string sqlText, string[,] Fields)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            for (int i = 0; i < Fields.GetLength(0); i++)
            {
                switch (Fields[i, 1].ToLower())
                {
                    case "string":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = Fields[i, 2];
                        break;
                    case "int":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.Int32).Value = int.Parse(Fields[i, 2]);
                        break;
                    case "double":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.Double).Value = double.Parse(Fields[i, 2]);
                        break;
                    case "float":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.Float).Value = float.Parse(Fields[i, 2]);
                        break;
                    case "date":
                        String[] _tempDates = Fields[i, 2].Split("-");
                        var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = _tempDate;
                        break;
                    case "time":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = Fields[i, 2];
                        break;
                }
            }
            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }

    public int ExecuteNonQueryTable(string sqlText, string[,] WhereFields, string[,] Fields)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                switch (WhereFields[i, 1].ToLower())
                {
                    case "string":
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = WhereFields[i, 2];
                        break;
                    case "int":
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Int32).Value = int.Parse(WhereFields[i, 2]);
                        break;
                    case "double":
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Double).Value = double.Parse(WhereFields[i, 2]);
                        break;
                    case "float":
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.Float).Value = float.Parse(WhereFields[i, 2]);
                        break;
                    case "date":
                        String[] _tempDates = WhereFields[i, 2].Split("-");
                        var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = _tempDate;
                        break;
                }
            }

            for (int i = 0; i < Fields.GetLength(0); i++)
            {
                switch (Fields[i, 1].ToLower())
                {
                    case "string":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = Fields[i, 2];
                        break;
                    case "int":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.Int32).Value = int.Parse(Fields[i, 2]);
                        break;
                    case "double":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.Double).Value = double.Parse(Fields[i, 2]);
                        break;
                    case "float":
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.Float).Value = float.Parse(Fields[i, 2]);
                        break;
                    case "date":
                        String[] _tempDates = Fields[i, 2].Split("-");
                        var _tempDate = _tempDates[2] + "-" + _tempDates[1] + "-" + _tempDates[0];
                        cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = _tempDate;
                        break;
                }
            }
            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    
    }
    #endregion Execute Non Query Table

    #region Create lists to populate dropdowns for order Page
    #region Fill the dropdownlists
    #region Fill Brand dropdown
    public List<Brand> GetBrandList(List<Brand> brandList)
    {
        string DatabaseTable = DbBrandTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Name";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            brandList.Add(new Brand
            {
                BrandName = dtSelection.Rows[i][0].ToString(),
                BrandId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return brandList;
    }
    #endregion Brand dropdown

    #region Fill Category dropdown
    public List<Category> GetCategoryList(List<Category> categoryList)
    {

        string DatabaseTable = DbCategoryTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DbCategoryTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            categoryList.Add(new Category
            {
                CategoryName = dtSelection.Rows[i][0].ToString(),
                CategoryId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });

        }
        return categoryList;
    }
    #endregion

    #region Fill ContactType dropdown
    public List<ContactType> GetContactTypeList(List<ContactType> contactTypeList)
    {
        string DatabaseTable = DbContactTypeTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Name";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            contactTypeList.Add(new ContactType
            {
                ContactTypeName = dtSelection.Rows[i][0].ToString(),
                ContactTypeId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture),
            });
        }
        return contactTypeList;
    }
    #endregion

    #region Fill Country dropdown
    public List<Country> GetCountryList(List<Country> countryList)
    {

        string DatabaseTable = DbCountryTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            countryList.Add(new Country
            {
                CountryName = dtSelection.Rows[i][0].ToString(),
                CountryId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return countryList;
    }
    #endregion

    #region Fill Currency dropdown
    public List<Currency> GetCurrencyList(List<Currency> currencyList)
    {
        string DatabaseTable = DbCurrencyTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Symbol, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            currencyList.Add(new Currency
            {
                CurrencySymbol = dtSelection.Rows[i][0].ToString(),
                CurrencyId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return currencyList;
    }
    # endregion Currency dropdown

    #region Fill Project dropdown
    public List<Project> GetProjectList(List<Project> projectList)
    {
        string DatabaseTable = DbProjectTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            projectList.Add(new Project
            {
                ProjectName = dtSelection.Rows[i][0].ToString(),
                ProjectId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return projectList;
    }
    #endregion Fill Project dropdown

    #region Fill Product dropdown
    public List<Product> GetProductList(List<Product> productList)
    {
        string DatabaseTable = DbProductTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        // To add the default line "Select a product"to the dropdownlist at the first position, there has to be an extra row added, so Rows.count + 1,
        // but for the real products this +1 has to be substraced again otherwise it points to the wrong row
        for (int i = 0; i < dtSelection.Rows.Count + 1; i++)
        {
            if (i == 0)
            {
                productList.Add(new Product { ProductName = "Selecteer een product", ProductId = 0 });
            }
            else
            {
                productList.Add(new Product
                {
                    ProductName = dtSelection.Rows[i - 1][0].ToString(),
                    ProductId = int.Parse(dtSelection.Rows[i - 1][1].ToString(), Culture)
                });
            }
        }
        return productList;
    }
    #endregion Fill Project dropdown

    #region Fill Storage dropdown
    public List<Storage> GetStorageList(List<Storage> storageList)
    {

        string DatabaseTable = DbStorageTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DbStorageTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            storageList.Add(new Storage
            {
                StorageName = dtSelection.Rows[i][0].ToString(),
                StorageId = int.Parse(dtSelection.Rows[i][1].ToString())
                });
        }
        return storageList;
    }
    #endregion Fill Storage dropdown

    #region Fill Supplier dropdown
    public List<Supplier> GetSupplierList(List<Supplier> supplierList)
    {
        string DatabaseTable = DbSupplierView;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id, Currency, Currency_Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            supplierList.Add(new Supplier
            {
                SupplierName = dtSelection.Rows[i][0].ToString(),
                SupplierId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture),
                SupplierCurrencySymbol = dtSelection.Rows[i][2].ToString(),
                SupplierCurrencyId = int.Parse(dtSelection.Rows[i][3].ToString(), Culture)
            });
        }
        return supplierList;
    }
    #endregion Fill Supplier dropdown

    #region Fill Unit dropdown
    public List<Unit> GetUnitList(List<Unit> unitList)
    {
        string DatabaseTable = DbUnitTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id";
        dbConnection.SqlOrderByString = "Id";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            unitList.Add(new Unit
            {
                UnitName = dtSelection.Rows[i][0].ToString(),
                UnitId = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return unitList;
    }
    #endregion

    #region Fill Worktype dropdown
    public List<Worktype> GetWorktypeList ( List<Worktype> worktypeList )
    {
        string DatabaseTable = DbWorktypeTable;
        Database dbConnection = new ()
        {
            TableName = DatabaseTable
        };

        dbConnection.SqlSelectionString = "Name, Id, ParentId, FullPath";
        dbConnection.SqlOrderByString = "Fullpath";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData ();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            if(dtSelection.Rows[i][2] == DBNull.Value) 
            {
                worktypeList.Add ( new Worktype
                {
                    WorktypeName = dtSelection.Rows[i][0].ToString (),
                    WorktypeId = int.Parse ( dtSelection.Rows[i][1].ToString (), Culture ),
                    WorktypeParentId = 0
                } );
            }
            else 
            {
                // In the Fullpath the dept of the item is visible by the number of \ in the fullpath
                //string _tempString = dtSelection.Rows[i][3].ToString ();
                //char _tempChar = '\\';
                var freq = dtSelection.Rows[i][3].ToString ().Count ( f => (f == '\\') );
                string _spacer = new string ( ' ', freq * 3 );
                //int freq = _tempString.Count(f => (f == _tempChar));

                worktypeList.Add ( new Worktype
                {
                    WorktypeSpacer = _spacer,
                    WorktypeName = dtSelection.Rows[i][0].ToString (),
                    WorktypeId = int.Parse ( dtSelection.Rows[i][1].ToString (), Culture ),
                    WorktypeParentId = int.Parse ( dtSelection.Rows[i][2].ToString (), Culture )
                } ); ;
            }

        }
        return worktypeList;
    }
    #endregion
    #endregion Fill the dropdownlists

    #region Helper classes to for creating objects to populate dropdowns
    #region Create object for all brands in table for dropdown
    public class Brand
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
    }
    #endregion Create object for all brands in table for dropdown

    #region Create object for all categories in table for dropdown
    public class Category
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
    #endregion

    #region Create object for all contacttypes in table for dropdown
    public class ContactType
    {
        public string ContactTypeName { get; set; }
        public int ContactTypeId { get; set; }
    }
    #endregion

    #region Create object for all countries in table for dropdown
    public class Country
    {
        public string CountryName { get; set; }
        public int CountryId { get; set; }
    }
    #endregion

    #region Create object for all currencies in table for dropdown
    public class Currency
    {
        public string CurrencySymbol { get; set; }
        public int CurrencyId { get; set; }
    }
    #endregion Create object for all currencies in table for dropdown

    #region Create object for all projects in table for dropdown
    public class Project
    {
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
    }
    #endregion Create object for all projects in table for dropdown

    #region Create object for all products in table for dropdown
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
    #endregion Create object for all products in table for dropdown    

    #region Create object for all storage locations in table for dropdown
    public class Storage
    {
        public string StorageName { get; set; }
        public int StorageId { get; set; }
    }
    #endregion Create object for all storage locations in table for dropdown

    #region Create object for all suppliers in table for dropdown
    public class Supplier
    {
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCurrencySymbol { get; set; }
        public int SupplierCurrencyId { get; set; }
    }
    #endregion Create object for all suppliers in table for dropdown

    #region Create object for all units in table for dropdown
    public class Unit
    {
        public string UnitName { get; set; }
        public int UnitId { get; set; }
    }
    #endregion

    #region Create object for all worktypes in table for dropdown
    public class Worktype
    {
        public string WorktypeName { get; set; }
        public string WorktypeSpacer { get; set; }
        public string WorktypeFullpath { get; set; }
        public int WorktypeId { get; set; }
        public int WorktypeParentId { get; set; }
    }
    #endregion
    #endregion Helper classes to for creating objects to populate dropdowns
    #endregion Create lists to populate dropdowns for order Page

    #region Get value from datagrid cell
    public DataGridCell GetCell(DataGrid datagrid, int row, int column)
    {

        DataGridRow rowContainer = GetRow(datagrid, row);
        if (rowContainer != null)
        {
            DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

            if (presenter == null)
            {
                datagrid.ScrollIntoView(rowContainer, datagrid.Columns[column]);
                presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
            }
            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);

            return cell;
        }
        return null;
    }

    public DataGridRow GetRow(DataGrid datagrid, int index)
    {
        DataGridRow row = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(index);

        if (row == null)
        {
            datagrid.UpdateLayout();
            datagrid.ScrollIntoView(datagrid.Items[index]);
            row = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(index);
        }
        return row;
    }


    public static T GetVisualChild<T>(Visual parent) where T : Visual
    {
        T child = default(T);

        int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

        for (int i = 0; i < numVisuals; i++)
        {
            Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
            child = v as T;
            if (child == null)
            {
                child = GetVisualChild<T>
                (v);
            }

            if (child != null)
            {
                break;
            }
        }
        return child;
    }
    #endregion Get value from datagrid cell
}

