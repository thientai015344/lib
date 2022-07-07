using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static String[] GetCompletionList(string prefixText, int count)
    {
        WebService client = new WebService();
        string environment = ConfigurationManager.AppSettings["Environment"];
        return client.GetCompletionList(prefixText, count).ToArray();
    }
   
   
    
}