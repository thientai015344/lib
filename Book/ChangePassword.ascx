<%@ Control Language="C#"  AutoEventWireup="true" CodeFile="ChangePassword.ascx.cs" Inherits="Book_ChangePassword" %>

<link rel="stylesheet" href="../HomeLib/css/bootstrap.min.css">

<div class="container">
    <div class="profile-edit">
        <div class="col-md-12">
           
            <div class=" mb-5">
                <P>ĐỔI MẬT KHẨU</P>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Mật Khẩu Hiện Tại</label>
                </div>
                <div class="col-md-6 pass_show">
                    <p>
                        <asp:TextBox ID="txtPass_old" CssClass="form-control" placeholder="******" TextMode="Password" required="required" runat="server"></asp:TextBox>

                    </p>                                    
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Mật Khẩu Mới</label>
                </div>
                <div class="col-md-6 pass_show">
                    <p>
                        <asp:TextBox ID="txtPass_new" CssClass="form-control" placeholder="******" TextMode="Password" required="required"  runat="server" ViewStateMode="Enabled" ></asp:TextBox>
                    </p>                                                   
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Xác Nhận Mật Khẩu</label>
                </div>
                <div class="col-md-6 pass_show">
                    <p>
                        <asp:TextBox ID="txtPass_renew" CssClass="form-control" placeholder="******" TextMode="Password" required="required"  runat="server"></asp:TextBox>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-6">
                    <asp:Button ID="Bt_changePass" CssClass="btn btn-info" runat="server" Text="Xác nhận" OnClick="Bt_changePass_Click" />
                    
                </div>               
            </div>
            <div class="row">
                <div class="col-md-8 mt-1">
                     <i class='fa fa-exclamation-circle' style='font-size:18px' id="icon1" runat="server" visible="false"></i> <asp:Label ID="LbInfo" runat="server" Font-Size="12px" ></asp:Label>
                </div>
            </div>
        </div>
    </div>

</div>
