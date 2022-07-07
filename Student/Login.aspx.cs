using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    string _mssv_Session;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack )
        {
            if (LayNgay().Rows.Count <=0 || KiemTraNgayHetHanCuaDotTheoNgay() <= 0)           
            {
                La_hantai.Visible = false;
                mes.Visible = false;
            }
           else
            {
                mes.Visible = true;
                foreach (DataRow r in LayNgay().Rows)
                {
                    La_startdate.Text =Convert.ToDateTime(r[0]).ToString("dd/MM/yyyy");
                    La_enddate.Text = Convert.ToDateTime(r[1]).ToString("dd/MM/yyyy");
                    if (Session["_mssv_Session"] != null)
                    {
                        Response.Redirect("dang-ky");
                    }
                }
                
            }           

        }

    }

    protected void bn_Login_Click(object sender, EventArgs e)
    {
        _mssv_Session = form_Code.Value;
        Session["_mssv_Session"] = null;
        if (ValidateUser(_mssv_Session))// neu da co mssv trong bang StudentInfo thi login 
        {
            if (KiemTraNgayHetHanCuaTungSinhVien(_mssv_Session) > 0 || KiemTraNgayHetHanCuaDot(_mssv_Session) > 0)//còn hạn tải sách
            {
                Session["_mssv_Session"] = _mssv_Session;
                Session.Timeout = 120;
                if (!KiemTraTonTai(_mssv_Session)) // neu chua co mssv trong bang StudentInfo_ins thi insert 
                {
                    Response.Redirect("dang-ky");
                }
                else
                    if (Session["_mssv_Session"] != null)
                {

                    Response.Redirect("danh-sach-tai");
                }
            }
            else
            {
                string script = "alert(\"Tài khoản của bạn đã hết hạn tải sách\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                //Lab_mes.Text = "Đã hết hạn tải sách";
                Session["_mssv_Session"] = null;
                
            }
            

        }
        else
        {

            //Mes.Text = "Mã sinh viên không đúng, vui lòng nhập lại";
            string script = "alert(\"Mã sinh viên không đúng, vui lòng nhập lại\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
    public bool ValidateUser(string _mssv)
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        SqlConnection conn = new SqlConnection(constring);

        SqlCommand cmd = new SqlCommand("SELECT count(1) FROM StudentInfo WHERE MSSV=@MSSV", conn);
        cmd.Parameters.Add(new SqlParameter("@MSSV", _mssv));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;

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
    public int KiemTraNgayHetHanCuaTungSinhVien(string _mssv)// kiem tra mssv co trong bang StudentInfo_ins chua
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        SqlConnection conn = new SqlConnection(constring);
        SqlCommand cmd = new SqlCommand("SELECT datediff(day, GETDATE(), NGAY_HET_HAN) FROM StudentInfo WHERE MSSV=@MSSV", conn);
        cmd.Parameters.Add(new SqlParameter("@MSSV", _mssv));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count ;
        //
    }
    public int KiemTraNgayHetHanCuaDot(string _mssv)// kiem tra ngày hết hàng của đợt tải
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        SqlConnection conn = new SqlConnection(constring);
        SqlCommand cmd = new SqlCommand("SELECT datediff(day, GETDATE(), a.NGAY_HET_HAN) FROM StudentGroup a, Studentinfo b WHERE a.NHOM_ID=b.NHOM_ID and CAP_QUYEN_TAI=1 and b.MSSV=@MSSV", conn);
        cmd.Parameters.Add(new SqlParameter("@MSSV", _mssv));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count;
        //
    }
    public int KiemTraNgayHetHanCuaDotTheoNgay()// kiem tra ngày hết hàng của cả đợt tải
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        SqlConnection conn = new SqlConnection(constring);
        SqlCommand cmd = new SqlCommand("SELECT datediff(day, GETDATE(), NGAY_HET_HAN) FROM StudentGroup", conn);
        //cmd.Parameters.Add(new SqlParameter("@NGAY_HET_HAN", DateTime.Now));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count;
        //
    }
    /*public DateTime LayNgayHienThi()
    {
        DateTime ngaybatdau = new DateTime();
        DateTime ngayketthuc =new DateTime();
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constring))
        {

            string oString = "Select NGAY_TAO_NHOM,NGAY_HET_HAN from StudentGroup where CAP_QUYEN_TAI=1 ";
            SqlCommand oCmd = new SqlCommand(oString, conn);
            conn.Open();
            using (SqlDataReader oReader = oCmd.ExecuteReader())
            {
                while (oReader.Read())
                {
                    ngaybatdau= Convert.ToDateTime(oReader[0]);
                    ngayketthuc = Convert.ToDateTime(oReader[1]);

                }                
                conn.Close();
            }
        }
        return ngayketthuc;
    }*/
    public DataTable LayNgay()
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        SqlConnection conn = new SqlConnection(constring);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();            
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select NGAY_TAO_NHOM,max(NGAY_HET_HAN) from StudentGroup where CAP_QUYEN_TAI=1 group by NGAY_TAO_NHOM,NGAY_HET_HAN");
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            return dt;

        }
        finally
        {
            if (conn != null)
                conn.Close();
        }

    }
}