using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewonline : System.Web.UI.Page
{
   
    public string FilenamePDF { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        //string filename = Request.QueryString["f"];
        string filename = (string)Session["file"];
        iframe(filename);
        //Session["file"] = null;
    }
    protected void iframe(string file)
    {
        string src = "http://data.lib.hutech.edu.vn/mucluc/" + file;
        LiteralControl literal = new LiteralControl();
        literal.Text = "<iframe src='" + src + "' width='100%' height='700px' scrolling='yes' frameborder='0' ></iframe>";
        //literal.Text = "<object data='" + src + "' type='application/pdf' width='700px' height='800px'></object>";        
        PlaceHolderView.Controls.Add(literal);

    }
}