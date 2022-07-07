<%@ Page Async="true" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="MainResult" Src="~/Book/BookResult.ascx" TagName="SearchResult" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<MainResult:SearchResult ID = "SResult"  runat="server" />
</asp:Content>
