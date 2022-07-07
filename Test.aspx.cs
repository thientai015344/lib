using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
    protected void saveFile(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        string fullPath = "/Images/Upload_test/" + e.FileName;

        // Save your File
        HtmlEditorExtender1.AjaxFileUpload.SaveAs(Server.MapPath(fullPath));

        // Tells the HtmlEditorExtender where the file is otherwise it will render as: <img src="" />
        e.PostedUrl = fullPath;
    }

	protected void Button1_Click(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
	{
        
        
    }
}