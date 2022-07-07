<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="HomeLib_Profile" MasterPageFile="~/HomeLib.master" %>

<%@ Register TagPrefix="profileControl" Src="~/Book/Profile.ascx" TagName="TagProfile" %>
<%@ Register TagPrefix="ChangeControl" Src="~/Book/ChangePassword.ascx" TagName="TagChangePass" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <link rel="stylesheet" href="../Content/Profile.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <div class=" container-fluid emp-profile  ">
        <div class="row mt-4">
            <div class="col-md-3">
                <div class="profile-img text-left ">                 
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Logo/avatar_2x.png" CssClass="rounded mx-auto d-block"  Width="150"/>                      
                  
                </div>
            </div>
            <div class="col-md-8">
                <div class="profile-head">
                    <h5><asp:Label ID="Lab_name" runat="server"></asp:Label></h5>
                    <h6><i class="fa fa-id-badge" style="font-size:18px"></i> <asp:Label ID="Lab_class" runat="server"></asp:Label></h6>
                    <h6><i class="fa fa-barcode" style="font-size:18px"></i> Mã thẻ: <asp:Label ID="Lab_code" runat="server"></asp:Label> </h6>
                    <h6><i class="fa fa-arrow-down" style="font-size:18px"></i><asp:Label ID="Lab_countdown" runat="server"></asp:Label> </h6>
                   
                    <!--<p class="proile-rating" >Điểm tích lũy : <span>8/10</span></p>-->
                    
                </div>
            </div>           
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="profile-work">
                    <p>THÔNG TIN GIAO DỊCH</p>
                    <a href="/sach-muon"><i class="fa fa-address-book-o" style="font-size:18px"></i>  Lịch sử mượn sách</a><br />
                    <a href="#"><i class="fa fa-cart-arrow-down" style="font-size:18px"></i>  Lịch sử tải tài liệu</a><br />
                    <a href="#"><i class="fa fa-heart-o" style="font-size:18px"></i>  Tài liệu yêu thích</a><br />
                    <p>QUẢN LÝ TÀI KHOẢN</p>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><i class="fa fa-user-o" style="font-size:18px"></i>  Thông tin tài khoản</asp:LinkButton><br />
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><i class="fa fa-key" style="font-size:18px"></i>  Đổi mật khẩu</asp:LinkButton>                                 

                </div>
            </div>
            <div class="col-md-9">
                <profileControl:TagProfile ID="PF" runat="server" Visible="false" />
                <ChangeControl:TagChangePass ID="CP" runat="server" Visible="false" />
            </div>
        </div>
    </div>
</asp:Content>
