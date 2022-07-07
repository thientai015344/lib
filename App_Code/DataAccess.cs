using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
    private static string constring;
    
    static DataAccess()
    {
         constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    }
    public static DataTable selectQuery(string query)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(constring);
        conn.Open();
        SqlCommand cmd = new SqlCommand(query, conn);
        dt.Load(cmd.ExecuteReader());
        conn.Close();
        return dt;
    }   
}