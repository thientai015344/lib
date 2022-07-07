<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" MasterPageFile="~/HomeLib.master" Async="true"%>

<asp:Content ID="ChangePassContent" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">    
<div style="margin-top: 150px; font-family: Arial">
        <div class="container">        
                <div class="row justify-content-center">
                    <div class="col-md-5 col-md-offset-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="text-center">
                                    <h3><i class="fa fa-unlock-alt fa-3x"></i></h3>
                                    <h3 class="text-center">Đổi mật khẩu</h3>                                    
                                    <div class="panel-body">
                                        <fieldset>
                                            <div>
                                                <div class="input-group  mb-3">
                                                    <div class="input-group-append">
                                                        <span class="input-group-text"><i class="fa fa-key color-blue"></i></span>
                                                    </div>
                                                    <asp:TextBox ID="txtNewPassword" TextMode="Password" placeholder="Nhập mật khẩu mới" class="form-control" runat="server" required=""></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPassword2"
                                                        runat="server"
                                                        ControlToValidate="txtNewPassword" ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="input-group  mb-3">
                                                    <div class="input-group-append">
                                                        <span class="input-group-text"><i class="fa fa-key color-blue"></i></span>
                                                    </div>
                                                    <asp:TextBox ID="txtConfirmNewPassword" TextMode="Password" placeholder="Xác nhận mật khẩu" class="form-control" runat="server" required="">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator"
                                                        runat="server"
                                                        ControlToValidate="txtConfirmNewPassword"
                                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                    ErrorMessage="Mật khẩu mới và xác nhận lại không giống nhau"
                                                    ControlToValidate="txtConfirmNewPassword" ForeColor="Red"
                                                    ControlToCompare="txtNewPassword">
                                                </asp:CompareValidator>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblMessage" runat="server"> </asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-check">
                                                     <asp:Button ID="ButChangePass" CssClass="btn btn-danger btn-block" runat="server" Text="Xác nhận" OnClick="ButChangePass_Click" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>        
    </div>
</asp:Content>
