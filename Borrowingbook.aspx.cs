using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class borrowingbook : System.Web.UI.Page
{
    string constring = ConfigurationManager.ConnectionStrings["LibThuvienSearch"].ConnectionString;
    public int _rid;
    public int _viewmode;
    private string _user;
    protected void Page_Load(object sender, EventArgs e)
    {
        _user = TxtMS.Text;

        if (!IsPostBack)
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
                if (!string.IsNullOrEmpty(_user.Trim()))
                {
                    if (CheckExists(_user))
                    {
                        ShowInfoUser(_user.Trim());
                        DropDownValue.Visible = true;
                        Showbook(_rid, 0);
                        //Label_info.Visible = false;
                    }
                    else
                    {
                        //Label_info.Visible = true;
                        //Label_info.Text = "MSSV hoặc Email không đúng, vui lòng nhập lại";
                        DetailsView_info.DataBind();
                        DropDownValue.Visible = false;
                        DataListBook.DataBind();
                    }
                }
                else
                {
                    DetailsView_info.DataBind();
                    DropDownValue.Visible = false;
                    DataListBook.DataBind();

                }

            }
        }
        else
        {

            if (!string.IsNullOrEmpty(_user.Trim()))
            {
                if (CheckExists(_user))
                {
                    ShowInfoUser(_user.Trim());
                    DropDownValue.Visible = true;
                    Showbook(_rid, _viewmode);
                    //Label_info.Visible = false;
                }
                else
                {
                    // Label_info.Visible = true;
                    // Label_info.Text = "MSSV hoặc Email không đúng, vui lòng nhập lại";
                    DetailsView_info.DataBind();
                    DropDownValue.Visible = false;
                    DataListBook.DataBind();
                }

            }
            else
            {
                DetailsView_info.DataBind();
                DropDownValue.Visible = false;
                DataListBook.DataBind();
            }

        }

    }
    private void ShowInfoUser(string username_OR_email)
    {
        using (SqlConnection con = new SqlConnection(constring))
        {
            try
            {
                SqlDataAdapter adapter;
                DataTable dt = new DataTable();
                // bang.Clear();               
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CardNumber", username_OR_email);
                cmd.CommandText = "TV_sp_psc_ReaderCopies_Sel_CardNumber_ForCirculations";
                cmd.Connection = con;
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);
                DetailsView_info.DataSource = dt;
                DetailsView_info.DataBind();

                // int _rid2=0;
                foreach (DataRow r in dt.Rows)
                {
                    _rid = int.Parse(r[0].ToString());
                }

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

    protected void Bu_check_Click(object sender, EventArgs e)
    {

        _viewmode = 1;
        //Response.Redirect(Request.Url.AbsoluteUri);
        DropDownValue.SelectedIndex = _viewmode;
        Showbook(_rid, _viewmode);

    }
    [WebMethod]
    private DataTable Showbook(int id, int viewmode)
    {
        using (SqlConnection con = new SqlConnection(constring))
        {
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();
            try
            {

                dt.Clear();
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReaderID", id);
                cmd.Parameters.AddWithValue("@viewmode", viewmode);
                cmd.CommandText = "TV_sp_psc_ReaderCopies_Sel_History";
                cmd.Connection = con;
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);

                DataListBook.DataSource = dt;
                DataListBook.DataBind();

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
            }
            return dt;
        }
    }



    protected void DataListBook_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            System.Web.UI.WebControls.Label colorday = (System.Web.UI.WebControls.Label)e.Item.FindControl("La_Note");
            string getcolor = DataBinder.Eval(e.Item.DataItem, "NoteColor").ToString();
            colorday.ForeColor = System.Drawing.ColorTranslator.FromHtml("#" + getcolor);

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
    protected void DropDownValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewmode = int.Parse(DropDownValue.SelectedItem.Value);
        Showbook(_rid, _viewmode);
    }
    [WebMethod]
    public static string GetData()
    {
        string resp = string.Empty;
        resp += "<p>This content is dynamically appended to the existing content on scrolling.</p>";
        return resp;
    }
    public bool CheckExists(string uORe)
    {
        // _user = username;
        // _pass = password;
        using (SqlConnection con = new SqlConnection(constring))
        {

            SqlCommand cmd = new SqlCommand("SELECT count(1) FROM psc_Readers WHERE CardNumber=@imstring OR Email=@imstring", con);
            cmd.Parameters.Add(new SqlParameter("@imstring", uORe));
            cmd.Connection = con;
            con.Open();
            int count = Convert.ToInt16(cmd.ExecuteScalar());
            con.Close();
            return count != 0;
        }
    }
}