using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    
    ShoppingCart myCart;
   
    protected void Page_Init(object sender, EventArgs e)
    {
        
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
           
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        
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
    /*public string labeltext
    {
        get
        {
            return Label1.Text;
        }
        set
        {
            Label1.Text = value;
        }
    }*/
    public int CartTotalCount
    {
        get
        {

            ShoppingCart ccart = new ShoppingCart();
            return ccart.Getcount;
        }
    }

}