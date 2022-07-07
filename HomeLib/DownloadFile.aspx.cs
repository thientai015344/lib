using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeLib_DownloadFile : System.Web.UI.Page
{
    private long bytesToRead;
    private string _pt;
    private string _key;
    private string _id;
    private string _user;    
    LibProcess pro = new LibProcess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _pt = Request.QueryString["pt"];// lấy đường dẫn
            _key = Request.QueryString["key"];// lấy key được mã hóa SHA1
            _id = Request.QueryString["id"]; //record sách
            if (Session["user_Session"] == null)
            {
                if (!this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
            else
            {
                _user = Session["user_Session"].ToString();
                /*if (pro.CheckUsernameDelete(_user))//có trong bảng psc_ReaderDeleted, user đã bị xóa
                {
                    //Response.Write("Tài khoản không có quyền tải tài liệu");
                    Lab_Mes.Text = "Tài khoản không có quyền tải tài liệu";
                }
                else*/
                {
                    if (Check_hash(_key))//kiểm tra trùng mã sha1
                    {
                        if (pro.CheckRoleDownload(_user) == 23)//tài khoản Full download
                        {
                            Download();
                        }
                        else
                        {
                           /* string _bst = Session["session_bst"].ToString();// lấy Session bộ sưu tập
                            if (_bst != null)
                            {
                                if (_bst == "Sách điện tử")
                                {
                                    if (pro.Countdown(_user) < 6)// giới hạn 5 lần tải
                                    {
                                        Download();
                                    }
                                    else
                                    {
                                        Lab_Mes.Text = "Sách điện tử được 1 lượt tải/ngày, vui lòng quay lại vào ngày sau.";
                                       
                                    }
                                }
                                else*/

                                if (pro.Countdown(_user) < 5)// giới hạn 5 lần tải
                                {
                                    Download();
                                }
                                else
                                {
                                    Lab_Mes.Text = "Tài khoản đã hết 5 lượt tải/ngày, vui lòng quay lại vào ngày sau.";
                                   
                                }                           


                        }
                            

                    }

                }
            }

        }
    }
    private void Download()
    {
        //if (fi.Exists)      
        {
            string decode_filePath = decode(_pt);// giải mã đường dẫn
            FileInfo fi = new FileInfo(decode_filePath); //lấy tên file
            Page.Response.Clear();
            bool success = ResponseFile(Page.Request, Page.Response, fi.Name, "//data" + decode_filePath, 102400000);

             if (!success)
                {
                    Lab_Mes.Text = "Downloading Error!";               
                }
                else
                {                   
                    
                    pro.insertdulieuThuvien(_user, fi.Name);// insert dữ liệu download và database thư viện               
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
        ValueChk = string.Compare(input_request_string, GetHashString("hutechlibrary" + _id));
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
    public static string decode(string text)
    {
        byte[] mybyte = System.Convert.FromBase64String(text);
        string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
        return returntext;
    }


}