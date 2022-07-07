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

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ButChangePass_Click(object sender, EventArgs e)
    {
        string code = Request.QueryString["code"];
        RequestChangePass(code, txtNewPassword.Text);
    }

    private async void RequestChangePass(string code, string newPassword)
    {
        Dictionary<string, string> requestData = new Dictionary<string, string>();
        requestData.Add("code", code);
        requestData.Add("pw", newPassword);

        var result = await ApiHelper.Post<ZlisResult>("http://app.lib.hutech.edu.vn/api/Reader/ChangePasswordWithRequestCode", requestData);
        lblMessage.Text = result.Message;
    }


}