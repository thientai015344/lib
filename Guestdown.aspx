<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Guestdown.aspx.cs" Inherits="guestdown" MasterPageFile="~/Site.master" %>
<%@ Register TagPrefix="GuestDownload" TagName="Gd" Src="~/Book/Guestdownload.ascx" %>

<asp:Content ID="CartContent" ContentPlaceHolderID="MainContent" runat="server">
     <GuestDownload:Gd ID="GuestD" runat="server" />
</asp:Content>
