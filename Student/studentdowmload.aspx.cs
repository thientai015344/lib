using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class studentdowmload : System.Web.UI.Page
{
    
    string sourcePdfPath = @"\\data\Temp\Giaotrinh\";
    private long bytesToRead;
    private string _mmh;
    private string _key;
    private string _filename;
    private FileInfo fi = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _mmh = Request.QueryString["mmh"];
            _key = Request.QueryString["key"];
            _filename= Request.QueryString["name"];
            if (Session["_mssv_Session"] != null)
            {
                if (Check_hash(_key))//trung ma
                {
                    Download();                   
                }
            }
            else
            {
                Response.Redirect("/Student/Login.aspx");
            }
            
        }
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
    private bool Check_hash(string input_request_string)
    {
        int ValueChk;
        bool chk = false;
        ValueChk = string.Compare(input_request_string, GetHashString("hutechlibrary" + _mmh));
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
    private void Download()
    {        
        //if (fi.Exists)
        {
            string decodefilename = decode(_filename);
            Page.Response.Clear();
            bool success = ResponseFile(Page.Request, Page.Response,decodefilename, sourcePdfPath + decodefilename, 102400000);
            if (!success)
            {

                Response.Write("Downloading Error!");
            }
            
        }
        //Response.Redirect(Request.Url.AbsoluteUri);
    }    

     public static string decode(string text)
     {
         byte[] mybyte = System.Convert.FromBase64String(text);
         string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
         return returntext;
     }

}