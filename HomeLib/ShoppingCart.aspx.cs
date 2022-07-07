using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeLib_Shopping_Cart : System.Web.UI.Page
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

        }
    }
    private void FillData()
    {
        Repeater1.DataSource = myCart.Items;
        Repeater1.DataBind();
        if (Repeater1.Items.Count ==0)
        {
            sectionCart.Visible = false;
             cartno.Visible = true;
        }   
        else
        {
            sectionCart.Visible = true;
            cartno.Visible = false;
        }    

        // Masterlabeltext = "Your value"; // set your value
        //Response.Redirect("/DocumentCart.aspx");            
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string _RecordID = e.CommandArgument.ToString();
        if (Session["myCart"] == null)
        {
            myCart = new ShoppingCart();
            Session["myCart"] = myCart;

        }
        myCart = (ShoppingCart)Session["myCart"];
        if (e.CommandName == "DeleteProducts")
        {
            myCart.Delete(e.Item.ItemIndex);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
    protected string ImageBook(string _RecordID, int _RecordGroupID)
    {

        {
            string result = null;
            if (UrlExists("http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg"))//neu hinh anh ton tai
            {
                return "http://data.lib.hutech.edu.vn/BookImg/" + _RecordID + ".jpg";
            }
            else
                switch (_RecordGroupID)
                {
                    case 1:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        break;
                    case 2:
                        result = "http://data.lib.hutech.edu.vn/BookImg/luanvan.jpg";
                        break;
                    case 6:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        /* if (!string.IsNullOrEmpty(_Publisher))
                         {
                             if (_Publisher.ToLower().Contains("hutech"))
                             {
                                 result = "http://data.lib.hutech.edu.vn/BookImg/gthutech.jpg";
                             }
                             else
                             {
                                 result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";

                             }
                         }
                         else
                             result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";*/
                        break;
                    case 7:
                        result = "http://data.lib.hutech.edu.vn/BookImg/magazine.jpg";
                        break;
                    case 8:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Baitrich.jpg";
                        break;
                    case 10:
                        result = "http://data.lib.hutech.edu.vn/BookImg/luanvan_ths.jpg";
                        break;
                    default:
                        result = "http://data.lib.hutech.edu.vn/BookImg/Default.jpg";
                        break;
                }
            return result;
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

    protected void Repeater1_DataBinding(object sender, EventArgs e)
    {
       
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
        {
            string _RecordID = DataBinder.Eval(e.Item.DataItem, "RecordID").ToString();
            string _RecordGroupID = DataBinder.Eval(e.Item.DataItem, "RecordGroupID").ToString();
            Image im_book = (Image)e.Item.FindControl("ImageBook");
           im_book.ImageUrl= ImageBook(_RecordID,int.Parse(_RecordGroupID));
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        this.EnableViewState = false;
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=excel-dosya-adi.xls");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Repeater1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        
    }
}