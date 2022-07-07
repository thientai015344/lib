using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows.Forms;

/// <summary>
/// Summary description for ShoppingCartAc:IDisposable
/// </summary>
public class ShoppingCartAc : System.Web.UI.MasterPage
{

    public ShoppingCartAc()
    {      //

    }
    
    public int GetCount { get; set; }   
    public string ShoppingCartId { get; set; }
    public const string CartSessionKey = "cart";
    public string GetCartId()
    {
        if (HttpContext.Current.Session[CartSessionKey] == null)
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
            {
                HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
            }
            else
            {
                // Generate a new random GUID using System.Guid class.     
                Guid tempCartId = Guid.NewGuid();
                HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
            }
            
        }
        return HttpContext.Current.Session[CartSessionKey].ToString();
    }
    public string cok()
    {
        return Response.Cookies["count"].Value = GetCount.ToString();
    }
    
}