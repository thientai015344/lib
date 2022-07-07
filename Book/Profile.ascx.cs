using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Book_Profile : System.Web.UI.UserControl
{
    private string constring = ConfigurationManager.ConnectionStrings["LibTV"].ConnectionString;
    private int ReaderID;
    private string CardNumber;
    public string LastName;
    //string MiddleName=" " ;
    private string FirstName;
    private bool Gender;
    private string Email;
    private string Phone;
    private string Address;
    private string Note;
    private int? GroupID = null;
    private string IdentityCardNumber;
    private int? RoleID = null;
    private string RoleName;
    private int? ReaderTypeID = null;
    private int? GradeID = null;
    private string DateOfBirth;
    private int LastUpdateBy;
    private string StudentID;
    private string GroupName;
    private byte[] Photo;
    private int? ReaderStatus = null;
    int kt;
    private string user;
    LibProcess pro = new LibProcess();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["user_Session"] == null)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
        else
        {
            user = Session["user_Session"].ToString();
            ShowInfo(user);
        }

        if (!IsPostBack)
        {
           
            TxtEmail.Text = Email;
            if (Gender)
            {
                Radio_nam.Checked = true;
            }
            else
                Radio_Nu.Checked = true;
            txtDate.Text = DateOfBirth;
            txtPhone.Text = Phone;
            txtAddress.Text = Address;


            //Response.Redirect(Request.Url.AbsoluteUri);
        }
        else
        {
           
        }   


    }
    private DataTable thongtindocgia(string masv)
    {
        SqlConnection cnn = new SqlConnection(constring);
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
    private void ShowInfo(string _user)
    {
        try
        {

            DataTable dt = thongtindocgia(_user);
            foreach (DataRow r in dt.Rows)
            {
                ReaderID = int.Parse(r[0].ToString());
                CardNumber = r[1].ToString();
                StudentID = r[2].ToString();
                LastName = r[3].ToString();
                FirstName = r[4].ToString();
                Gender = bool.Parse(r[5].ToString());
                Email = r[6].ToString();
                Phone = r[7].ToString();
                Address = r[8].ToString();
                IdentityCardNumber = r[9].ToString();
                Note = r[13].ToString();
                GroupID = int.Parse(r[14].ToString());
                GroupName = r[15].ToString();
                RoleID = int.Parse(r[16].ToString());
                RoleName = r[17].ToString();
                ReaderTypeID = int.Parse(r[18].ToString());
                //GradeID =Convert.ToUInt16(r[20].ToString());
                DateOfBirth = r[24].ToString();
                LastUpdateBy = int.Parse(r[28].ToString());
                ReaderStatus = int.Parse((r[30].ToString()));


            }

        }
        catch { }
    }
    private void Update_Thongtindocgia()
    {
        string lName = "";
        string mName = "";
        SqlConnection cnn = new SqlConnection(constring);
        // try
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            string[] words = LastName.Split(delimiterChars);
            lName = words[0].Trim(); //lay ho
            for (int i = 1; i < words.Length; i++)
            {
                mName += words[i].Trim() + " ";//lay tu ho lot den het

            }

            SqlCommand cmd = new SqlCommand();
            using (cnn)
            {

                cmd = new SqlCommand("sp_psc_Readers_Upd", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ReaderID", SqlDbType.Int).Value = ReaderID;
                cmd.Parameters.Add("@CardNumber", SqlDbType.VarChar, 25).Value = CardNumber.Trim();
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = ((null == mName) ? LastName : lName);//neu ho lot rong thi lay lastname 
                cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = ((null == mName) ? "" : mName);
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = FirstName;
                cmd.Parameters.Add("@Gender", SqlDbType.Bit).Value = (GT() ? 1 : 0);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = TxtEmail.Text.Trim();
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = txtPhone.Text.Trim();
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 512).Value = txtAddress.Text.Trim();
                cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 2000).Value = (null == Note) ? "" : Note;
                cmd.Parameters.Add("@GroupID", SqlDbType.Int).Value = ((null == GroupID) ? 2 : GroupID);
                cmd.Parameters.Add("@IdentityCardNumber", SqlDbType.VarChar, 50).Value = ((null == IdentityCardNumber || "" == IdentityCardNumber) ? "" : IdentityCardNumber);
                cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = (null == RoleID ? 1 : RoleID);
                cmd.Parameters.Add("@ReaderTypeID", SqlDbType.Int).Value = (null == ReaderTypeID) ? 1 : ReaderTypeID;
                cmd.Parameters.Add("@GradeID", SqlDbType.Int).Value = Convert.DBNull;
                cmd.Parameters.Add("@OlogyID", SqlDbType.VarChar).Value = Convert.DBNull;
                cmd.Parameters.Add("@ReaderStatus", SqlDbType.Int).Value = (null == ReaderStatus) ? 1 : ReaderStatus; ;
                cmd.Parameters.Add("@DateOfBirth", SqlDbType.VarChar, 10).Value = txtDate.Text;
                cmd.Parameters.Add("@LastUpdateBy", SqlDbType.Int).Value = 11;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar, 50).Value = StudentID;

                cmd.Connection = cnn;
                cnn.Open();
                int kt = cmd.ExecuteNonQuery();
                cnn.Close();
                if (kt == -2)
                    Label_error.Text = "Cập nhật thất bại.";
                else
                {
                    Label_error.Text = "Chỉnh sửa thành công.";
                }
            }
        }
        // catch (Exception ex)
        {
            //  Label_error.Text = ex.Message;
            // insert_error(user_rutgon(), ex.Message);
        }

    }
    private bool GT()
    {
        bool kt_gt = false;
        if (Radio_nam.Checked)
            kt_gt = true;
        else
            if (Radio_Nu.Checked)
            kt_gt = false;
        return kt_gt;
    }
    protected void bt_update_Click(object sender, EventArgs e)
    {
        Update_Thongtindocgia();
    }
}