using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetUser
/// </summary>
public class GetUser
{
    public GetUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    public string username { get; set; }
    public string name { get; set; }   
    public GetUser(string User, string Name)
    {
        this.username = User;
        this.name = Name;     

    }
}