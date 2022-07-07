using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeLib_Tailieuchude : System.Web.UI.Page
{
    LibProcess xl = new LibProcess();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataList1.DataSource = xl.LinkObject();
        DataList1.DataBind();
    }

}