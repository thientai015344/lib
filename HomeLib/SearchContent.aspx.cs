using System;
using System.Data;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using Lucene.Net.Util;
using System.Linq;
using System.Data.SqlClient;

public partial class HomeLib_Search : System.Web.UI.Page
{

   
    protected DataTable Results = new DataTable();
     
    string _Publisher = null;

    List<Getfield> _getf = new List<Getfield>();

    static string IndexPath;

    goSearch sf = new goSearch();

    string iconremove = "<i> &times;</i>";

    ShoppingCart myCart;

    HighlighText _HighlighText = new HighlighText();

    LibProcess pro = new LibProcess();

    const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DropListField();
            DropListFieldFile();         

            if (CheckBoxFullText == "false")// tìm kiếm Marc
            {
                CheckFullText.Checked = false;
                IndexPath = Convert.ToString(ConfigurationManager.AppSettings["IndexPathMarc"]);
                if (this.SearchKeyword != null)
                {

                    // search(SearchKeyword, getSearchField());
                    Results = sf.SearchField(SearchKeyword, getPage(), IndexPath, ddlSearchFieldsFile.SelectedValue, SearchKeyword, getField_Author, getField_Publisher, getAuthor_count, getPublisher_count, getField_GroupName, getField_GroupName_count, getField_FileName, getField_FileName_count, getSearchField);
                    CurrentPage = sf.CurrentPage;// cập nhật số trang hiện tại
                    DataBind();
                    Add_and_Clear_field();
                    
                    
                }
            }
            else if (CheckBoxFullText == "true")// tìm kiếm Full Text Search
            {
                CheckFullText.Checked = true;
                div_file.Visible = false;
                IndexPath = Convert.ToString(ConfigurationManager.AppSettings["IndexPathFullText"]);
                if (this.SearchKeyword != null)
                {
                    // SearchFullText(SearchKeyword, getSearchField());
                    Results = sf.SearchFullText(SearchKeyword, getPage(), IndexPath, getField_Author, getField_Publisher, getAuthor_count, getPublisher_count, getField_GroupName, getField_GroupName_count, getSearchField);
                    CurrentPage = sf.CurrentPage;// cập nhật số trang hiện tại
                    DataBind();
                   
                    divFilter.Visible = false;// tạm thời ẩn
                }
            }
            if (Results.Rows.Count == 0)
            {
                divFilter.Visible = false;// ẩn fiter khi không có kết quả
            }
            try
            {
                if (Results.Rows.Count > 1)
                {                    
                    pro.CheckValueStopword(SearchKeyword);// thêm keyword vào data
                }
            }
            catch
            {

               
            }
            
        }
        

    }

    protected string SearchKeyword
    {
        get
        {
            string query = this.Request.Params["q"];
            if (query == String.Empty)
                return null;
            return CountWords(query);
        }
    }
    private string getPage()
    {
        return Request.QueryString["start"] ?? "";

    }
    protected string getSearchField
    {
        get
        {
            return Request.QueryString["Field"] ?? "";
        }

    }
    protected string getSearchFieldFile
    {
        get
        {
            return Request.QueryString["file"] ?? "";
        }

    }
    private string getAuthor()
    {
        return Request.QueryString["au"] ?? "";
    }
    private string getPublishplace()
    {
        return Request.QueryString["pub"] ?? "";
    }
    protected string CheckBoxFullText
    {
        get
        {
            string query = this.Request.Params["ck"];
            if (query == String.Empty)
                return "false";
            return query;
        }
    }
    protected string getField_Author
    {
        get
        {
            return Request.QueryString["filter_au"] ?? "";
        }
    }
    protected string getField_Publisher
    {
        get
        {
            return Request.QueryString["filter_ps"] ?? "";
        }
    }
    protected string getAuthor_count
    {
        get
        {
            return Request.QueryString["au_count"] ?? "";
        }
    }
    protected string getPublisher_count
    {
        get
        {
            return Request.QueryString["ps_count"] ?? "";
        }
    }
    protected string getField_GroupName
    {
        get
        {
            return Request.QueryString["filter_gn"] ?? "";
        }
    }
    protected string getField_GroupName_count
    {
        get
        {
            return Request.QueryString["gn_count"] ?? "";
        }
    }
    protected string getField_FileName
    {
        get
        {
            return Request.QueryString["filter_fn"] ?? "";
        }
    }
    protected string getField_FileName_count
    {
        get
        {
            return Request.QueryString["fn_count"] ?? "";
        }
    }
    protected string Limit(object Desc, int length)
    {
        StringBuilder strDesc = new StringBuilder();
        strDesc.Insert(0, Desc.ToString());

        if (strDesc.Length > length)
            return ReplaceSpecialChar(strDesc.ToString().Substring(0, length)) + "...";
        else
            return ReplaceSpecialChar(strDesc.ToString());
    }

    private string ReplaceSpecialChar(string desc)
    {
        return desc.Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace("<summary>", "summary").Replace("</summary>", "summary");
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

    protected string TagHtml(string input)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("<html>");
        stringBuilder.AppendLine(input);
        stringBuilder.AppendLine("</html>");
        //to get the html use
        var formatedHtml = stringBuilder.ToString();
        return formatedHtml;
    }
    protected string Summary
    {
        get
        {
            if (sf.total > 0)
            {
                LabelSummary2.Text = "Những cuốn sách sau có chứa cụm từ: " + "<b class='textbold'>" + this.SearchKeyword + "</b>";
                return "Trang <b class='textbold'>" + (sf.fromItem) + "</b> trong <b>" + sf.total + " kết quả " + "</b> (" + sf.duration.TotalSeconds + " giây)";
            }
            return "Không tìm thấy kết quả nào";
        }
    }

    public int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] != null)
                return Convert.ToInt32(ViewState["CurrentPage"]);
            else
                return 0;
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }


    /// <summary>
    /// How many pages are there in the results.
    /// </summary>
    private int PageCount
    {
        get
        {
            return (sf.total - 1) / sf.maxResults; // floor
        }
    }
    /// <summary>
    /// First item of the last page
    /// </summary>
    private int LastPageStartsAt
    {
        get
        {
            return PageCount * sf.maxResults;
        }
    }
    protected DataTable Paging
    {
        get
        {
            // pageNumber starts at 1
            int pageNumber = (sf.startAt + sf.maxResults - 1) / sf.maxResults;
            DataTable dt = new DataTable();
            dt.Columns.Add("html", typeof(string));

            DataRow ar = dt.NewRow();
            ar["html"] = PagingItemHtml(sf.startAt, pageNumber + 1, false);          
            dt.Rows.Add(ar);

            int previousPagesCount = 4;
            for (int i = pageNumber - 1; i >= 0 && i >= pageNumber - previousPagesCount + 2; i--)
            {
                int step = i - pageNumber;
                DataRow r = dt.NewRow();
                r["html"] = PagingItemHtml(sf.startAt + (sf.maxResults * step), i + 1, true);
                dt.Rows.InsertAt(r, 0);

            }
            int nextPagesCount = 4;
            for (int i = pageNumber + 1; i <= PageCount && i <= pageNumber + nextPagesCount - 2; i++)
            {
                int step = i - pageNumber;
                DataRow r = dt.NewRow();
                r["html"] = PagingItemHtml(sf.startAt + (sf.maxResults * step), i + 1, true);
                dt.Rows.Add(r);
            }

            if (CurrentPage == 0 && pageNumber == 0)
            {
                btnPrev.Visible = false;
            }
            else
            if (sf.total - CurrentPage < sf.maxResults)
            {
                btnNext.Visible = false;

            }
            if (sf.total <= sf.maxResults)
            {
                btnNext.Visible = false;
            }

            return dt;
        }
    }
    private string PagingItemHtml(int start, int number, bool active)
    {

        if (active)
            return "<a class='textpaging' href=\"fts?q=" + HttpUtility.HtmlEncode(this.SearchKeyword) + "&Field=" + ddlSearchFields.SelectedValue + "&start=" + start + "&ck=" + CheckBoxFullText + "&file=" + ddlSearchFieldsFile.SelectedValue + "&filter_au=" + getField_Author + "&filter_ps=" + getField_Publisher + "&au_count=" + getAuthor_count + "&ps_count=" + getPublisher_count + "&filter_gn=" + getField_GroupName + "&gn_count=" + getField_GroupName_count + "&filter_fn="+ getField_FileName + "&fn_count=" + getField_FileName_count + "\">" + number + "</a>";
        else
            return "<b class='textbold'>" + number + "</b>";
    }
    protected void ButtonSearch_Click(object sender, System.EventArgs e)
    {
        if (TxtSearch.Text != "")
        {
            Response.Redirect("fts?q=" + this.TxtSearch.Text + "&Field=" + ddlSearchFields.SelectedValue + "&start=0" + "&ck=" + CheckBoxFullText + "&file=" + ddlSearchFieldsFile.SelectedValue);
        }
        else
            TxtSearch.Text = SearchKeyword;
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        CurrentPage = CurrentPage - 10;
        Response.Redirect("fts?q=" + SearchKeyword + "&Field=" + ddlSearchFields.SelectedValue + "&start=" + CurrentPage + "&ck=" + CheckBoxFullText + "&file=" + ddlSearchFieldsFile.SelectedValue + "&filter_au=" + getField_Author + "&filter_ps=" + getField_Publisher + "&au_count=" + getAuthor_count + "&ps_count=" + getPublisher_count + "&filter_gn=" + getField_GroupName + "&gn_count=" + getField_GroupName_count + "&filter_fn=" + getField_FileName + "&fn_count=" + getField_FileName_count);
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        CurrentPage = CurrentPage + 10;
        Response.Redirect("fts?q=" + SearchKeyword + "&Field=" + ddlSearchFields.SelectedValue + "&start=" + CurrentPage + "&ck=" + CheckBoxFullText + "&file=" + ddlSearchFieldsFile.SelectedValue + "&filter_au=" + getField_Author + "&filter_ps=" + getField_Publisher + "&au_count=" + getAuthor_count + "&ps_count=" + getPublisher_count + "&filter_gn=" + getField_GroupName + "&gn_count=" + getField_GroupName_count + "&filter_fn=" + getField_FileName + "&fn_count=" + getField_FileName_count);
    }
    protected string CountWords(string S)
    {
        //if (S.Length == 0)
        //return null;
        if (!string.IsNullOrEmpty(S))
        {
            S = S.Trim();
            while (S.Contains("  "))
                S = S.Replace("  ", " ");
            this.TxtSearch.Text = S.TrimStart();
        }
        return S;
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
    public List<CartItem> Items { get; set; }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            string _issn = DataBinder.Eval(e.Item.DataItem, "Issn").ToString();
            string _isbn = DataBinder.Eval(e.Item.DataItem, "Isbn").ToString();
            string _dewey = DataBinder.Eval(e.Item.DataItem, "Dewey").ToString();
            int _grid = Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "RecordGroupID").ToString());
            string _file = DataBinder.Eval(e.Item.DataItem, "Filetype").ToString();
            string _RecordID = DataBinder.Eval(e.Item.DataItem, "RecordID").ToString();
            _Publisher = DataBinder.Eval(e.Item.DataItem, "Publisher").ToString();
            string _au = DataBinder.Eval(e.Item.DataItem, "Author_lnk").ToString();


            HyperLink link_dewey = (HyperLink)(e.Item.FindControl("HyperLink_dewey"));
            Label lb_isbn_issn = (Label)e.Item.FindControl("Lb_isbn_issn");
            Label lb_content = (Label)e.Item.FindControl("Lab_Content");
            Control au_div = e.Item.FindControl("author_div");
            Image im_open = (Image)e.Item.FindControl("Img_open_access");
            Label lb_file = (Label)e.Item.FindControl("spanfile");
            Image im_book = (Image)e.Item.FindControl("ImageBook");
            Repeater re_au = (Repeater)e.Item.FindControl("repLinks_au");
            LinkButton link_cart = (LinkButton)(e.Item.FindControl("LinkCart"));

            string[] split = _au.Split(new Char[] { ',', '.', ';', '\n', '\t' });
            re_au.DataSource = split;
            re_au.DataBind();

            if (Session["myCart"] == null)
            {                
                Session["myCart"] = new ShoppingCart();
            }
            myCart = (ShoppingCart)Session["myCart"];
            if (myCart.Items.Count == 0)
            {
                link_cart.Text = "<i class='fa fa-star''></i>" +"&nbsp;Lưu vào danh sách";
            }
            else
            {               
                foreach (var item in myCart.Items)
                {
                     if (string.Compare(_RecordID,item.RecordID.ToString())==0)
                     {
                         link_cart.Text = "<i class='fa fa-shopping-cart'></i>" + "&nbsp;Đã lưu";
                         link_cart.Enabled = false;
                     }                     
                }
               
            }           

            im_book.ImageUrl = ImageBook(_RecordID, _grid);//a();nh bia sach

            if (string.IsNullOrEmpty(_file))
            {
                lb_file.Visible = false;
            }

            if (CheckFullText.Checked)
            {
                lb_content.Visible = true;
                lb_content.Text = "<strong >Nội dung: </strong> " + DataBinder.Eval(e.Item.DataItem, "sample").ToString();

            }

            if (re_au.Items.Count == 0)
            {
                re_au.Visible = false;
            }

            if (_isbn != "" && _issn == "")
            {
                lb_isbn_issn.Text = "<strong>ISBN: </strong> " + _isbn;
            }
            else if (_isbn == "" && _issn != "")
                lb_isbn_issn.Text = "<strong >ISSN:</strong> " + _issn;
            else if (_isbn == "" || _issn == "")
            {
                lb_isbn_issn.Visible = false;
            }
            if (Repeater1.Items.Count > 0)
            {
                btnNext.Visible = true;
                btnPrev.Visible = true;
            }
            if (_dewey == "")
            {
                link_dewey.Visible = false;
            }
            if (_grid == 4)
            {
                im_open.Visible = true;
            }
        }
    }
    protected string ImageBook(string _RecordID, int _RecordGroupID)
    {

        {
            string result = null;
            if (UrlExists("http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg"))//neu hinh anh ton tai
            {
                return "http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg";
            }
            else
                switch (_RecordGroupID)
                {
                    case 1:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        break;
                    case 2:
                        result = "http://data.lib.hutech.edu.vn/BookImg/luanvan.jpg";
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(_Publisher))
                        {
                            if (_Publisher.ToLower().Contains("hutech"))
                            {
                                result = "http://data.lib.hutech.edu.vn/BookImg/gthutech.jpg";
                            }
                            else
                            {
                                result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";

                            }
                        }
                        else
                            result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        break;
                    case 7:
                        result = "http://data.lib.hutech.edu.vn/BookImg/magazine.jpg";
                        break;
                    case 8:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Baitrich.jpg";
                        break;
                    case 10:
                        result = "http://data.lib.hutech.edu.vn/BookImg/luanvan_ths.jpg";
                        break;
                    default:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        break;
                }
            return result;
        }

    }
    public string LimitLength(string orgText, int maxLength, string append)
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


    private List<SelectedList> ListToDropDownList()
    {
        var search_field_list = new
          List<SelectedList> {
                             new SelectedList {Text = "(-Tất cả-)", Value = "" } ,
                             new SelectedList {Text = "Nhan đề", Value = "ti"},
                             new SelectedList {Text = "Tác giả", Value =  "au"} ,
                             new SelectedList {Text = "Ký hiệu phân loại", Value = "ddc"} ,
                             new SelectedList {Text = "Nhà xuất bản", Value = "ps" },
                             new SelectedList {Text = "Nơi xuất bản", Value = "pp"} ,
                             new SelectedList {Text = "Năm xuất bản", Value = "py"} ,
                             new SelectedList {Text = "ISBN", Value = "nb" },
                             new SelectedList {Text = "ISSN", Value = "ns"} ,
                             new SelectedList {Text = "Chủ đề", Value ="su"} ,
                           };
        return search_field_list;
    }
    private List<SelectedList> ListFilterFile()
    {
        var search_field_list = new
          List<SelectedList> {
                             new SelectedList {Text = "(-Tất cả-)", Value = "" } ,
                             new SelectedList {Text = "Sách in", Value =  "(grid:1 OR grid:6)"},
                             new SelectedList {Text = "Tài liệu điện tử", Value =  "(filetype:pdf OR grid:4)"},
                             new SelectedList {Text = "Luận văn - Đồ án TN", Value = "(grid:2 OR grid:10 OR grid:11)"}
                           };
        return search_field_list;
    }
    private void DropListField()//droplist chọn từng field
    {
        ddlSearchFields.DataValueField = "Value";
        ddlSearchFields.DataTextField = "Text";
        ddlSearchFields.DataSource = ListToDropDownList();
        ddlSearchFields.DataBind();
        ddlSearchFields.SelectedValue = getSearchField;
    }

    private void DropListFieldFile()// droplist bộ sưu tập
    {
        ddlSearchFieldsFile.DataValueField = "Value";
        ddlSearchFieldsFile.DataTextField = "Text";
        ddlSearchFieldsFile.DataSource = ListFilterFile();
        ddlSearchFieldsFile.DataBind();
        ddlSearchFieldsFile.SelectedValue = getSearchFieldFile;
    }



    protected void CheckFullText_CheckedChanged(object sender, EventArgs e)
    {
        if (!CheckFullText.Checked)// tìm kiếm Marc
        {
            Response.Redirect("fts?ck=false");
        }
        else // tìm kiếm Full Text Search
        {
            Response.Redirect("fts?ck=true");
        }
    }
    protected string RecordGroupName(int grid)
    {
        switch (grid)
        {
            case 1:
                return "Sách tham khảo";
            case 2:
                return "Đồ án TN ĐH-CĐ";
            case 3:
                return "Sách điện tử";
            case 4:
                return "Tài liệu truy cập mở";
            case 5:
                return "Nghiên cứu khoa học";
            case 6:
                return "Sách giáo trình";
            case 7:
                return "Báo-Tạp chí";
            case 8:
                return "Bài trích";
            case 9:
                return "Sách tra cứu";
            case 10:
                return "Luận văn Thạc sĩ";
            case 11:
                return "Luận án Tiến sĩ";
            case 12:
                return "Đồ án môn học";
            default:
                return "";


        }
    }
    // xóa sạch và search kết quả về ban đầu
    protected void link_filter_Click(object sender, EventArgs e)
    {

        //kiểm tra các field nếu khác null thì xóa sạch và cho search kết quả về ban đầu
        string str = getField_Author + getField_Publisher + getField_GroupName + getField_FileName;       
        if (str.Length > 0)
        {           
            link_filter.Text = "";           
            Response.Redirect("fts?q=" + this.TxtSearch.Text + "&Field=" + ddlSearchFields.SelectedValue + "&start=0" + "&ck=" + CheckBoxFullText + "&file=" + ddlSearchFieldsFile.SelectedValue);
            
        }
                  
    }
    //add field chọn vào label
    private void Add_and_Clear_field()   
    {
        string str = getField_Author + getField_Publisher + getField_GroupName + getField_FileName;      
        if (str.Length > 0)
        {           
            link_filter.Text = String.Join(" ", str) + iconremove;
        }
        else           
        {
            iconremove = "";
        }       
    }    

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _RecordID = e.CommandArgument.ToString();
        if (Session["myCart"] == null)
        {
            myCart = new ShoppingCart();
            Session["myCart"] = myCart;

        }
        myCart = (ShoppingCart)Session["myCart"];
        if (e.CommandName == "AddProducts")
        {           
            
            DataTable dt = DataAccess.selectQuery(";WITH XMLNAMESPACES('http://www.loc.gov/MARC21/slim' as marc)" + "SELECT RecordID,ltrim(rtrim(CONVERT(nvarchar(2000),MarcXML.query('/marc:collection/marc:record/marc:datafield[@tag=''245''][1]/marc:subfield[@code=''a'']/text()')))) AS [Title], RecordGroupID  FROM psc_Records  WHERE RecordID =" + _RecordID);
            DataRow row = dt.Rows[0];
            myCart.Insert(new CartItem(Int32.Parse(_RecordID), "", row["Title"].ToString(),Convert.ToInt16(row["RecordGroupID"]),1));
            
            Response.Redirect(Request.Url.AbsoluteUri);
            
        }
        /*if (e.CommandName == "DeleteProducts")
        {
            myCart.Delete(int.Parse(_RecordID));
            Response.Redirect(Request.Url.AbsoluteUri);
        }*/
    }
    

}

