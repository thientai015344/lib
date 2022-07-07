using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeLib_HomeLib : System.Web.UI.MasterPage
{
    ShoppingCart myCart;
    
    GetUser _getuser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_Session"] != null)
        {           
            LabUser.Text = Session["user_Session"].ToString();
        }
        else
        {
            LnkBt_login.Visible = true;
            bt_dropdown.Visible = false;
           // LnkBt_login.Text = "Đăng nhập";
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        // if (HttpContext.Current.User.Identity.IsAuthenticated == true)
        if (Session["myCart"] == null)// giỏ hàng rỗng
        {
            myCart = new ShoppingCart();//tạo đối tượng giỏ hàng
            Session["myCart"] = myCart;//gán session 
        }

        myCart = (ShoppingCart)Session["myCart"];
        string cartStr = string.Format("{0}", myCart.Getcount);
        CartNumber.InnerText = cartStr;


    }

    protected void LnkBut_Click(object sender, EventArgs e)
    {
        if (Session["user_Session"] == null)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }      
     
        
    }
    protected void LnkBt_logout_Click(object sender, EventArgs e)
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
            Session["user_Session"] = null;
            Response.Redirect("/");
        }

    }
}
