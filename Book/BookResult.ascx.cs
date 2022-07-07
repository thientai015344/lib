using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Configuration;
using System.Net;
using java.io;
using System.Threading.Tasks;

public partial class Book_BookResult : System.Web.UI.UserControl
{
    private SqlConnection cnn;
    private int PageSize = 10;
    private string rg;//chi so bo suu tap
    protected string sor;
    public String keyword;
    public String sub;
    public String au;
    public string pro;
    public string filter;
    // public string _RecordID;
    ShoppingCart myCart;
    SuggestSearch su = new SuggestSearch();
    private string constring = ConfigurationManager.ConnectionStrings["LibThuvienSearch"].ConnectionString;
    private string constringSearchUser = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = keyword + " Thư viện Đại học HUTECH";
            //Response.Cookies["count"].Value = car.GetCount.ToString();
            keyword = HttpUtility.HtmlDecode(Request.QueryString["q"]);
            String rgc = Request.QueryString["rag"];//chọn bộ sưu tập           
            String page = Request.QueryString["pg"];
            String sr = Request.QueryString["sort"];

            if (Request.QueryString["filter"] != null)
            {
                filter = Request.QueryString["filter"].ToString();
                switch (Convert.ToInt16(filter))
                {
                    case 0:
                        pro = "SearchResultAll";
                        break;
                    case 1:
                        pro = "SearchResultsTitle";
                        break;
                    case 2:
                        pro = "SearchResultsAuthor";
                        break;
                    case 3:
                        pro = "SearchResultsSubject";
                        break;
                    case 4:
                        pro = "SearchResultsYear";
                        break;
                    case 5:
                        pro = "SearchResultsPublisher";
                        break;
                    case 6:
                        pro = "SearchResultsPublishPlace";
                        break;
                }

            }

            if (!IsPostBack)//load lan dau tien
            {
                sub = HttpUtility.HtmlDecode(Request.QueryString["sub"]);
                if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(rgc))// truong hop keyword va rgc co gia tri(từ khóa và bộ sưu tạp có giá trị khác rỗng)
                {

                    ImageLogo.Visible = true;

                    if (string.IsNullOrEmpty(page) && string.IsNullOrEmpty(sub))// page,sub  chưa có giá trị 
                    {
                        this.GetResult(1, keyword.Trim(), Convert.ToInt16(rgc), Convert.ToInt16(sr), "", pro, Convert.ToInt16(filter));
                        //Insert_Log(keyword);

                        TextBox_search.Text = null;
                        TextBox_search.Text = CountWords(keyword);
                        ImageLogo.Visible = true;
                        if (sub != null)
                        {
                            // Label_chude.Text = "Chủ đề: ";
                        }
                        else
                        {
                            Label_loctheo.Visible = false;
                        }
                        LalObject.Text = sub;
                        MOJ.GetKeyword = TextBox_search.Text;
                        MOJ.GetRag = Convert.ToInt16(rgc);
                        Page.Title = keyword + " - Thư viện Đại học HUTECH";

                    }
                    else
                        if (string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(sub))// page chưa có giá trị, sub đã có giá trị
                    {
                        this.GetResult(1, keyword.Trim(), Convert.ToInt16(rgc), Convert.ToInt16(sr), sub, pro, Convert.ToInt16(filter));
                        TextBox_search.Text = null;
                        TextBox_search.Text = CountWords(keyword);

                        if (sub != null)
                        {
                            //Label_chude.Text = "Chủ đề: ";
                        }
                        else
                        {
                            Label_loctheo.Visible = false;
                        }
                        LalObject.Text = sub;
                        MOJ.GetKeyword = TextBox_search.Text;
                        MOJ.GetRag = Convert.ToInt16(rgc);
                        Page.Title = keyword + " - Thư viện Đại học HUTECH";

                    }
                    else//da chon trang                       
                            if (!string.IsNullOrEmpty(page) && !string.IsNullOrEmpty(sub))// page và sub điều có giá trị
                    {

                        this.GetResult(Convert.ToInt16(page), keyword.Trim(), Convert.ToInt16(rgc), Convert.ToInt16(sr), sub, pro, Convert.ToInt16(filter));
                        TextBox_search.Text = null;
                        TextBox_search.Text = CountWords(keyword);
                        if (sub == null)
                        {
                            Label_loctheo.Visible = false;

                        }
                        LalObject.Text = sub;
                        MOJ.GetKeyword = TextBox_search.Text;
                        MOJ.GetRag = Convert.ToInt16(rgc);
                        //MOJ.GetFilter = Convert.ToInt16(filter);
                        Page.Title = keyword + " - Thư viện Đại học HUTECH";
                    }
                    else
                                if (!string.IsNullOrEmpty(page) && string.IsNullOrEmpty(sub))// page điều có giá trị và sub không giá giá trị (hiển thị tất cả)
                    {

                        this.GetResult(Convert.ToInt16(page), keyword.Trim(), Convert.ToInt16(rgc), Convert.ToInt16(sr), "", pro, Convert.ToInt16(filter));
                        TextBox_search.Text = null;
                        TextBox_search.Text = keyword;
                        if (sub == null)
                        {
                            Label_loctheo.Visible = false;
                        }

                        LalObject.Text = sub;
                        MOJ.GetKeyword = TextBox_search.Text;
                        MOJ.GetRag = Convert.ToInt16(rgc);
                        Page.Title = keyword + " - Thư viện Đại học HUTECH";

                    }
                }

                else
                {
                    Label_loctheo.Visible = false;
                    hienthibosuutap();
                    rg = DDLListDocument.SelectedItem.Value;
                    filter = DropDown_Filter.SelectedItem.Value;
                    sor = DropDownSorting.SelectedItem.Value;
                    Panel_Menu.Visible = false;
                    MOJ.GetRag = Convert.ToInt16(rg);

                }
            }

            else
            {
                rg = DDLListDocument.SelectedItem.Value;
                filter = DropDown_Filter.SelectedItem.Value;
            }


        }
        catch (Exception)
        {

            hienthibosuutap();
            rg = DDLListDocument.SelectedItem.Value;
            filter = DropDown_Filter.SelectedItem.Value;
            Panel_Menu.Visible = false;
            TextBox_search.Text = keyword;
            Label_loctheo.Visible = false;
            Page.Title = keyword + " - Thư viện Đại học HUTECH";
            Label_thongtin.Text = "Trang 1 trong 0 kết quả";
            //Insert_Error(ex.Message);

        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        search();
    }
    public void search()
    {
        try
        {
            if (TextBox_search.Text != "")
            {
                if (rg == "" || rg == null)
                {
                    rg = "0";
                    filter = "0";
                }
                else
                {
                    string key = TextBox_search.Text.Trim();
                    Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(key) + "&rag=" + rg + "&filter=" + filter);
                    MOJ.GetRag = Convert.ToInt16(rg);


                }
            }
            else
                Response.Redirect("/");

        }
        catch (Exception ex)
        {
            Label_thongtin.Text = ex.Message;
        }
    }
    public void hienthibosuutap()
    {
        
        cnn = new SqlConnection(constring);
        DataTable dt = new DataTable();
        cnn.Open();
        string query = "select RecordGroupID,RecordGroupName from psc_RecordGroups ";
        SqlDataAdapter da = new SqlDataAdapter(query, cnn);
        da.Fill(dt);

        DataRow dr = dt.NewRow();
        dr["RecordGroupID"] = 0;
        dr["RecordGroupName"] = "--Tất cả bộ sưu tập--";
        dt.Rows.Add(dr);

        DataView dv = dt.DefaultView;
        dv.Sort = "RecordGroupID";
        DDLListDocument.DataSource = dv;
        DDLListDocument.DataValueField = dt.Columns[0].Caption;
        DDLListDocument.DataTextField = dt.Columns[1].Caption;
        DDLListDocument.DataBind();
        cnn.Close();

    }

    private string CountWords(string S)
    {
        // if (S.Length == 0)
        //return 0;

        S = S.Trim();
        while (S.Contains("  "))
            S = S.Replace("  ", " ");
        TextBox_search.Text = S.TrimStart();
        return S;
    }

    private static string CountWords2(string S)// cắt bớt khoảng cách để ký tự gần hơn
    {

        S = S.Trim();
        while (S.Contains("  "))
            S = S.Replace("  ", "");
        return S.TrimStart();
    }

    private void GetResult(int pageIndex, String keys, int selectBST, int numberSort, string subject, string procdu, int filter_colum)
    {
        try
        {

            
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constringSearchUser))
            {
                using (SqlCommand cmd = new SqlCommand(procdu, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.AddWithValue("@keyword",CountWords2(keys).Replace(" ", "&"));
                    //cmd.Parameters.AddWithValue("@keyword",keys);
                    cmd.Parameters.AddWithValue("@bst", selectBST);
                    cmd.Parameters.AddWithValue("@subject", subject);
                    //cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.AddWithValue("@SortCol", numberSort);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 10);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    IDataReader idr = cmd.ExecuteReader();
                    dt.Load(idr);
                    rptBooks.DataSource = dt;
                    rptBooks.DataBind();
                    MenuAuthor(dt);// lấy cột tacgia từ bảng làm menu
                    MenuSubject(dt);
                    MenuYear(dt);
                    idr.Close();
                    con.Close();
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    hienthibosuutap();
                    DDLListDocument.SelectedIndex = selectBST;//ghi nhận vị trí thuộc tính đã chọn
                    DropDown_Filter.SelectedIndex = filter_colum; //ghi nhận vị trí thuộc tính đã chọn
                    DropDownSorting.SelectedIndex = Convert.ToInt16(Request.QueryString["sort"]);
                    sor = DropDownSorting.SelectedItem.Value;
                    checkResult(recordCount);
                    this.PopulatePager(recordCount, pageIndex);
                    Labelsapxep.Visible = true;
                    DropDownSorting.Visible = true;
                    //ds.Load(idr,LoadOption.OverwriteChanges,"*");

                }
            }
        }
        catch 
        {
            //Label_thongtin.Text = ex.Message;
            Label_thongtin.Text = "Trang 1 trong 0 kết quả";
            Panel_Menu.Visible = false;
            hienthibosuutap();//hiển thị lại bộ sưu tập
            DropDown_Filter.SelectedIndex = filter_colum;// search khong co kq thì vẫn hiển thị đúng vị trí đã chọn
        }

    }

    /* private void PopulatePager(int recordCount, int currentPage)//hien thi 10 ket qua
     {
         double dblPageCount = (double)((decimal)recordCount / (decimal)PageSize);
         int pageCount = (int)Math.Ceiling(dblPageCount);
         List<ListItem> pages = new List<ListItem>();
          
         Label_thongtin.Text = "Trang " + currentPage + " trong " + recordCount + " kết quả" ;
         page = currentPage.ToString();
          
        // Response.Redirect(string.Format("Default.aspx?pageIndex={0}&pageSize={1}"));

         if (pageCount > 0)
         {
            // pages.Add(new ListItem("đầu", "1", currentPage > 1));
             if (currentPage != 1)
             {
                 pages.Add(new ListItem("Trước ", (currentPage - 1).ToString()));
             }
             if (pageCount < 10)
             {
                 for (int i = 1; i <= pageCount; i++)
                 {
                     pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                 }
             }
             else if (currentPage < 10)//pageCount > 10
             {
                 for (int i = 1; i <= 10; i++)
                 {
                     pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                 }
                 //pages.Add(new ListItem("   ", (currentPage).ToString(), false));
             }
             else if (currentPage > pageCount - 4)//cộng 4 số tiếp theo
             {
                // pages.Add(new ListItem("   ", (currentPage).ToString(), false));
                 for (int i = currentPage - 1; i <= pageCount; i++)
                 {
                     pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                 }
             }
             else
             {
                 //pages.Add(new ListItem("   ", (currentPage).ToString(), false));
                 for (int i = currentPage - 5; i <= currentPage + 4; i++)
                 {
                     pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                 }
                 //pages.Add(new ListItem("   ", (currentPage).ToString(), false));
             }
             if (currentPage != pageCount)
             {
                 pages.Add(new ListItem(" Tiếp", (currentPage + 1).ToString()));
             }
            // pages.Add(new ListItem("cuối", pageCount.ToString(), currentPage < pageCount));
         }
         rptPager.DataSource = pages;
         rptPager.DataBind();
         //Response.Redirect("Default.aspx?q=" + TextBox_search.Text + "&rg=" + rg + "&pg=" + currentPage);
     }*/
    private void Insert_Log(String _keywork)
    {
        
        using (SqlConnection con = new SqlConnection(constringSearchUser))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Proc_Search_Log]", con))
            {
                // string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });

                // String ecn = System.Environment.MachineName;
                // txtECN.Text = computer_name[0].ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@keyword", _keywork.Trim());
                // cmd.Parameters.AddWithValue("@keywork", keywork);
                cmd.Parameters.AddWithValue("@ip", Request.UserHostAddress);
                //cmd.Parameters.AddWithValue("@PCName", Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName); 
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

    }
    public string DetermineCompName(string IP)
    {
        IPAddress myIP = IPAddress.Parse(IP);
        IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
        return compName.First();
    }
    public string GetComputerName(string clientIP)
    {
        try
        {
            var hostEntry = Dns.GetHostEntry(clientIP);
            return hostEntry.HostName;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    private void Insert_Error(String _error)
    {
       
        using (SqlConnection con = new SqlConnection(constringSearchUser))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Proc_Search_Error]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;
                cmd.Parameters.AddWithValue("@Error", _error); con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }

    private void PopulatePager(int recordCount, int currentPage)//hien thi 5 ket qua
    {
        double dblPageCount = (double)((decimal)recordCount / (decimal)PageSize);
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();

        Label_thongtin.Text = "Trang " + currentPage + " trong " + recordCount + " kết quả";
        // page = currentPage.ToString();

        // Response.Redirect(string.Format("Default.aspx?pageIndex={0}&pageSize={1}"));

        if (pageCount > 0)
        {
            // pages.Add(new ListItem("|<<", "1", currentPage > 1)); //ve dau trang
            if (currentPage != 1)
            {
                pages.Add(new ListItem("Trước ", (currentPage - 1).ToString()));
            }
            if (pageCount < 5)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            else if (currentPage < 5)//pageCount > 10
            {
                for (int i = 1; i <= 5; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //pages.Add(new ListItem("   ", (currentPage).ToString(), false));
            }
            else if (currentPage > pageCount - 2)//cộng 2 số tiếp theo
            {
                // pages.Add(new ListItem("   ", (currentPage).ToString(), false));
                for (int i = currentPage - 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            else
            {
                //pages.Add(new ListItem("   ", (currentPage).ToString(), false));
                for (int i = currentPage - 2; i <= currentPage + 2; i++)//hien thi 2 vi tri dau va 2 vi tri cuoi
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                //pages.Add(new ListItem("   ", (currentPage).ToString(), false));
            }
            if (currentPage != pageCount)
            {
                pages.Add(new ListItem(" Tiếp", (currentPage + 1).ToString()));
            }
            //pages.Add(new ListItem(">>|", pageCount.ToString(), currentPage < pageCount)); //ve cuoi trang
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
        if (Convert.ToInt16(Request.QueryString["pg"]) > pageCount)//trường hợp nhập số trang trên thanh address lớn hơn số trang hiện tại
        {
            //Response.Redirect("Default.aspx");
            sor = DropDownSorting.SelectedItem.Value;
            rg = DDLListDocument.SelectedItem.Value;
            filter = DropDown_Filter.SelectedItem.Value;
            if (string.IsNullOrEmpty(sub))
                Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageCount + "&sort=" + sor);
            else
                if (!string.IsNullOrEmpty(sub))
                Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageCount + "&sort=" + sor + "&sub=" + HttpUtility.UrlEncode(sub));

        }

        //Response.Redirect("Default.aspx?q=" + TextBox_search.Text + "&rg=" + rg + "&pg=" + currentPage);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        sor = DropDownSorting.SelectedItem.Value;
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        if (string.IsNullOrEmpty(sub))// đã chọn chủ đề
            Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageIndex + "&sort=" + sor + "&filter=" + filter);
        else
            if (!string.IsNullOrEmpty(sub)) // chưa chọn chủ đề
            Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageIndex + "&sort=" + sor + "&filter=" + filter + "&sub=" + HttpUtility.UrlEncode(sub));

        this.GetResult(pageIndex, keyword, Convert.ToInt16(DDLListDocument.SelectedItem.Value.ToString().Trim()), 2, LalObject.Text, pro, Convert.ToInt16(DropDown_Filter.SelectedItem.Value.ToString().Trim()));
        TextBox_search.Text = keyword;
    }

    public string HighlightText(string InputTxt)
    {
        //string key = TextBox_search.Text.Trim();


        string Search_Str = HttpUtility.UrlDecode(keyword);
        // string Search_Str = key;
        // Setup the regular expression and add the Or operator.
        Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
        // Highlight keywords by calling the
        //delegate each time a keyword is found.
        return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
        // Set the RegExp to null.
        //RegExp = null;
    }
    public string ReplaceKeyWords(Match m)
    {
        return "<span class=highlight>" + m.Value + "</span>";
    }
    protected void rptBooks_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {

            System.Web.UI.WebControls.Image im = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImageBook");
            string imagename = DataBinder.Eval(e.Item.DataItem, "RecordID").ToString();
            System.Web.UI.WebControls.Label loaitailieu = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblDocument");
            int get_loaitailieu = int.Parse(DataBinder.Eval(e.Item.DataItem, "Nhomtailieu").ToString());                      
           
            System.Web.UI.WebControls.Label xb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbNamXB");
            string _nxb = DataBinder.Eval(e.Item.DataItem, "Nhà XB").ToString();
            System.Web.UI.WebControls.HyperLink lnkTile = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("hplBookName");
            System.Web.UI.WebControls.HyperLink lnkdewey = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HyperLinkDewey");
            System.Web.UI.WebControls.Label LabISBN = (System.Web.UI.WebControls.Label)e.Item.FindControl("LabelISBN");          
            System.Web.UI.WebControls.Repeater repe = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("repLinks");
            string Title = DataBinder.Eval(e.Item.DataItem, "Nhan đề").ToString();
            string dewey = DataBinder.Eval(e.Item.DataItem, "Phân loại").ToString();
            string author = DataBinder.Eval(e.Item.DataItem, "Tacgia100").ToString();
            string Sachtrongkho = DataBinder.Eval(e.Item.DataItem, "Trong kho").ToString();
            string isbn = DataBinder.Eval(e.Item.DataItem, "isbn").ToString();

            if (string.IsNullOrEmpty(isbn))
            {
                LabISBN.Visible = false;
            }
            else
            {
                string script = @"<tr><td><strong> ISBN: </strong>" + isbn + " </td></tr>";
                LabISBN.Text = "<html>" + script + "</html>";
            }

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
            

            System.Web.UI.WebControls.Label sachbandau = (System.Web.UI.WebControls.Label)e.Item.FindControl("Labe_trongkho");
            System.Web.UI.WebControls.Label sachdangmuon = (System.Web.UI.WebControls.Label)e.Item.FindControl("Labe_dangmuon");

            System.Web.UI.WebControls.Label sachbandau_xs = (System.Web.UI.WebControls.Label)e.Item.FindControl("Labe_trongkho_xs");
            System.Web.UI.WebControls.Label sachdangmuon_xs = (System.Web.UI.WebControls.Label)e.Item.FindControl("Labe_dangmuon_xs");

            if (get_loaitailieu == 1 || get_loaitailieu == 6)
            {
                if (dewey.Length > 7)
                {

                    //lposi.NavigateUrl = "http://data.lib.hutech.edu.vn/bs/bookshelf.aspx?dw=" + dewey.Substring(0, 7) + "&Title=" + HttpUtility.UrlEncode(DataBinder.Eval(e.Item.DataItem, "Nhan đề").ToString());


                }
                else
                {
                    //  lposi.NavigateUrl = "http://data.lib.hutech.edu.vn/bs/bookshelf.aspx?dw=" + dewey + "&Title=" + HttpUtility.UrlEncode(DataBinder.Eval(e.Item.DataItem, "Nhan đề").ToString());

                }
                sachbandau.Visible = true;
                sachdangmuon.Visible = true;

                sachbandau_xs.Visible = true;
                sachdangmuon_xs.Visible = true;

            }
            else
            {
                // lposi.Visible = false;
                // ldangkysach.Visible = false;
                sachbandau.Visible = false;
                sachdangmuon.Visible = false;

                sachbandau_xs.Visible = false;
                sachdangmuon_xs.Visible = false;

            }
            // link sach cung loai
            if (!string.IsNullOrEmpty(dewey))
            {
                if (dewey.Length > 6)
                {

                    lnkdewey.NavigateUrl = "/tim-kiem?q=" + dewey.Substring(0, 6).Replace("/", "") + "&rag=0&filter=0";
                }
                else
                {
                    lnkdewey.NavigateUrl = "/tim-kiem?q=" + dewey.Replace("/", "") + "&rag=0&filter=0";
                }
            }
            else
            {
                lnkdewey.Visible = false;
            }
            // tinh trang sach khong kho

            if (!string.IsNullOrEmpty(Sachtrongkho))
            {

                sachbandau.Text = "- Số bản có sẵn: " + Sachtrongkho;
                sachdangmuon.Text = "- Đang mượn: " + DataBinder.Eval(e.Item.DataItem, "Đang mượn").ToString();

                sachbandau_xs.Text = "- Số bản có sản: " + Sachtrongkho;
                sachdangmuon_xs.Text = "- Đang mượn: " + DataBinder.Eval(e.Item.DataItem, "Đang mượn").ToString();

               /* if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "Còn lại")) <= 1)
                {
                    sachbandau.Text = "*Sách đọc tại chỗ";
                    sachbandau.ForeColor = System.Drawing.ColorTranslator.FromHtml("#547A44");
                    sachdangmuon.Visible = false;

                    sachbandau_xs.Text = "*Sách đọc tại chỗ";
                    sachbandau_xs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#547A44");
                    sachdangmuon_xs.Visible = false;
                }
                else
                    if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "Còn lại")) > 1)
                {
                    sachbandau.ToolTip = "Sách được mượn về";
                    sachbandau.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003366");

                    sachbandau_xs.ToolTip = "Sách được mượn về";
                    sachbandau_xs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003366");
                }*/
                // sachconlai.Text = "còn lại: " + DataBinder.Eval(e.Item.DataItem, "Còn lại").ToString();
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



    protected void DDLListDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        rg = DDLListDocument.SelectedItem.Value;//chon bo suu tap
        MOJ.GetRag = Convert.ToInt16(rg);

    }
    public static bool IsNumeric(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c) && c != '.')
            {
                return false;
            }
        }

        return true;
    }
    public static string[] SplitWhitespace(string input)
    {
        char[] whitespace = new char[] { ' ', '\t' };
        return input.Split(whitespace);
    }

    protected void TextBox_search_TextChanged(object sender, EventArgs e)
    {
        search();
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

    /*private string addlink(string input)
    {
        
        string[] strs = input.Split(',');
        ArrayList alStr = new ArrayList();
        foreach (string ss in strs)
        {
           return ss;
        }
       // return sb.ToString();
        
    }*/

    protected void DropDownSorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        sor = DropDownSorting.SelectedItem.Value;
        if (string.IsNullOrEmpty(sub))
            Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(TextBox_search.Text) + "&rag=" + rg + "&sort=" + sor + "&filter=" + filter);
        else
            if (!string.IsNullOrEmpty(sub))
            Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(TextBox_search.Text) + "&rag=" + rg + "&sort=" + sor + "&sub=" + sub + "&filter=" + filter);
    }
    protected void DataList1_DataBinding(object sender, EventArgs e)
    {

    }
    protected void DataList1_Load(object sender, EventArgs e)
    {

    }
    private void checkResult(int resu)//kiem tra ket qua de an hien panel
    {
        if (resu == 0)
        {
            Panel_Menu.Visible = false;
            DropDownSorting.Visible = false;
            Labelsapxep.Visible = false;
        }
        else
        {
            Panel_Menu.Visible = true;
            DropDownSorting.Visible = true;
            Labelsapxep.Visible = true;
        }
    }
    private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

    public static string LocDau(string str)
    {
        //Thay thế và lọc dấu từng char      
        for (int i = 1; i < VietNamChar.Length; i++)
        {
            for (int j = 0; j < VietNamChar[i].Length; j++)
                str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
        }
        return str;
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

    protected void LalObject_Click(object sender, EventArgs e)
    {
        LalObject.Text = string.Empty;
        MOJ.GetKeyword = TextBox_search.Text;
        Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&filter=" + filter);
    }
    protected void ImageButton_clear_Click(object sender, ImageClickEventArgs e)
    {
        LalObject.Text = string.Empty;
        MOJ.GetKeyword = TextBox_search.Text;
        Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&filter=" + filter);
    }
    protected void AddnewLinkcontrol(string _textSubject)
    {
        LinkButton l_subject = new LinkButton();
        l_subject.ID = "_sub";
        l_subject.Text = _textSubject;

        l_subject.OnClientClick += new EventHandler(l_subject_Click);
        DivControl.Controls.Add(l_subject);
        l_subject = null;


    }
    void l_subject_Click(object sender, EventArgs e)
    {

        //Label_chude.Text="Link button Click Event";
        LinkButton clickedControl = (LinkButton)sender;
        if (clickedControl.ID == "_sub")
        {
            //Label_chude.Text = "Link button Click Event";
            // MOJ.GetKeyword = TextBox_search.Text;
            // Response.Redirect("Default.aspx?q=" + HttpUtility.UrlEncode(CountWords(keyword)) + "&rag=" + rg);
        }
    }

    public void FormatUrls(string input, HyperLink linkauthor)
    {
        //string[] wordArray = lineOfText.Split(',');
        //string output = input;
        /*string[] split = input.Split(new Char[] {',',';', '\t' });

        foreach (string s in split)
        {
            //if (s.Trim() != "")
            {
                linkauthor.Text +=  s +", ";
                linkauthor.NavigateUrl = string.Format("~/Default.aspx?q={0}&rag=0", HttpUtility.UrlEncode(s));                //linkauthor.Text = s ;
            }
                //NavigateUrl= '<%# Eval("[Phân loại]", "~/Default.aspx?q={0}&rag=0") %>'            
        }
        
        //return output;*/


    }

    private void MenuAuthor(DataTable dt)
    {
        dt = dt.AsEnumerable()               
               .GroupBy(r => new { Tacgia100 = r["Tacgia100"] })
               .Select(g => g.OrderBy(r => r["Tacgia100"]).First())
               .CopyToDataTable();

        DataView dv = new DataView(dt);
        dv.RowFilter = "Tacgia100 <> ''";
        //dv.RowFilter = "[Chủ đề] <> ''";
        // dv.Sort = "Tacgia100 ASC";
        MOJ.GetDataAuthor = dv.ToTable();
    }
    private void MenuSubject(DataTable dt)
    {
        dt = dt.AsEnumerable()
               .GroupBy(r => new { subject = r["Chủ đề"] })
               .Select(g => g.OrderBy(r => r["Chủ đề"]).First())
               .CopyToDataTable();

        DataView dv = new DataView(dt);
        dv.RowFilter = "[Chủ đề] <> ''";
        // dv.Sort = "Tacgia100 ASC";
        MOJ.GetDataSubject = dv.ToTable();
    }
    private void MenuYear(DataTable dt)
    {
        dt = dt.AsEnumerable()
               .GroupBy(r => new { subject = r["Năm XB"] })
               .Select(g => g.OrderBy(r => r["Năm XB"]).First())
               .CopyToDataTable();

        DataView dv = new DataView(dt);
        dv.RowFilter = "[Năm XB] <> ''";
        dv.Sort = "[Năm XB] ASC";
        MOJ.GetDataYear = dv.ToTable();
    }



    protected void DropDown_Filter_SelectedIndexChanged(object sender, EventArgs e)
    {
        filter = DropDown_Filter.SelectedItem.Value;

    }

    protected void rptBooks_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "AddProducts")
        {
            // Determine the CategoryID
            int _RecordID = Convert.ToInt32(e.CommandArgument);
            if (Session["myCart"] == null)
            {
                myCart = new ShoppingCart();
                Session["myCart"] = myCart;

            }
            myCart = (ShoppingCart)Session["myCart"];
            DataTable dt = DataAccess.selectQuery(";WITH XMLNAMESPACES('http://www.loc.gov/MARC21/slim' as marc)" + "SELECT RecordID,ltrim(rtrim(CONVERT(nvarchar(2000),MarcXML.query('/marc:collection/marc:record/marc:datafield[@tag=''245''][1]/marc:subfield[@code=''a'']/text()')))) AS [Title]  FROM psc_Records WHERE RecordID =" + _RecordID);
            DataRow row = dt.Rows[0];
            myCart.Insert(new CartItem(_RecordID, "http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg", row["Title"].ToString(), 1,1));

            //Response.Redirect(Request.Url.AbsoluteUri);
            Response.Redirect("/gio-tai-lieu");
        }
    }



}

