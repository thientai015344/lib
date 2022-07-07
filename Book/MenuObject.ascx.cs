using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Book_MenuObject : System.Web.UI.UserControl
{
    public string GetKeyword { get; set; }
    public int GetRag { get; set; }
    // public int GetFilter { get; set; }
    public DataTable GetDataAuthor { get; set; }
    public DataTable GetDataSubject { get; set; }
    public DataTable GetDataYear { get; set; }
    int demRowAuthor;
    int demRowSubject;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataList_Author100.DataSource = GetDataAuthor;
        DataList_Author100.DataBind();

        DL_Chude.DataSource = GetDataSubject;
        DL_Chude.DataBind();

        DataList_Year.DataSource = GetDataYear;
        DataList_Year.DataBind();
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            System.Web.UI.WebControls.HyperLink Author100 = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HPAuthor100");
            string LaAuthor100 = DataBinder.Eval(e.Item.DataItem, "Tacgia100").ToString();
            if (!GetKeyword.Contains(LaAuthor100))//nếu đường dẫn url ko có tên tác giả giống nhau thì thêm vào
            {
                Author100.NavigateUrl = "~/tim-kiem?q=" + HttpUtility.UrlEncode(GetKeyword) + " " + HttpUtility.UrlEncode(LaAuthor100) + "&rag=" + GetRag + "&filter=0";
            }
            else// đường dẫn url đã có tên tác giả trùng nhau thì ko thêm tên tác giả vào nữa
                Author100.NavigateUrl = "~/tim-kiem?q=" + HttpUtility.UrlEncode(GetKeyword) + " " + "&rag=" + GetRag + "&filter=0";

            //System.Web.UI.WebControls.HyperLink MoreAuthor100 = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("Hyper_viewmoreAuthor");
            // MoreAuthor100.Text = "Xem thêm";
            //MoreAuthor100.NavigateUrl = "~/Viewmoreauthor.aspx?q=" + HttpUtility.UrlEncode(GetKeyword);

        }
    }
    protected void DataList_Chude_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            System.Web.UI.WebControls.HyperLink Subject = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HPSubject");
            string LaSubject = DataBinder.Eval(e.Item.DataItem, "[Chủ đề]").ToString();
            Subject.Text = CatchuoiChude(LaSubject);

            //Subject.NavigateUrl = "~/tim-kiem?q=" + GetKeyword +"&rag="+ GetRag + "&sub="+HttpUtility.UrlEncode(CatchuoiChude(LaSubject).Replace(',', ' ').Replace("--", ""));
            Subject.NavigateUrl = "~/tim-kiem?q=" + HttpUtility.UrlEncode(GetKeyword) + "&rag=" + GetRag + "&sub=" + HttpUtility.UrlEncode(LaSubject) + "&filter=0";
        }
    }
    public string CatchuoiChude(string str)
    {
        string output = "";
        int commaPos = str.IndexOf(',');
        // textBox1.Text = str.LastIndexOf(',').ToString();
        if (commaPos != -1 && str.Contains(','))
        {

            commaPos = str.IndexOf(',', commaPos + 2);
            if (commaPos > 0)
                output = str.Substring(0, commaPos).TrimEnd(',').Trim();
            else
                output = str.TrimEnd(',').Trim();
        }
        else
        {
            output = str.TrimEnd(',').Trim();
        }
        return output;
    }
    public string LoaiboChar(string ch)
    {
        string str = "";
        string[] MyChar = { "c", "@", ".", "[", "]", "!", "" };
        foreach (String c in MyChar)
        {
            str = str.Replace(c, "");
        }
        return str;
    }
}