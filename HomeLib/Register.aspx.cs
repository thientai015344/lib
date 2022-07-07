using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeLib_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ButRegister_Click(object sender, EventArgs e)
    {
        if (!CheckBox1.Checked)
        {
            Lab_error.Text = "Vui lòng chọn đã đọc nội quy Thư viện.";
        }
        else
        {
            if (true)
            {

            }
        }
    }
}