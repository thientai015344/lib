
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ButResetPass_Click(object sender, EventArgs e)
    {
        RequestResetPass(txtUserName.Text, TxtEmail.Text);
    }
    private async void RequestResetPass(string cardnumber, string email)
    {
        Dictionary<string, string> requestData = new Dictionary<string, string>();
        requestData.Add("cardnumber", cardnumber);
        requestData.Add("email", email);

        var result = await ApiHelper.Post<ZlisResult>("http://app.lib.hutech.edu.vn/api/Reader/RequestResetPass", requestData);
        lblMessage.Text = result.Message;
    }


}
