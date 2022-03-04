namespace Modelbuilder;
internal class HelperProduct
{
    #region public Variables
    public string ConnectionStr { get; set; }

    public string DbProductTable = "product";
    public string DbProductView = "view_product";
    public string DbProductSupplierTable = "productsupplier";
    public string DbProductSupplierView = "view_productsupplier";

    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables

    #region Connector to database
    public HelperProduct(string serverName, int portNumber, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", serverName, portNumber, databaseName, username, userPwd);
    }
    #endregion Connector to database

    #region Uncheck the DefaultSupplier: ProductSupplier
    public int UncheckDefaultSupplierTblProductSupplier(string sqlText, int Id, int ProductId)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new MySqlCommand ( sqlText, con );

            cmd.Parameters.Add ( "@Id", MySqlDbType.Int32 ).Value = Id;
            cmd.Parameters.Add ( "@ProductId", MySqlDbType.Int32 ).Value = ProductId;

            //execute; returns the number of rows affected
            rowsAffected = cmd.ExecuteNonQuery ();
        }
        return rowsAffected;
    }
    #endregion

    #region Uncheck other default suppliers for product
    public string UncheckDefaultSupplierTblProductSupplier(int Id, int ProductId)
    {
        string sqlText = "UPDATE " + DbProductSupplierTable + " SET DefaultSupplier = '' WHERE Id != @Id AND Product_Id = @ProductId;";

        string result;
        try
        {
            int rowsAffected = UncheckDefaultSupplierTblProductSupplier ( sqlText, Id, ProductId );

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? Id + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine ( "Fout (UpdateTblProductSupplier - MySqlException): " + ex.Message );
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine ( "Fout (UpdateTblProductSupplier): " + ex.Message );
            throw;
        }
        return result;
    }
    #endregion Uncheck other default suppliers for product
}
