using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;

public partial class ViewCart : System.Web.UI.Page
{
    ShoppingCart myCart;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["myCart"] == null)
        {            
            // myCart = new ShoppingCart();
            Session["myCart"] = new ShoppingCart();
        }
        myCart = (ShoppingCart)Session["myCart"];
       
        if (!IsPostBack)
        {      
                FillData();
                if (GridViewCart.Rows.Count == 0)
                {
                    div_checkCart.Visible = true;
                    divCart.Visible = false;
                    prev.Visible = false;
                    DivReInfo_Nohutech.Visible = false;
                    Div_hutech.Visible = false;

                }
                else               
                {                   
                    //Div_hutech.Visible = true;
                    //DivReInfo_Nohutech.Visible = true;
                    div_select.Visible =true;
                if (!Radio_hutech.Checked && !Radio_nohutech.Checked)
                {
                   // Div_hutech.Visible = false;
                   // DivReInfo_Nohutech.Visible = false;
                
                }
               

            }
            
        }
    }

    private void FillData()
    {
        GridViewCart.DataSource = myCart.Items;
        GridViewCart.DataBind();
       
        // Masterlabeltext = "Your value"; // set your value
        //Response.Redirect("/DocumentCart.aspx");            
    }
    protected void GridViewCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewCart.EditIndex = -1;
        Response.Redirect(Request.Url.AbsoluteUri);
        FillData();
    }   

    protected void GridViewCart_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox TxtQuanttity = (TextBox)GridViewCart.Rows[e.RowIndex].Cells[2].Controls[0];
        int quantity = Int32.Parse(TxtQuanttity.Text);
        if (quantity < 2)
        {
            myCart.Update(e.RowIndex, quantity);
            GridViewCart.EditIndex = -1;
            Response.Redirect(Request.Url.AbsoluteUri);
            FillData();
        }
        else
        {
            string script = "alert(\"Bạn chỉ được mượn mỗi nhan đề 1 cuốn\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }

    protected void GridViewCart_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewCart.EditIndex = e.NewEditIndex;       
        FillData();
        
    }

    protected void GridViewCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        myCart.Delete(e.RowIndex);
        Response.Redirect(Request.Url.AbsoluteUri);
        FillData();
       
    }
    public DataTable Laythongtinsachmuon(int ID) //view
    {
        string constring = ConfigurationManager.ConnectionStrings["LibThuvienSearch"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
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


    protected void Bt_muon_Click(object sender, EventArgs e)
    {

        Insert_UserBookCart();
        //Checkout();
        Response.Redirect(Request.Url.AbsoluteUri);
       


    }
    

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    protected void Checkout()
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {

            foreach (GridViewRow rowno in GridViewCart.Rows)
            {
                if (rowno.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("[dbo].[Proc_Cart]", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            int RecordID = int.Parse(rowno.Cells[0].Text.ToString());
                            string Title = rowno.Cells[1].Text.ToString();     
                           // string Title = myCart.Items[].Title.ToString();
                            int quantitynow = int.Parse(rowno.Cells[2].Text);
                            
                            cmd.Parameters.AddWithValue("@Quantity", quantitynow);
                            cmd.Parameters.AddWithValue("@RecordID", RecordID);
                            cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Title", Title);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            /*if (a != 0)
                            {
                                Session["myCart"] = null;
                                Response.Redirect("~/Checkout.aspx");
                            }*/
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        break;
                    }
                }
            }             
        }
    }
    private void Insert_UserBookCart()
    {
        string constring = ConfigurationManager.ConnectionStrings["LibSearchUser"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Proc_AddBookCart]", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@FirstName", form_name_sv.Value);
                    cmd.Parameters.AddWithValue("@LastName", form_lastname_sv.Value);
                    cmd.Parameters.AddWithValue("@Address", form_Address_sv.Value);
                    cmd.Parameters.AddWithValue("@Ghichu", form_message_sv.Value);
                    cmd.Parameters.AddWithValue("@Phone", form_phone_sv.Value);
                    cmd.Parameters.AddWithValue("@Email", form_email_sv.Value);
                    cmd.Parameters.AddWithValue("@Vitri", DropDown_sv.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Username", form_mssv.Value);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    Checkout();
                    if (a!= 0)
                    {
                        Session["myCart"] = null;
                        Response.Redirect("~/checkout");
                    }                                    
                   
                }
                catch
                {
                    con.Close();
                }

            }
        }

    }

    protected void Radio_hutech_CheckedChanged(object sender, EventArgs e)
    {        

        if (Radio_hutech.Checked)
        {
            Div_hutech.Visible = true;
            DivReInfo_Nohutech.Visible = false;
            
        }
        
        //Radio_hutech.Checked = true;
    }

    protected void Radio_nohutech_CheckedChanged(object sender, EventArgs e)
    {
       // Response.Redirect(Request.Url.AbsoluteUri);
        if (Radio_nohutech.Checked)
        {
            Div_hutech.Visible = false;
            DivReInfo_Nohutech.Visible = true;
            
        }
       
    }
    /*public void Img()
    {

        if (UrlExists("http://data.lib.hutech.edu.vn/BookImg/" + Request.QueryString["id"] + ".jpg"))//neu hinh anh ton tai
        {
            ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/" + Request.QueryString["id"] + ".jpg";

        }
        else
            switch (_loaitailieu)
            {
                case "Luận văn ĐH-CĐ":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/luanvan.jpg";
                    break;
                case "Luận văn Thạc sĩ":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/luanvan_ths.jpg";
                    break;
                case "Báo-Tạp chí":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/magazine.jpg";
                    break;
                case "Bài trích":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Baitrich.jpg";
                    break;
                case "Sách tham khảo":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                    break;
                case "Sách giáo trình":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/gthutech.jpg";
                    break;
                case "Tài liệu mutimedia":
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Media.jpg";
                    break;
                default:
                    ImageBook.ImageUrl = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                    break;

            }
        if (_loaitailieu == "Sách tham khảo" || _loaitailieu == "Sách giáo trình" || _loaitailieu == "Sách tra cứu")
        {
            pos.Visible = true;
            sachmuon.Visible = true;
        }
        else
        {
            pos.Visible = false;
        }
    }*/

    
}