
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Book_ChangePassword : System.Web.UI.UserControl
{
    private string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    private string user=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_Session"] == null)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
        else
        {
            user = Session["user_Session"].ToString();            
        }        
        
    }
    protected void Bt_changePass_Click(object sender, EventArgs e)
    {       
        
            if (ValidateUser(user ,txtPass_old.Text))
            {

                if (txtPass_new.Text != txtPass_renew.Text)
                {
                    icon1.Visible = true;
                    LbInfo.Text = "Mật khẩu mới không giống nhau.";

                }
                else
                {
                    if (ChangePassword_tv(txtPass_old.Text, txtPass_new.Text))
                    {
                        icon1.Visible = true;
                        LbInfo.Text = "Đã thay đổi thành công.";
                    }
                    else
                    {
                        icon1.Visible = true;
                        LbInfo.Text = "Thay đổi thất bại.";

                    }
                }
               
            }
            else
            {
                icon1.Visible = true;
                LbInfo.Text ="Mật khẩu hiện tại không đúng";
               
            }       
       // txtPass_old.Text = txtPass_new.Text = txtPass_renew.Text = string.Empty;
    }
    public bool ValidateUser(string username, string password)
    {

        SqlConnection conn = new SqlConnection(constring);
        string pass = GetHashString("MarcLogin=" + username.ToUpper() + ";MarcPW=" + password);
        SqlCommand cmd = new SqlCommand("SELECT count(1) FROM psc_Readers WHERE CardNumber=@username AND PW=@password", conn);
        cmd.Parameters.Add(new SqlParameter("@username", username));
        cmd.Parameters.Add(new SqlParameter("@password", pass));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;

    }
    /*private bool kiemtra_oldpass(string _oldpass)
    {
       
        string p = "";
        bool kt_oldpass = false;
        SqlConnection conn = new SqlConnection(constring);
        //string old_pass = FormsAuthentication.HashPasswordForStoringInConfigFile("MarcLogin=" + user.ToUpper() + ";MarcPW=" + _oldpass, "MD5");
        string old_pass = GetHashString("MarcLogin=" + user.ToUpper() + ";MarcPW=" + _oldpass);
        try
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = ("select PW from psc_Readers where CardNumber='" + user.Trim() + "'");
            conn.Open();
            SqlDataReader mySqlDataReader = cmd.ExecuteReader(CommandBehavior.Default);
            while (mySqlDataReader.Read())
            {
                p = mySqlDataReader["PW"].ToString();
                if (p.Equals(old_pass))
                {
                    kt_oldpass = true;
                }
                else
                    kt_oldpass = false;
            }
        }
        catch (SqlException e)
        {
            kt_oldpass = false;
        }
        finally
        {
            conn.Close();
        }
        return kt_oldpass;
    }*/

    private bool ChangePassword_tv(string _oldpass, string _newpass)
    {
        bool kt = false;
        SqlConnection conn = new SqlConnection(constring);
        try
        {            
            string new_pass = GetHashString("MarcLogin=" + user.ToUpper() + ";MarcPW=" + _newpass);
            string old_pass = GetHashString("MarcLogin=" + user.ToUpper() + ";MarcPW=" + _oldpass);
            //string new_pass = FormsAuthentication.HashPasswordForStoringInConfigFile("MarcLogin=" + user.ToUpper() + ";MarcPW=" + _newpass, "MD5");
            // string old_pass = FormsAuthentication.HashPasswordForStoringInConfigFile("MarcLogin=" + user.ToUpper() + ";MarcPW=" + _oldpass, "MD5");
            conn.Open();
            SqlCommand sql_CardNumber = new SqlCommand("update psc_Readers set PW ='" + new_pass + "' where CardNumber='" + user + "' ", conn);

            sql_CardNumber.Parameters.Add(new SqlParameter("@CardNumber", SqlDbType.VarChar, 255)).Value = user;
            sql_CardNumber.Parameters.Add(new SqlParameter("@PW", SqlDbType.VarChar, 255)).Value = new_pass;
            sql_CardNumber.ExecuteNonQuery();
            kt = true;

        }
        catch (SqlException ex)
        {
            Console.Write(ex.Message);
            kt = false;
        }
        finally
        {
            conn.Close();

        }
        return kt;
    }
    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));
        return sb.ToString().ToLower();
    }
    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = MD5.Create();  // MD5.Create()
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    
}