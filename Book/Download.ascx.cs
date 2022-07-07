using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Book_Download : System.Web.UI.UserControl
{
    public int RecordID { get; set; }
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        // DataListDownload.DataSource = ShowLink(RecordID).DefaultView;
        // DataListDownload.DataBind();
        dt = ShowLink(RecordID);
        if (dt.Rows.Count > 0)
        {
            //HyperLink_cofile.Text = "Có file";
            HyperLink_cofile.NavigateUrl = "/chi-tiet?id=" + RecordID;
        }
        else
            HyperLink_cofile.Text = "";
    }
    private DataTable ShowLink(int ID)
    {
        string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            DataTable dt = new DataTable();
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[TV_RecordDocuments_Download_RecordID]";
                cmd.Parameters.AddWithValue("@RecordID", ID);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

            }
            finally
            {

                con.Close();

            }
            return dt;
        }

    }
}