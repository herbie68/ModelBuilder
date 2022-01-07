using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbuilder;
internal class HelperReceipt
{
    #region public Variables
    public string ConnectionStr { get; set; }

    public string DbCategoryTable = "category";
    public string DbOrderTable = "supplyorder";
    public string DbOrderView = "view_supplyopenorder";
    public string DbOrderLineTable = "supplyorderline";
    public string DbOrderLineView = "view_supplyopenorderline";
    public string DbCurrencyTable = "currency";
    public string DbProductTable = "product";
    public string DbProductSupplierTable = "productsupplier";
    public string DbProjectTable = "project";
    public string DbSupplierTable = "supplier";

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

    #region Get Data from TableView: SupplyOpenOrder
    public DataTable GetDataTblOrderOpen(int OrderId = 0)
    {
        DataTable dt = new();
        string sqlText = string.Empty;

        if (OrderId > 0)
        {
            sqlText = "SELECT * from " + DbOrderView + " where Id = @IdOrder";
        }
        else
        {
            sqlText = "SELECT * from " + DbOrderView;
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            if (OrderId > 0) { cmd.Parameters.Add("@OrderId", MySqlDbType.Int32).Value = OrderId; }

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }
        return dt;
    }
    #endregion Get Data from TableView: SupplyOpenOrder

    #region Get Data from TableView: SupplyOpenOrderline
    public DataTable GetDataTblOrderlineOpen(int Id = 0)
    {
        DataTable dt = new ();
        string sqlText = string.Empty;

        if (Id > 0)
        {
            sqlText = "SELECT * from " + DbOrderLineView + " where Supplyorder_Id = @Id";
        }
        else
        {
            sqlText = "SELECT * from " + DbOrderLineView;
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }
        return dt;
    }
    #endregion Get Data from TableView: SupplyOpenOrderline


}
