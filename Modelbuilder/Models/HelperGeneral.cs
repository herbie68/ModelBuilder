
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Modelbuilder;
internal class HelperGeneral
{
    #region public Variables
    public string ConnectionStr { get; set; }

    public static readonly string DbBrandTable = "brand";

    public static readonly string DbCategoryTable = "category";

    public static readonly string DbContactTypeTable = "contacttype";

    public static readonly string DbCurrencyTable = "currency";

    public static readonly string DbCountryTable = "country";

    public static readonly string DbProductTable = "product";
    public static readonly string DbProductView = "view_product";
    public static readonly string DbProductTableFieldNameId = "Id";
    public static readonly string DbProductTableFieldTypeId = "int";
    public static readonly string DbProductTableFieldNameStorageId = "Storage_Id";
    public static readonly string DbProductTableFieldTypeStorageId = "int";


    public static readonly string DbProductSupplierTable = "productsupplier";
    public static readonly string DbProductSupplierView = "view_productsupplier";
    public static readonly string DbProductSupplierTableFieldNameId = "Id";
    public static readonly string DbProductSupplierTableFieldTypeId = "int";
    public static readonly string DbProductSupplierTableFieldNameProductId = "Product_Id";
    public static readonly string DbProductSupplierTableFieldTypeProductId = "int";
    
    public static readonly string DbProjectTable = "project";
    public static readonly string DbProjectTableFieldNameId = "Id";
    public static readonly string DbProjectTableFieldTypeId = "int";
    public static readonly string DbProjectTableFieldNameName = "Name";
    public static readonly string DbProjectTableFieldTypeName = "string";
    public static readonly string DbProjectTableFieldNameCode = "Code";
    public static readonly string DbProjectTableFieldTypeCode = "string";

    public static readonly string DbStockTable = "stock";
    public static readonly string DbStockView = "view_stock";
    public static readonly string DbStockTableFieldNameId = "Id";
    public static readonly string DbStockTableFieldTypeId = "int";
    public static readonly string DbStockTableFieldNameProductId = "product_Id";
    public static readonly string DbStockTableFieldTypeProductId = "int";
    public static readonly string DbStockTableFieldNameStorageId = "storage_Id";
    public static readonly string DbStockTableFieldTypeStorageId = "int";
    public static readonly string DbStockTableFieldNameAmount = "Amount";
    public static readonly string DbStockTableFieldTypeAmount = "double";
    public static readonly string DbStockViewFieldNameId = "Id";
    public static readonly string DbStockViewFieldTypeId = "int";
    public static readonly string DbStockViewFieldNameProductId = "product_Id";
    public static readonly string DbStockViewFieldTypeProductId = "int";
    public static readonly string DbStockViewFieldNameStorageId = "storage_Id";
    public static readonly string DbStockViewFieldTypeStorageId = "Int";

    public static readonly string DbStocklogTable = "stocklog";
    public static readonly string DbStocklogTableFieldNameProductId = "product_Id";
    public static readonly string DbStocklogTableFieldTypeProductId = "int";
    public static readonly string DbStocklogTableFieldNameStorageId = "storage_Id";
    public static readonly string DbStocklogTableFieldTypeStorageId = "int";
    public static readonly string DbStocklogTableFieldNameSupplyOrderId = "supplyorder_Id";
    public static readonly string DbStocklogTableFieldTypeSupplyOrderId = "int";
    public static readonly string DbStocklogTableFieldNameProductUsageId = "productusage_Id";
    public static readonly string DbStocklogTableFieldTypeProductUsageId = "int";
    public static readonly string DbStocklogTableFieldNameSupplyOrderlineId = "supplyorderline_Id";
    public static readonly string DbStocklogTableFieldTypeSupplyOrderlineId = "int";
    public static readonly string DbStocklogTableFieldNameAmountReceived = "AmountReceived";
    public static readonly string DbStocklogTableFieldTypeAmountReceived = "double";
    public static readonly string DbStocklogTableFieldNameAmountUsed = "AmountUsed";
    public static readonly string DbStocklogTableFieldTypeAmountUsed = "double";
    public static readonly string DbStocklogTableFieldNameDate = "Date";
    public static readonly string DbStocklogTableFieldTypeDate = "date";

    public static readonly string DbStorageTable = "storage";

    public static readonly string DbSupplierTable = "supplier";
    public static readonly string DbSupplierView = "view_supplier";

    public static readonly string DbSupplierContactTable = "suppliercontact";
    public static readonly string DbSupplierContactView = "view_suppliercontact";

    #region Time
    public static readonly string DbTimeTable = "time";
    public static readonly string DbTimeTableFieldNameId = "Id";
    public static readonly string DbTimeTableFieldTypeId = "int";
    public static readonly string DbTimeTableFieldNameProjectId = "project_Id";
    public static readonly string DbTimeTableFieldTypeProjectId = "int";
    public static readonly string DbTimeTableFieldNameWorktypeId = "worktype_Id";
    public static readonly string DbTimeTableFieldTypeWorktypeId = "int";
    public static readonly string DbTimeTableFieldNameWorkDate = "WorkDate";
    public static readonly string DbTimeTableFieldTypeWorkDate = "date";
    public static readonly string DbTimeTableFieldNameStartTime = "StartTime";
    public static readonly string DbTimeTableFieldTypeStartTime = "time";
    public static readonly string DbTimeTableFieldNameEndTime = "EndTime";
    public static readonly string DbTimeTableFieldTypeEndTime = "time";
    public static readonly string DbTimeTableFieldNameComment = "Comment";
    public static readonly string DbTimeTableFieldTypeComment = "string";

    public static readonly string DbTimeView = "view_time";
    public static readonly string DbTimeViewFieldNameId = "Id";
    public static readonly string DbTimeViewFieldTypeId = "int";
    public static readonly string DbTimeViewFieldNameProjectId = "ProjectId";
    public static readonly string DbTimeViewFieldTypeProjectId = "int";
    public static readonly string DbTimeViewFieldNameWorktypeId = "WorktypeId";
    public static readonly string DbTimeViewFieldTypeWorktypeId = "int";
    public static readonly string DbTimeViewFieldNameProjectName = "ProjectName";
    public static readonly string DbTimeViewFieldTypeProjectName = "string";
    public static readonly string DbTimeViewFieldNameWorktypeName = "WorktypeName";
    public static readonly string DbTimeViewFieldTypeWorktypeName = "string";
    public static readonly string DbTimeViewFieldNameWorkDate = "WorkDate";
    public static readonly string DbTimeViewFieldTypeWorkDate = "date";
    public static readonly string DbTimeViewFieldNameStartTime = "StartTime";
    public static readonly string DbTimeViewFieldTypeStartTime = "string";
    public static readonly string DbTimeViewFieldNameEndTime = "EndTime";
    public static readonly string DbTimeViewFieldTypeEndTime = "string";
    public static readonly string DbTimeViewFieldNameElapsedTime = "ELapsedTime";
    public static readonly string DbTimeViewFieldTypeElapsedTime = "string";
    public static readonly string DbTimeViewFieldNameComment = "Comment";
    public static readonly string DbTimeViewFieldTypeComment = "string";
    #endregion Time

    #region ProductUsage
    public static readonly string DbProductUsageTable = "productusage";
    public static readonly string DbProductUsageTableFieldNameId = "Id";
    public static readonly string DbProductUsageTableFieldTypeId = "int";
    public static readonly string DbProductUsageTableFieldNameProjectId = "project_Id";
    public static readonly string DbProductUsageTableFieldTypeProjectId = "int";
    public static readonly string DbProductUsageTableFieldNameProductId = "product_Id";
    public static readonly string DbProductUsageTableFieldTypeProductId = "int";
    public static readonly string DbProductUsageTableFieldNameStorageId = "storage_Id";
    public static readonly string DbProductUsageTableFieldTypeStorageId = "int";
    public static readonly string DbProductUsageTableFieldNameAmountUsed = "AmountUsed";
    public static readonly string DbProductUsageTableFieldTypeAmountUsed = "double";
    public static readonly string DbProductUsageTableFieldNameUsageDate = "UsageDate";
    public static readonly string DbProductUsageTableFieldTypeUsageDate = "date";
    public static readonly string DbProductUsageTableFieldNameComment = "Comment";
    public static readonly string DbProductUsageTableFieldTypeComment = "string";

    public static readonly string DbProductUsageView = "view_productusage";
    public static readonly string DbProductUsageViewFieldNameId = "Id";
    public static readonly string DbProductUsageViewFieldTypeId = "int";
    public static readonly string DbProductUsageViewFieldNameProjectId = "ProjectId";
    public static readonly string DbProductUsageViewFieldTypeProjectId = "int";
    public static readonly string DbProductUsageViewFieldNameProductId = "ProductId";
    public static readonly string DbProductUsageViewFieldTypeProductId = "int";
    public static readonly string DbProductUsageViewFieldNameStorageId = "StorageId";
    public static readonly string DbProductUsageViewFieldTypeStorageId = "int";
    public static readonly string DbProductUsageViewFieldNameCategoryId = "CategoryId";
    public static readonly string DbProductUsageViewFieldTypeCategoryId = "int";
    public static readonly string DbProductUsageViewFieldNameProjectName = "ProjectName";
    public static readonly string DbProductUsageViewFieldTypeProjectName = "string";
    public static readonly string DbProductUsageViewFieldNameProductName = "ProductName";
    public static readonly string DbProductUsageViewFieldTypeProductName = "string";
    public static readonly string DbProductUsageViewFieldNameStorageName = "StorageName";
    public static readonly string DbProductUsageViewFieldTypeStorageName = "string";
    public static readonly string DbProductUsageViewFieldNameCategoryName = "CategoryName";
    public static readonly string DbProductUsageViewFieldTypeCategoryName = "string";
    public static readonly string DbProductUsageViewFieldNameAmountUsed = "AmountUsed";
    public static readonly string DbProductUsageViewFieldTypeAmountUsed = "double";
    public static readonly string DbProductUsageViewFieldNameUsageDate = "UsageDate";
    public static readonly string DbProductUsageViewFieldTypeUsageDate = "date";
    #endregion ProductUsage

    public static readonly string DbOrderTable = "supplyorder";
    public static readonly string DbOrderView = "view_supplyorder";
    public static readonly string DbOpenOrderView = "view_supplyopenorder";
    public static readonly string DbOrderTableFieldNameId = "Id";
    public static readonly string DbOrderTableFieldTypeId = "int";
    public static readonly string DbOrderTableFieldNameClosed = "Closed";
    public static readonly string DbOrderTableFieldTypeClosed = "int";
    public static readonly string DbOrderTableFieldNameClosedDate = "ClosedDate";
    public static readonly string DbOrderTableFieldTypeClosedDate = "date";

    public static readonly string DbOrderLineTable = "supplyorderline";
    public static readonly string DbOrderLineView = "view_supplyorderline";
    public static readonly string DbOpenOrderLineView = "view_supplyopenorderline";
    public static readonly string DbOrderLineFieldNameId = "Id";
    public static readonly string DbOrderLineFieldTypeId = "int";
    public static readonly string DbOrderLineFieldNameOrderId = "supplyorder_Id";
    public static readonly string DbOrderLineFieldTypeOrderId = "int";
    public static readonly string DbOrderLineFieldNameAmount = "Amount";
    public static readonly string DbOrderLineFieldTypeAmount = "double";
    public static readonly string DbOrderLineFieldNameOpenAmount = "OpenAmount";
    public static readonly string DbOrderLineFieldTypeOpenAmount = "double";
    public static readonly string DbOrderLineFieldNameClosed = "Closed";
    public static readonly string DbOrderLineFieldTypeClosed = "int";
    public static readonly string DbOrderLineFieldNameClosedDate = "ClosedDate";
    public static readonly string DbOrderLineFieldTypeClosedDate = "date";


    public static readonly string DbOpenOrderLineFieldNameSupplyOrderId = "Supplyorder_Id";
    public static readonly string DbOpenOrderLineFieldTypeSupplyOrderId = "int";

    public static readonly string DbUnitTable = "unit";

    public static readonly string DbWorktypeTable = "worktype";

    private static readonly string SqlSelect = "SELECT ";
    private static readonly string SqlSelectAll = "SELECT *";
    private static readonly string SqlFrom = " FROM ";
    private static readonly string SqlWhere = " WHERE ";
    private static readonly string SqlOrderBy = " ORDER BY ";
    private static readonly string SqlAnd = " AND ";
    private static readonly string SqlOr = " OR ";
    private static readonly string SqlMax = " MAX(";
    private static readonly string SqlMin = " MIN(";
    private static readonly string SqlCount = " COUNT(";
    private static readonly string SqlUpdate = "UPDATE ";
    private static readonly string SqlSet = " SET ";
    private static readonly string SqlDelete = "DELETE ";

    private HelperClass az;


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
        StringBuilder sqlText = new();
        sqlText.Append ( SqlSelectAll + SqlFrom + Table.ToLower() );

        if (Id > 0)
        {
            sqlText.Append ( SqlWhere + WhereString + "@Id" );
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), con);
            
            if (Id > 0) { cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id; }

            using MySqlDataAdapter da = new(cmd);
            
            da.Fill(dt);
        }
        return dt;
    }
    public DataTable GetData ( string Table, string[,] WhereFields )
    {
        DataTable dt = new ();
        StringBuilder sqlText = new ();
        sqlText.Append ( SqlSelectAll + SqlFrom + Table.ToLower () + SqlWhere );

        string prefix = "";

        if (WhereFields.GetLength ( 0 ) > 0)
        {
            for (int i = 0; i < WhereFields.GetLength ( 0 ); i++)
            {
                if (i != 0) { prefix = SqlAnd; }
                sqlText.Append ( prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0] );
            }
        }

        MySqlConnection con = new MySqlConnection ( ConnectionStr );

        con.Open ();

        MySqlCommand cmd = new MySqlCommand ( sqlText.ToString(), con );

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

                    // Add leading zero's to date and month
                    az = new HelperClass ();
                    var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2 ) + "-" + az.AddZeros ( _tempDates[0], 2 );
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
        StringBuilder sqlText = new ();
        sqlText.Append ( SqlSelect );
        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText.Append ( prefix + Fields[i, 0] );
        }

        sqlText.Append ( SqlFrom + Table.ToLower () + " " );
        prefix = "";

        if(WhereFields.GetLength(0) > 0)
        {
            sqlText.Append(SqlWhere);

            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                if (i != 0) { prefix = SqlAnd; }
                sqlText.Append ( prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0] );
            }
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), con);

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

                    // Add leading zero's to date and month
                    az = new HelperClass();
                    var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2) + "-" + az.AddZeros ( _tempDates[0], 2);
                    cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = _tempDate;
                    break;
            }
        }

        string resultString="";
        int resultInt;
        double resultDouble;
        float resultFloat;

        if (Fields[0, 1].ToLower() == "string" || Fields[0, 1].ToLower() == "date" || Fields[0, 1].ToLower() == "time") { resultString = (string)cmd.ExecuteScalar(); }
        if (Fields[0, 1].ToLower() == "int") { resultInt = (int)cmd.ExecuteScalar(); resultString = resultInt.ToString(); }
        if (Fields[0, 1].ToLower() == "double") { resultDouble = (double)cmd.ExecuteScalar(); resultString = resultDouble.ToString(); }
        if (Fields[0, 1].ToLower() == "float") { resultFloat = (float)cmd.ExecuteScalar(); resultString = resultFloat.ToString(); }

        return resultString;
    }
    #endregion Get Field(s) from table

    #region Get Max value for Field from table
    public string GetMaxValueFromTable(string Table, string[,] WhereFields, string[,] Fields)
    {
        // There is an Id or String available for each condition, so one of them has a value the other one is 0 or ""
        StringBuilder sqlText = new ();
        sqlText.Append ( SqlSelect );
        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText.Append ( prefix + SqlMax + Fields[i, 0] +")" );
        }

        sqlText.Append ( SqlFrom + Table.ToLower () + " " );
        prefix = "";

        if (WhereFields.GetLength(0) > 0)
        {
            sqlText.Append ( SqlWhere );

            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                if (i != 0) { prefix = SqlAnd; }
                sqlText.Append ( prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0] );
            }
        }

        MySqlConnection con = new MySqlConnection(ConnectionStr);

        con.Open();

        MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), con);

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

                    // Add leading zero's to date and month
                    az = new HelperClass ();
                    var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2 ) + "-" + az.AddZeros ( _tempDates[0], 2 );
                    cmd.Parameters.Add("@" + Fields[i, 0], MySqlDbType.String).Value = _tempDate;
                    break;
            }
        }
        string resultString = "";
        int resultInt;
        double resultDouble;
        float resultFloat;
        DateTime resultDate;

        if (Fields[0, 1].ToLower() == "string") { resultString = (string)cmd.ExecuteScalar(); }
        if (Fields[0, 1].ToLower() == "int") { resultInt = (int)cmd.ExecuteScalar(); resultString = resultInt.ToString(); }
        if (Fields[0, 1].ToLower() == "double") { resultDouble = (double)cmd.ExecuteScalar(); resultString = resultDouble.ToString(); }
        if (Fields[0, 1].ToLower() == "float") { resultFloat = (float)cmd.ExecuteScalar(); resultString = resultFloat.ToString(); }
        if (Fields[0, 1].ToLower() == "date") { resultDate = (DateTime)cmd.ExecuteScalar(); resultString = resultDate.ToShortDateString(); }

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

        return resultString;
    }
    #endregion Get Latest added Id from table

    #region Check if there is a record in the table based (returns no of records)
    public int CheckForRecords(string Table, string[,] WhereFields)
    {
        int result = 0;
        StringBuilder sqlText = new ();
        sqlText.Append ( SqlSelect + SqlCount + "*) " + SqlFrom + Table.ToLower() + SqlWhere);
        string prefix = "";

        for (int i = 0; i < WhereFields.GetLength(0); i++)
        {
            if (i != 0) { prefix = SqlAnd; }
            sqlText.Append ( prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0] );
        }

        using (MySqlConnection con = new(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new(sqlText.ToString(), con))
            {
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

                            // Add leading zero's to date and month
                            az = new HelperClass ();
                            var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2 ) + "-" + az.AddZeros ( _tempDates[0], 2 );
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
        StringBuilder sqlText = new ();
        sqlText.Append ( SqlUpdate + Table.ToLower () + SqlSet );

        string prefix = "";

        for (int i = 0; i < Fields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText.Append(prefix + Fields[i, 0] + " = @" + Fields[i, 0]) ;
        }

        sqlText.Append(SqlWhere);
        prefix = "";

        for (int i = 0; i < WhereFields.GetLength(0); i++)
        {
            if (i != 0) { prefix = ", "; }
            sqlText.Append(prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0]);
        }

        try
        {
            int rowsAffected = ExecuteNonQueryTable(sqlText.ToString(), WhereFields, Fields);

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
        StringBuilder sqlText = new ();
        sqlText.Append(SqlDelete + SqlFrom + Table.ToLower() + SqlWhere);

        string prefix = "";

        if (WhereFields.GetLength(0) > 0)
        {
            for (int i = 0; i < WhereFields.GetLength(0); i++)
            {
                if (i != 0) { prefix = SqlAnd; }
                sqlText.Append(prefix + WhereFields[i, 0] + " = @" + WhereFields[i, 0]);
            }
        }

        try
        {
            int rowsAffected = ExecuteNonQueryTable(sqlText.ToString(), WhereFields);

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

                        // Add leading zero's to date and month
                        az = new HelperClass ();
                        var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2 ) + "-" + az.AddZeros ( _tempDates[0], 2 );
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

                        // Add leading zero's to date and month
                        az = new HelperClass ();
                        var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2 ) + "-" + az.AddZeros ( _tempDates[0], 2 );
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = _tempDate;
                        break;
                    case "time":
                        cmd.Parameters.Add("@" + WhereFields[i, 0], MySqlDbType.String).Value = Fields[i, 2];
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

                        // Add leading zero's to date and month
                        az = new HelperClass ();
                        var _tempDate = _tempDates[2] + "-" + az.AddZeros ( _tempDates[1], 2 ) + "-" + az.AddZeros ( _tempDates[0], 2 );
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
    #endregion Execute Non Query Table

    #region Create List for available working hours
    public List<WorkingHours> GetWorkingHoursList(List<WorkingHours> workinghoursList, string WorkingDate)
    {
        string DatabaseTable = DbTimeTable;
        Database dbConnection = new()
        {
            TableName = DatabaseTable
        };

        String[] _tempDate = WorkingDate.Split("-");
        var _tempWorkingDate = _tempDate[2] + "-" + ("0" + _tempDate[1]).Substring(("0" + _tempDate[1]).Length - 2, 2) + "-" + ("0" + _tempDate[0]).Substring(("0" + _tempDate[0]).Length - 2, 2);


        dbConnection.SqlSelectionString = "HOUR(StartTime) * 60 + MINUTE(StartTime) AS StartTime, HOUR(EndTime) * 60 + MINUTE(EndTime) AS EndTime";
        dbConnection.SqlOrderByString = "StartTime";
        dbConnection.SqlWhereString = "WorkDate=\"" + _tempWorkingDate + "\"";
        dbConnection.TableName = DatabaseTable;

        DataTable dtSelection = dbConnection.LoadSpecificMySqlData();

        for (int i = 0; i < dtSelection.Rows.Count; i++)
        {
            workinghoursList.Add(new WorkingHours
            {
                StartTime = int.Parse(dtSelection.Rows[i][0].ToString()),
                EndTime = int.Parse(dtSelection.Rows[i][1].ToString(), Culture)
            });
        }
        return workinghoursList;
    }
    #endregion Create List for available working hours

    #region Create object for all workinghours in table for specific date
    public class WorkingHours
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
    #endregion Create object for all workinghours in table for specific date

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
                var freq = dtSelection.Rows[i][3].ToString ().Count ( f => (f == '\\') );
                string _spacer = new string ( ' ', freq * 3 );

                worktypeList.Add ( new Worktype
                {
                    WorktypeSpacer = _spacer,
                    WorktypeName = dtSelection.Rows[i][0].ToString (),
                    WorktypeId = int.Parse ( dtSelection.Rows[i][1].ToString (), Culture ),
                    WorktypeParentId = int.Parse ( dtSelection.Rows[i][2].ToString (), Culture )
                } ); 
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

