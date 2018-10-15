
using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Entity_Common
/// </summary>
public class Entity_Common
{
    public Entity_Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable get_SP_DataTable(vt_QSFHEntities context, string sp_Name)
    {
        return get_SP_DataTable(context, sp_Name, null);
    }
    public static DataTable get_DataTable(vt_QSFHEntities context, string sp_Name)
    {
        return get_DataTable(context, sp_Name, null);
    }
    public static DataTable get_DataTable(vt_QSFHEntities context, string Query, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        var conn = context.Database.Connection;
        var connectionState = conn.State;
        try
        {
          
            {
                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Query;
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        foreach (var item in param)
                        {
                            cmd.Parameters.Add(item);
                        }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (connectionState != ConnectionState.Open)
                conn.Close();
        }
        return dt;
    }
    public static DataTable get_SP_DataTable(vt_QSFHEntities context, string sp_Name, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        var conn = context.Database.Connection;
        var connectionState = conn.State;
        try
        {
           
            {
                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sp_Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (param != null)
                        foreach (var item in param)
                        {
                            cmd.Parameters.Add(item);
                        }

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (connectionState != ConnectionState.Open)
                conn.Close();
        }
        return dt;
    }
    public static string get_Scalar(vt_QSFHEntities context, string sp_Name, SqlParameter[] param)
    {
        object Value = null;
        var conn = context.Database.Connection;
        var connectionState = conn.State;
        try
        {
           
            {
                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sp_Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (param != null)
                        foreach (var item in param)
                        {
                            cmd.Parameters.Add(item);
                        }

                    Value = cmd.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (connectionState != ConnectionState.Open)
                conn.Close();
        }
        return Value.ToString();
    }

    public static void ExecuteSql(vt_QSFHEntities context, string sql)
    {
      

        var conn = context.Database.Connection;
        var connectionState = conn.State;
        try
        {
            
            {
                if (connectionState != ConnectionState.Open)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteReader();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (connectionState != ConnectionState.Open)
                conn.Close();
        }
    }
}