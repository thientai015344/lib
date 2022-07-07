<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Student_Registe2t" MasterPageFile="~/Site.master" %>

<asp:Content ID="CartContent" ContentPlaceHolderID="MainContent" runat="server">
  <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
<script src='https://www.google.com/recaptcha/api.js'></script>

<style>
    img {
        width: 100%;
    }

    .form-group.required .control-label:after {
        color: #d00;
        content: "*";
        position: absolute;
        margin-left: 8px;
        top: 7px;
    }
</style>
<div class="container-fluid" style="font-family: Arial">
    <div class="row">
        <div style="margin-top: 80px">
            <div class="container">
                <div class="row ">
                    <div class="col-md-4 py-5 bg-primary text-white text-center ">
                        <div class=" ">
                            <div class="card-body">
                                <img src="http://www.ansonika.com/mavia/img/registration_bg.svg" style="width: 30%">
                                <h4 class="py-3">Đăng ký tải tài liệu học tập</h4>
                                <p>
                                   
                                </p>
                            </div>
                        </div>
                    </div>
                    <div id="div_register" class="col-md-8 py-5 border">
                        <h4 class="pb-4">Vui lòng bổ sung đầy đủ thông tin</h4>
                        <div class="row">
                            <div class="form-group col-md-6 " style="margin:20px 0 20px 0">  
                                <asp:Label ID="Label1" class="col-md-2 control-label" runat="server" Text="MSSV: "></asp:Label>                                                            
                                <asp:Label ID="Lab_Code" runat="server"  Font-Bold="True"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6 required ">
                                <label class="col-md-6 control-label">E-mail</label>
                                <input id="form_email" runat="server" type="email" name="email" class="form-control" placeholder="E-mail" required="required" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6 required">
                                <label class="col-md-6 control-label">Điện thoại</label>
                                <input id="form_Mobile" runat="server" type="text" name="Mobile" class="form-control" placeholder="Điện thoại" required="required" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6 required">
                                <asp:Label ID="Mes" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-row">
                            <asp:Button ID="Register" runat="server" class="btn btn-danger" Text="Đăng ký" OnClick="Register_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>  
    
</asp:Content>
