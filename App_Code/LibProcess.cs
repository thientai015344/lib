
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for Process
/// </summary>
public class LibProcess
{
    string LibTV = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    string LibSearchUser = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
    public LibProcess()
    {
        //
        // TODO: Add constructor logic here
        //        
    }
    #region Download
    public void insertdulieuThuvien(string username, string filename)
    {
        SqlConnection cnn = new SqlConnection(LibTV);
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[dbo].[TV_Ins_RecordDocuments_Download]";
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@finish_time", DateTime.Now);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@url", HttpContext.Current.Request.Url.AbsoluteUri);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }
        catch
        {
            //ex.Message
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }


    }
    public int Countdown(string username)
    {
        int count = 0;
        using (SqlConnection con = new SqlConnection(LibTV))
        {
            DataTable dt = new DataTable();
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[TV_CountDown]";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    count = Convert.ToInt16(r[1].ToString());
                }

            }
            finally
            {

                con.Close();

            }
            return count;
        }

    }
    public bool CheckUsernameDelete(string user)//nếu user có trong bảng psc_ReaderDeleted thì không được down
    {
        SqlConnection conn = new SqlConnection(LibTV);
        SqlCommand cmd = new SqlCommand("select count(1) from psc_ReaderDeleted where CardNumber=@CardNumber", conn);
        cmd.Parameters.Add(new SqlParameter("@CardNumber", user));
        cmd.Connection = conn;
        conn.Open();
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;


    }
    public int check_download(string user)
    {
        int kq;
        SqlCommand cmd;
        SqlConnection conn = new SqlConnection(LibSearchUser);
        cmd = new SqlCommand("[dbo].[check_actice]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@username", SqlDbType.VarChar, 20).Value = user;
        SqlParameter returnvalue = new SqlParameter("@KT_NULL", "");
        returnvalue.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(returnvalue);
        cmd.Connection = conn;
        conn.Open();
        cmd.ExecuteNonQuery();
        kq = Convert.ToInt32(returnvalue.Value);
        conn.Close();
        return kq;
    }
    #endregion
    #region Profile
    public DataTable thongtindocgia(string masv)
    {

        SqlConnection cnn = new SqlConnection(LibTV);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CardNumber", masv);
            cmd.CommandText = "sp_psc_Readers_Sel_CardNumber";
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    public int CheckRoleDownload(string username)
    {

        int _roleID =0;
        SqlConnection conn = new SqlConnection(LibTV);        
        try
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = ("select RoleID from psc_Readers where CardNumber=@CardNumber");
            cmd.Parameters.Add(new SqlParameter("@CardNumber", username));
            conn.Open();
            SqlDataReader mySqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow);
            while (mySqlDataReader.Read())
            {
                _roleID = Convert.ToUInt16(mySqlDataReader["RoleID"].ToString());           
               
            }
        }       
        finally
        {
            conn.Close();
        }
        return _roleID;

    }
    #endregion        
    #region Majors
    public DataTable MenuSDH()
    {

        SqlConnection cnn = new SqlConnection(LibTV);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();        
            cmd.CommandText = "SELECT a.OlogyID, b.OlogyName , count(a.OlogyID) as sl FROM psc_OlogyCurriculums a inner join psc_Ologies b on a.OlogyID=b.OlogyID WHERE SUBSTRING(a.OlogyID,1,1)='8' group by  a.OlogyID,b.OlogyName order by b.OlogyName";
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    public DataTable MenuDH()
    {

        SqlConnection cnn = new SqlConnection(LibTV);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT a.OlogyID, b.OlogyName, count(a.OlogyID) as sl FROM psc_OlogyCurriculums a , psc_Ologies b WHERE a.OlogyID=b.OlogyID and SUBSTRING(a.OlogyID,1,1)='7' group by  a.OlogyID,b.OlogyName order by b.OlogyName";
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    public DataTable CountOlogyID(string _OlogyID)// số lượng môn trong ngành học
    {

        SqlConnection cnn = new SqlConnection(LibTV);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT a.OlogyID, count(a.OlogyID) as OlogyIDNumber FROM psc_Ologies a inner join psc_OlogyCurriculums b on a.OlogyID=b.OlogyID WHERE a.OlogyID='"+_OlogyID+"' group by  a.OlogyID";
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    public DataTable Curriculums(string _OlogyID)
    {

        SqlConnection cnn = new SqlConnection(LibTV);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OlogyID", _OlogyID);
            cmd.CommandText = "sp_psc_Curriculums_Sel_OlogyID";            
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            //dt.DefaultView.Sort = "CurriculumName";            
            cnn.Close();
            return dt;
        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    #endregion
    #region Detail
    public string GetDewayName(string _dewey)
    {
        string getdewey =null;
        using (SqlConnection con = new SqlConnection(LibTV))
        {
            DataTable dt = new DataTable();
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[TV_GetDewayName]";
                cmd.Parameters.AddWithValue("@dewey", _dewey);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    getdewey = r["DeweyName"].ToString();
                }

            }
            finally
            {
                con.Close();

            }
            return getdewey;
        }

    }
    #endregion
    public DataTable LinkObject()
    {

        SqlConnection cnn = new SqlConnection(LibSearchUser);
        try
        {
            DataTable dt = new DataTable();
            dt.Clear();
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM LinkObject order by Dewey";
            cmd.Connection = cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cnn.Close();           
            return dt;

        }
        finally
        {
            if (cnn != null)
                cnn.Close();
        }

    }
    #region Keyword
    public void CheckValueStopword(string _keyword)
    {
        if (!ValidateStopword(_keyword) && !IsNumeric(_keyword))//nếu keyword không có trong stopword và phải là chữ thì insert vào Suggest
        {
            Insert_Keyword(_keyword.Trim());
        }
    }
    private void Insert_Keyword(String _keywork)
    {
            using (SqlConnection con = new SqlConnection(LibSearchUser))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Proc_Suggestions_ins]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.AddWithValue("@keyword", _keywork.ToLower().Replace("\"","").Trim());
                    cmd.Parameters.AddWithValue("@ipclient", GetIPAddress());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }      
    }
    public bool ValidateStopword(string stopword)
    {
       
        SqlConnection conn = new SqlConnection(LibSearchUser);       
        SqlCommand cmd = new SqlCommand("SELECT count(1) FROM StopWords where freetext(Stopword,@stopword) ", conn);
        cmd.Parameters.Add(new SqlParameter("@stopword", stopword.Trim()));       
        cmd.Connection = conn;
        conn.Open(); 
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        conn.Close();
        return count != 0;

    }
    //get client ip address
    public static string GetIPAddress()
    {
        string ipAddress = HttpContext.Current.Request.UserHostAddress;
        return ipAddress;
    }
    //hàm kiểm tra từ nhập vào số hay chữ
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
    /* public DataTable ValidateUser(string stopword)
     {
         DataTable dt=new DataTable();
         DataClassesDataContext db = new DataClassesDataContext();
         IQueryable result = from ed in db.GetTable<StopWord>()
                      where ed.Stopword1.Contains(stopword)                     
                      select ed;
        result;
     }*/
    #endregion
}