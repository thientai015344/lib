<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="HomeLib_Register" MasterPageFile="~/HomeLib.master" %>

<asp:Content ID="Register" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <link href="../Content/Registration.css" rel="stylesheet" />
    <link href="css/bootstrap-imageupload.css" rel="stylesheet">
    <script src="js/bootstrap-imageupload.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>

    <script>
        $(".imgAdd").click(function () {
            $(this).closest(".row").find('.imgAdd').before('<div class="col-sm-2 imgUp"><div class="imagePreview"></div><label class="btn btn-danger">Upload<input type="file" class="uploadFile img" value="Upload Photo" style="width:0px;height:0px;overflow:hidden;"></label><i class="fa fa-times del"></i></div>');
        });
        $(document).on("click", "i.del", function () {
            $(this).parent().remove();
        });
        $(function () {
            $(document).on("change", ".uploadFile", function () {
                var uploadFile = $(this);
                var files = !!this.files ? this.files : [];
                if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

                if (/^image/.test(files[0].type)) { // only image file
                    var reader = new FileReader(); // instance of the FileReader
                    reader.readAsDataURL(files[0]); // read the local file

                    reader.onloadend = function () { // set image data as background of div
                        //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                        uploadFile.closest(".imgUp").find('.imagePreview').css("background-image", "url(" + this.result + ")");
                    }
                }

            });
        });
    </script>

    <section>
        <div class="container mt">
            <h4 class=" ml-3 mb-5">Đăng ký tài khoản Thư viện</h4>
            <div class="row justify-content-center ">
                <div class="col-md-12">
                    <div class="row ">
                        <div class=" col-sm-5 form-group">
                            <label>Mã sinh viên / CBNV:</label>
                            <asp:TextBox ID="Txt_MSSV" class="form-control" required="required" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5 form-group">
                            <label>Họ và tên:</label>
                            <asp:TextBox ID="Txt_Hoten" class="form-control" required="required" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-5 form-group">
                            <label>Điện thoại:</label>
                            <asp:TextBox ID="Txt_Dienthoai" class="form-control" required="required" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5 form-group">
                            <label>Email:</label>
                            <asp:TextBox ID="Txt_Email" TextMode="Email" class="form-control" required="required" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-5 form-group">
                            <label>Xác nhận Email:</label>
                            <asp:TextBox ID="Txt_Email_re" TextMode="Email" class="form-control" required="required" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mt-3">                        
                        <div class="col-md-2 col-5 imgUp">
                            <div class="imagePreview image1" ></div>
                            <label class="btn btn-danger">
                               Hình 1<input type="file" class="uploadFile img" value="Upload Photo" style="width: 0px; height: 0px; overflow: hidden;">
                            </label>
                        </div>
                        <!-- col-2 -->
                        <div class="col-md-2 col-5 imgUp">
                            <div class="imagePreview image2"></div>
                            <label class="btn btn-danger">
                               Hình 2<input type="file" class="uploadFile img" value="Upload Photo" style="width: 0px; height: 0px; overflow: hidden;">
                            </label>
                        </div>
                    </div>
                    <!-- row -->
                    <div class="row">
                        <div class="form-check form-group col-sm-5 ml-3">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="&nbsp;Đã đọc nội quy" />
                            <a href="/Noi-quy" class="text_noiquy">&nbsp;(Nội quy Thư viện HUTECH)</a>
                            <br />
                            <asp:Label ID="Lab_error" runat="server" ForeColor="#cc0000"></asp:Label>
                        </div>
                        <div class="col-sm-5 form-group">
                        </div>
                    </div>
                    <div class="row ml-3">
                      <asp:Button ID="ButRegister" runat="server" class="btn btn-lg btn-info mb-5 mt-3" Text="Đăng ký" OnClick="ButRegister_Click" />
                    </div>

                </div>
            </div>
        </div>
    </section>
</asp:Content>
