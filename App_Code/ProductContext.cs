using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
/// <summary>
/// Summary description for ProductContext
/// </summary>
public class ProductContext:DbContext 
{
    public ProductContext()
        :base("SearchDocument")
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DbSet<CartItem> ShoppingCartItems { get; set; }
}