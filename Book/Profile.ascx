<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profile.ascx.cs" Inherits="Book_Profile" %> 
<link rel="stylesheet" href="HomeLib/css/bootstrap.min.css">
<style>   
    .datepicker{     
      font-family:Arial;         
      padding: 7px;     
      border: 1px solid #ccc;
      border-radius: 4px;     
    }
  </style>
<div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="profile-edit">
                <div class="mb-5">
                    <p>CHỈNH SỬA THÔNG TIN</p>
                </div>
                <div class="form-group ">
                    <div class="row form-group required">
                        <div class="col-md-3">
                            <label>Email:</label>
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox ID="TxtEmail" CssClass="form-control" placeholder="Nhập Email" required="required" TextMode="Email" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row form-group ">
                        <div class=" col-md-3">
                            <label>Giới tính:</label>
                        </div>
                        <div class="col-md-8 ">
                            <!-- Default inline 1-->
                            <div class="custom-control custom-radio custom-control-inline">
                                <asp:RadioButton ID="Radio_nam" CssClass="form-check" runat="server" Text="Nam" GroupName="a" />
                            </div>
                            <!-- Default inline 2-->
                            <div class="custom-control custom-radio custom-control-inline">
                                <asp:RadioButton ID="Radio_Nu" CssClass="form-check" runat="server" Text="Nữ" GroupName="a" />
                            </div>

                        </div>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-3">
                            <label>Ngày sinh:</label>
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtDate" data-date-format="dd/mm/yyyy"  CssClass="datepicker form-control"  required="required" runat="server"></asp:TextBox>
                        </div> 
                        <script type="text/javascript">
                            $('.datepicker').datepicker({
                                weekStart: 1,
                                color: 'red'
                            });
                          
	                    </script>                       
                        
                    </div>

                    <div class="row form-group">
                        <div class="col-md-3">
                            <label>Điện thoại:</label>
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtPhone" CssClass="form-control" placeholder="Nhập Số điện thoại" required="required" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-3">
                            <label>Địa chỉ:</label>
                        </div>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="Nhập Địa chỉ" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row form-group ">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="bt_update" CssClass="btn btn-danger" runat="server" Text="Cập nhật" OnClick="bt_update_Click" />
                            <asp:Label ID="Label_error" runat="server"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
</div>
