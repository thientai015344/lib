using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Majors : System.Web.UI.Page
{
    LibProcess pro = new LibProcess();
    string _OID;
    string _title;
    string _number;
    string _curriID;    
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!Page.IsPostBack)
        {
            _OID = Request.QueryString["id"];// lấy mã ngành
            _title = Request.QueryString["title"];//lấy tên ngành
            _number = Request.QueryString["number"];// lấy số lượng môn
            _curriID = Request.QueryString["Curri"];// lấy mã môn
            DataList_SDH.DataSource = pro.MenuSDH();//menu sau đại học
            DataList_SDH.DataBind();

            DropDown_SDH.Items.Clear();
            DropDown_SDH.DataSource = pro.MenuSDH().DefaultView;          
            DropDown_SDH.DataTextField = pro.MenuSDH().Columns[1].Caption; // tên ngành sau đại học          
            DropDown_SDH.DataValueField = pro.MenuSDH().Columns[0].Caption;// mã ngành sau đại học  
            DropDown_SDH.DataBind();                  
           
            DropDown_SDH.SelectedValue = _OID; // hiển thị lại vị trí đã chọn

            if (!string.IsNullOrEmpty( _OID))// nếu mà ngành không null
            {
                DataView dvSDH = new DataView(pro.Curriculums(_OID));
                dvSDH.Sort = "CurriculumName";
                DropDown_SDH_OlogyCurriculums.Items.Clear();
                DropDown_SDH_OlogyCurriculums.DataSource = dvSDH;
                DropDown_SDH_OlogyCurriculums.DataTextField = "CurriculumName";
                DropDown_SDH_OlogyCurriculums.DataValueField = "CurriculumID";
                DropDown_SDH_OlogyCurriculums.DataBind();
                DropDown_SDH_OlogyCurriculums.SelectedValue = _curriID;
                Lab_monhoc_SDH.Text = "Môn học: " + "(" + _number + " môn)";

                DataView dvDH = new DataView(pro.Curriculums(_OID));
                dvDH.Sort = "CurriculumName";
                DropDown_DH_OlogyCurriculums.Items.Clear();
                DropDown_DH_OlogyCurriculums.DataSource = dvDH;
                DropDown_DH_OlogyCurriculums.DataTextField= "CurriculumName";
                DropDown_DH_OlogyCurriculums.DataValueField= "CurriculumID";
                DropDown_DH_OlogyCurriculums.DataBind();
                DropDown_DH_OlogyCurriculums.SelectedValue = _curriID;
                Lab_monhoc_DH.Text= "Môn học: " + "(" + _number + " môn)";
            }
            else
            {
                MH_SDH.Visible = false;
                MH_DH.Visible = false;
               // DropDown_SDH_OlogyCurriculums.Visible = false;
            }


            DataList_DH.DataSource = pro.MenuDH();//menu đại học cao đẳng
            DataList_DH.DataBind();

            DropDown_DH.Items.Clear();
            DropDown_DH.DataSource = pro.MenuDH().DefaultView;
            DropDown_DH.DataTextField = pro.MenuDH().Columns[1].Caption;
            DropDown_DH.DataValueField = pro.MenuDH().Columns[0].Caption;
            DropDown_DH.DataBind();
            DropDown_DH.SelectedValue = _OID;

            if (!string.IsNullOrEmpty(_OID))// kiểm tra mã ngành khác null
            {
                DataTable dt = new DataTable();
                dt = pro.Curriculums(_OID);
                dt = dt.AsEnumerable()
                .GroupBy(r => new { CurriculumName = r["CurriculumName"].ToString().ToLower() })
                .Select(g => g.OrderBy(r => r["CurriculumName"]).First())
                .CopyToDataTable();
                dt.DefaultView.Sort = "CurriculumName";// sắp xếp tên môn
                DataList_Curriculums.DataSource = dt.DefaultView;
                DataList_Curriculums.DataBind();
            }
            if (DataList_Curriculums.Items.Count == 0)
            {
                Lab_SDH.Visible = false;
            }
            else
                Lab_SDH.Text ="Ngành: " + _title + " (" + _number + " môn)";

        }        

    }

    public static string LimitLength(string orgText, int maxLength, string append)
    {
        if (orgText == null) return null;
        if (orgText.Length <= maxLength) return orgText;
        orgText = HttpContext.Current.Server.HtmlDecode(orgText);
        string[] words = orgText.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();
        foreach (string word in words)
        {
            if ((sb + word).Length > maxLength)
                return string.Format("{0}{1}", sb.ToString().TrimEnd(' '), append);
            sb.Append(word + " ");
        }
        return string.Format("{0}{1}", sb.ToString().TrimEnd(' '), append);

        //return string.Format("{0}{1}", orgText.Substring(0, maxLength), append);
    }
    public static string ToUpperFirstLetter(string source)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;
        // convert to char array of the string
        char[] letters = source.ToCharArray();
        // upper case the first char
        letters[0] = char.ToUpper(letters[0]);
        // return the array made of the new char array
        return new string(letters);
    }
    string CurriculumID;
    protected void DataList_Curriculums_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            System.Web.UI.WebControls.HyperLink LinkCurriculumID = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HyperCurriculumID");
            CurriculumID = DataBinder.Eval(e.Item.DataItem, "CurriculumID").ToString();            

            if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("Curri="))
            {
                LinkCurriculumID.NavigateUrl = HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&Curri=")) + "&Curri=" + CurriculumID + "&page=1#mh";

            }
            else
                LinkCurriculumID.NavigateUrl = HttpContext.Current.Request.Url + "&Curri=" + CurriculumID + "&page=1#mh";

        }
    }

    protected void DropDown_SDH_SelectedIndexChanged(object sender, EventArgs e)
    {       
        // if (!string.IsNullOrEmpty(_OID) || !string.IsNullOrEmpty(_title) || !string.IsNullOrEmpty(_number))
        string _OlogyID = DropDown_SDH.SelectedItem.Value;
        string _OlogyName = DropDown_SDH.SelectedItem.Text.Replace("_SĐH", "");  
        Response.Redirect(String.Format("nganh-hoc-mon-hoc?id={0}&title={1}&number={2}", _OlogyID, _OlogyName, pro.CountOlogyID(_OlogyID).Rows[0][1].ToString()));
 
    }

    protected void DropDown_DH_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _OlogyID = DropDown_DH.SelectedItem.Value;//mã ngành
        string _OlogyName = DropDown_DH.SelectedItem.Text;//tên ngành
        Response.Redirect(String.Format("nganh-hoc-mon-hoc?id={0}&title={1}&number={2}", _OlogyID, _OlogyName, pro.CountOlogyID(_OlogyID).Rows[0][1].ToString()));
    }

    protected void DropDown_SDH_OlogyCurriculums_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _CurriculumsID = DropDown_SDH_OlogyCurriculums.SelectedItem.Value;// mã môn      
        //Response.Redirect(String.Format("nganh-hoc-mon-hoc?id={0}&title={1}&number={2}&Curri={3}", _OID, _title, _number,_CurriculumsID) + "&page = 1");
        if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("Curri="))
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&Curri=")) + "&Curri=" + _CurriculumsID + "&page=1#mh");

        }
        else
            Response.Redirect(HttpContext.Current.Request.Url + "&Curri=" + _CurriculumsID + "&page=1#mh");

    }

    protected void DropDown_DH_OlogyCurriculums_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _CurriculumsID = DropDown_DH_OlogyCurriculums.SelectedItem.Value;// mã môn        
        if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("Curri="))
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&Curri=")) + "&Curri=" + _CurriculumsID + "&page=1#mh");

        }
        else
            Response.Redirect(HttpContext.Current.Request.Url + "&Curri=" + _CurriculumsID + "&page=1#mh");
    }
}