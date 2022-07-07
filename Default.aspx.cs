using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    ShoppingCart myCart;
    protected void Page_Load(object sender, EventArgs e)
    {
       /* if(!IsPostBack)
        {
            if (Session["myCart"] == null)
            {
                myCart = new ShoppingCart();
                Session["myCart"] = myCart;
            }
            else
               Session["myCart"] = myCart;*/
              // myCart.Getcount
           // ((Label)Master.FindControl("Label1")).Text = String.Format("{0} Items In Cart", myCart.Getcount);
        
    }
}