using System;
using System.Collections.Generic;
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


public partial class HomeLib_Login : System.Web.UI.Page
{

    // LibProcess pro = new LibProcess();
    SLibrary.Login synlib = new SLibrary.Login();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_Session"] != null)
        {
            Session["user_Session"] = null;
        }
        /*if (ck != null && this.ChkRememberme.Checked == true)
        {
            int timeout = rememberMe ? 525600 : 30; // Timeout in minutes, 525600 = 365 days.
            var ticket = new FormsAuthenticationTicket(TxtUserName.Text, TxtPassword.Text);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            cookie.Expires = System.DateTime.Now.AddMinutes(timeout);// Not my line
            cookie.HttpOnly = true; // cookie not available in javascript.
            Response.Cookies.Add(cookie);
        }*/

    }
    protected void But_login_Click(object sender, EventArgs e)
    {
        if (!synlib.ValidateEmail(txtUser.Text.Trim()))// kiểm tra email có hay không rồi mới đăng nhập
        {
            // ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Tài khoản chưa được đăng ký vui lòng liên hệ Thu viện')</script>");
            Laberorr.Visible = true;
            Laberorr.Text = "Tài khoản chưa đăng ký vui lòng liên hệ Thu viện";

        }
        else
        {

            if (synlib.ValidateUser(txtUser.Text, txtPass.Text))//đăng nhập thành công
            {
                string username = txtUser.Text;
                Session["user_Session"] = username;
                string url = Request.QueryString["ReturnUrl"];
                synlib.InsertLogin(username, GetWebBrowserName());
                if (url.Contains("DownloadFile") && Session["ID_Session"] != null)// kiểm tra url Session ID để quay lại trang chi-tiet khi đăng nhập thành công 
                {
                    string id = Session["ID_Session"].ToString();
                    Response.Redirect("/chi-tiet?id=" + id);
                    Session["ID_Session"] = null;
                }
                else
                    Response.Redirect(url);

            }
            else
            {
                Laberorr.Visible = true;
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Tài khoản hoặc mật khẩu không đúng')</script>");
                Laberorr.Text = "Mã thẻ hoặc mật khẩu không đúng.";
            }
        }

    }
    public string GetWebBrowserName()
    {
        string WebBrowserName = string.Empty;
        try
        {
            WebBrowserName = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return WebBrowserName;
    }



}