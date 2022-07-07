using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using iTextSharp.text.pdf;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;

public partial class Book_Guestdownload : System.Web.UI.UserControl
{
    private static bool checkpage = false;
    private FileInfo myfile = null;
    string sourcePdfPath;
    string outputPdfPath = @"\\data\Temp\";
    string extractRange = "50";
    string password = "";
    private long bytesToRead;
    public string RecordID { get; set; }
    private string key;
    string _nhande;
    string _tacga;
    string _isbn;
    int _nhomtailieu;
    string _nxb;
    string _namxb;
    string _mota;
    string _lanxb;
    string _dewey;
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (!IsPostBack)
        {
            RecordID = Request.QueryString["id"];
            key = Request.QueryString["key"];
            if (Check_hash(key))//trung ma
            {

                if (!String.IsNullOrEmpty(RecordID))
                {
                    foreach (DataRow r in GetInfo(int.Parse(RecordID)).Rows)
                    {
                        Title.Text = r[0].ToString().Replace("/", "");
                        // Author.Text = r[2].ToString();
                        bst.Text = r[1].ToString();
                        InfoPublisher.Text = r[6].ToString() + " " + r[7].ToString() + ", " + r[9].ToString();
                        Description.Text = r[8].ToString();

                        _nhande = r[0].ToString().Replace("/", "");
                        
                        _tacga = r[2].ToString();
                        _dewey = r[5].ToString();
                        _nxb = r[6].ToString();// noixb va nhaxb
                        _namxb = r[7].ToString();
                        _lanxb = r[9].ToString();
                        _nhomtailieu = int.Parse(r[20].ToString());

                    }
                    Img();
                    DataTable dt_rating = this.GetData("SELECT ISNULL(AVG(Rating), 0) AverageRating, COUNT(Rating) RatingCount FROM UserRatings WHERE RecordID='" + RecordID + "'");
                    Rating1.CurrentRating = Convert.ToInt32(dt_rating.Rows[0]["AverageRating"]);
                    lblRatingStatus.Text = string.Format("{0} Người đánh giá. Xếp hạng trung bình {1}", dt_rating.Rows[0]["RatingCount"], dt_rating.Rows[0]["AverageRating"]);
                }
                string[] split = _tacga.Split(new Char[] { ',', ';', '.', '\n' });
                repLinks.DataSource = split;
                repLinks.DataBind();

                DataTable dt = new DataTable();
                dt = Linklocation(Convert.ToInt32(RecordID));
                foreach (DataRow r in dt.Rows)
                {
                    sourcePdfPath = r[2].ToString();
                }
                myfile = new FileInfo("@" + sourcePdfPath);
                Getpages(sourcePdfPath);
            }
            else
            {
                panel1.Visible = false;
                panel2.Visible = false;
                Label_info.Visible = true;
            }
        }

    }
    private DataTable GetInfo(int ID)
    {
        string constring = ConfigurationManager.ConnectionStrings["LibThuvienSearch"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
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

        if (UrlExists("http://data.lib.hutech.edu.vn/BookImg/" + RecordID + ".jpg"))//neu hinh anh ton tai
        {
            ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/" + RecordID + ".jpg";

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
        if (_nhomtailieu == 1 || _nhomtailieu == 6 || _nhomtailieu == 9)
        {
            // pos.Visible = true;
            // sachmuon.Visible = true;
        }
        else
        {
            // pos.Visible = false;
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
    private void Getpages(string pdfFilePath)
    {

        if (myfile.Extension == ".pdf")
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {

                Document document = new Document();
                document.Open();
                for (int pagenumber = 1; pagenumber < (reader.NumberOfPages * 50/100); pagenumber++)                
                {
                    if (reader.NumberOfPages >= pagenumber)
                    {
                        Labelpage1.Text = "1. Trang 1-" + pagenumber.ToString();
                        //Labelpage2.Text = "Trang " + (pagenumber + 1).ToString() + "-" + reader.NumberOfPages;
                        Labelpage2.Text = "2. Số trang còn lại";
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            panel1.Visible = false;
            panel2.Visible = false;
            Label_info.Visible = true;
        }
    }
    private void SplitAndSaveInterval(string pdfFilePath, string outputPath, string pdfFileName)//cắt và tạo file mới
    {
        using (PdfReader reader = new PdfReader(pdfFilePath))
        {
            Document document = new Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
            document.Open();
            for (int pagenumber = 1; pagenumber < (reader.NumberOfPages * 50 / 100); pagenumber++)
            {
                if (reader.NumberOfPages >= pagenumber)
                {
                    copy.AddPage(copy.GetImportedPage(reader, pagenumber));

                }
                else
                {
                    break;
                }
            }
            document.Close();
        }      
            Page.Response.Clear();
            bool success = ResponseFile(Page.Request, Page.Response, pdfFileName + ".pdf", outputPdfPath + pdfFileName + ".pdf", 102400000);
            if (!success)
            {

                Response.Write("Downloading Error!");
            }
            Page.Response.End();       
        

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
            result = kq.ToString() + " " + "GB";//GB

        }
        else if (Bytes >= 1048576)
        {
            size = Decimal.Divide(Bytes, 1048576);
            kq = Decimal.Round(size, 1);
            result = kq.ToString() + " " + "MB";//GB
        }
        else if (Bytes >= 1024)
        {
            size = Decimal.Divide(Bytes, 1024);
            kq = Decimal.Round(size);
            result = kq.ToString() + " " + "KB";

        }
        else if (Bytes > 0 & Bytes < 1024)
        {
            size = Bytes;
            kq = Decimal.Round(size);
            result = kq.ToString() + " " + "Byte";

        }

        return result;
    }
    public bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {

                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 102400; //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = http://lib.hutech.edu.vn:8081/Lists/sach/AllItems.aspx',0);", true);
                //Application.DoEvents();
                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                        System.Threading.Thread.Sleep(sleep);
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;

            }
            finally
            {
                br.Close();
                myFile.Close();

            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    /* public static string encode(string text)
     {
         byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
         string returntext = System.Convert.ToBase64String(mybyte);
         return returntext;
     }

     public static string decode(string text)
     {
         byte[] mybyte = System.Convert.FromBase64String(text);
         string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
         return returntext;
     }*/
    private bool Check_hash(string input_request_string)
    {
        int ValueChk;
        bool chk = false;
        ValueChk = string.Compare(input_request_string, GetHashString("hutechlibrary" + RecordID));
        if (ValueChk == 0)
        {
            chk = true;// 2 chuoi bang nhau
        }
        else
            chk = false;
        return chk;
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

    protected void LinkButton_load_Click(object sender, EventArgs e)// nút download
    {
        if (myfile.Extension == ".pdf")
        {

            int pageNameSuffix = 0;
            PdfReader reader = new PdfReader(sourcePdfPath);

            string pdfFileName = myfile.Name.Substring(0, myfile.Name.LastIndexOf("."));
            string newPdfFileName = string.Format(pdfFileName, pageNameSuffix);

            FileInfo fi = new FileInfo(outputPdfPath + myfile.Name);
            if (!fi.Exists)
            {
                SplitAndSaveInterval(sourcePdfPath, outputPdfPath, newPdfFileName);
            }
            else
            {
                Page.Response.Clear();
                bool success = ResponseFile(Page.Request, Page.Response, pdfFileName + ".pdf", outputPdfPath + pdfFileName + ".pdf", 102400000);
                if (!success)
                {

                    Response.Write("Downloading Error!");
                }
                Page.Response.End();
            }


        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    private DataTable Linklocation(int ID)
    {
        string constring = ConfigurationManager.ConnectionStrings["LibThuvienSearch"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
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
                    cmd.Parameters.AddWithValue("@RecordID", RecordID);
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
        string constr = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
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
}