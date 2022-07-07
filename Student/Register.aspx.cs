using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_Registe2t : System.Web.UI.Page
{
    string _mssv_Session;
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session["_mssv_Session"] != null)
        {
            Lab_Code.Text = Session["_mssv_Session"].ToString();
            // neu đã co mssv trong bang StudentInfo_ins thi chuyển thẳng qua trang listdown.aspx
            if (KiemTraTonTai(Lab_Code.Text))
            {
                Response.Redirect("danh-sach-tai");
            }
        }
        else
            Response.Redirect("dang-nhap");
    }
    protected void Register_Click(object sender, EventArgs e)
    {
        if (!KiemTraTonTai(Lab_Code.Text)) // neu chua co mssv trong bang StudentInfo_ins thi insert 
        {
            if (Session["_mssv_Session"] != null)
            {
                Insert_Sinhvien(Lab_Code.Text, form_email.Value, form_Mobile.Value);
                Response.Redirect("danh-sach-tai");
            }
            else
            {
                Response.Redirect("dang-nhap");
            }
        }

    }

    public bool KiemTraTonTai(string _mssv)// kiem tra mssv co trong bang StudentInfo_ins chua
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        SqlConnection conn = new SqlConnection(constring);

        SqlCommand cmd = new SqlCommand("SELECT count(1) FROM StudentGetInfo WHERE MSSV=@MSSV", conn);
        cmd.Parameters.Add(new SqlParameter("@MSSV", _mssv));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;

    }
    private void Insert_Sinhvien(string _mssv, string _email, string _phone)
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Proc_StudentGetInfo_ins]", con))
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@mssv", _mssv);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.Parameters.AddWithValue("@phone", _phone);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}