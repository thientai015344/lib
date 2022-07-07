<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="HomeLib_Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Thư viện Đại học Công nghệ Tp.HCM</title>
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <!--===============================================================================================-->

    <!--===============================================================================================-->    

    
    <link rel="stylesheet" type="text/css" href="../Content/util.css"/>
    <link rel="stylesheet" type="text/css" href="../Content/main.css?a=2"/>
    <link rel="stylesheet" href="../Content/bootstrap.min.css"/>
    <link rel="stylesheet" href="../Content/jquery-ui.css"/>
    
    
    <!--===============================================================================================-->
</head>
<body>
    <div class="limiter">
        <div class="container-login100" style="background-image: url(/HomeLib/images/bg-01.jpg);">            
            <div class="wrap-login100">
                <div class="custom-breadcrumns ">
                    <div class="mt-5">
                        <a href="/">Trang chủ</a>                        
                        <span class="mx-3 icon-keyboard_arrow_right">> Đăng nhập</span>
                    </div>
                </div>
                <div class="login100-form-title">
                    <span class="login100-form-title-1">ĐĂNG NHẬP
                    </span>
                </div>
                <form class="login100-form validate-form" runat="server">
                    <div class="wrap-input100 validate-input m-b-26" data-validate="Username is required">
                        <span class="label-input100 text-left">MÃ THẺ</span>
                        <asp:TextBox ID="txtUser" class="input100" required="required" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                    </div>
                    <div class="wrap-input100 validate-input m-b-18" data-validate="Password is required">
                        <span class="label-input100 text-left">MẬT KHẨU</span>
                        <asp:TextBox ID="txtPass" class="input100" TextMode="Password" required="required" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                    </div>
                    <div class="flex-sb-m w-full p-b-8">
                        <div class="contact100-form-checkbox" hidden="hidden">
                            <input class="input-checkbox100" id="ckb1" runat="server" type="checkbox" name="remember-me" />
                            <label class="label-checkbox100" for="ckb1">
                                Nhớ mật khẩu                                    
                            </label>
                        </div>

                    </div>
                    <div class="flex-sb-m w-full p-b-10">
                        <asp:Label ID="Laberorr" runat="server" Visible="False" ForeColor="#ffffff"></asp:Label>
                    </div>

                    <div class="container-login100-form-btn">
                        <asp:Button ID="But_login" class="login100-form-btn" runat="server" Text="ĐĂNG NHẬP" OnClick="But_login_Click" />
                    </div>

                    <div class="container-login100-form-btn">
                        <a href="/ResetPassword" class="txt1">Quên mật khẩu?
                        </a>
                    </div>
                      <div class="copyri">
                        <h6>@ 2022 Thư viện HUTECH</h6>
                    </div>
                </form>              
            </div>
        </div>
    </div>

</body>
</html>
