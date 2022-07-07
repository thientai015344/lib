
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class StudentDown : System.Web.UI.Page
{
   
    string sourcePdfPath = @"\\data\Temp\Giaotrinh\";
    string extractRange = "50";
    string password = "";
    private long bytesToRead;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["_mssv_Session"] != null)
            {
                DataListDownload.DataSource = GetInfo(Session["_mssv_Session"].ToString());
                DataListDownload.DataBind();
            }
            else
            {
                Response.Redirect("dang-nhap");
            }
        }        
        
    }
    private DataTable GetInfo(string id)
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            DataTable dt = new DataTable();
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "StudentInfo_code";
                cmd.Parameters.AddWithValue("@ID", id);
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
    protected void DataListDownload_ItemDataBound(object sender, DataListItemEventArgs e)
    {
         if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            string Lay_mamonhoc = DataBinder.Eval(e.Item.DataItem, "MA_MON").ToString();
            System.Web.UI.WebControls.Label _mamon = (System.Web.UI.WebControls.Label)e.Item.FindControl("La_mamon");
            string Lay_tenmonhoc = DataBinder.Eval(e.Item.DataItem, "TEN_MON").ToString();
            System.Web.UI.WebControls.Label _tenmon = (System.Web.UI.WebControls.Label)e.Item.FindControl("La_tenmon");
            string Lay_tenfile = DataBinder.Eval(e.Item.DataItem, "link_file").ToString();
            System.Web.UI.WebControls.HyperLink _linkfile = (System.Web.UI.WebControls.HyperLink)e.Item.FindControl("Hyper_down");
            System.Web.UI.WebControls.Label _dungluong = (System.Web.UI.WebControls.Label)e.Item.FindControl("Lab_dungluong");
            _mamon.Text = Lay_mamonhoc;
            _tenmon.Text = Lay_tenmonhoc;
            FileInfo fi = new FileInfo(sourcePdfPath + Lay_tenfile);
            // _dungluong.Text = sourcePdfPath + Lay_tenfile;
           
                if (fi.Exists)
                {
                    _dungluong.Text = " (" + FormatFileSize(fi.Length) + ")";
                    _linkfile.NavigateUrl = "studentdowmload.aspx?mmh=" + Lay_mamonhoc + "&key=" + GetHashString("hutechlibrary" + Lay_mamonhoc) + "&name=" + encode(Lay_tenfile);
                }
              
            
            //Response.Redirect("tim-kiem?q=" + HttpUtility.UrlEncode(keyword) + "&rag=" + rg + "&pg=" + pageIndex + "&sort=" + sor + "&filter=" + filter);
        }
        
    }
    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString().ToLower();
    }
    public static string encode(string text)
    {
        byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
        string returntext = System.Convert.ToBase64String(mybyte);
        return returntext;
    }
    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
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


    protected void logout_Click(object sender, EventArgs e)
    {
        Session["_mssv_Session"] = null;
        Response.Redirect("dang-nhap");
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
    
}