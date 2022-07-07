using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// Summary description for CartItem
/// </summary>
public class CartItem
{
    public CartItem()
    {      //
       
    }
    
    public int RecordID { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public int Quantity { get; set; }
    public int RecordGroupID { get; set; }
    public Double Price { get; set; }
    public CartItem( int RecordID, string Image, string Title, int RecordGroupID, int Quantity)
    {
        this.RecordID = RecordID;
        this.Image = Image;
        this.Title = Title;   
        this.RecordGroupID = RecordGroupID;
       this.Quantity = Quantity;


    }
   
}