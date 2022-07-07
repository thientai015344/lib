
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
    SLibrary.Login synlib = new SLibrary.Login();
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
        
            if (synlib.ValidateUser(user ,txtPass_old.Text))//khớp thông tin
            {

                if (txtPass_new.Text != txtPass_renew.Text)
                {
                    icon1.Visible = true;
                    LbInfo.Text = "Mật khẩu mới không giống nhau.";

                }
                else
                {
                    if (synlib.ChangePassword_tv(user, txtPass_old.Text, txtPass_new.Text))
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
    
}