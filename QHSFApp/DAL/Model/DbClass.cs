using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DbEntities;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace DAL.Model
{
    public class DbClass
    {
        public DbClass() { }

        public static DataTable ReturnDataTable(vt_QSFHEntities ContextDB, string Query, SqlParameter[] Params)
        {
            DataTable Dt = new DataTable();
            var Conn = ContextDB.Database.Connection;
            var ConnectionState = Conn.State;
            try
            {
                {
                    if (ConnectionState != ConnectionState.Open)
                        Conn.Open();
                    using (var cmd = Conn.CreateCommand())
                    {
                        cmd.CommandText = Query;
                        cmd.CommandType = CommandType.Text;

                        if (Params != null)
                            foreach (var item in Params)
                            {
                                cmd.Parameters.Add(item);
                            }

                        using (var reader = cmd.ExecuteReader())
                        {
                            Dt.Load(reader);
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
                if (ConnectionState != ConnectionState.Open)
                    Conn.Close();
            }
            return Dt;
        }
    }
}
