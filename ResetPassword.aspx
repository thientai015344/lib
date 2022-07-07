<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" MasterPageFile="~/HomeLib.master" Async="true"%>

<asp:Content ID="ResetPassContent" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <script type='text/javascript'>
        function DisplayLoadingImage() {
            document.getElementById("HiddenLoadingImage").style.display = "block";
        }
    </script>    
 <div style="margin-top: 150px; font-family:Arial">
        <div class="container">          
                <div class="row justify-content-center">
                    <div class="col-md-5 col-md-offset-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="text-center">
                                    <h3><i class="fa fa-lock fa-3x"></i></h3>
                                    <h3 class="text-center">Bạn đã quên mật khẩu?</h3>
                                    <p>Bạn có thể lấy lại mật khẩu ở đây.</p>
                                    <div class="panel-body">
                                        <fieldset>                                            
                                                                                        
                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-user-o "></i></span>
                                                </div>
                                                <asp:TextBox ID="txtUserName" runat="server" placeholder="MSSV Hutech" class="form-control" type="text" oninvalid="setCustomValidity('Please enter a valid username!')" onchange="try{setCustomValidity('')}catch(e){}" required=""></asp:TextBox>
                                            </div>
                                               
                                            <div class="input-group mb-5">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-envelope color-blue"></i></span>

                                                </div>
                                                <asp:TextBox ID="TxtEmail" runat="server" placeholder="Email" class="form-control" type="email" oninvalid="setCustomValidity('Please enter a valid email address!')" onchange="try{setCustomValidity('')}catch(e){}" required=""></asp:TextBox>
                                            </div>                                            
                                            <div style="text-align: center">
                                                <img id='HiddenLoadingImage' src="Logo/wait.gif" height:"60" style="display: none" width="60" />
                                            </div>
                                            <div class="form-group text-left">
                                                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC0000"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="ButResetPass" CssClass="btn btn-lg btn-danger btn-block" runat="server" OnClientClick="DisplayLoadingImage();"  Text="Lấy lại mật khẩu" OnClick="ButResetPass_Click" />
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
