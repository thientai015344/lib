using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeLib_Profile : System.Web.UI.Page
{  
    private int ReaderID;
    private string CardNumber;
    private string StudentID;
    private string LastName;
    private string FirstName;
    private string GroupName;
    string username;
    LibProcess pro = new LibProcess();
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
            username = Session["user_Session"].ToString();
            ShowInfo(username);
            
        }

        if (!IsPostBack)
        {

            Lab_name.Text = LastName + " " + FirstName;
            Lab_class.Text =GroupName;
            Lab_code.Text = CardNumber;
            string script = @"<span style = 'font-size:14px; font-style:italic; color: brown'> (Mỗi ngày bạn có 5 lượt tải tài liệu) </span>";
            if (pro.CheckRoleDownload(username)==23)//tài khoản Full download
            {
                Lab_countdown.Text = " Tài khoản không giới hạn lượt tải tài liệu";
            }
            else
                Lab_countdown.Text = " Lượt tải còn lại trong ngày: " + (5 - pro.Countdown(username)).ToString() + "<html>" + script + "</html>";          
           
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        PF.Visible = true;
        CP.Visible = false;

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        CP.Visible = true;
        PF.Visible = false;
    }
    
    private void ShowInfo(string _user)
    {
        try
        {            
            DataTable dt = pro.thongtindocgia(_user);
            foreach (DataRow r in dt.Rows)
            {
                ReaderID = int.Parse(r[0].ToString());
                CardNumber = r[1].ToString();
                StudentID = r[2].ToString();
                LastName = r[3].ToString();
                FirstName = r[4].ToString();
                GroupName = r[15].ToString();
            }
        }
        catch { }
    }
    
}