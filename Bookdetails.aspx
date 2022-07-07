<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bookdetails.aspx.cs" Inherits="Bookdetails" MasterPageFile="~/Site.master" %>
<%@ Register TagPrefix="DT" TagName="Detail" Src="~/Book/Detail.ascx"%>

<asp:Content ID="DetailtContent" ContentPlaceHolderID="MainContent" runat="server">
    <DT:Detail ID="chitiet"  runat="server" />
</asp:Content>