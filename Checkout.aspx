<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" MasterPageFile="~/Site.master" %>
<asp:Content ID="checkout" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="text-center" style="margin-top: 100px">
                <h4>Đặt mượn thành công!</h4>
            </div>
            <div class="text-center">                
                <asp:LinkButton ID="LinkButton1" class="btn  btn-danger" PostBackUrl="Default.aspx" runat="server"><span class="glyphicon glyphicon glyphicon-chevron-left" aria-hidden="true"></span>Quay lại trang tìm kiếm</asp:LinkButton>
                
            </div>
        </div>
    </div>
    
</asp:Content>
