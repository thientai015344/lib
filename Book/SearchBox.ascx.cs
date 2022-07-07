using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Book_SearchBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    

    protected void Search_Click1(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TextBox_search.Value))
        {
            Response.Redirect("fts?q=" + HttpUtility.UrlEncode(TextBox_search.Value) + "&ck=false");           
        }
        else
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
       
    }
}