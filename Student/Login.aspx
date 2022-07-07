<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/Site.master" %>

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
                                    <img src="../Logo/new book.jpg" style="width: 50px">
                                    <h4 class="py-3">Đăng ký tải tài liệu học tập</h4>
                                    <p>
                                        <asp:Label ID="La_hantai" runat="server" Text="Đã hết hạn tải tài liệu" Visible="False" ForeColor="Yellow"></asp:Label>

                                    </p>
                                    <div id="mes" runat="server">
                                        Chú ý:Thời gian bắt đầu:
                                        <asp:Label ID="La_startdate" runat="server"></asp:Label><br />
                                        Thời gian kết thúc:
                                        <asp:Label ID="La_enddate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="div_register" class="col-md-8 py-5 border">
                            <h4 class="pb-4">Đăng nhập</h4>
                            <div class="row" style="margin-top: 20px">
                                <div class="form-group col-md-6 required ">
                                    <label class="col-md-6 control-label">Mã số sinh viên</label>
                                    <input id="form_Code" runat="server" type="text" name="code" class="form-control" placeholder="Mã số sinh viên" required="required" />
                                </div>
                            </div>

                            <div class="form-row">
                                <asp:Button ID="bn_Login" runat="server" class="btn btn-danger" Text="Đăng nhập" OnClick="bn_Login_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
