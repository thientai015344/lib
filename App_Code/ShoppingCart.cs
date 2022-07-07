using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for ShoppingCart
/// </summary>
public class ShoppingCart : IDisposable     
{
    private int CountCart;
    public int countcart
    {
            get {return CountCart; }
            set { CountCart=value; }
    }
     
    public string ShoppingCartId { get; set; }
    public List<CartItem> Items { get; set; }

    

    public const string CartSessionKey = "myCart";
    public ShoppingCart()
    {
        Items = new List<CartItem>();
       
    }
   
    public int ItemIndexOf(int RecordID)
    {
        foreach (CartItem item in Items)
        {
            if (item.RecordID == RecordID)
            {
                return Items.IndexOf(item);
            }           
        }
         return -1;
    }
   
    public void Insert(CartItem item)
    {
        int index = ItemIndexOf(item.RecordID);
        if(index==-1)
        {
            Items.Add(item);            
        }
        else
        {
            Items[index].Quantity++;
                 
        }
       
    }
    public void Delete(int rowID)
    {
        Items.RemoveAt(rowID);
    }
   
    public void Update(int rowID, int Quantity)
    {
        if(Quantity > 0)
        {
            Items[rowID].Quantity = Quantity;

        }
        else
        {
            Delete(rowID);
        }
    }
    public int Getcount
    {
        get
        {
            if (Items==null)
            {
                return 0;
            }
            else
            {
                int count = 0;
                foreach (CartItem item in Items)
                {
                   count += item.Quantity;                 
                }
                return count;
            }
          
        }
       
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            disposedValue = true;
        }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~ShoppingCart() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(true);
        // TODO: uncomment the following line if the finalizer is overridden above.
        // GC.SuppressFinalize(this);
    }
    #endregion




    /*public string GetCartId()
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
    public int GetCount()
    {
        ShoppingCartId = GetCartId();

        // Get the count of each item in the cart and sum them up          
        int? count = (from CartItems in _db.ShoppingCartItems
                      where CartItems.ID == int.Parse(ShoppingCartId)
                      select (int?)CartItems.Quantity).Sum();
        // Return 0 if all entries are null         
        return count ?? 0;
    }*/
}