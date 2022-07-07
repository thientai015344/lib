using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Services;
using System.Text;
using AjaxControlToolkit;
public partial class test : System.Web.UI.Page
{
    string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    PagedDataSource objPage = new PagedDataSource();
    int _time;
    string _lv;
    private int PageSize = 10;
    //public string _selectDay;
    int _pageIndex;
    DateTime currentDateTime = new DateTime(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        currentDateTime= DateTime.Now;
        if (!IsPostBack)
        {

            //DRLV.Items.Clear();            
            Comno_linhvuc();
            DRLV.SelectedIndex = LVIndex;
            CurrentDate = currentDateTime.ToString("dd/MM/yyyy");
            CurrentDewey = "0";
            CurrentCheckFile = 0;            
            GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey, CurrentCheckFile);
           
        }
        else
        {

            //Response.Redirect(Request.Url.AbsoluteUri);
            
        }
        
    }
    private DateTime ConvertToDateTime(string strDateTime)
    {
        DateTime dtFinaldate; string sDateTime;
        try { dtFinaldate = Convert.ToDateTime(strDateTime); }
        catch (Exception e)
        {
            string[] sDate = strDateTime.Split('/');
            sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
            dtFinaldate = Convert.ToDateTime(sDateTime);
        }
        return dtFinaldate;
    }
    private void Comno_linhvuc()
    {
        using (SqlConnection con = new SqlConnection(constring))
        {
            try
            {
                DataTable dt = new DataTable();
                con.Open();
                string query = "select distinct * from TV_Linhvuc";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                DRLV.DataSource = dt.DefaultView;
                DRLV.DataTextField = dt.Columns[1].Caption;
                DRLV.DataValueField = dt.Columns[0].Caption;
                DRLV.DataBind();
                con.Close();
                SortDDL(DRLV);
                
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    
                    con.Close();
                }
                con.Dispose();
            }
        }
    }
    private void SortDDL(DropDownList objDDL)
    {
        ArrayList textList = new ArrayList();
        ArrayList valueList = new ArrayList();
        foreach (ListItem li in objDDL.Items)
        {
            textList.Add(li.Text);
        }
        textList.Sort();
        foreach (object item in textList)
        {
            string value = objDDL.Items.FindByText(item.ToString()).Value;
            valueList.Add(value);
        }
        objDDL.Items.Clear();
        for (int i = 0; i < textList.Count; i++)
        {
            ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
            objDDL.Items.Add(objItem);
        }
    }
    public int LVIndex
    {

        get
        {
            if (this.ViewState["LVIndex"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["LVIndex"].ToString());
        }

        set
        {
            this.ViewState["LVIndex"] = value;
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
    public int CurrentCheckFile
    {

        get
        {
            if (this.ViewState["CurrentCheckFile"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["CurrentCheckFile"].ToString());
        }

        set
        {
            this.ViewState["CurrentCheckFile"] = value;
        }

    }
    public string CurrentDate
    {

        get
        {
            if (this.ViewState["CurrentDate"] == null)
                return null;
            else
                return this.ViewState["CurrentDate"].ToString();
        }

        set
        {
            this.ViewState["CurrentDate"] = value;
        }

    }
    public string CurrentDewey
    {

        get
        {
            if (this.ViewState["CurrentDewey"] == null)
                return null;
            else
                return this.ViewState["CurrentDewey"].ToString();
        }

        set
        {
            this.ViewState["CurrentDewey"] = value;
        }

    }
    public DataTable GetBookData(int pageIndex, string StartDate, string EndDate, string phanloai, int ltailieu)
    {
        
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("TV_sp_Sachmoi", con))
            {
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@phanloai", phanloai);
                cmd.Parameters.AddWithValue("@loaitailieu", ltailieu);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", 10);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                con.Open();
                
                DataTable dt = new DataTable();
                dt.Clear();
                IDataReader idr = cmd.ExecuteReader();
                dt.Load(idr);
                RData.DataSource = dt;
                RData.DataBind();
                int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                this.PopulatePager(recordCount, pageIndex);
                idr.Close();
                con.Close();                
                return dt;
                
            }
        }
    }
    private void PopulatePager(int recordCount, int currentPage)//hien thi 5 ket qua
    {
        double dblPageCount = (double)((decimal)recordCount / (decimal)PageSize);
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();

        Label_thongtin.Text = "Kết quả: " + recordCount + " nhan đề";
        // page = currentPage.ToString();

        // Response.Redirect(string.Format("Default.aspx?pageIndex={0}&pageSize={1}"));

        if (pageCount > 0)
        {
            //pages.Add(new ListItem("|<<", "1", currentPage > 1)); ve dau trang
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
            //pages.Add(new ListItem(">>|", pageCount.ToString(), currentPage < pageCount)); ve cuoi trang
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();

        //Response.Redirect("Default.aspx?q=" + TextBox_search.Text + "&rg=" + rg + "&pg=" + currentPage);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        
        CurrentPage = int.Parse((sender as LinkButton).CommandArgument);        
        GetBookData(CurrentPage, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
        
        /* sor = DropDownSorting.SelectedItem.Value;
         int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

         if (string.IsNullOrEmpty(sub))// đã chọn chủ đề
             Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageIndex + "&sort=" + sor + "&filter=" + filter);
         else
             if (!string.IsNullOrEmpty(sub)) // chưa chọn chủ đề
             Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageIndex + "&sort=" + sor + "&filter=" + filter + "&sub=" + HttpUtility.UrlEncode(sub));

         this.GetResult(pageIndex, keyword, Convert.ToInt16(DDLListDocument.SelectedItem.Value.ToString().Trim()), 2, LalObject.Text, pro, Convert.ToInt16(DropDown_Filter.SelectedItem.Value.ToString().Trim()));
         TextBox_search.Text = keyword;*/

    }
    protected void DropDown_time_SelectedIndexChanged(object sender, EventArgs e)
    {
        _time = int.Parse(DropDown_time.SelectedItem.Value);
        DRLV.SelectedIndex = 0;// đưa lĩnh vực về trạng thái hiển thị tất cả
        if (Check_haveFile.Checked)
        {
            Check_haveFile.Checked = false;
        }
       
        switch (_time)
        {
            case 0:
                {
                    CurrentDate =DateTime.Now.ToString("dd/MM/yyyy");                   
                    GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
                }
                break;
            case 1:
                {
                    CurrentDate = currentDateTime.AddDays(-7).ToString("dd/MM/yyyy");                
                    GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
                }
                break;
            case 2:
                {
                    CurrentDate = currentDateTime.AddDays(-30).ToString("dd/MM/yyyy");                    
                    GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);                   
                   
                }
                break;
            case 3:
                {
                    CurrentDate = currentDateTime.AddDays(-90).ToString("dd/MM/yyyy");                   
                    GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
                    
                }
                break;
            case 4:
                {
                    CurrentDate = currentDateTime.AddDays(-365).ToString("dd/MM/yyyy");                  
                    GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
                    
                }
                break;

        }
    }
    protected void DRLV_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Response.Redirect("NewBookInfo.aspx", false);
        CurrentDewey = DRLV.SelectedItem.Value.ToString();
       
        GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
        if (Check_haveFile.Checked)
        {
            Check_haveFile.Checked = false;
        }
        LVIndex = DRLV.SelectedIndex;
    }

    protected void Check_haveFile_CheckedChanged(object sender, EventArgs e)
    {
        if(Check_haveFile.Checked)
        {
            CurrentCheckFile = 1;
            GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
        }
        else
        {
            CurrentCheckFile = 0;
            GetBookData(1, CurrentDate, currentDateTime.ToString("dd/MM/yyyy"), CurrentDewey.Trim(), CurrentCheckFile);
        }
    }

    protected void RData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            Image im = (Image)e.Item.FindControl("ImageBook");
            string imagename = DataBinder.Eval(e.Item.DataItem, "RecordID").ToString();
            int get_loaitailieu = int.Parse(DataBinder.Eval(e.Item.DataItem, "Nhomtailieu").ToString());
            Repeater repe = (Repeater)e.Item.FindControl("repLinks");
            string author = DataBinder.Eval(e.Item.DataItem, "Tác giả").ToString().Replace("[edited by]", "").Trim();
            string _nxb = DataBinder.Eval(e.Item.DataItem, "Nhà XB").ToString();
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
   
}