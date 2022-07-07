using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Book_MajorsList : System.Web.UI.UserControl
{
    PagedDataSource objPage = new PagedDataSource();
    //DataTable dt_temp = new DataTable();
    int sotrang;
    int _page;
    string _id;
    string _title;
    string _curriID;
    string _number;
    string valuesearch;
    string _keywork;
    string _gtc;//giáo trình chính
    public string GetCurri { get; set; }
    string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        _id = Request.QueryString["id"];
        _title = Request.QueryString["title"];
        _number = Request.QueryString["number"];
        _curriID = Request.QueryString["Curri"];
        _keywork = Request.QueryString["key"];

        _page = Convert.ToInt32(Request.QueryString["page"]);

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(_curriID))
            {
                valuesearch = QueryUrl(_curriID).Rows[0]["Querry"].ToString(); //lấy query 
                if (!string.IsNullOrEmpty(QueryUrl(_curriID).Rows[0]["Descriptions"].ToString()))
                {
                    Lab_mota.Text = "<b>Mô tả chi tiết: </b>" + QueryUrl(_curriID).Rows[0]["Descriptions"].ToString();//lay tom tat mon hoc  
                }
                else
                {
                    Lab_mota.Visible = false;
                }

                BindList(valuesearch, _gtc);
            }
            if (DataList_Document.Items.Count==0 && string.IsNullOrEmpty(_keywork))
            {
                div_page.Visible = false;
                div_info_search.Visible = false;
                div_info.Visible = false;
            }
        }

    }
    public int CurrentPage
    {

        get
        {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
        }

        set
        {
            this.ViewState["CurrentPage"] = value;
        }

    }
    public string Currentkeywork
    {

        get
        {
            if (this.ViewState["Currentkeywork"] == null)
                return null;
            else
                return this.ViewState["Currentkeywork"].ToString();
        }

        set
        {
            this.ViewState["CurrentPage"] = value;
        }

    }

    public DataTable FillQuerry(string _Querry, string _key, string _BookTypeID)
    {
        SqlConnection cnn = new SqlConnection(constring);
        try
        {
            DataTable dt = new DataTable();
            // dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(_Querry))
            {

                if (!string.IsNullOrEmpty(_BookTypeID))//giáo trình chính hoặc tài liệu tham khảo được chọn
                {

                    cmd.CommandText = "SELECT distinct b.ShortTitle as [Nhan đề],b.Author245 as [Tác giả], b.Dewey ,b.PublishPlace as [Nơi XB] ,b.Publisher as [Nhà XB] ,b.PublishYear as [Năm XB],b.RecordID, a.RecordGroupID, c.RecordGroupName FROM psc_Records a inner join psc_RecordInfos b on a.RecordID = b.RecordID inner join psc_RecordGroups c on a.RecordGroupID=c.RecordGroupID right outer join psc_RecordCurriculums d on a.RecordID = d.RecordID WHERE d.BookTypeID = @BookTypeID and d.CurriculumID = @CurriculumID and  b.Title like N'%" + _key + "%' ORDER BY b.ShortTitle ";
                    cmd.Parameters.AddWithValue("@BookTypeID", _BookTypeID);
                    cmd.Parameters.AddWithValue("@CurriculumID", _curriID.Trim());
                    cmd.Connection = cnn;
                    SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (sdr.HasRows)
                    {
                        dt.Load(sdr);
                    }
                    
                }
                else
                {
                    cmd.CommandText = "SELECT b.ShortTitle as [Nhan đề],b.Author245 as [Tác giả], b.Dewey ,b.PublishPlace as [Nơi XB] ,b.Publisher as [Nhà XB] ,b.PublishYear as [Năm XB],b.RecordID, a.RecordGroupID, c.RecordGroupName FROM psc_Records a inner join psc_RecordInfos b on a.RecordID = b.RecordID inner join psc_RecordGroups c on a.RecordGroupID=c.RecordGroupID  WHERE " + _Querry + " and  b.Title like N'%" + _key + "%' ORDER BY b.ShortTitle ";
                    cmd.Connection = cnn;
                    SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (sdr.HasRows)
                    {
                        dt.Load(sdr);
                    }                  

                }

            }
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }


    }
    private void BindList(string geturl, string _select)
    {
        try
        {

            DataTable dt = new DataTable();
            dt.Clear();
            CurrentPage = _page;

            dt = FillQuerry(geturl, _keywork, _select);
            objPage.AllowPaging = true;
            objPage.DataSource = dt.DefaultView;
            objPage.PageSize = 10;
            objPage.CurrentPageIndex = CurrentPage - 1;
            // ViewState["TotalPages"] = objPage.PageCount;
            lbtnNext.Visible = !objPage.IsLastPage;
            lbtnPrev.Visible = !objPage.IsFirstPage;
            //DataList_tailieu = null;
            DataList_Document.DataSource = objPage;
            DataList_Document.DataBind();
            int TotalRecord = dt.Rows.Count;
            sotrang = objPage.PageCount;
            DropDownList_page.Items.Clear();
            for (int i = 1; i <= sotrang; i++)
            {
                DropDownList_page.Items.Add(i.ToString());
            }
            DropDownList_page.SelectedIndex = CurrentPage - 1;
            Lab_tenmon.Text = "Môn: " + QueryUrl(_curriID).Rows[0]["CurriculumName"].ToString() + " (" + TotalRecord.ToString() + " tài liệu)"; ;//lay tom tat mon hoc 
            Txt_search.Text = _keywork;

        }
        catch (Exception)
        {
        }
        finally
        {
            objPage = null;

        }

    }

    private DataTable QueryUrl(string IDMonhoc)
    {
        DataTable dt_url = new DataTable();
        SqlConnection con = new SqlConnection(constring);
        try
        {
            //dt_url.Clear();

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[TV_Reader_CurriculumID]";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Mamonhoc", IDMonhoc);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dt_url);

        }
        finally
        {

            con.Close();

            // con.Dispose();
        }
        return dt_url;
    }

    

    protected void DataList_Document_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            int get_loaitailieu = int.Parse(DataBinder.Eval(e.Item.DataItem, "RecordGroupID").ToString());
            string imagename = DataBinder.Eval(e.Item.DataItem, "RecordID").ToString();
            System.Web.UI.WebControls.Image im = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImageBook");
            System.Web.UI.WebControls.Repeater repe = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("repLinks");
            System.Web.UI.WebControls.Label xb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbNamXB");
            string _nxb = DataBinder.Eval(e.Item.DataItem, "Nhà XB").ToString();
            string author = DataBinder.Eval(e.Item.DataItem, "Tác giả").ToString();
            string[] split = author.Split(new Char[] { ',', ';', '.', '\n' });
            repe.DataSource = split;
            repe.DataBind();

            if (UrlExists("http://data.lib.hutech.edu.vn/BookImg/" + imagename + ".jpg"))//neu hinh anh ton tai
            {
                im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/" + imagename + ".jpg";

            }
            else
                switch (get_loaitailieu)
                {
                    case 2:
                        //vitri.Visible = false;
                        im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/luanvan.jpg";
                        break;
                    case 10:
                        //vitri.Visible = false;
                        im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/luanvan_ths.jpg";
                        break;
                    case 7:
                        // vitri.Visible = false;
                        im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/magazine.jpg";
                        break;
                    case 8:
                        // vitri.Visible = false;
                        im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Baitrich.jpg";
                        break;
                    case 1:
                        im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        // vitri.Visible = true;
                        break;
                    case 6:
                        if (_nxb.Contains("Hutech") || _nxb.Contains("hutech") || _nxb.Contains("HUTECH"))
                        {
                            im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/gthutech.jpg";
                        }
                        else
                        {
                            im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        }
                        break;
                    default:
                        im.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        //vitri.Visible = false;
                        break;

                }
        }
    }
    private static bool UrlExists(string url)
    {
        try
        {
            new System.Net.WebClient().DownloadData(url);
            return true;
        }
        catch (System.Net.WebException e)
        {
            if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;
            else
                throw;
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
    protected void DropDownList_page_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = Convert.ToInt32(DropDownList_page.SelectedItem.Text);
        if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("page="))
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&page=")) + "&page=" + CurrentPage + "&key=" + Txt_search.Text + "#mh");
        }
        else
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString() + "&page=" + CurrentPage + "&key=" + Txt_search.Text + "#mh");
        }

    }
    protected void lbtnNext_Click(object sender, ImageClickEventArgs e)
    {
        CurrentPage += 1;
        if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("page="))
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&page=")) + "&page=" + CurrentPage + "&key=" + Txt_search.Text + "#mh");
        }
        else
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString() + "&page=" + CurrentPage + "&key=" + Txt_search.Text + "#mh");
        }

    }

    protected void lbtnPrev_Click(object sender, ImageClickEventArgs e)
    {
        CurrentPage -= 1;
        if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("page="))
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&page=")) + "&page=" + CurrentPage + "&key=" + Txt_search.Text + "#mh");
        }
        else
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString() + "&page=" + CurrentPage + "&key=" + Txt_search.Text + "#mh");
        }
    }

    protected void Txt_search_TextChanged(object sender, EventArgs e)
    {

        if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("key="))// đã có từ khóa tìm kiếm thì xóa từ cũ đi và cập nhật từ mới
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString().Remove(HttpContext.Current.Request.Url.ToString().LastIndexOf("&key=")) + "&key=" + Txt_search.Text.Trim() + "#mh");
        }
        else
        {
            Response.Redirect(HttpContext.Current.Request.Url.ToString() + "&key=" + Txt_search.Text.Trim() + "#mh");
        }

    }
    public string RemoveUnicode(string text)
    {
        string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
        "đ",
        "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
        "í","ì","ỉ","ĩ","ị",
        "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
        "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
        "ý","ỳ","ỷ","ỹ","ỵ",};
        string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
        "d",
        "e","e","e","e","e","e","e","e","e","e","e",
        "i","i","i","i","i",
        "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
        "u","u","u","u","u","u","u","u","u","u","u",
        "y","y","y","y","y",};
        for (int i = 0; i < arr1.Length; i++)
        {
            text = text.Replace(arr1[i], arr2[i]);
            text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
        }
        return text;
    }



    protected void CheckBox_GTC_CheckedChanged1(object sender, EventArgs e)
    {

        valuesearch = QueryUrl(_curriID).Rows[0]["Querry"].ToString(); //lấy query 
        if (CheckBox_GTC.Checked)// giáo trình chính được chọn
        {
            CheckBox_TLTK.Checked = false;
            BindList(valuesearch, "GTC");

        }
        else
            BindList(valuesearch, "");
    }

    protected void CheckBox_TLTK_CheckedChanged(object sender, EventArgs e)
    {

        valuesearch = QueryUrl(_curriID).Rows[0]["Querry"].ToString(); //lấy query 
        if (CheckBox_TLTK.Checked)//tài liệu tham khảo được chọn
        {
            CheckBox_GTC.Checked = false;
            BindList(valuesearch, "TLTK");

        }
        else
            BindList(valuesearch, "");

    }
    public string Thaythechuoi(string _imstring)
    {
        string script = @"<i class='fa fa-book'></i>";
        string s = "";
        if (_imstring == "GTC")
        {
            s = "<html>" + script + " </ html>  Giáo trình chính";
        }
        if (_imstring == "TLTK")
        {
            s = "<html>" + script + " </html> TL tham khảo theo ĐCCT";
        }
        return s;
    }

}