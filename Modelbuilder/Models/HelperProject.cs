using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace Modelbuilder;

public class HelperProject
{
    #region public Variables
    public string ConnectionStr { get; set; } = string.Empty;

    public string DbBrandTable = "brand";
    public string DbCategoryTable = "category";
    public string DbCountryTable = "country";
    public string DbCurrencyTable = "currency";
    public string DbProductTable = "product";
    public string DbProductSupplierTable = "productsupplier";
    public string DbProjectTable = "project";
    public string DbStorageTable = "storage";
    public string DbSupplierTable = "supplier";
    public string DbUnitTable = "unit";
    public string DbWorktypeTable = "worktype";
    public string DbContactTypeTable = "contacttype";

    public CultureInfo Culture = new("nl-NL");
    #endregion public Variables

    #region Connector to database
    public HelperProject(string serverName, string databaseName, string username, string userPwd)
    {
        ConnectionStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", serverName, databaseName, username, userPwd);
    }

    public HelperProject(string serverName, int portNumber, string databaseName, string username, string userPwd)
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

    #region Execute Non Query Table: Project
    public int ExecuteNonQueryTblProject(string sqlText, string projectCode, string projectName, string projectStartDate, string projectEndDate, int projectClosed, string projectMemo, string projectImageRotationAngle, byte[] projectImage, int projectId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new(ConnectionStr))
        {
            con.Open();

            using MySqlCommand cmd = new(sqlText, con);
            // Add Int values
            cmd.Parameters.Add("@projectId", MySqlDbType.Int32).Value = projectId;
            cmd.Parameters.Add("@projectClosed", MySqlDbType.Int32).Value = projectClosed;

            // Add VarChar values
            cmd.Parameters.Add("@projectCode", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@projectName", MySqlDbType.VarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@projectStartDate", MySqlDbType.Date).Value = DBNull.Value;
            cmd.Parameters.Add("@projectEndDate", MySqlDbType.Date).Value = DBNull.Value;
            cmd.Parameters.Add("@projectImageRotationAngle", MySqlDbType.VarChar).Value = DBNull.Value;

            // Add LongText values
            cmd.Parameters.Add("@projectMemo", MySqlDbType.LongText).Value = DBNull.Value;

            // Add Images
            cmd.Parameters.Add("@projectImage", MySqlDbType.Blob).Value = projectImage;


            //set values
            if (!String.IsNullOrEmpty(projectCode))
            {
                cmd.Parameters["@projectCode"].Value = projectCode;
            }

            if (!String.IsNullOrEmpty(projectName))
            {
                cmd.Parameters["@projectName"].Value = projectName;
            }

            if (!String.IsNullOrEmpty(projectStartDate))
            {
                cmd.Parameters["@projectStartDate"].Value = DateTime.Parse(projectStartDate, Culture);
            }

            if (!String.IsNullOrEmpty(projectEndDate))
            {
                cmd.Parameters["@projectEndDate"].Value = DateTime.Parse(projectEndDate, Culture);
            }

            if (!String.IsNullOrEmpty(projectMemo))
            {
                cmd.Parameters["@projectMemo"].Value = projectMemo;
            }

            if (!String.IsNullOrEmpty(projectImageRotationAngle))
            {
                cmd.Parameters["@projectImageRotationAngle"].Value = projectImageRotationAngle;
            }

            rowsAffected = cmd.ExecuteNonQuery();
        }
        return rowsAffected;
    }
    #endregion

    #region Execute Non Query Table Project_Id: Project
    public int ExecuteNonQueryTblProjectId(string sqlText, int projectId = 0)
    {
        int rowsAffected = 0;

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using (MySqlCommand cmd = new MySqlCommand(sqlText, con))
            {

                //add parameters setting string values to DBNull.Value
                cmd.Parameters.Add("@projectId", MySqlDbType.Int32).Value = projectId;

                //execute; returns the number of rows affected
                rowsAffected = cmd.ExecuteNonQuery();
            }
        }

        return rowsAffected;
    }
    #endregion Execute Non Query Table Project_Id: Project

    #region Get Data from Table: Project
    public DataTable GetDataTblProject(int projectId = 0)
    {
        DataTable dt = new DataTable();
        string sqlText = string.Empty;

        if (projectId > 0)
        {
            sqlText = "SELECT * from Project where project_Id = @projectId";
        }
        else
        {
            sqlText = "SELECT * from Project";
        }

        using (MySqlConnection con = new MySqlConnection(ConnectionStr))
        {
            //open
            con.Open();

            using MySqlCommand cmd = new MySqlCommand(sqlText, con);
            //add parameter
            cmd.Parameters.Add("@projectId", MySqlDbType.Int32).Value = projectId;

            using MySqlDataAdapter da = new(cmd);
            //use DataAdapter to fill DataTable
            da.Fill(dt);
        }
        return dt;
    }
    #endregion Get Data from Table: Project

    #region Insert in Table: Project
    public string InsertTblProject(string projectCode, string projectName, string projectStartDate, string projectEndDate, int projectClosed, string projectMemo, string projectImageRotationAngle, byte[] projectImage)
    {
        string result = string.Empty;
        string sqlText = "INSERT INTO Project (project_Code, project_Name, project_StartDate, project_EndDate, project_Closed, project_Memo, project_ImageRotationAngle, project_Image) VALUES (@projectCode, @projectName, @projectStartDate, @projectEndDate, @projectClosed, @projectmemo, @projectImageRotationAngle, @projectImage);";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProject(sqlText, projectCode, projectName, projectStartDate, projectEndDate, projectClosed, projectMemo, projectImageRotationAngle, projectImage);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij toegevoegd.");
            }
            else
            {
                result = "Rij niet toegevoegd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (UpdateTblProject - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (UpdateTblProject): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Insert in Table: Project

    #region Delete row in Table: Project
    public string DeleteTblProject(int projectId)
    {
        string result = string.Empty;
        string sqlText = "DELETE FROM Project WHERE project_Id=@projectId";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProjectId(sqlText, projectId);

            if (rowsAffected > 0)
            {

                result = String.Format("Rij verwijderd.");
            }
            else
            {
                result = "Rij niet verwijderd.";
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Error (DeleteTblProject - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error (DeleteTblProject): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Delete row in Table: Project

    #region Update Table: Project
    public string UpdateTblProject(int projectId, string projectCode, string projectName, string projectStartDate, string projectEndDate, int projectClosed, string projectMemo, string projectImageRotationAngle, byte[] projectImage)
    {
        string result = string.Empty;
        string sqlText = "UPDATE Project SET project_Code = @projectCode, project_Name = @projectName, project_StartDate = @projectStartDate, project_EndDate = @projectEndDate, project_Closed = @projectClosed, project_Memo = @projectMemo, project_ImageRotationAngle = @projectImageRotationAngle, project_Image = @projectImage WHERE project_Id = @projectId;";

        try
        {
            int rowsAffected = ExecuteNonQueryTblProject(sqlText, projectCode, projectName, projectStartDate, projectEndDate, projectClosed, projectMemo, projectImageRotationAngle, projectImage, projectId);

            //Better alternative for simple If/Then/Else (If rowsAffected>0 then "succes" else "no rows updated")
            result = rowsAffected > 0 ? projectName + " bijgewerkt." : "Geen wijzigingen door te voeren.";
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine("Fout (UpdateTblProject - MySqlException): " + ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Fout (UpdateTblProject): " + ex.Message);
            throw;
        }

        return result;
    }
    #endregion Update Table: Project
}
