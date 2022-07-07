<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public class Handler : IHttpHandler {

    private string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;

    /* public void ProcessRequest (HttpContext context)
     {
         context.Response.ContentType = "text/plain";
         context.Response.Write("Hello World");
     }*/

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            string id = context.Request.QueryString["user"];
            if (id != null)
            {
                DataTable dt_thongtin = Getthongtin(id);

                byte[] Photo = (byte[])dt_thongtin.Rows[0]["Photo"];
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.Write(Photo, 0, Photo.Length);
                context.Response.Buffer = true;
                context.Response.BinaryWrite(Photo);
                memoryStream.Dispose();
            }

        }
        catch { }

    }
    private DataTable Getthongtin(string masv)
    {
        SqlConnection  cnn = new SqlConnection(constring);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();

            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CardNumber", masv);
            cmd.CommandText = "TV_Readers_Sel_CardNumber";
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}