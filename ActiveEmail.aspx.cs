using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ActiveEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int kt_update=0;

            string email = Request.QueryString["em"];
            string guid_active = Request.QueryString["idact"];
            kt_update = update_active(HttpUtility.UrlDecode(email), guid_active);
            if (kt_update == 1)
                activesecfull.Text = "Email " + HttpUtility.UrlDecode(email) + " đã được xác nhận, bạn có thể tải tài liệu ngay bây giờ.";
            else
                if (kt_update == 0)
            {
                activesecfull.Text = "Email này đã được xác nhận trước đó, bạn không cần xác nhận lại.";
            }
            /*else
                    if (kt_update == 0)
            {
                activesecfull.Text = "Xác nhận email thất bại, vui lòng liện hệ với Thư viện.";
            }*/
        }

    }
    private int update_active(string email_active, string active)
    {
        int kq;
        string constring = ConfigurationManager.ConnectionStrings["ActiveEmail"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            try
            {

                SqlCommand cmd;
                cmd = new SqlCommand("update_actice_email", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email_active;
                cmd.Parameters.Add("@active", SqlDbType.VarChar, 32).Value = active;
                SqlParameter returnvalue = new SqlParameter("@KQ", "");
                returnvalue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnvalue);
                con.Open();
                cmd.ExecuteNonQuery();
                kq = Convert.ToInt32(returnvalue.Value);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
            }
        }

        return kq;

    }
}