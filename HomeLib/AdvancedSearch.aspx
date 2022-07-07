<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvancedSearch.aspx.cs" Inherits="HomeLib_SearchResult" MasterPageFile="~/HomeLib.master" %>
<asp:Content ID="Search" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">    
    <link href="../Content/AdvancedSearch.css" rel="stylesheet" />
    
    <div class="container main">
        <div class="row">
             <h4>Tìm kiếm nâng cao</h4>
        </div>
        <div class="row mb-5">           
            <div class="col-3">
                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-5">
                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-4">
                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>        
    </div>
</asp:Content>