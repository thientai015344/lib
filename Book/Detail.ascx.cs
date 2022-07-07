using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using AjaxControlToolkit;
using System.IO;
using System.Web.Security;
using System.Security.Cryptography;

public partial class Book_Detailt : System.Web.UI.UserControl
{
   
    public string Getsachbandau { get; set; }
    public string Getsachdangmuon { get; set; }
    string _nhande;
    string _tacga;   
    string _nxb;
    string _namxb;
    string _mota;
    string _lanxb;
    string _svideo;
    string _subject;
    string _dewey;
    string _dewey090;
    string _linkoutsite;
    int _nhomtailieu;
    string _RecordID;
    String[] split_subject = new String[] { ",", ";", ".", "\n" };
    String[] split_Tomtat = new String[] { ";", "<\br>" };
    ShoppingCart myCart;
    public LibProcess pro = new LibProcess();
     
    string constring = ConfigurationManager.ConnectionStrings["VitrisachConn"].ConnectionString;
    string constr = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
    string constringSearch = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        _RecordID = Request.QueryString["id"];
        if (!IsPostBack)
        {
            GetstatusCart();
            if (!String.IsNullOrEmpty(_RecordID))
            {
                foreach (DataRow r in GetInfo(int.Parse(_RecordID)).Rows)
                {
                    Title.Text = r[0].ToString().Replace("/", "");
                    // Author.Text = r[2].ToString();
                    Lab_bst.Text = r[1].ToString();
                    Session["session_bst"] = r[1].ToString();
                    isbn.Text = r[3].ToString();
                    Languages.Text = r[4].ToString();
                    Dewey.Text = r[5].ToString();
                    Dewey.NavigateUrl = "/fts?q=ddc:" + Dewey.Text + "&ck=false";
                    InfoPublisher.Text = r[6].ToString() + " " + r[7].ToString() + ", " + r[9].ToString();
                    Description.Text = r[8].ToString();
                    // _subject = r[12].ToString().Replace(";", "\n" + "->").Trim();
                    _subject = r[12].ToString();
                    //subject.InnerText = "- " + _subject.Remove(_subject.Length - 1);
                    subject.InnerText = _subject;
                    Tomtat.Text = r[13].ToString().Replace(";", "<br/>");
                    BookID.Text = _RecordID;
                    _nhande = r[0].ToString().Replace("/", "");
                    _tacga = r[2].ToString();
                    _dewey = r[5].ToString();

                    _nxb = r[6].ToString();// noixb va nhaxb
                    _namxb = r[7].ToString();
                    _lanxb = r[9].ToString();
                    _svideo = r[17].ToString();
                    _dewey090 = r[18].ToString();
                    _linkoutsite = r[19].ToString();
                    _nhomtailieu = int.Parse(r[20].ToString());
                    DeweyName.Text = pro.GetDewayName(_dewey);
                    if (_dewey.Length >= 7)
                    {
                        DeweyName.NavigateUrl = "/fts?q=ddc:" + _dewey.ToString().Substring(0, 6) + "*&ck=false";
                    }
                    else
                    {
                        DeweyName.NavigateUrl = "/fts?q=ddc:" + _dewey + "*&ck=false";
                    }
                    if (!string.IsNullOrEmpty(_linkoutsite))
                    {
                        Link_outsite.Visible = true;
                        Link_outsite.NavigateUrl = HttpUtility.HtmlDecode(_linkoutsite);
                    }
                    
                }

                DataTable dt_bookshow = new DataTable();
                dt_bookshow = BookShow(Convert.ToInt32(_RecordID));
                foreach (DataRow rbs in dt_bookshow.Rows)
                {
                    if (dt_bookshow.Rows.Count > 0)
                    {
                        DivBookShow.Visible = true;
                        La_subbook.Text = rbs[0].ToString().Substring(3, rbs[0].ToString().Length - 3);
                        La_infosubbook.Text = "Dãy kệ:" + rbs[0].ToString().Substring(0, 3) + "-" + rbs[1].ToString();
                    }
                    else
                        DivBookShow.Visible = false;
                }

                string[] split = _tacga.Split(new Char[] { ',', ';', '.', '\n' });
                repLinks.DataSource = split;
                repLinks.DataBind();

                if (string.IsNullOrEmpty(_svideo))
                    film.Visible = false;
                else
                {
                    ifvideo.Src = _svideo + "?autoplay=1";
                    if (_svideo == "" || _svideo == "https://www.youtube.com/embed /" || !_svideo.Contains("youtube"))
                    {
                        LVideo.Visible = false;
                        Mesvideo.Visible = true;
                    }
                    else
                    {
                        LVideo.Visible = true;
                        Mesvideo.Visible = false;
                    }
                }
                DataListDownload.DataSource = ShowLink(Convert.ToInt32(_RecordID)).DefaultView;
                DataListDownload.DataBind();
                if (DataListDownload.Items.Count > 0)//kiểm tra có file thì hiển thị tab down
                 {
                     //download.Visible = true;
                     tabdownload.Visible = true;
                 }
                 else
                     //download.Visible = false;
                     tabdownload.Visible = false;
                Img();                

               
                Page.Title = _nhande;

                foreach (DataRow r in Laythongtinsachmuon(int.Parse(_RecordID)).Rows)
                {
                    La_soban.Text = "- Số bản có sẵn: " + r[2].ToString();
                    La_dangmuon.Text = "- Đang mượn: " + r[3].ToString();
                    /*if (int.Parse(r[2].ToString()) == 1)
                    {
                        La_khongchomuon.Text = "(Cuốn này chỉ phục vụ đọc tại chỗ)";
                    }
                    else
                        La_khongchomuon.Visible=false;*/
                }
                GetPositBook(int.Parse(_RecordID));
                
            }
        }
        // hàm đánh giá thứ hạng sao
        DataTable dt_rating = this.GetData("SELECT ISNULL(AVG(Rating), 0) AverageRating, COUNT(Rating) RatingCount FROM UserRatings WHERE RecordID='" + _RecordID + "'");
        Rating1.CurrentRating = Convert.ToInt32(dt_rating.Rows[0]["AverageRating"]);
        lblRatingStatus.Text = string.Format("{0} Người đánh giá. Xếp hạng trung bình {1}", dt_rating.Rows[0]["RatingCount"], dt_rating.Rows[0]["AverageRating"]);
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
    private DataTable ShowLink(int ID)
    {
        using (SqlConnection con = new SqlConnection(constringSearch))
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[TV_RecordDocuments_Download_RecordID]";
                cmd.Parameters.AddWithValue("@RecordID", ID);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

            }
            finally
            {

                con.Close();

            }
            return dt;
        }

    }
    private DataTable GetPositBook(int ID)
    {
        using (SqlConnection con = new SqlConnection(constringSearch))
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();            
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[TV_psc_Inventories_RecordID]";
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataColumn InventoryLocation = new DataColumn("InventoryLocation", typeof(string));
                dt.Columns.Add(InventoryLocation);
                DataColumn InventoryID = new DataColumn("InventoryID", typeof(int));
                dt.Columns.Add(InventoryID);
                DataColumn RecordID = new DataColumn("RecordID", typeof(int));
                dt.Columns.Add(RecordID);
                DataColumn Dewey090 = new DataColumn("Dewey090", typeof(string));
                dt.Columns.Add(Dewey090);
                DataColumn Dayke = new DataColumn("Dayke", typeof(string));
                dt.Columns.Add(Dayke);
                DataRow row;
                adapter.Fill(dt2);              
                // kiểm tra bộ sưu tập để hiển thị
                //if ( _nhomtailieu == 1 || _nhomtailieu == 3 || _nhomtailieu == 5 || _nhomtailieu == 6 || _nhomtailieu == 9 || _nhomtailieu == 13 || _nhomtailieu == 14 || _nhomtailieu == 15 || _nhomtailieu == 16 || _nhomtailieu == 17 || _nhomtailieu == 18 || _nhomtailieu == 19 || _nhomtailieu == 20)
                if (ValidatCopyID(int.Parse(_RecordID)) && !string.IsNullOrEmpty(_dewey090) && !ValidatBookShow(int.Parse(_RecordID)))               
                {
                    sachmuon.Visible = true;
                    List<string> list = new List<string>();
                    string info = "";
                    
                    foreach (DataRow r in dt2.Rows)
                    {
                        row = dt.NewRow();
                        row["InventoryLocation"] = r[0].ToString(); // chi nhánh thư viện
                        row["InventoryID"] = r[1].ToString().Trim();
                        row["RecordID"] = r[2].ToString().Trim();
                        row["Dewey090"] = r[3].ToString().Trim();                       
                        if (!string.IsNullOrEmpty(r[3].ToString()))
                        {
                            if (int.Parse(r[1].ToString()) == 9)// vị trí sách DBP
                            {
                                string daykeDBP= ShowBookDBP(checkstring(row["Dewey090"].ToString().Replace("/", ""))).Rows[0]["Dayke"].ToString().ToUpper();
                                row["Dayke"] = daykeDBP;
                                list.Add("Dãy kệ " + daykeDBP +"-"+ r[0].ToString());
                            }
                            else
                        if (int.Parse(r[1].ToString()) == 5)// vị trí sách Q9
                            {
                                string daykeQ9 = ShowBookQ9(checkstring(row["Dewey090"].ToString().Replace("/", ""))).Rows[0]["Dayke"].ToString().ToUpper(); ;
                                row["Dayke"] = daykeQ9;
                                list.Add("Dãy kệ " + daykeQ9 +"-"+ r[0].ToString());
                            }
                            Lab_dewey090.Text = "Ký hiệu xếp giá: " + r[3].ToString().Trim();
                        }
                       
                        String[] str = list.ToArray();                       
                        info = _nhande + " - " + _tacga + " - " + "(" + _namxb + ")" + ", Xếp giá: " + r[3].ToString() + ", " + "Vị trí sách: " + String.Join("; ", list.ToArray());

                        dt.Rows.Add(row);
                        Repeater_Pos.DataSource = dt;
                        Repeater_Pos.DataBind();
                       
                        ImageQrCode.ImageUrl = "https://api.qrserver.com/v1/create-qr-code/?size=140x140&data=" + HttpUtility.HtmlDecode(info);
                    }
                }
                else
                {
                    string info = _nhande + " - " + _tacga + " - " + "(" + _namxb + ")" + " - ID: " + ID;
                    ImageQrCode.ImageUrl = "https://api.qrserver.com/v1/create-qr-code/?size=140x140&data=" + HttpUtility.HtmlDecode(info);
                }    

                

            }

            finally
            {

                con.Close();

            }
            return dt;
        }

    }
    public bool ValidatBookShow(int RecordID)// trường hơp có đăng ký cá biệt trong BookShowDetails
    {
      
        SqlConnection conn = new SqlConnection(constringSearch);
        SqlCommand cmd = new SqlCommand("SELECT count(1) FROM psc_BookShowDetails WHERE RecordID = @RecordID", conn);
        cmd.Parameters.Add(new SqlParameter("@RecordID", RecordID));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;

    }
    public bool ValidatCopyID(int RecordID)// trường hơp có đăng ký cá biệt
    {

        SqlConnection conn = new SqlConnection(constringSearch);
        SqlCommand cmd = new SqlCommand("SELECT count(1) FROM psc_Copies WHERE RecordID = @RecordID", conn);
        cmd.Parameters.Add(new SqlParameter("@RecordID", RecordID));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;

    }
    private DataTable ShowBookDBP(Double dewey)
    {
        using (SqlConnection con = new SqlConnection(constring))
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[sp_ProBookshelfDBP]";
                cmd.Parameters.AddWithValue("@indeway", dewey);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

            }
            finally
            {

                con.Close();

            }
            return dt;
        }

    }
    private DataTable ShowBookQ9(Double dewey)
    {
        using (SqlConnection con = new SqlConnection(constring))
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[sp_ProBookshelfQ9]";
                cmd.Parameters.AddWithValue("@indeway", dewey);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

            }
            finally
            {

                con.Close();

            }
            return dt;
        }

    }
    private DataTable BookShow(int ID)//
    {
        using (SqlConnection con = new SqlConnection(constringSearch))
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[TV_BookShow]";
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

    }

    private DataTable GetInfo(int ID)
    {
        using (SqlConnection con = new SqlConnection(constringSearch))
        {
            DataTable dt = new DataTable();
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TV_RecordDetail_RecordID";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

            }
            finally
            {

                con.Close();

            }
            return dt;
        }

    }
    public void Img()
    {

        if (UrlExists("http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg"))//neu hinh anh ton tai
        {
            ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg";

        }
        else
            switch (_nhomtailieu)
            {
                case 2:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/luanvan.jpg";
                    break;
                case 10:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/luanvan_ths.jpg";
                    break;
                case 7:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/magazine.jpg";
                    break;
                case 8:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Baitrich.jpg";
                    break;
                case 1:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                    break;
                case 6:
                    if (_nxb.Contains("Hutech") || _nxb.Contains("hutech") || _nxb.Contains("HUTECH"))
                    {
                        ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/gthutech.jpg";
                    }
                    else
                    {
                        ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                    }
                    break;
                default:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                    break;

            }

    }


    protected void DataListDownload_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            System.Web.UI.WebControls.Label _Lenght = (System.Web.UI.WebControls.Label)e.Item.FindControl("Lenght");
            System.Web.UI.WebControls.Label _Fname = (System.Web.UI.WebControls.Label)e.Item.FindControl("LaFilename");
            System.Web.UI.WebControls.HyperLink LinkDown = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HpDown"); 
            string Down = DataBinder.Eval(e.Item.DataItem, "Location").ToString();
            string _filename = DataBinder.Eval(e.Item.DataItem, "FileName").ToString();
            System.Web.UI.WebControls.HyperLink Linkviewonline = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("HyperView");            

            if (!string.IsNullOrEmpty(Down))//nếu khác null
            {
                string duongdanURL;                
                Session["ID_Session"] = _RecordID.ToString();
                FileInfo fi;
                if (Down.Contains(@"D:\files\"))
                {
                    duongdanURL = Down.Replace(@"D:\files", "//data");  //nếu là đường dẫn chưa D:\files\               
                    LinkDown.NavigateUrl = "/DownloadFile?id=" + _RecordID + "&pt=" + encode(Down.Replace(@"D:\files", "").Replace(@"\", "/")) + "&key=" + GetHashString("hutechlibrary" + _RecordID);
                    fi = new FileInfo(duongdanURL);// lấy dung lượng file
                    if (fi.Exists)// nếu có file
                    {
                        _Lenght.Text = "(" + FormatFileSize(fi.Length) + ")";
                        if (_filename.Contains("$"))
                        {
                            _Fname.Text = _filename.Replace("$"," - ");
                        }
                        if (fi.Extension==".pdf")
                        {
                            if (UrlExists("http://data.lib.hutech.edu.vn/mucluc/" + fi.Name))                            {
                               
                                Linkviewonline.NavigateUrl = "~/phu-luc";
                                Session["file"] = string.Empty;
                                Session["file"] = fi.Name;                              
                            }
                            else
                                Linkviewonline.Visible = false;
                        }    
                    }
                    else
                    {
                        LinkDown.Visible = false;                       
                    }    
                       
                }
                else
                {
                    duongdanURL = Down.Replace(@"\", "/");//nếu là đường dẫn chứa //data                    
                    LinkDown.NavigateUrl = "/DownloadFile?id=" + _RecordID + "&pt=" + encode(Down.Replace(@"\", "/").ToLower().Replace(@"//data", "")) + "&key=" + GetHashString("hutechlibrary" + _RecordID);
                    fi = new FileInfo(duongdanURL);// lấy dung lượng file
                    if (fi.Exists)// nếu có file
                    {
                        _Lenght.Text = "(" + FormatFileSize(fi.Length) + ")";
                        if (_filename.Contains("$"))
                        {
                            _Fname.Text = _filename.Replace("$", " - ");
                        }
                        if (fi.Extension == ".pdf")
                        {
                            if (UrlExists("http://data.lib.hutech.edu.vn/mucluc/" + fi.Name))
                            {                               
                                Linkviewonline.NavigateUrl = "~/phu-luc";
                                Session["file"] = string.Empty;
                                Session["file"] = fi.Name;                                
                            }
                            else
                                Linkviewonline.Visible = false;
                        }
                    }
                    else
                    {
                        LinkDown.Visible = false;                      
                       
                    }    
                        
                }
            }
            else
                LinkDown.Visible = false;
        }
    }
    private string FormatFileSize(long Bytes)
    {
        Decimal size = 0;
        string result = null;
        Decimal kq;
        if (Bytes >= 1073741824)
        {
            size = Decimal.Divide(Bytes, 1073741824);
            kq = Decimal.Round(size, 1);
            result = kq.ToString() + "GB";//GB

        }
        else if (Bytes >= 1048576)
        {
            size = Decimal.Divide(Bytes, 1048576);
            kq = Decimal.Round(size, 1);
            result = kq.ToString() + "MB";//GB
        }
        else if (Bytes >= 1024)
        {
            size = Decimal.Divide(Bytes, 1024);
            kq = Decimal.Round(size);
            result = kq.ToString() + "KB";

        }
        else if (Bytes > 0 & Bytes < 1024)
        {
            size = Bytes;
            kq = Decimal.Round(size);
            result = kq.ToString() + "Byte";

        }

        return result;
    }

    public Double checkstring(string inputstring)// cắt chuỗi dewey chỉ lấy 8 ký tự
    {
        Double d = 0;
        try
        {

            if (!string.IsNullOrEmpty(inputstring))
            {
                string s = inputstring.Replace("/", "");
                if (inputstring.Length > 7)
                {
                    //string s = inputstring.Replace("/", "");
                    d = Convert.ToDouble(s.Substring(0, 7));

                }
                else
                {
                    d = Convert.ToDouble(s);

                }

            }

        }
        catch
        { }
        return d;

    }
    public DataTable Laythongtinsachmuon(int ID) //view
    {
        SqlConnection con = new SqlConnection(constringSearch);
        DataTable dt = new DataTable();
        try
        {
            dt.Clear();
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from vw_psc_Copy_Qties Where RecordID ='" + ID + "' ", con);
            adapter.Fill(dt);
        }
        finally
        {
            if (con != null)
                con.Close();
        }
        return dt;

    }
    protected void Repeater_Pos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            System.Web.UI.WebControls.Label khuvuc = (System.Web.UI.WebControls.Label)e.Item.FindControl("LaPosition");
            //int InventoryID =int.Parse(DataBinder.Eval(e.Item.DataItem, "InventoryID").ToString());
            int ID = int.Parse(DataBinder.Eval(e.Item.DataItem, "RecordID").ToString());

            /*if (!khuvuc.Text.Contains("Quận 9"))//Sách có trong kho ĐBP
            {
                divDBP.Visible = true;

            }
            if (!khuvuc.Text.Contains("ĐBPhủ"))// Sách có trong kho Q9
            {
                divQ9.Visible = true;

            }*/

        }
    }
    public void ButtonAddtoCart(object sender, EventArgs e)
    {
        //int _RecordID = Int32.Parse(_RecordID);
        if (Session["myCart"] == null)
        {
            myCart = new ShoppingCart();
            Session["myCart"] = myCart;

        }
        myCart = (ShoppingCart)Session["myCart"];
        DataTable dt = DataAccess.selectQuery(";WITH XMLNAMESPACES('http://www.loc.gov/MARC21/slim' as marc)" + "SELECT RecordID,ltrim(rtrim(CONVERT(nvarchar(2000),MarcXML.query('/marc:collection/marc:record/marc:datafield[@tag=''245''][1]/marc:subfield[@code=''a'']/text()')))) AS [Title], RecordGroupID  FROM psc_Records  WHERE RecordID =" + _RecordID);
        DataRow row = dt.Rows[0];
        myCart.Insert(new CartItem(Int32.Parse(_RecordID), "", row["Title"].ToString(), Convert.ToInt16(row["RecordGroupID"]), 1));

        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void OnRatingChanged(object sender, RatingEventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO UserRatings VALUES(@RecordID, @Rating, @DateRating)"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@RecordID", _RecordID);
                    cmd.Parameters.AddWithValue("@Rating", e.Value);
                    cmd.Parameters.AddWithValue("@DateRating", DateTime.Now);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString().ToLower();
    }
    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
    public static string encode(string text)
    {
        byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
        string returntext = System.Convert.ToBase64String(mybyte);
        return returntext;
    }
    private void GetstatusCart()
    {
        if (Session["myCart"] == null)
        {
            Session["myCart"] = new ShoppingCart();
        }
        myCart = (ShoppingCart)Session["myCart"];
        if (myCart.Items.Count == 0)
        {
            AddCart.Text = "<span class='glyphicon glyphicon-star'></span>" + "&nbsp;Lưu vào danh sách";
        }
        else
        {
            foreach (var item in myCart.Items)
            {
                if (string.Compare(_RecordID, item.RecordID.ToString()) == 0)
                {
                    AddCart.Text = "<span class='glyphicon glyphicon-shopping-cart'></span>" + "&nbsp;Đã lưu";
                    AddCart.Enabled = false;
                }
            }

        }
    }

}